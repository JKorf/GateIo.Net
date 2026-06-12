using Microsoft.Extensions.Logging;
using GateIo.Net.Interfaces.Clients.PerpetualFuturesApi;
using System.Collections.Generic;
using CryptoExchange.Net.Objects;
using System.Threading.Tasks;
using GateIo.Net.Objects.Models;
using System.Net.Http;
using System.Threading;
using GateIo.Net.Enums;
using System;
using System.Linq;
using CryptoExchange.Net;

namespace GateIo.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    internal class GateIoRestClientPerpetualFuturesApiTrading : IGateIoRestClientPerpetualFuturesApiTrading
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly GateIoRestClientPerpetualFuturesApi _baseClient;

        internal GateIoRestClientPerpetualFuturesApiTrading(ILogger logger, GateIoRestClientPerpetualFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Positions

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPosition[]>> GetPositionsAsync(string settlementAsset, bool? holding = null, int? offset = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("offset", offset);
            parameters.Add("limit", limit);
            parameters.Add("holding", holding);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/positions", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPosition[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Position

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPosition>> GetPositionAsync(string settlementAsset, string contract, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/positions/{contract}", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPosition>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Update Position Margin

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPosition>> UpdatePositionMarginAsync(string settlementAsset, string contract, decimal change, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("change", change);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/positions/{contract}/margin", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPosition>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Update Position Leverage

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPosition>> UpdatePositionLeverageAsync(string settlementAsset, string contract, decimal leverage, decimal? crossLeverageLimit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("leverage", leverage);
            parameters.Add("cross_leverage_limit", crossLeverageLimit);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/positions/{contract}/leverage", GateIoExchange.RateLimiter.RestFuturesOther, 1, true, parameterPosition: HttpMethodParameterPosition.InUri);
            return await _baseClient.SendAsync<GateIoPosition>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Update Position Risk Limit

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPosition>> UpdatePositionRiskLimitAsync(string settlementAsset, string contract, decimal riskLimit, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("risk_limit", riskLimit);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/positions/{contract}/risk_limit", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPosition>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Dual Mode Positions

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPosition[]>> GetDualModePositionsAsync(string settlementAsset, string contract, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/dual_comp/positions/{contract}", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPosition[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Update Dual Mode Position Margin

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPosition[]>> UpdateDualModePositionMarginAsync(string settlementAsset, string contract, decimal change, PositionMode mode, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("change", change);
            parameters.Add("dual_side", mode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/dual_comp/positions/{contract}/margin", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPosition[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Update Dual Mode Position Leverage

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPosition[]>> UpdateDualModePositionLeverageAsync(string settlementAsset, string contract, decimal leverage, decimal? crossLeverageLimit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("leverage", leverage);
            parameters.Add("cross_leverage_limit", crossLeverageLimit);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/dual_comp/positions/{contract}/leverage", GateIoExchange.RateLimiter.RestFuturesOther, 1, true, parameterPosition: HttpMethodParameterPosition.InUri);
            return await _baseClient.SendAsync<GateIoPosition[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Update Dual Mode Position Risk Limit

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPosition[]>> UpdateDualModePositionRiskLimitAsync(string settlementAsset, string contract, int riskLimit, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("risk_limit", riskLimit);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/dual_comp/positions/{contract}/risk_limit", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPosition[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Place Order

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPerpOrder>> PlaceOrderAsync(
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
            decimal? takeProfitTriggerPrice = null,
            decimal? stopLossTriggerPrice = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("contract", contract);
            parameters.Add("size", orderSide == OrderSide.Buy ? quantity: -quantity);
            parameters.Add("price", price ?? 0);
            parameters.Add("close", closePosition);
            parameters.Add("reduce_only", reduceOnly);
            parameters.Add("tif", timeInForce);
            parameters.Add("iceberg", icebergQuantity);
            parameters.Add("text", text);
            parameters.Add("auto_size", closeSide);
            parameters.Add("stp_act", stpMode);
            parameters.Add("tpsl_tp_trigger_price", takeProfitTriggerPrice);
            parameters.Add("tpsl_sl_trigger_price", stopLossTriggerPrice);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/orders", GateIoExchange.RateLimiter.RestFuturesOrderPlacement, 1, true);
            return await _baseClient.SendAsync<GateIoPerpOrder>(
                request, 
                parameters, 
                ct, 
                1, 
                new Dictionary<string, string> 
                { 
                    { "X-Gate-Channel-Id", LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange) } 
                }).ConfigureAwait(false);
        }

        #endregion

        #region Place Multiple Orders

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPerpOrder[]>> PlaceMultipleOrderAsync(
            string settlementAsset, 
            IEnumerable<GateIoPerpBatchPlaceRequest> orders,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(orders.ToArray(), GateIoExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/batch_orders", GateIoExchange.RateLimiter.RestFuturesOrderPlacement, 1, true);
            var result = await _baseClient.SendAsync<GateIoPerpOrder[]>(
                request,
                parameters,
                ct, 
                1,
                new Dictionary<string, string> {
                    { "X-Gate-Channel-Id", LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange) }
                }).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Orders

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPerpOrder[]>> GetOrdersAsync(
            string settlementAsset,
            OrderStatus status,
            string? contract = null,
            int? limit = null,
            int? offset = null,
            string? lastId = null, 
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("contract", contract);
            parameters.Add("status", status);
            parameters.Add("limit", limit);
            parameters.Add("offset", offset);
            parameters.Add("last_id", lastId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/orders", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPerpOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel All Orders

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPerpOrder[]>> CancelAllOrdersAsync(string settlementAsset, string contract, OrderSide? side = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("contract", contract);
            parameters.Add("side", side);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/orders", GateIoExchange.RateLimiter.RestFuturesOrderCancelation, 1, true);
            return await _baseClient.SendAsync<GateIoPerpOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel After

        /// <inheritdoc />
        public async Task<HttpResult<GateIoCancelAfter>> CancelOrdersAfterAsync(string settlementAsset, TimeSpan cancelAfter, string? contract = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("timeout", (int)cancelAfter.TotalSeconds);
            parameters.Add("contract", contract);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/countdown_cancel_all", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoCancelAfter>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Orders

        /// <inheritdoc />
        public async Task<HttpResult<GateIoFuturesCancelResult[]>> CancelOrdersAsync(string settlementAsset, IEnumerable<long> orderIds, CancellationToken ct = default)
        {
            var parameters = new Parameters(orderIds.Select(x => x.ToString()).ToArray(), GateIoExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/batch_cancel_orders", GateIoExchange.RateLimiter.RestFuturesOrderCancelation, 1, true);
            return await _baseClient.SendAsync<GateIoFuturesCancelResult[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Orders By Timestamp

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPerpOrder[]>> GetOrdersByTimestampAsync(
            string settlementAsset,
            string? contract = null,
            OrderStatus? status = null,
            int? limit = null,
            int? offset = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("contract", contract);
            parameters.Add("status", status);
            parameters.Add("limit", limit);
            parameters.Add("offset", offset);
            parameters.Add("from", startTime, DateTimeSerialization.SecondsNumber);
            parameters.Add("to", endTime, DateTimeSerialization.SecondsNumber);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/orders_timerange", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPerpOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPerpOrder>> GetOrderAsync(
            string settlementAsset,
            long? orderId = null,
            string? clientOrderId = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/orders/" + (orderId?.ToString() ?? clientOrderId), GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPerpOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPerpOrder>> CancelOrderAsync(
            string settlementAsset,
            long? orderId = null,
            string? clientOrderId = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/orders/" + (orderId?.ToString() ?? clientOrderId), GateIoExchange.RateLimiter.RestFuturesOrderCancelation, 1, true);
            return await _baseClient.SendAsync<GateIoPerpOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Edit Order

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPerpOrder>> EditOrderAsync(
            string settlementAsset,
            long? orderId = null,
            string? clientOrderId = null,
            int? quantity = null,
            decimal? price = null,
            string? amendText = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("size", quantity);
            parameters.Add("price", price);
            parameters.Add("amend_text", amendText);
            var request = _definitions.GetOrCreate(HttpMethod.Put, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/orders/" + (orderId?.ToString() ?? clientOrderId), GateIoExchange.RateLimiter.RestFuturesOrderPlacement, 1, true);
            return await _baseClient.SendAsync<GateIoPerpOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Edit Multiple Orders

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPerpOrder[]>> EditMultipleOrdersAsync(string settlementAsset, IEnumerable<GateIoPerpBatchEditRequest> requests, CancellationToken ct = default)
        {
            var parameters = new Parameters(requests.ToArray(), GateIoExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/batch_amend_orders", GateIoExchange.RateLimiter.RestFuturesOrderPlacement, 1, true);
            var result = await _baseClient.SendAsync<GateIoPerpOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPerpUserTrade[]>> GetUserTradesAsync(
            string settlementAsset,
            string? contract = null,
            long? orderId = null,
            int? limit = null,
            int? offset = null,
            long? lastId = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("order", orderId);
            parameters.Add("contract", contract);
            parameters.Add("limit", limit);
            parameters.Add("offset", offset);
            parameters.Add("last_id", lastId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/my_trades", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPerpUserTrade[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get User Trades By Timestamp

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPerpUserTrade[]>> GetUserTradesByTimestampAsync(
            string settlementAsset,
            string? contract = null,
            long? orderId = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            int? offset = null,
            Role? role = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("order", orderId);
            parameters.Add("contract", contract);
            parameters.Add("limit", limit);
            parameters.Add("offset", offset);
            parameters.Add("from", startTime, DateTimeSerialization.SecondsNumber);
            parameters.Add("to", endTime, DateTimeSerialization.SecondsNumber);
            parameters.Add("role", role);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/my_trades_timerange", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPerpUserTrade[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Position Close History

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPerpPositionClose[]>> GetPositionCloseHistoryAsync(
            string settlementAsset,
            string? contract = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            int? offset = null,
            Role? role = null,
            PositionSide? side = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("contract", contract);
            parameters.Add("limit", limit);
            parameters.Add("offset", offset);
            parameters.Add("from", startTime, DateTimeSerialization.SecondsNumber);
            parameters.Add("to", endTime, DateTimeSerialization.SecondsNumber);
            parameters.Add("role", role);
            parameters.Add("side", side);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/position_close", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPerpPositionClose[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Liquidation History

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPerpLiquidation[]>> GetLiquidationHistoryAsync(
            string settlementAsset,
            string? contract = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("contract", contract);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/liquidates", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPerpLiquidation[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Auto Deleveraging History

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPerpAutoDeleverage[]>> GetAutoDeleveragingHistoryAsync(
            string settlementAsset,
            string? contract = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("contract", contract);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/auto_deleverages", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPerpAutoDeleverage[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Place Trigger Order

        /// <inheritdoc />
        public async Task<HttpResult<GateIoId>> PlaceTriggerOrderAsync(
            string settlementAsset,
            string contract,
            OrderSide orderSide,
            int quantity,
            TriggerType triggerType,
            decimal triggerPrice,
            decimal? orderPrice = null,
            bool? closePosition = null,
            bool? reduceOnly = null,
            CloseSide? closeSide = null,
            PriceType? priceType = null,
            TriggerOrderType? triggerOrderType = null,
            TimeInForce? timeInForce = null,
            string? text = null,
            TimeSpan? expiration = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            var initial = new Parameters(GateIoExchange._parameterSerializationSettings);
            initial.Add("contract", contract);
            initial.Add("price", orderPrice ?? 0);
            initial.Add("size", orderSide == OrderSide.Buy ? quantity : -quantity);
            initial.Add("close", closePosition);
            initial.Add("reduce_only", reduceOnly);
            initial.Add("tif", timeInForce);
            initial.Add("text", text);
            initial.Add("auto_size", closeSide);

            var order = new Parameters(GateIoExchange._parameterSerializationSettings);
            order.Add("price", triggerPrice);
            order.Add("price_type", priceType, EnumSerialization.Number);
            order.Add("rule", triggerType == TriggerType.EqualOrHigher ? 1 : 2);
            order.Add("expiration", (int?)expiration?.TotalSeconds);

            parameters.Add("initial", initial);
            parameters.Add("trigger", order);
            parameters.Add("order_type", triggerOrderType);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/price_orders", GateIoExchange.RateLimiter.RestFuturesOrderPlacement, 1, true);
            return await _baseClient.SendAsync<GateIoId>(
                request,
                parameters, 
                ct,
                1,
                new Dictionary<string, string> 
                {
                    { "X-Gate-Channel-Id", LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange) }
                }).ConfigureAwait(false);
        }

        #endregion

        #region Get Trigger Orders

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPerpTriggerOrder[]>> GetTriggerOrdersAsync(
            string settlementAsset,
            bool open,
            string? contract = null,
            int? limit = null,
            int? offset = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("status", open ? "open" : "finished");
            parameters.Add("contract", contract);
            parameters.Add("limit", limit);
            parameters.Add("offset", offset);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/price_orders", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPerpTriggerOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Trigger Orders

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPerpTriggerOrder[]>> CancelTriggerOrdersAsync(
            string settlementAsset,
            string contract,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("contract", contract);

            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/price_orders", GateIoExchange.RateLimiter.RestFuturesOrderCancelation, 1, true);
            return await _baseClient.SendAsync<GateIoPerpTriggerOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Trigger Order

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPerpTriggerOrder>> GetTriggerOrderAsync(
            string settlementAsset,
            long orderId,
            CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/price_orders/{orderId}", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPerpTriggerOrder>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Trigger Order

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPerpTriggerOrder>> CancelTriggerOrderAsync(
            string settlementAsset,
            long orderId,
            CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/price_orders/{orderId}", GateIoExchange.RateLimiter.RestFuturesOrderCancelation, 1, true);
            return await _baseClient.SendAsync<GateIoPerpTriggerOrder>(request, null, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
