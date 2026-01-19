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
        private readonly TimeSpan _initialDataTimeout;

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
            _sequencesAreConsecutive = true;
            _initialDataTimeout = options?.InitialDataTimeout ?? TimeSpan.FromSeconds(30);

            Levels = options?.Limit ?? 50;
            _clientOwner = socketClient == null;
            _socketClient = socketClient ?? new GateIoSocketClient();
            _restClient = restClient ?? new GateIoRestClient();
        }

        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> DoStartAsync(CancellationToken ct)
        {
            var subResult = await _socketClient.PerpetualFuturesApi.SubscribeToOrderBookV2UpdatesAsync(_settleAsset, Symbol, Levels!.Value, HandleUpdate).ConfigureAwait(false);

            if (!subResult)
                return new CallResult<UpdateSubscription>(subResult.Error!);

            if (ct.IsCancellationRequested)
            {
                await subResult.Data.CloseAsync().ConfigureAwait(false);
                return subResult.AsError<UpdateSubscription>(new CancellationRequestedError());
            }

            Status = OrderBookStatus.Syncing;

            var setResult = await WaitForSetOrderBookAsync(_initialDataTimeout, ct).ConfigureAwait(false);
            return setResult ? subResult : new CallResult<UpdateSubscription>(setResult.Error!);
        }

        private void HandleUpdate(DataEvent<GateIoPerpOrderBookV2Update> data)
        {
            if (data.UpdateType == SocketUpdateType.Snapshot)
                SetSnapshot(data.Data.LastUpdateId, data.Data.Bids, data.Data.Asks, data.DataTime, data.DataTimeLocal);
            else
                UpdateOrderBook(data.Data.FirstUpdateId, data.Data.LastUpdateId, data.Data.Bids, data.Data.Asks, data.DataTime, data.DataTimeLocal);
        }

        /// <inheritdoc />
        protected override void DoReset()
        {
        }

        /// <inheritdoc />
        protected override async Task<CallResult<bool>> DoResyncAsync(CancellationToken ct)
        {
            return await WaitForSetOrderBookAsync(_initialDataTimeout, ct).ConfigureAwait(false);
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
