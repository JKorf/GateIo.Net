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
using GateIo.Net.Objects.Internal;
using GateIo.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using System.Diagnostics.Contracts;

namespace GateIo.Net.Clients.FuturesApi
{
    /// <summary>
    /// Client providing access to the GateIo futures websocket Api
    /// </summary>
    internal partial class GateIoSocketClientPerpetualFuturesApi : SocketApiClient, IGateIoSocketClientPerpetualFuturesApi
    {
        #region fields
        private static readonly MessagePath _idPath = MessagePath.Get().Property("id");
        private static readonly MessagePath _channelPath = MessagePath.Get().Property("channel");
        private static readonly MessagePath _contractPath = MessagePath.Get().Property("result").Index(0).Property("contract");
        private static readonly MessagePath _contractPath2 = MessagePath.Get().Property("result").Property("s");
        private static readonly MessagePath _klinePath = MessagePath.Get().Property("result").Index(0).Property("n");
        private static readonly MessagePath _idPath2 = MessagePath.Get().Property("request_id");
        private static readonly MessagePath _ackPath = MessagePath.Get().Property("ack");
        private static readonly MessagePath _statusPath = MessagePath.Get().Property("header").Property("status");
        internal string _brokerId;
        #endregion

        #region constructor/destructor

        /// <summary>
        /// ctor
        /// </summary>
        internal GateIoSocketClientPerpetualFuturesApi(ILogger logger, GateIoSocketOptions options) :
            base(logger, options.Environment.FuturesSocketClientAddress!, options, options.PerpetualFuturesOptions)
        {
            _brokerId = string.IsNullOrEmpty(options.BrokerId) ? "copytraderpw" : options.BrokerId!;

            RegisterPeriodicQuery(
                "Ping",
                TimeSpan.FromSeconds(30),
                x => new GateIoPingQuery("futures.ping"),
                (connection, result) =>
                {
                    if (result.Error?.Message.Equals("Query timeout") == true)
                    {
                        // Ping timeout, reconnect
                        _logger.LogWarning("[Sckt {SocketId}] Ping response timeout, reconnecting", connection.SocketId);
                        _ = connection.TriggerReconnectAsync();
                    }
                });
        }
        #endregion

        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(GateIoExchange.SerializerContext));
        /// <inheritdoc />
        protected override IByteMessageAccessor CreateAccessor() => new SystemTextJsonByteMessageAccessor(SerializerOptions.WithConverters(GateIoExchange.SerializerContext));

        public IGateIoSocketClientPerpetualFuturesApiShared SharedClient => this;

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new GateIoAuthenticationProvider(credentials);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string settlementAsset, string contract, Action<DataEvent<IEnumerable<GateIoPerpTradeUpdate>>> onMessage, CancellationToken ct = default)
            => await SubscribeToTradeUpdatesAsync(settlementAsset, [contract], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string settlementAsset, IEnumerable<string> contracts, Action<DataEvent<IEnumerable<GateIoPerpTradeUpdate>>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoSubscription<IEnumerable<GateIoPerpTradeUpdate>>(_logger, "futures.trades", contracts.Select(x => "futures.trades." + x ).ToArray(), contracts, x => onMessage(x.WithSymbol(x.Data.First().Contract)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset.ToLowerInvariant()), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string settlementAsset, string contract, Action<DataEvent<IEnumerable<GateIoPerpTickerUpdate>>> onMessage, CancellationToken ct = default)
            => await SubscribeToTickerUpdatesAsync(settlementAsset, [contract], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string settlementAsset, IEnumerable<string> contracts, Action<DataEvent<IEnumerable<GateIoPerpTickerUpdate>>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoSubscription<IEnumerable<GateIoPerpTickerUpdate>>(_logger, "futures.tickers", contracts.Select(x => "futures.tickers." + x ).ToArray(), contracts.ToArray(), x => onMessage(x.WithSymbol(x.Data.First().Contract)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset.ToLowerInvariant()), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string settlementAsset, string contract, Action<DataEvent<GateIoPerpBookTickerUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToBookTickerUpdatesAsync(settlementAsset, [contract], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string settlementAsset, IEnumerable<string> contracts, Action<DataEvent<GateIoPerpBookTickerUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoSubscription<GateIoPerpBookTickerUpdate>(_logger, "futures.book_ticker", contracts.Select(x => "futures.book_ticker." + x ).ToArray(), contracts.ToArray(), x => onMessage(x.WithSymbol(x.Data.Contract)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset.ToLowerInvariant()), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string settlementAsset, string contract, int updateMs, int depth, Action<DataEvent<GateIoPerpOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            updateMs.ValidateIntValues(nameof(updateMs), 20, 100);
            depth.ValidateIntValues(nameof(depth), 20, 50, 100);

            var subscription = new GateIoSubscription<GateIoPerpOrderBookUpdate>(_logger, "futures.order_book_update", ["futures.order_book_update." + contract], new[] { contract, updateMs + "ms", depth.ToString() }, x => onMessage(x.WithSymbol(x.Data.Contract)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset.ToLowerInvariant()), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string settlementAsset, string contract, KlineInterval interval, Action<DataEvent<IEnumerable<GateIoPerpKlineUpdate>>> onMessage, CancellationToken ct = default)
        {
            var intervalStr = EnumConverter.GetString(interval);
            var subscription = new GateIoSubscription<IEnumerable<GateIoPerpKlineUpdate>>(_logger, "futures.candlesticks", ["futures.candlesticks." + intervalStr + "_" + contract], new[] { intervalStr, contract }, x => onMessage(x.WithSymbol(x.Data.First().Contract)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset.ToLowerInvariant()), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<IEnumerable<GateIoPerpOrder>>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<IEnumerable<GateIoPerpOrder>>(_logger, "futures.orders", new[] { "futures.orders" }, new[] { userId.ToString(), "!all" }, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset.ToLowerInvariant()), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<IEnumerable<GateIoPerpUserTrade>>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<IEnumerable<GateIoPerpUserTrade>>(_logger, "futures.usertrades", new[] { "futures.usertrades" }, new[] { userId.ToString(), "!all" }, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset.ToLowerInvariant()), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserLiquidationUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<IEnumerable<GateIoPerpLiquidation>>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<IEnumerable<GateIoPerpLiquidation>>(_logger, "futures.liquidates", new[] { "futures.liquidates" }, new[] { userId.ToString(), "!all" }, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset.ToLowerInvariant()), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserAutoDeleverageUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<IEnumerable<GateIoPerpAutoDeleverage>>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<IEnumerable<GateIoPerpAutoDeleverage>>(_logger, "futures.auto_deleverages", new[] { "futures.auto_deleverages" }, new[] { userId.ToString(), "!all" }, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset.ToLowerInvariant()), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPositionCloseUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<IEnumerable<GateIoPerpPositionCloseUpdate>>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<IEnumerable<GateIoPerpPositionCloseUpdate>>(_logger, "futures.position_closes", new[] { "futures.position_closes" }, new[] { userId.ToString(), "!all" }, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset.ToLowerInvariant()), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<IEnumerable<GateIoPerpBalanceUpdate>>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<IEnumerable<GateIoPerpBalanceUpdate>>(_logger, "futures.balances", new[] { "futures.balances" }, new[] { userId.ToString() }, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset.ToLowerInvariant()), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToReduceRiskLimitUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<IEnumerable<GateIoPerpRiskLimitUpdate>>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<IEnumerable<GateIoPerpRiskLimitUpdate>>(_logger, "futures.reduce_risk_limits", new[] { "futures.reduce_risk_limits" }, new[] { userId.ToString(), "!all" }, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset.ToLowerInvariant()), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<IEnumerable<GateIoPositionUpdate>>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<IEnumerable<GateIoPositionUpdate>>(_logger, "futures.positions", new[] { "futures.positions" }, new[] { userId.ToString(), "!all" }, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset.ToLowerInvariant()), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTriggerOrderUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<IEnumerable<GateIoPerpTriggerOrderUpdate>>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<IEnumerable<GateIoPerpTriggerOrderUpdate>>(_logger, "futures.autoorders", new[] { "futures.autoorders" }, new[] { userId.ToString(), "!all" }, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset.ToLowerInvariant()), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<GateIoPerpOrder>> PlaceOrderAsync(
            string settlementAsset,
            string contract,
            OrderSide orderSide,
            int quantity,
            decimal? price = null,
            bool? closePosition = null,
            bool? reduceOnly = null,
            TimeInForce? timeInForce = null,
            int? icebergQuantity = null,
            CloseSide? closeSide = null,
            SelfTradePreventionMode? stpMode = null,
            string? text = null,
            CancellationToken ct = default)
        {
            var id = ExchangeHelpers.NextId();
            var query = new GateIoRequestQuery<GateIoFuturesPlaceOrderRequest, GateIoPerpOrder>(id, "futures.order_place", "api", new GateIoFuturesPlaceOrderRequest
            {
                Close = closePosition,
                CloseSide = closeSide,
                Contract = contract,
                Iceberg = icebergQuantity,
                Price = price,
                Quantity = orderSide == OrderSide.Buy ? quantity : -quantity,
                ReduceOnly = reduceOnly,
                StpMode = stpMode,
                Text = text,
                TimeInForce = timeInForce
            }, true,
            new Dictionary<string, string>
            {
                { "X-Gate-Channel-Id", _brokerId }
            });

            return await QueryAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset.ToLowerInvariant()), query, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<IEnumerable<GateIoPerpOrder>>> PlaceMultipleOrderAsync(
            string settlementAsset,
            IEnumerable<GateIoPerpBatchPlaceRequest> orders,
            CancellationToken ct = default)
        {
            var id = ExchangeHelpers.NextId();
            var query = new GateIoRequestQuery<IEnumerable<GateIoFuturesPlaceOrderRequest>, IEnumerable<GateIoPerpOrder>>(id, "futures.order_batch_place", "api", orders.Select(o => new GateIoFuturesPlaceOrderRequest
            {
                Close = o.ClosePosition,
                CloseSide = o.CloseSide,
                Contract = o.Contract,
                Iceberg = o.IcebergQuantity,
                Price = o.Price,
                Quantity = o.Quantity,
                ReduceOnly = o.ReduceOnly,
                StpMode = o.StpMode,
                Text = o.Text,
                TimeInForce = o.TimeInForce
            }), true,
            new Dictionary<string, string>
            {
                { "X-Gate-Channel-Id", _brokerId }
            });

            return await QueryAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset.ToLowerInvariant()), query, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<GateIoPerpOrder>> GetOrderAsync(string settlementAsset, long orderId, CancellationToken ct = default)
        {
            var id = ExchangeHelpers.NextId();
            var query = new GateIoRequestQuery<GateIoFuturesGetOrderRequest, GateIoPerpOrder>(id, "futures.order_status", "api", new GateIoFuturesGetOrderRequest
            {
                OrderId = orderId.ToString()
            }, true);

            return await QueryAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset.ToLowerInvariant()), query, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<IEnumerable<GateIoPerpOrder>>> GetOrdersAsync(
            string settlementAsset,
            bool open,
            string? contract = null, 
            int? limit = null,
            int? offset = null,
            string? lastId = null,
            CancellationToken ct = default)
        {
            var id = ExchangeHelpers.NextId();
            var query = new GateIoRequestQuery<GateIoFuturesListOrdersRequest, IEnumerable<GateIoPerpOrder>>(id, "futures.order_list", "api", new GateIoFuturesListOrdersRequest
            {
                Contract = contract,
                LastId = lastId,
                Limit = limit,
                Offset = offset,
                Status = open ? "open" : "close"
            }, true);

            return await QueryAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset.ToLowerInvariant()), query, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<GateIoPerpOrder>> CancelOrderAsync(string settlementAsset, long orderId, CancellationToken ct = default)
        {
            var id = ExchangeHelpers.NextId();
            var query = new GateIoRequestQuery<GateIoFuturesGetOrderRequest, GateIoPerpOrder>(id, "futures.order_cancel", "api", new GateIoFuturesGetOrderRequest
            {
                OrderId = orderId.ToString()
            }, true);

            return await QueryAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset.ToLowerInvariant()), query, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<IEnumerable<GateIoPerpOrder>>> CancelOrdersAsync(
            string settlementAsset,
            string contract,
            OrderSide? side = null,
            CancellationToken ct = default)
        {
            var id = ExchangeHelpers.NextId();
            var query = new GateIoRequestQuery<GateIoFuturesCancelAllOrderRequest, IEnumerable<GateIoPerpOrder>>(id, "futures.order_cancel_cp", "api", new GateIoFuturesCancelAllOrderRequest
            {
                Contract = contract,
                Side = side == null ? null : side == OrderSide.Buy ? "bid" : "ask"
            }, true);

            return await QueryAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset.ToLowerInvariant()), query, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<GateIoOrder>> EditOrderAsync(string settlementAsset,
            long orderId,
            decimal? price = null,
            int? quantity = null,
            string? amendText = null,
            CancellationToken ct = default)
        {
            var id = ExchangeHelpers.NextId();
            var query = new GateIoRequestQuery<GateIoFuturesAmendOrderRequest, GateIoOrder>(id, "futures.order_amend", "api", new GateIoFuturesAmendOrderRequest
            {
                Quantity = quantity,
                Price = price,
                AmendText = amendText,
                OrderId = orderId.ToString()
            }, true);

            return await QueryAsync(BaseAddress.AppendPath("v4/ws/" + settlementAsset.ToLowerInvariant()), query, ct).ConfigureAwait(false);
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
                {
                    return id2 + "ack";
                }

                return id2;
            }

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
            {
                return channel + "." + message.GetValue<string>(_contractPath2);
            }

            return channel;
        }

        /// <inheritdoc />
        protected override Task<Query?> GetAuthenticationRequestAsync(SocketConnection connection)
        {
            var provider = (GateIoAuthenticationProvider)AuthenticationProvider!;
            var timestamp = DateTimeConverter.ConvertToSeconds(DateTime.UtcNow.AddSeconds(-1)).Value;
            var signStr = $"api\nfutures.login\n\n{timestamp}";
            var id = ExchangeHelpers.NextId();
            return Task.FromResult<Query?>(new GateIoLoginQuery(id, "futures.login", "api", provider.ApiKey, provider.SignSocketRequest(signStr), timestamp));
        }

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => GateIoExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);
    }
}
