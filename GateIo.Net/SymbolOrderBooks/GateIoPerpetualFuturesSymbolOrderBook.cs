using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.OrderBook;
using Microsoft.Extensions.DependencyInjection;
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
    public class GateIoPerpetualFuturesSymbolOrderBook : SymbolOrderBook
    {
        private readonly IGateIoRestClient _restClient;
        private readonly IGateIoSocketClient _socketClient;
        private readonly bool _clientOwner;
        private readonly string _settleAsset;

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public GateIoPerpetualFuturesSymbolOrderBook(string settlementAsset, string symbol, Action<GateIoOrderBookOptions>? optionsDelegate = null)
            : this(settlementAsset, symbol, optionsDelegate, null, null, null)
        {
            _clientOwner = true;
        }

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">The symbol the order book is for</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        /// <param name="logger">Logger</param>
        /// <param name="restClient">Rest client instance</param>
        /// <param name="socketClient">Socket client instance</param>
        [ActivatorUtilitiesConstructor]
        public GateIoPerpetualFuturesSymbolOrderBook(
            string settlementAsset,
            string contract,
            Action<GateIoOrderBookOptions>? optionsDelegate,
            ILoggerFactory? logger,
            IGateIoRestClient? restClient,
            IGateIoSocketClient? socketClient) : base(logger, "GateIo", "PerpetualFutures", contract)
        {
            var options = GateIoOrderBookOptions.Default.Copy();
            if (optionsDelegate != null)
                optionsDelegate(options);
            Initialize(options);

            _strictLevels = false;
            _settleAsset = settlementAsset;
            _sequencesAreConsecutive = options?.Limit == null;

            Levels = options?.Limit ?? 20;
            _clientOwner = socketClient == null;
            _socketClient = socketClient ?? new GateIoSocketClient();
            _restClient = restClient ?? new GateIoRestClient();
        }

        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> DoStartAsync(CancellationToken ct)
        {
            var subResult = await _socketClient.PerpetualFuturesApi.SubscribeToOrderBookUpdatesAsync(_settleAsset, Symbol, 20, Levels!.Value, HandleUpdate).ConfigureAwait(false);

            if (!subResult)
                return new CallResult<UpdateSubscription>(subResult.Error!);

            if (ct.IsCancellationRequested)
            {
                await subResult.Data.CloseAsync().ConfigureAwait(false);
                return subResult.AsError<UpdateSubscription>(new CancellationRequestedError());
            }

            Status = OrderBookStatus.Syncing;

            // Small delay to make sure the snapshot is from after our first stream update
            await Task.Delay(200).ConfigureAwait(false);
            var bookResult = await _restClient.PerpetualFuturesApi.ExchangeData.GetOrderBookAsync(_settleAsset, Symbol, null, Levels!.Value).ConfigureAwait(false);
            if (!bookResult)
            {
                _logger.Log(LogLevel.Debug, $"{Api} order book {Symbol} failed to retrieve initial order book");
                await _socketClient.UnsubscribeAsync(subResult.Data).ConfigureAwait(false);
                return new CallResult<UpdateSubscription>(bookResult.Error!);
            }

            SetInitialOrderBook(bookResult.Data.Id, bookResult.Data.Bids, bookResult.Data.Asks);
            return new CallResult<UpdateSubscription>(subResult.Data);
        }

        private void HandleUpdate(DataEvent<GateIoPerpOrderBookUpdate> data)
        {
            if (data.UpdateType == SocketUpdateType.Snapshot)
                SetInitialOrderBook(data.Data.FirstUpdateId, data.Data.Bids, data.Data.Asks);
            else
                UpdateOrderBook(data.Data.FirstUpdateId, data.Data.LastUpdateId, data.Data.Bids, data.Data.Asks);
        }

        /// <inheritdoc />
        protected override void DoReset()
        {
        }

        /// <inheritdoc />
        protected override async Task<CallResult<bool>> DoResyncAsync(CancellationToken ct)
        {
            var bookResult = await _restClient.PerpetualFuturesApi.ExchangeData.GetOrderBookAsync(_settleAsset, Symbol, null, Levels ?? 100).ConfigureAwait(false);
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
