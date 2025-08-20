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
        public async Task<WebCallResult<GateIoPosition[]>> GetPositionsAsync(string settlementAsset, bool? holding = null, int? page = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("page", page);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("holding", holding);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/positions", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPosition[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Position

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPosition>> GetPositionAsync(string settlementAsset, string contract, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/positions/{contract}", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPosition>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Update Position Margin

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPosition>> UpdatePositionMarginAsync(string settlementAsset, string contract, decimal change, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddString("change", change);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/positions/{contract}/margin", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPosition>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Update Position Leverage

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPosition>> UpdatePositionLeverageAsync(string settlementAsset, string contract, decimal leverage, decimal? crossLeverageLimit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddString("leverage", leverage);
            parameters.AddOptionalString("cross_leverage_limit", crossLeverageLimit);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/positions/{contract}/leverage", GateIoExchange.RateLimiter.RestFuturesOther, 1, true, parameterPosition: HttpMethodParameterPosition.InUri);
            return await _baseClient.SendAsync<GateIoPosition>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Update Position Risk Limit

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPosition>> UpdatePositionRiskLimitAsync(string settlementAsset, string contract, decimal riskLimit, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddString("risk_limit", riskLimit);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/positions/{contract}/risk_limit", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPosition>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Dual Mode Positions

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPosition[]>> GetDualModePositionsAsync(string settlementAsset, string contract, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/dual_comp/positions/{contract}", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPosition[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Update Dual Mode Position Margin

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPosition[]>> UpdateDualModePositionMarginAsync(string settlementAsset, string contract, decimal change, PositionMode mode, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddString("change", change);
            parameters.AddEnum("dual_side", mode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/dual_comp/positions/{contract}/margin", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPosition[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Update Dual Mode Position Leverage

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPosition[]>> UpdateDualModePositionLeverageAsync(string settlementAsset, string contract, decimal leverage, decimal? crossLeverageLimit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddString("leverage", leverage);
            parameters.AddOptionalString("cross_leverage_limit", crossLeverageLimit);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/dual_comp/positions/{contract}/leverage", GateIoExchange.RateLimiter.RestFuturesOther, 1, true, parameterPosition: HttpMethodParameterPosition.InUri);
            return await _baseClient.SendAsync<GateIoPosition[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Update Dual Mode Position Risk Limit

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPosition[]>> UpdateDualModePositionRiskLimitAsync(string settlementAsset, string contract, int riskLimit, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddString("risk_limit", riskLimit);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/dual_comp/positions/{contract}/risk_limit", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPosition[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Place Order

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPerpOrder>> PlaceOrderAsync(
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
            var parameters = new ParameterCollection();
            parameters.Add("contract", contract);
            parameters.Add("size", orderSide == OrderSide.Buy ? quantity: -quantity);
            parameters.AddString("price", price ?? 0);
            parameters.AddOptional("close", closePosition);
            parameters.AddOptional("reduce_only", reduceOnly);
            parameters.AddOptionalEnum("tif", timeInForce);
            parameters.AddOptionalString("iceberg", icebergQuantity);
            parameters.AddOptional("text", text);
            parameters.AddOptional("auto_size", closeSide);
            parameters.AddOptional("stp_act", stpMode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/orders", GateIoExchange.RateLimiter.RestFuturesOrderPlacement, 1, true);
            return await _baseClient.SendAsync<GateIoPerpOrder>(request, parameters, ct, 1, new Dictionary<string, string> { { "X-Gate-Channel-Id", _baseClient._brokerId } }).ConfigureAwait(false);
        }

        #endregion

        #region Place Multiple Orders

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPerpOrder[]>> PlaceMultipleOrderAsync(
            string settlementAsset, 
            IEnumerable<GateIoPerpBatchPlaceRequest> orders,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.SetBody(orders.ToArray());
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/batch_orders", GateIoExchange.RateLimiter.RestFuturesOrderPlacement, 1, true);
            var result = await _baseClient.SendAsync<GateIoPerpOrder[]>(request, parameters, ct, 1, new Dictionary<string, string> { { "X-Gate-Channel-Id", _baseClient._brokerId } }).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Orders

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPerpOrder[]>> GetOrdersAsync(
            string settlementAsset,
            OrderStatus status,
            string? contract = null,
            int? limit = null,
            int? offset = null,
            string? lastId = null, 
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("contract", contract);
            parameters.AddEnum("status", status);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("offset", offset);
            parameters.AddOptional("last_id", lastId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/orders", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPerpOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel All Orders

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPerpOrder[]>> CancelAllOrdersAsync(string settlementAsset, string contract, OrderSide? side = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contract", contract);
            parameters.AddOptionalEnum("side", side);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/orders", GateIoExchange.RateLimiter.RestFuturesOrderCancelation, 1, true);
            return await _baseClient.SendAsync<GateIoPerpOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel After

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoCancelAfter>> CancelOrdersAfterAsync(string settlementAsset, TimeSpan cancelAfter, string? contract = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("timeout", (int)cancelAfter.TotalSeconds);
            parameters.AddOptional("contract", contract);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/countdown_cancel_all", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoCancelAfter>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Orders

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoFuturesCancelResult[]>> CancelOrdersAsync(string settlementAsset, IEnumerable<long> orderIds, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.SetBody(orderIds.Select(x => x.ToString()).ToArray());
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/batch_cancel_orders", GateIoExchange.RateLimiter.RestFuturesOrderCancelation, 1, true);
            return await _baseClient.SendAsync<GateIoFuturesCancelResult[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Orders By Timestamp

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPerpOrder[]>> GetOrdersByTimestampAsync(
            string settlementAsset,
            string? contract = null,
            OrderStatus? status = null,
            int? limit = null,
            int? offset = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("contract", contract);
            parameters.AddOptionalEnum("status", status);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("offset", offset);
            parameters.AddOptionalSeconds("from", startTime);
            parameters.AddOptionalSeconds("to", endTime);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/orders_timerange", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPerpOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPerpOrder>> GetOrderAsync(
            string settlementAsset,
            long? orderId = null,
            string? clientOrderId = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/orders/" + (orderId?.ToString() ?? clientOrderId), GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPerpOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPerpOrder>> CancelOrderAsync(
            string settlementAsset,
            long? orderId = null,
            string? clientOrderId = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Delete, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/orders/" + (orderId?.ToString() ?? clientOrderId), GateIoExchange.RateLimiter.RestFuturesOrderCancelation, 1, true);
            return await _baseClient.SendAsync<GateIoPerpOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Edit Order

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPerpOrder>> EditOrderAsync(
            string settlementAsset,
            long? orderId = null,
            string? clientOrderId = null,
            int? quantity = null,
            decimal? price = null,
            string? amendText = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("size", quantity);
            parameters.AddOptionalString("price", price);
            parameters.AddOptional("amend_text", amendText);
            var request = _definitions.GetOrCreate(HttpMethod.Put, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/orders/" + (orderId?.ToString() ?? clientOrderId), GateIoExchange.RateLimiter.RestFuturesOrderPlacement, 1, true);
            return await _baseClient.SendAsync<GateIoPerpOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Edit Multiple Orders

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPerpOrder[]>> EditMultipleOrdersAsync(string settlementAsset, IEnumerable<GateIoPerpBatchEditRequest> requests, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.SetBody(requests.ToArray());
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/batch_amend_orders", GateIoExchange.RateLimiter.RestFuturesOrderPlacement, 1, true);
            var result = await _baseClient.SendAsync<GateIoPerpOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPerpUserTrade[]>> GetUserTradesAsync(
            string settlementAsset,
            string? contract = null,
            long? orderId = null,
            int? limit = null,
            int? offset = null,
            long? lastId = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("order", orderId);
            parameters.AddOptional("contract", contract);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("offset", offset);
            parameters.AddOptional("last_id", lastId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/my_trades", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPerpUserTrade[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get User Trades By Timestamp

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPerpUserTrade[]>> GetUserTradesByTimestampAsync(
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
            var parameters = new ParameterCollection();
            parameters.AddOptional("order", orderId);
            parameters.AddOptional("contract", contract);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("offset", offset);
            parameters.AddOptionalSeconds("from", startTime);
            parameters.AddOptionalSeconds("to", endTime);
            parameters.AddOptionalEnum("role", role);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/my_trades_timerange", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPerpUserTrade[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Position Close History

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPerpPositionClose[]>> GetPositionCloseHistoryAsync(
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
            var parameters = new ParameterCollection();
            parameters.AddOptional("contract", contract);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("offset", offset);
            parameters.AddOptionalSeconds("from", startTime);
            parameters.AddOptionalSeconds("to", endTime);
            parameters.AddOptionalEnum("role", role);
            parameters.AddOptionalEnum("side", side);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/position_close", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPerpPositionClose[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Liquidation History

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPerpLiquidation[]>> GetLiquidationHistoryAsync(
            string settlementAsset,
            string? contract = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("contract", contract);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/liquidates", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPerpLiquidation[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Auto Deleveraging History

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPerpAutoDeleverage[]>> GetAutoDeleveragingHistoryAsync(
            string settlementAsset,
            string? contract = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("contract", contract);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/auto_deleverages", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPerpAutoDeleverage[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Place Trigger Order

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoId>> PlaceTriggerOrderAsync(
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
            var parameters = new ParameterCollection();
            var initial = new ParameterCollection();
            initial.Add("contract", contract);
            initial.AddString("price", orderPrice ?? 0);
            initial.Add("size", orderSide == OrderSide.Buy ? quantity : -quantity);
            initial.AddOptional("close", closePosition);
            initial.AddOptional("reduce_only", reduceOnly);
            initial.AddOptionalEnum("tif", timeInForce);
            initial.AddOptional("text", text);
            initial.AddOptionalEnum("auto_size", closeSide);

            var order = new ParameterCollection();
            order.AddOptionalString("price", triggerPrice);
            order.AddOptionalEnumAsInt("price_type", priceType);
            order.AddOptional("rule", triggerType == TriggerType.EqualOrHigher ? 1 : 2);
            order.AddOptionalString("expiration", (int?)expiration?.TotalSeconds);

            parameters.Add("initial", initial);
            parameters.Add("trigger", order);
            parameters.AddOptionalEnum("order_type", triggerOrderType);

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/price_orders", GateIoExchange.RateLimiter.RestFuturesOrderPlacement, 1, true);
            return await _baseClient.SendAsync<GateIoId>(request, parameters, ct, 1, new Dictionary<string, string> { { "X-Gate-Channel-Id", _baseClient._brokerId } }).ConfigureAwait(false);
        }

        #endregion

        #region Get Trigger Orders

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPerpTriggerOrder[]>> GetTriggerOrdersAsync(
            string settlementAsset,
            bool open,
            string? contract = null,
            int? limit = null,
            int? offset = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("status", open ? "open" : "finished");
            parameters.AddOptional("contract", contract);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("offset", offset);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/price_orders", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPerpTriggerOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Trigger Orders

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPerpTriggerOrder[]>> CancelTriggerOrdersAsync(
            string settlementAsset,
            string contract,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contract", contract);

            var request = _definitions.GetOrCreate(HttpMethod.Delete, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/price_orders", GateIoExchange.RateLimiter.RestFuturesOrderCancelation, 1, true);
            return await _baseClient.SendAsync<GateIoPerpTriggerOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Trigger Order

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPerpTriggerOrder>> GetTriggerOrderAsync(
            string settlementAsset,
            long orderId,
            CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/price_orders/{orderId}", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPerpTriggerOrder>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Trigger Order

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPerpTriggerOrder>> CancelTriggerOrderAsync(
            string settlementAsset,
            long orderId,
            CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Delete, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/price_orders/{orderId}", GateIoExchange.RateLimiter.RestFuturesOrderCancelation, 1, true);
            return await _baseClient.SendAsync<GateIoPerpTriggerOrder>(request, null, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
