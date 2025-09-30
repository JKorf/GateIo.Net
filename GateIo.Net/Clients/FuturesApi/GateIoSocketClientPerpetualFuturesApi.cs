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
using System.Net.WebSockets;
using CryptoExchange.Net.Objects.Errors;

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
        private static readonly MessagePath _contractPath3 = MessagePath.Get().Property("result").Property("contract");
        private static readonly MessagePath _klinePath = MessagePath.Get().Property("result").Index(0).Property("n");
        private static readonly MessagePath _idPath2 = MessagePath.Get().Property("request_id");
        private static readonly MessagePath _ackPath = MessagePath.Get().Property("ack");
        private static readonly MessagePath _statusPath = MessagePath.Get().Property("header").Property("status");
        internal string _brokerId;
        private readonly bool _demoTrading;
        #endregion

        #region constructor/destructor

        /// <summary>
        /// ctor
        /// </summary>
        internal GateIoSocketClientPerpetualFuturesApi(ILogger logger, GateIoSocketOptions options) :
            base(logger, options.Environment.FuturesSocketClientAddress!, options, options.PerpetualFuturesOptions)
        {
            _brokerId = string.IsNullOrEmpty(options.BrokerId) ? "copytraderpw" : options.BrokerId!;

            _demoTrading = options.Environment.Name == TradeEnvironmentNames.Testnet;

            RegisterPeriodicQuery(
                "Ping",
                TimeSpan.FromSeconds(30),
                x => new GateIoPingQuery("futures.ping"),
                (connection, result) =>
                {
                    if (result.Error?.ErrorType == ErrorType.Timeout)
                    {
                        // Ping timeout, reconnect
                        _logger.LogWarning("[Sckt {SocketId}] Ping response timeout, reconnecting", connection.SocketId);
                        _ = connection.TriggerReconnectAsync();
                    }
                });
        }
        #endregion

        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(GateIoExchange._serializerContext));
        /// <inheritdoc />
        protected override IByteMessageAccessor CreateAccessor(WebSocketMessageType type) => new SystemTextJsonByteMessageAccessor(SerializerOptions.WithConverters(GateIoExchange._serializerContext));

        public IGateIoSocketClientPerpetualFuturesApiShared SharedClient => this;

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new GateIoAuthenticationProvider(credentials);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string settlementAsset, string contract, Action<DataEvent<GateIoPerpTradeUpdate[]>> onMessage, CancellationToken ct = default)
            => await SubscribeToTradeUpdatesAsync(settlementAsset, [contract], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string settlementAsset, IEnumerable<string> contracts, Action<DataEvent<GateIoPerpTradeUpdate[]>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoSubscription<GateIoPerpTradeUpdate[]>(_logger, this, "futures.trades", contracts.Select(x => "futures.trades." + x ).ToArray(), contracts.ToArray(), x => onMessage(x.WithSymbol(x.Data.First().Contract)), false);
            return await SubscribeAsync(BaseAddress.AppendPath(GetSocketPath(settlementAsset)), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string settlementAsset, string contract, Action<DataEvent<GateIoPerpTickerUpdate[]>> onMessage, CancellationToken ct = default)
            => await SubscribeToTickerUpdatesAsync(settlementAsset, [contract], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string settlementAsset, IEnumerable<string> contracts, Action<DataEvent<GateIoPerpTickerUpdate[]>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoSubscription<GateIoPerpTickerUpdate[]>(_logger, this, "futures.tickers", contracts.Select(x => "futures.tickers." + x ).ToArray(), contracts.ToArray(), x => onMessage(x.WithSymbol(x.Data.First().Contract)), false);
            return await SubscribeAsync(BaseAddress.AppendPath(GetSocketPath(settlementAsset)), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string settlementAsset, string contract, Action<DataEvent<GateIoPerpBookTickerUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToBookTickerUpdatesAsync(settlementAsset, [contract], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string settlementAsset, IEnumerable<string> contracts, Action<DataEvent<GateIoPerpBookTickerUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoSubscription<GateIoPerpBookTickerUpdate>(_logger, this, "futures.book_ticker", contracts.Select(x => "futures.book_ticker." + x ).ToArray(), contracts.ToArray(), x => onMessage(x.WithSymbol(x.Data.Contract)), false);
            return await SubscribeAsync(BaseAddress.AppendPath(GetSocketPath(settlementAsset)), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookV2UpdatesAsync(string settlementAsset, string contract, int depth, Action<DataEvent<GateIoPerpOrderBookV2Update>> onMessage, CancellationToken ct = default)
        {
            depth.ValidateIntValues(nameof(depth), 50, 400);

            var subscription = new GateIoSubscription<GateIoPerpOrderBookV2Update>(_logger, this, "futures.obu", [$"ob.{contract}.{depth}"], new[] { $"ob.{contract}.{depth}" }, x => onMessage(x.WithUpdateType(x.Data.Full ? SocketUpdateType.Snapshot : SocketUpdateType.Update)), false);
            return await SubscribeAsync(BaseAddress.AppendPath(GetSocketPath(settlementAsset)), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string settlementAsset, string contract, int updateMs, int depth, Action<DataEvent<GateIoPerpOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            updateMs.ValidateIntValues(nameof(updateMs), 20, 100);
            depth.ValidateIntValues(nameof(depth), 20, 50, 100);

            var subscription = new GateIoSubscription<GateIoPerpOrderBookUpdate>(_logger, this, "futures.order_book_update", ["futures.order_book_update." + contract], new[] { contract, updateMs + "ms", depth.ToString() }, x => onMessage(x.WithSymbol(x.Data.Contract).WithUpdateType(x.Data.Full ? SocketUpdateType.Snapshot : SocketUpdateType.Update)), false);
            return await SubscribeAsync(BaseAddress.AppendPath(GetSocketPath(settlementAsset)), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string settlementAsset, string contract, KlineInterval interval, Action<DataEvent<GateIoPerpKlineUpdate[]>> onMessage, CancellationToken ct = default)
        {
            var intervalStr = EnumConverter.GetString(interval);
            var subscription = new GateIoSubscription<GateIoPerpKlineUpdate[]>(_logger, this, "futures.candlesticks", ["futures.candlesticks." + intervalStr + "_" + contract], new[] { intervalStr, contract }, x => onMessage(x.WithSymbol(x.Data.First().Contract)), false);
            return await SubscribeAsync(BaseAddress.AppendPath(GetSocketPath(settlementAsset)), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToContractStatsUpdatesAsync(string settlementAsset, string contract, KlineInterval interval, Action<DataEvent<GateIoPerpContractStats>> onMessage, CancellationToken ct = default)
        {
            var intervalStr = EnumConverter.GetString(interval);
            var subscription = new GateIoSubscription<GateIoPerpContractStats>(_logger, this, "futures.contract_stats", ["futures.contract_stats." + contract], new[] { contract, intervalStr }, onMessage, false);
            return await SubscribeAsync(BaseAddress.AppendPath(GetSocketPath(settlementAsset)), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<GateIoPerpOrder[]>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<GateIoPerpOrder[]>(_logger, this, "futures.orders", new[] { "futures.orders" }, new[] { userId.ToString(), "!all" }, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath(GetSocketPath(settlementAsset)), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<GateIoPerpUserTrade[]>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<GateIoPerpUserTrade[]>(_logger, this, "futures.usertrades", new[] { "futures.usertrades" }, new[] { userId.ToString(), "!all" }, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath(GetSocketPath(settlementAsset)), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserLiquidationUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<GateIoPerpLiquidation[]>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<GateIoPerpLiquidation[]>(_logger, this, "futures.liquidates", new[] { "futures.liquidates" }, new[] { userId.ToString(), "!all" }, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath(GetSocketPath(settlementAsset)), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserAutoDeleverageUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<GateIoPerpAutoDeleverage[]>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<GateIoPerpAutoDeleverage[]>(_logger, this, "futures.auto_deleverages", new[] { "futures.auto_deleverages" }, new[] { userId.ToString(), "!all" }, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath(GetSocketPath(settlementAsset)), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPositionCloseUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<GateIoPerpPositionCloseUpdate[]>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<GateIoPerpPositionCloseUpdate[]>(_logger, this, "futures.position_closes", new[] { "futures.position_closes" }, new[] { userId.ToString(), "!all" }, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath(GetSocketPath(settlementAsset)), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<GateIoPerpBalanceUpdate[]>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<GateIoPerpBalanceUpdate[]>(_logger, this, "futures.balances", new[] { "futures.balances" }, new[] { userId.ToString() }, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath(GetSocketPath(settlementAsset)), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToReduceRiskLimitUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<GateIoPerpRiskLimitUpdate[]>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<GateIoPerpRiskLimitUpdate[]>(_logger, this, "futures.reduce_risk_limits", new[] { "futures.reduce_risk_limits" }, new[] { userId.ToString(), "!all" }, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath(GetSocketPath(settlementAsset)), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<GateIoPositionUpdate[]>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<GateIoPositionUpdate[]>(_logger, this, "futures.positions", new[] { "futures.positions" }, new[] { userId.ToString(), "!all" }, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath(GetSocketPath(settlementAsset)), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTriggerOrderUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<GateIoPerpTriggerOrderUpdate[]>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<GateIoPerpTriggerOrderUpdate[]>(_logger, this, "futures.autoorders", new[] { "futures.autoorders" }, new[] { userId.ToString(), "!all" }, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath(GetSocketPath(settlementAsset)), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAdlUpdatesAsync(string settlementAsset, Action<DataEvent<GateIoAdlUpdate[]>> onMessage, CancellationToken ct = default)
        {
            var subscription = new GateIoAuthSubscription<GateIoAdlUpdate[]>(_logger, this, "futures.position_adl_rank", new[] { "futures.position_adl_rank" }, new[] { "!all" }, onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath(GetSocketPath(settlementAsset)), subscription, ct).ConfigureAwait(false);
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
            var query = new GateIoRequestQuery<GateIoFuturesPlaceOrderRequest, GateIoPerpOrder>(this, id, "futures.order_place", "api", new GateIoFuturesPlaceOrderRequest
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

            return await QueryAsync(BaseAddress.AppendPath(GetSocketPath(settlementAsset)), query, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<GateIoPerpOrder[]>> PlaceMultipleOrderAsync(
            string settlementAsset,
            IEnumerable<GateIoPerpBatchPlaceRequest> orders,
            CancellationToken ct = default)
        {
            var id = ExchangeHelpers.NextId();
            var query = new GateIoRequestQuery<GateIoFuturesPlaceOrderRequest[], GateIoPerpOrder[]>(this, id, "futures.order_batch_place", "api", orders.Select(o => new GateIoFuturesPlaceOrderRequest
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
            }).ToArray(), true,
            new Dictionary<string, string>
            {
                { "X-Gate-Channel-Id", _brokerId }
            });

            return await QueryAsync(BaseAddress.AppendPath(GetSocketPath(settlementAsset)), query, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<GateIoPerpOrder>> GetOrderAsync(string settlementAsset, long orderId, CancellationToken ct = default)
        {
            var id = ExchangeHelpers.NextId();
            var query = new GateIoRequestQuery<GateIoFuturesGetOrderRequest, GateIoPerpOrder>(this, id, "futures.order_status", "api", new GateIoFuturesGetOrderRequest
            {
                OrderId = orderId.ToString()
            }, true);

            return await QueryAsync(BaseAddress.AppendPath(GetSocketPath(settlementAsset)), query, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<GateIoPerpOrder[]>> GetOrdersAsync(
            string settlementAsset,
            bool open,
            string? contract = null,
            int? limit = null,
            int? offset = null,
            string? lastId = null,
            CancellationToken ct = default)
        {
            var id = ExchangeHelpers.NextId();
            var query = new GateIoRequestQuery<GateIoFuturesListOrdersRequest, GateIoPerpOrder[]>(this, id, "futures.order_list", "api", new GateIoFuturesListOrdersRequest
            {
                Contract = contract,
                LastId = lastId,
                Limit = limit,
                Offset = offset,
                Status = open ? "open" : "close"
            }, true);

            return await QueryAsync(BaseAddress.AppendPath(GetSocketPath(settlementAsset)), query, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<GateIoPerpOrder>> CancelOrderAsync(string settlementAsset, long orderId, CancellationToken ct = default)
        {
            var id = ExchangeHelpers.NextId();
            var query = new GateIoRequestQuery<GateIoFuturesGetOrderRequest, GateIoPerpOrder>(this, id, "futures.order_cancel", "api", new GateIoFuturesGetOrderRequest
            {
                OrderId = orderId.ToString()
            }, true);

            return await QueryAsync(BaseAddress.AppendPath(GetSocketPath(settlementAsset)), query, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<GateIoPerpOrder[]>> CancelOrdersAsync(
            string settlementAsset,
            string contract,
            OrderSide? side = null,
            CancellationToken ct = default)
        {
            var id = ExchangeHelpers.NextId();
            var query = new GateIoRequestQuery<GateIoFuturesCancelAllOrderRequest, GateIoPerpOrder[]>(this, id, "futures.order_cancel_cp", "api", new GateIoFuturesCancelAllOrderRequest
            {
                Contract = contract,
                Side = side == null ? null : side == OrderSide.Buy ? "bid" : "ask"
            }, true);

            return await QueryAsync(BaseAddress.AppendPath(GetSocketPath(settlementAsset)), query, ct).ConfigureAwait(false);
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
            var query = new GateIoRequestQuery<GateIoFuturesAmendOrderRequest, GateIoOrder>(this, id, "futures.order_amend", "api", new GateIoFuturesAmendOrderRequest
            {
                Quantity = quantity,
                Price = price,
                AmendText = amendText,
                OrderId = orderId.ToString()
            }, true);

            return await QueryAsync(BaseAddress.AppendPath(GetSocketPath(settlementAsset)), query, ct).ConfigureAwait(false);
        }

        private string GetSocketPath(string settlementAsset) => $"{(!_demoTrading ? "v4/ws" : "v4/ws/futures")}/{settlementAsset.ToLowerInvariant()}";

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

            if (string.Equals(channel, "futures.obu"))
                return message.GetValue<string>(_contractPath2);

            if (string.Equals(channel, "futures.trades")
                || string.Equals(channel, "futures.tickers"))
            {
                return channel + "." + message.GetValue<string>(_contractPath);
            }

            if (string.Equals(channel, "futures.candlesticks"))
                return channel + "." + message.GetValue<string>(_klinePath);

            if (string.Equals(channel, "futures.contract_stats"))
                return channel + "." + message.GetValue<string>(_contractPath3);

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
            return Task.FromResult<Query?>(new GateIoLoginQuery(this, id, "futures.login", "api", provider.ApiKey, provider.SignSocketRequest(signStr), timestamp));
        }

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => GateIoExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);
    }
}
