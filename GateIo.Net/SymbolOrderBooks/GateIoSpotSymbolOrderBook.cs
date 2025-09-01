﻿using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.OrderBook;
using Microsoft.Extensions.Logging;
using GateIo.Net.Clients;
using GateIo.Net.Interfaces.Clients;
using GateIo.Net.Objects.Options;
using GateIo.Net.Objects.Models;

namespace GateIo.Net.SymbolOrderBooks
{
    /// <summary>
    /// Implementation for a synchronized order book. After calling Start the order book will sync itself and keep up to date with new data. It will automatically try to reconnect and resync in case of a lost/interrupted connection.
    /// Make sure to check the State property to see if the order book is synced.
    /// </summary>
    public class GateIoSpotSymbolOrderBook : SymbolOrderBook
    {
        private readonly bool _clientOwner;
        private readonly IGateIoRestClient _restClient;
        private readonly IGateIoSocketClient _socketClient;
        private readonly TimeSpan _initialDataTimeout;
        private readonly int? _updateInterval;

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public GateIoSpotSymbolOrderBook(string symbol, Action<GateIoOrderBookOptions>? optionsDelegate = null)
            : this(symbol, optionsDelegate, null, null, null)
        {
            _clientOwner = true;
        }

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        /// <param name="logger">Logger</param>
        /// <param name="restClient">Rest client instance</param>
        /// <param name="socketClient">Socket client instance</param>
        public GateIoSpotSymbolOrderBook(
            string symbol,
            Action<GateIoOrderBookOptions>? optionsDelegate,
            ILoggerFactory? logger,
            IGateIoRestClient? restClient,
            IGateIoSocketClient? socketClient) : base(logger, "GateIo", "GateIo", symbol)
        {
            var options = GateIoOrderBookOptions.Default.Copy();
            if (optionsDelegate != null)
                optionsDelegate(options);
            Initialize(options);

            _strictLevels = false;
            _sequencesAreConsecutive = options?.Limit == null;
            _updateInterval = options?.UpdateInterval;

            Levels = options?.Limit;
            _initialDataTimeout = options?.InitialDataTimeout ?? TimeSpan.FromSeconds(30);
            _clientOwner = socketClient == null;
            _socketClient = socketClient ?? new GateIoSocketClient();
            _restClient = restClient ?? new GateIoRestClient();
        }


        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> DoStartAsync(CancellationToken ct)
        {
            CallResult<UpdateSubscription> subResult;
            if (Levels == null)
                subResult = await _socketClient.SpotApi.SubscribeToOrderBookUpdatesAsync(Symbol, HandleUpdate).ConfigureAwait(false);
            else
                subResult = await _socketClient.SpotApi.SubscribeToPartialOrderBookUpdatesAsync(Symbol, Levels.Value, _updateInterval, HandleUpdate).ConfigureAwait(false);

            if (!subResult)
                return new CallResult<UpdateSubscription>(subResult.Error!);

            if (ct.IsCancellationRequested)
            {
                await subResult.Data.CloseAsync().ConfigureAwait(false);
                return subResult.AsError<UpdateSubscription>(new CancellationRequestedError());
            }

            Status = OrderBookStatus.Syncing;
            if (Levels == null)
                // Small delay to make sure the snapshot is from after our first stream update
                await Task.Delay(200).ConfigureAwait(false);

            var bookResult = await _restClient.SpotApi.ExchangeData.GetOrderBookAsync(Symbol, null, Levels ?? 100).ConfigureAwait(false);
            if (!bookResult)
            {
                _logger.Log(LogLevel.Debug, $"{Api} order book {Symbol} failed to retrieve initial order book");
                await _socketClient.UnsubscribeAsync(subResult.Data).ConfigureAwait(false);
                return new CallResult<UpdateSubscription>(bookResult.Error!);
            }

            SetInitialOrderBook(bookResult.Data.Id, bookResult.Data.Bids, bookResult.Data.Asks);
            return new CallResult<UpdateSubscription>(subResult.Data);
        }

        private void HandleUpdate(DataEvent<GateIoOrderBookUpdate> data)
        {
            UpdateOrderBook(data.Data.FirstUpdateId, data.Data.LastUpdateId, data.Data.Bids, data.Data.Asks);
        }

        private void HandleUpdate(DataEvent<GateIoPartialOrderBookUpdate> data)
        {
            SetInitialOrderBook(data.Data.LastUpdateId, data.Data.Bids, data.Data.Asks);
        }

        /// <inheritdoc />
        protected override void DoReset()
        {
        }

        /// <inheritdoc />
        protected override async Task<CallResult<bool>> DoResyncAsync(CancellationToken ct)
        {
            if (Levels != null)
                return await WaitForSetOrderBookAsync(_initialDataTimeout, ct).ConfigureAwait(false);

            var bookResult = await _restClient.SpotApi.ExchangeData.GetOrderBookAsync(Symbol, null, Levels ?? 100).ConfigureAwait(false);
            if (!bookResult)
                return new CallResult<bool>(bookResult.Error!);

            SetInitialOrderBook(bookResult.Data.Id, bookResult.Data.Bids, bookResult.Data.Asks);
            return new CallResult<bool>(true);
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (_clientOwner)
            {
                _restClient?.Dispose();
                _socketClient?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
