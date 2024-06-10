using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using GateIo.Net.Objects.Models;
using Microsoft.Extensions.Logging;
using GateIo.Net.Objects.Options;
using GateIo.Net.Objects.Sockets.Subscriptions;
using GateIo.Net.Interfaces.Clients.PerpetualFuturesApi;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Collections.Generic;
using System.Linq;
using GateIo.Net.Enums;

namespace GateIo.Net.Clients.FuturesApi
{
    /// <summary>
    /// Client providing access to the GateIo futures websocket Api
    /// </summary>
    public class GateIoSocketClientPerpetualFuturesApi : SocketApiClient, IGateIoSocketClientPerpetualFuturesApi
    {
        #region fields
        private static readonly MessagePath _idPath = MessagePath.Get().Property("id");
        private static readonly MessagePath _channelPath = MessagePath.Get().Property("channel");
        private static readonly MessagePath _contractPath = MessagePath.Get().Property("result").Index(0).Property("contract");
        private static readonly MessagePath _contractPath2 = MessagePath.Get().Property("result").Property("s");
        private static readonly MessagePath _klinePath = MessagePath.Get().Property("result").Index(0).Property("n");
        #endregion

        #region constructor/destructor

        /// <summary>
        /// ctor
        /// </summary>
        internal GateIoSocketClientPerpetualFuturesApi(ILogger logger, GateIoSocketOptions options) :
            base(logger, options.Environment.FuturesSocketClientAddress!, options, options.PerpetualFuturesOptions)
        {
        }
        #endregion

        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer();
        /// <inheritdoc />
        protected override IByteMessageAccessor CreateAccessor() => new SystemTextJsonByteMessageAccessor();

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new GateIoAuthenticationProvider(credentials);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string settlementAsset, string contract, Action<DataEvent<IEnumerable<GateIoPerpTradeUpdate>>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoSubscription<IEnumerable<GateIoPerpTradeUpdate>>(_logger, "futures.trades", new[] { "futures.trades." + contract }, new[] { contract }, x => onMessage(x.WithSymbol(x.Data.First().Contract)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string settlementAsset, string contract, Action<DataEvent<IEnumerable<GateIoPerpTickerUpdate>>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoSubscription<IEnumerable<GateIoPerpTickerUpdate>>(_logger, "futures.tickers", new[] { "futures.tickers." + contract }, new[] { contract }, x => onMessage(x.WithSymbol(x.Data.First().Contract)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string settlementAsset, string contract, Action<DataEvent<GateIoPerpBookTickerUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoSubscription<GateIoPerpBookTickerUpdate>(_logger, "futures.book_ticker", new[] { "futures.book_ticker." + contract }, new[] { contract }, x => onMessage(x.WithSymbol(x.Data.Contract)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string settlementAsset, string contract, int updateMs, int depth, Action<DataEvent<GateIoPerpOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            updateMs.ValidateIntValues(nameof(updateMs), 20, 100, 1000);
            depth.ValidateIntValues(nameof(depth), 5, 10, 20, 50, 100);

            var subscription = new GateIoSubscription<GateIoPerpOrderBookUpdate>(_logger, "futures.order_book_update", new[] { "futures.order_book_update." + contract }, new[] { contract, updateMs + "ms", depth.ToString() }, x => onMessage(x.WithSymbol(x.Data.Contract)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string settlementAsset, string contract, KlineInterval interval, Action<DataEvent<IEnumerable<GateIoPerpKlineUpdate>>> onMessage, CancellationToken ct = default)
        {
            var intervalStr = EnumConverter.GetString(interval);
            var subscription = new GateIoSubscription<IEnumerable<GateIoPerpKlineUpdate>>(_logger, "futures.candlesticks", new[] { "futures.candlesticks." + intervalStr + "_" + contract }, new[] { intervalStr, contract }, x => onMessage(x.WithSymbol(x.Data.First().Contract)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<IEnumerable<GateIoPerpOrder>>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<IEnumerable<GateIoPerpOrder>>(_logger, "futures.orders", new[] { "futures.orders" }, new[] { userId.ToString(), "!all" }, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<IEnumerable<GateIoPerpUserTrade>>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<IEnumerable<GateIoPerpUserTrade>>(_logger, "futures.usertrades", new[] { "futures.usertrades" }, new[] { userId.ToString(), "!all" }, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserLiquidationUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<IEnumerable<GateIoPerpLiquidation>>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<IEnumerable<GateIoPerpLiquidation>>(_logger, "futures.liquidates", new[] { "futures.liquidates" }, new[] { userId.ToString(), "!all" }, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserAutoDeleverageUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<IEnumerable<GateIoPerpAutoDeleverage>>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<IEnumerable<GateIoPerpAutoDeleverage>>(_logger, "futures.auto_deleverages", new[] { "futures.auto_deleverages" }, new[] { userId.ToString(), "!all" }, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPositionCloseUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<IEnumerable<GateIoPerpPositionCloseUpdate>>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<IEnumerable<GateIoPerpPositionCloseUpdate>>(_logger, "futures.position_closes", new[] { "futures.position_closes" }, new[] { userId.ToString(), "!all" }, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<IEnumerable<GateIoPerpBalanceUpdate>>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<IEnumerable<GateIoPerpBalanceUpdate>>(_logger, "futures.balances", new[] { "futures.balances" }, new[] { userId.ToString() }, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToReduceRiskLimitUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<IEnumerable<GateIoPerpRiskLimitUpdate>>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<IEnumerable<GateIoPerpRiskLimitUpdate>>(_logger, "futures.reduce_risk_limits", new[] { "futures.reduce_risk_limits" }, new[] { userId.ToString(), "!all" }, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<IEnumerable<GateIoPositionUpdate>>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<IEnumerable<GateIoPositionUpdate>>(_logger, "futures.positions", new[] { "futures.positions" }, new[] { userId.ToString(), "!all" }, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTriggerOrderUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<IEnumerable<GateIoPerpTriggerOrderUpdate>>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<IEnumerable<GateIoPerpTriggerOrderUpdate>>(_logger, "futures.autoorders", new[] { "futures.autoorders" }, new[] { userId.ToString(), "!all" }, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public override string? GetListenerIdentifier(IMessageAccessor message)
        {
            var id = message.GetValue<long?>(_idPath);
            if (id != null)
                return id.ToString();

            var channel = message.GetValue<string>(_channelPath);

            if (string.Equals(channel, "futures.trades")
                || string.Equals(channel, "futures.tickers"))
            {
                return channel + "." + message.GetValue<string>(_contractPath);
            }

            if (string.Equals(channel, "futures.candlesticks"))
                return channel + "." + message.GetValue<string>(_klinePath);

            if (string.Equals(channel, "futures.book_ticker")
             || string.Equals(channel, "futures.order_book_update")
             || string.Equals(channel, "futures.order_book"))
                return channel + "." + message.GetValue<string>(_contractPath2);

            return channel;
        }

        /// <inheritdoc />
        protected override Query? GetAuthenticationRequest() => null;

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset) => baseAsset.ToUpperInvariant() + "_" + quoteAsset.ToUpperInvariant();
    }
}
