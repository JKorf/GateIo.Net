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
using Microsoft.Extensions.Logging;
using GateIo.Net.Interfaces.Clients.SpotApi;
using GateIo.Net.Objects.Models;
using GateIo.Net.Objects.Options;
using GateIo.Net.Objects.Sockets.Subscriptions;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Collections.Generic;
using System.Linq;
using GateIo.Net.Enums;

namespace GateIo.Net.Clients.SpotApi
{
    /// <summary>
    /// Client providing access to the GateIo spot websocket Api
    /// </summary>
    public class GateIoSocketClientSpotApi : SocketApiClient, IGateIoSocketClientSpotApi
    {
        #region fields
        private static readonly MessagePath _idPath = MessagePath.Get().Property("id");
        private static readonly MessagePath _channelPath = MessagePath.Get().Property("channel");
        private static readonly MessagePath _symbolPath = MessagePath.Get().Property("result").Property("currency_pair");
        private static readonly MessagePath _symbolPath2 = MessagePath.Get().Property("result").Property("s");
        private static readonly MessagePath _klinePath = MessagePath.Get().Property("result").Property("n");
        #endregion

        #region constructor/destructor

        /// <summary>
        /// ctor
        /// </summary>
        internal GateIoSocketClientSpotApi(ILogger logger, GateIoSocketOptions options) :
            base(logger, options.Environment.SpotSocketClientAddress!, options, options.SpotOptions)
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
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<GateIoTradeUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoSubscription<GateIoTradeUpdate>(_logger, "spot.trades", new[] { "spot.trades." + symbol }, new[] { symbol }, x => onMessage(x.WithSymbol(x.Data.Symbol)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v4/") + "/", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<GateIoTradeUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoSubscription<GateIoTradeUpdate>(_logger, "spot.trades", symbols.Select(x => "spot.trades." + x), symbols, x => onMessage(x.WithSymbol(x.Data.Symbol)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v4/") + "/", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<GateIoTickerUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoSubscription<GateIoTickerUpdate>(_logger, "spot.tickers", new[] { "spot.tickers." + symbol }, new[] { symbol }, x => onMessage(x.WithSymbol(x.Data.Symbol)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v4/") + "/", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<GateIoTickerUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoSubscription<GateIoTickerUpdate>(_logger, "spot.tickers", symbols.Select(x => "spot.tickers." + x), symbols, x => onMessage(x.WithSymbol(x.Data.Symbol)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v4/") + "/", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<GateIoKlineUpdate>> onMessage, CancellationToken ct = default)
        {
            var intervalStr = EnumConverter.GetString(interval);
            var subscription = new GateIoSubscription<GateIoKlineUpdate>(_logger, "spot.candlesticks", new[] { "spot.candlesticks." + intervalStr + "_" + symbol }, new[] { intervalStr, symbol }, x => onMessage(x.WithSymbol(x.Data.Symbol).WithStreamId(x.StreamId + "." + x.Data.Interval)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v4/") + "/", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<GateIoBookTickerUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoSubscription<GateIoBookTickerUpdate>(_logger, "spot.book_ticker", new[] { "spot.book_ticker." + symbol }, new[] { symbol }, x => onMessage(x.WithSymbol(x.Data.Symbol)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v4/") + "/", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<GateIoBookTickerUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoSubscription<GateIoBookTickerUpdate>(_logger, "spot.book_ticker", symbols.Select(x => "spot.book_ticker." + x), symbols, x => onMessage(x.WithSymbol(x.Data.Symbol)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v4/") + "/", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int? updateMs, Action<DataEvent<GateIoOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            updateMs ??= 1000;
            updateMs.Value.ValidateIntValues(nameof(updateMs), 100, 1000);

            var subscription = new GateIoSubscription<GateIoOrderBookUpdate>(_logger, "spot.order_book_update", new[] { "spot.order_book_update." + symbol }, new[] { symbol, updateMs + "ms" }, x => onMessage(x.WithSymbol(x.Data.Symbol)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v4/") + "/", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int depth, int? updateMs, Action<DataEvent<GateIoPartialOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            updateMs ??= 1000;
            depth.ValidateIntValues(nameof(depth), 5, 10, 20, 50, 100);
            updateMs.Value.ValidateIntValues(nameof(updateMs), 100, 1000);

            var subscription = new GateIoSubscription<GateIoPartialOrderBookUpdate>(_logger, "spot.order_book", new[] { "spot.order_book." + symbol }, new[] { symbol, depth.ToString(), updateMs + "ms" }, x => onMessage(x.WithSymbol(x.Data.Symbol)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v4/") + "/", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<DataEvent<IEnumerable<GateIoOrderUpdate>>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<IEnumerable<GateIoOrderUpdate>>(_logger, "spot.orders", new[] { "spot.orders" }, new[] { "!all" }, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v4/") + "/", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(Action<DataEvent<IEnumerable<GateIoUserTradeUpdate>>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<IEnumerable<GateIoUserTradeUpdate>>(_logger, "spot.usertrades", new[] { "spot.usertrades" }, new[] { "!all" }, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v4/") + "/", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(Action<DataEvent<IEnumerable<GateIoBalanceUpdate>>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<IEnumerable<GateIoBalanceUpdate>>(_logger, "spot.balances", new[] { "spot.balances" }, null, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v4/") + "/", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarginBalanceUpdatesAsync(Action<DataEvent<IEnumerable<GateIoMarginBalanceUpdate>>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<IEnumerable<GateIoMarginBalanceUpdate>>(_logger, "spot.margin_balances", new[] { "spot.margin_balances" }, null, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v4/") + "/", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToFundingBalanceUpdatesAsync(Action<DataEvent<IEnumerable<GateIoFundingBalanceUpdate>>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<IEnumerable<GateIoFundingBalanceUpdate>>(_logger, "spot.funding_balances", new[] { "spot.funding_balances" }, null, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v4/") + "/", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToCrossMarginBalanceUpdatesAsync(Action<DataEvent<IEnumerable<GateIoCrossMarginBalanceUpdate>>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<IEnumerable<GateIoCrossMarginBalanceUpdate>>(_logger, "spot.cross_balances", new[] { "spot.cross_balances" }, null, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v4/") + "/", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTriggerOrderUpdatesAsync(Action<DataEvent<GateIoTriggerOrderUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<GateIoTriggerOrderUpdate>(_logger, "spot.priceorders", new[] { "spot.priceorders" }, new[] { "!all" }, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v4/") + "/", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public override string? GetListenerIdentifier(IMessageAccessor message)
        {
            var id = message.GetValue<long?>(_idPath);
            if (id != null)
                return id.ToString();

            var channel = message.GetValue<string>(_channelPath);

            if (string.Equals(channel, "spot.trades")
                || string.Equals(channel, "spot.tickers"))
            {
                return channel + "." + message.GetValue<string>(_symbolPath);
            }

            if (string.Equals(channel, "spot.candlesticks"))
                return channel + "." + message.GetValue<string>(_klinePath);

            if (string.Equals(channel, "spot.book_ticker")
             || string.Equals(channel, "spot.order_book_update")
             || string.Equals(channel, "spot.order_book"))
                    return channel + "." + message.GetValue<string>(_symbolPath2);

            return channel;
        }

        /// <inheritdoc />
        protected override Query? GetAuthenticationRequest() => null;

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset) => baseAsset.ToUpperInvariant() + "_" + quoteAsset.ToUpperInvariant();
    }
}
