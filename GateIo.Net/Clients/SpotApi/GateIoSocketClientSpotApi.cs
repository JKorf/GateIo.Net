﻿using System;
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
using GateIo.Net.Objects.Sockets;
using GateIo.Net.Objects.Internal;
using CryptoExchange.Net.SharedApis;

namespace GateIo.Net.Clients.SpotApi
{
    /// <summary>
    /// Client providing access to the GateIo spot websocket Api
    /// </summary>
    internal partial class GateIoSocketClientSpotApi : SocketApiClient, IGateIoSocketClientSpotApi
    {
        #region fields
        private static readonly MessagePath _idPath = MessagePath.Get().Property("id");
        private static readonly MessagePath _channelPath = MessagePath.Get().Property("channel");
        private static readonly MessagePath _symbolPath = MessagePath.Get().Property("result").Property("currency_pair");
        private static readonly MessagePath _symbolPath2 = MessagePath.Get().Property("result").Property("s");
        private static readonly MessagePath _klinePath = MessagePath.Get().Property("result").Property("n");
        private static readonly MessagePath _idPath2 = MessagePath.Get().Property("request_id");
        private static readonly MessagePath _ackPath = MessagePath.Get().Property("ack");
        private static readonly MessagePath _statusPath = MessagePath.Get().Property("header").Property("status");
        internal string _brokerId;
        #endregion

        #region constructor/destructor

        /// <summary>
        /// ctor
        /// </summary>
        internal GateIoSocketClientSpotApi(ILogger logger, GateIoSocketOptions options) :
            base(logger, options.Environment.SpotSocketClientAddress!, options, options.SpotOptions)
        {
            _brokerId = string.IsNullOrEmpty(options.BrokerId) ? "copytraderpw" : options.BrokerId!;

            SetDedicatedConnection(BaseAddress.AppendPath("ws/v4/") + "/", true);
        }
        #endregion 

        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer();
        /// <inheritdoc />
        protected override IByteMessageAccessor CreateAccessor() => new SystemTextJsonByteMessageAccessor();

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new GateIoAuthenticationProvider(credentials);

        public IGateIoSocketClientSpotApiShared SharedClient => this;

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
            if (AuthenticationProvider == null)
                return new CallResult<UpdateSubscription>(new NoApiCredentialsError());

            var subscription = new GateIoAuthSubscription<IEnumerable<GateIoOrderUpdate>>(_logger, "spot.orders", new[] { "spot.orders" }, new[] { "!all" }, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v4/") + "/", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(Action<DataEvent<IEnumerable<GateIoUserTradeUpdate>>> onMessage, CancellationToken ct = default)
        {
            if (AuthenticationProvider == null)
                return new CallResult<UpdateSubscription>(new NoApiCredentialsError());

            var subscription = new GateIoAuthSubscription<IEnumerable<GateIoUserTradeUpdate>>(_logger, "spot.usertrades", new[] { "spot.usertrades" }, new[] { "!all" }, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v4/") + "/", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(Action<DataEvent<IEnumerable<GateIoBalanceUpdate>>> onMessage, CancellationToken ct = default)
        {
            if (AuthenticationProvider == null)
                return new CallResult<UpdateSubscription>(new NoApiCredentialsError());

            var subscription = new GateIoAuthSubscription<IEnumerable<GateIoBalanceUpdate>>(_logger, "spot.balances", new[] { "spot.balances" }, null, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v4/") + "/", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarginBalanceUpdatesAsync(Action<DataEvent<IEnumerable<GateIoMarginBalanceUpdate>>> onMessage, CancellationToken ct = default)
        {
            if (AuthenticationProvider == null)
                return new CallResult<UpdateSubscription>(new NoApiCredentialsError());

            var subscription = new GateIoAuthSubscription<IEnumerable<GateIoMarginBalanceUpdate>>(_logger, "spot.margin_balances", new[] { "spot.margin_balances" }, null, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v4/") + "/", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToFundingBalanceUpdatesAsync(Action<DataEvent<IEnumerable<GateIoFundingBalanceUpdate>>> onMessage, CancellationToken ct = default)
        {
            if (AuthenticationProvider == null)
                return new CallResult<UpdateSubscription>(new NoApiCredentialsError());

            var subscription = new GateIoAuthSubscription<IEnumerable<GateIoFundingBalanceUpdate>>(_logger, "spot.funding_balances", new[] { "spot.funding_balances" }, null, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v4/") + "/", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToCrossMarginBalanceUpdatesAsync(Action<DataEvent<IEnumerable<GateIoCrossMarginBalanceUpdate>>> onMessage, CancellationToken ct = default)
        {
            if (AuthenticationProvider == null)
                return new CallResult<UpdateSubscription>(new NoApiCredentialsError());

            var subscription = new GateIoAuthSubscription<IEnumerable<GateIoCrossMarginBalanceUpdate>>(_logger, "spot.cross_balances", new[] { "spot.cross_balances" }, null, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v4/") + "/", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTriggerOrderUpdatesAsync(Action<DataEvent<GateIoTriggerOrderUpdate>> onMessage, CancellationToken ct = default)
        {
            if (AuthenticationProvider == null)
                return new CallResult<UpdateSubscription>(new NoApiCredentialsError());

            var subscription = new GateIoAuthSubscription<GateIoTriggerOrderUpdate>(_logger, "spot.priceorders", new[] { "spot.priceorders" }, new[] { "!all" }, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v4/") + "/", subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<GateIoOrder>> PlaceOrderAsync(string symbol,
            OrderSide side,
            NewOrderType type,
            decimal quantity,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            decimal? icebergQuantity = null,
            SpotAccountType? accountType = null,
            bool? autoBorrow = null,
            bool? autoRepay = null,
            SelfTradePreventionMode? selfTradePreventionMode = null,
            string? text = null, 
            CancellationToken ct = default)
        {
            var id = ExchangeHelpers.NextId();
            var query = new GateIoRequestQuery<GateIoSpotPlaceOrderRequest, GateIoOrder>(id, "spot.order_place", "api", new GateIoSpotPlaceOrderRequest
            {
                Symbol = symbol,
                AccountType = accountType,
                Side = side,
                OrderType = type,
                Quantity = quantity,
                Price = price,
                TimeInForce = timeInForce,
                Iceberg = icebergQuantity,
                AutoBorrow = autoBorrow,
                AutoRepay = autoRepay,
                StpMode = selfTradePreventionMode,
                Text = text ?? "t-" + ExchangeHelpers.RandomString(20)
            }, true,
            new Dictionary<string, string>
            {
                { "X-Gate-Channel-Id", _brokerId }
            });

            return await QueryAsync(BaseAddress.AppendPath("ws/v4/") + "/", query, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<IEnumerable<GateIoOrder>>> PlaceMultipleOrdersAsync(IEnumerable<GateIoBatchPlaceRequest> orders, CancellationToken ct = default)
        {
            var id = ExchangeHelpers.NextId();
            var query = new GateIoRequestQuery<IEnumerable<GateIoSpotPlaceOrderRequest>, IEnumerable<GateIoOrder>>(id, "spot.order_place", "api", orders.Select(o => new GateIoSpotPlaceOrderRequest
            {
                Text = o.Text ?? "t-" + ExchangeHelpers.RandomString(20),
                TimeInForce = o.TimeInForce,
                AccountType = o.AccountType,
                AutoBorrow = o.AutoBorrow,
                AutoRepay = o.AutoRepay,
                Iceberg = o.IcebergQuantity,
                OrderType = o.Type,
                Price = o.Price,
                Quantity = o.Quantity,
                Side = o.Side,
                StpMode = o.SelfTradePreventionMode,
                Symbol = o.Symbol
            }), true,
            new Dictionary<string, string>
            {
                { "X-Gate-Channel-Id", _brokerId }
            });

            return await QueryAsync(BaseAddress.AppendPath("ws/v4/") + "/", query, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<GateIoOrder>> EditOrderAsync(string symbol,
            long orderId,
            decimal? price = null,
            decimal? quantity = null,
            string? amendText = null,
            SpotAccountType? accountType = null,
            CancellationToken ct = default)
        {
            var id = ExchangeHelpers.NextId();
            var query = new GateIoRequestQuery<GateIoSpotAmendOrderRequest, GateIoOrder>(id, "spot.order_amend", "api", new GateIoSpotAmendOrderRequest
            {
                Symbol = symbol,
                AccountType = accountType,
                Quantity = quantity,
                Price = price,
                AmendText = amendText,
                OrderId = orderId.ToString()
            }, true);

            return await QueryAsync(BaseAddress.AppendPath("ws/v4/") + "/", query, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<GateIoOrder>> CancelOrderAsync(string symbol, long orderId, SpotAccountType? accountType = null, CancellationToken ct = default)
        {
            var id = ExchangeHelpers.NextId();
            var query = new GateIoRequestQuery<GateIoSpotGetOrderRequest, GateIoOrder>(id, "spot.order_cancel", "api", new GateIoSpotGetOrderRequest
            {
                OrderId = orderId.ToString(),
                Symbol = symbol,
                AccountType = accountType
            }, true);

            return await QueryAsync(BaseAddress.AppendPath("ws/v4/") + "/", query, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<IEnumerable<GateIoCancelResult>>> CancelOrdersAsync(IEnumerable<GateIoBatchCancelRequest> cancelRequests, CancellationToken ct = default)
        {
            var id = ExchangeHelpers.NextId();
            var query = new GateIoRequestQuery<IEnumerable<GateIoBatchCancelRequest>, IEnumerable<GateIoCancelResult>>(id, "spot.order_cancel_ids", "api", cancelRequests, true);

            return await QueryAsync(BaseAddress.AppendPath("ws/v4/") + "/", query, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<IEnumerable<GateIoOrder>>> CancelAllOrdersAsync(string symbol, OrderSide? side = null, SpotAccountType? accountType = null, CancellationToken ct = default)
        {
            var id = ExchangeHelpers.NextId();
            var query = new GateIoRequestQuery<GateIoSpotCancelAllOrderRequest, IEnumerable<GateIoOrder>>(id, "spot.order_cancel_cp", "api", new GateIoSpotCancelAllOrderRequest
            {
                Symbol = symbol,
                Side = side == null ? null : side == OrderSide.Buy ? "buy" : "sell",
                AccountType = accountType
            }, true);

            return await QueryAsync(BaseAddress.AppendPath("ws/v4/") + "/", query, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<GateIoOrder>> GetOrderAsync(string symbol, long orderId, SpotAccountType? accountType = null, CancellationToken ct = default)
        {
            var id = ExchangeHelpers.NextId();
            var query = new GateIoRequestQuery<GateIoSpotGetOrderRequest, GateIoOrder>(id, "spot.order_status", "api", new GateIoSpotGetOrderRequest
            {
                OrderId = orderId.ToString(),
                Symbol = symbol,
                AccountType = accountType
            }, true);

            return await QueryAsync(BaseAddress.AppendPath("ws/v4/") + "/", query, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public override string? GetListenerIdentifier(IMessageAccessor message)
        {
            var id = message.GetValue<long?>(_idPath);
            if (id != null)
                return id.ToString();

            var id2 = message.GetValue<string?>(_idPath2);
            if (id2 != null)
            {
                if (message.GetValue<bool?>(_ackPath) == true
                    && message.GetValue<string>(_statusPath) == "200")
                    return null;

                return id2;
            }

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
        protected override Query? GetAuthenticationRequest(SocketConnection connection)
        {

            var provider = (GateIoAuthenticationProvider)AuthenticationProvider!;
            var timestamp = DateTimeConverter.ConvertToSeconds(DateTime.UtcNow.AddSeconds(-1)).Value;
            var signStr = $"api\nspot.login\n\n{timestamp}";
            var id = ExchangeHelpers.NextId();
            return new GateIoLoginQuery(id, "spot.login", "api", provider.ApiKey, provider.SignSocketRequest(signStr), timestamp);
        }

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliveryDate = null) => baseAsset.ToUpperInvariant() + "_" + quoteAsset.ToUpperInvariant();
    }
}
