using Microsoft.Extensions.Logging;
using GateIo.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Objects;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using GateIo.Net.Enums;
using GateIo.Net.Objects.Models;
using System;
using System.Globalization;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Linq;
using CryptoExchange.Net;

namespace GateIo.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class GateIoRestClientSpotApiTrading : IGateIoRestClientSpotApiTrading
    {
        private readonly GateIoRestClientSpotApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();


        internal GateIoRestClientSpotApiTrading(ILogger logger, GateIoRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Place Order

        /// <inheritdoc />
        public async Task<HttpResult<GateIoOrder>> PlaceOrderAsync(
            string symbol,
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
            OrderActionMode? actionMode = null,
            decimal? slippage = null,
            decimal? takeProfitTriggerPrice = null,
            decimal? takeProfitOrderPrice = null,
            decimal? stopLossTriggerPrice = null,
            decimal? stopLossOrderPrice = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency_pair", symbol);
            parameters.Add("type", type);
            parameters.Add("side", side);
            parameters.Add("amount", quantity);
            parameters.Add("price", price);
            parameters.Add("iceberg", icebergQuantity);
            parameters.Add("account", accountType);
            parameters.Add("time_in_force", timeInForce);
            parameters.Add("stp_act", selfTradePreventionMode);
            parameters.Add("auto_borrow", autoBorrow);
            parameters.Add("auto_repay", autoRepay);
            parameters.Add("text", text);
            parameters.Add("action_mode", actionMode);
            parameters.Add("slippage", slippage);
            if (takeProfitTriggerPrice != null)
            {
                var takeProfitParams = new Parameters(GateIoExchange._parameterSerializationSettings);
                if (takeProfitTriggerPrice != null)
                    takeProfitParams.Add("trigger_price", takeProfitTriggerPrice);
                takeProfitParams.Add("order_price", takeProfitOrderPrice);
                parameters.Add("stop_profit", takeProfitParams);
            }
            if (stopLossTriggerPrice != null)
            {
                var stopLossParams = new Parameters(GateIoExchange._parameterSerializationSettings);
                if (stopLossTriggerPrice != null)
                    stopLossParams.Add("trigger_price", stopLossTriggerPrice);
                stopLossParams.Add("order_price", stopLossOrderPrice);
                parameters.Add("stop_loss", stopLossParams);
            }

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v4/spot/orders", GateIoExchange.RateLimiter.RestSpotOrderPlacement, 1, true);
            var result = await _baseClient.SendAsync<GateIoOrder>(
                request,
                parameters, 
                ct, 
                1, 
                new Dictionary<string, string> 
                {
                    { "X-Gate-Channel-Id", LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange) }
                }, 
                rateLimitKeySuffix: symbol).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Multiple Orders

        /// <inheritdoc />
        public async Task<HttpResult<GateIoOrderOperation[]>> PlaceMultipleOrderAsync(
            IEnumerable<GateIoBatchPlaceRequest> orders,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(orders.ToArray(), GateIoExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v4/spot/batch_orders", GateIoExchange.RateLimiter.RestSpotOrderPlacement, 1, true);
            var result = await _baseClient.SendAsync<GateIoOrderOperation[]>(
                request, 
                parameters, 
                ct, 
                1,
                new Dictionary<string, string> 
                { 
                    { "X-Gate-Channel-Id", LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange) }
                }).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public async Task<HttpResult<GateIoSymbolOrders[]>> GetOpenOrdersAsync(
            int? page = null,
            int? limit = null,
            SpotAccountType? accountType = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("page", page);
            parameters.Add("limit", limit);
            parameters.Add("account", accountType);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/spot/open_orders", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync<GateIoSymbolOrders[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Orders

        /// <inheritdoc />
        public async Task<HttpResult<GateIoOrder[]>> GetOrdersAsync(
            bool open,
            string? symbol = null,
            int? page = null,
            int? limit = null,
            SpotAccountType? accountType = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            OrderSide? side = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("status", open ? "open" : "finished");
            parameters.Add("currency_pair", symbol);
            parameters.Add("page", page);
            parameters.Add("limit", limit);
            parameters.Add("account", accountType);
            parameters.Add("side", side);
            parameters.Add("from", startTime);
            parameters.Add("to", endTime);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/spot/orders", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync<GateIoOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<HttpResult<GateIoOrder>> GetOrderAsync(
            string symbol,
            long? orderId = null,
            string? clientOrderId = null,
            SpotAccountType? accountType = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency_pair", symbol);
            parameters.Add("account", accountType);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/spot/orders/" + (orderId?.ToString() ?? clientOrderId), GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync<GateIoOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel All Orders

        /// <inheritdoc />
        public async Task<HttpResult<GateIoOrderOperation[]>> CancelAllOrdersAsync(
            string symbol,
            OrderSide? side = null,
            SpotAccountType? accountType = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency_pair", symbol);
            parameters.Add("side", side);
            parameters.Add("account", accountType);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "/api/v4/spot/orders", GateIoExchange.RateLimiter.RestSpotOrderCancelation, 1, true);
            var result = await _baseClient.SendAsync<GateIoOrderOperation[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Orders

        /// <inheritdoc />
        public async Task<HttpResult<GateIoCancelResult[]>> CancelOrdersAsync(
            IEnumerable<GateIoBatchCancelRequest> orders,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(orders.ToArray(), GateIoExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v4/spot/cancel_batch_orders", GateIoExchange.RateLimiter.RestSpotOrderCancelation, 1, true);
            var result = await _baseClient.SendAsync<GateIoCancelResult[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Edit Order

        /// <inheritdoc />
        public async Task<HttpResult<GateIoOrder>> EditOrderAsync(
            string symbol,
            long? orderId = null,
            string? clientOrderId = null,
            decimal? price = null,
            decimal? quantity = null,
            string? amendText = null,
            SpotAccountType? accountType = null,
            decimal? takeProfitTriggerPrice = null,
            decimal? takeProfitOrderPrice = null,
            decimal? stopLossTriggerPrice = null,
            decimal? stopLossOrderPrice = null,
            CancellationToken ct = default)
        {
            var id = orderId?.ToString() ?? clientOrderId;

            var queryParameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            queryParameters.Add("currency_pair", symbol);

            var bodyParameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            bodyParameters.Add("price", price);
            bodyParameters.Add("amount", quantity);
            bodyParameters.Add("amend_text", amendText);
            bodyParameters.Add("account", accountType);
            if (takeProfitTriggerPrice != null)
            {
                var takeProfitParams = new Parameters(GateIoExchange._parameterSerializationSettings);
                if (takeProfitTriggerPrice != null)
                    takeProfitParams.Add("trigger_price", takeProfitTriggerPrice);
                takeProfitParams.Add("order_price", takeProfitOrderPrice);
                bodyParameters.Add("stop_profit", takeProfitParams);
            }
            if (stopLossTriggerPrice != null)
            {
                var stopLossParams = new Parameters(GateIoExchange._parameterSerializationSettings);
                if (stopLossTriggerPrice != null)
                    stopLossParams.Add("trigger_price", stopLossTriggerPrice);
                stopLossParams.Add("order_price", stopLossOrderPrice);
                bodyParameters.Add("stop_loss", stopLossParams);
            }
            var request = _definitions.GetOrCreate(new HttpMethod("Patch"), _baseClient.BaseAddress, "/api/v4/spot/orders/" + id, GateIoExchange.RateLimiter.RestSpotOrderPlacement, 1, true);
            return await _baseClient.SendAsync<GateIoOrder>(request, queryParameters, bodyParameters, ct, rateLimitKeySuffix: symbol).ConfigureAwait(false);
        }

        #endregion

        #region Edit Multiple Orders

        /// <inheritdoc />
        public async Task<HttpResult<GateIoOrderOperation[]>> EditMultipleOrderAsync(
            IEnumerable<GateIoBatchEditRequest> orders,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(orders.ToArray(), GateIoExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v4/spot/amend_batch_orders", GateIoExchange.RateLimiter.RestSpotOrderPlacement, 1, true);
            return await _baseClient.SendAsync<GateIoOrderOperation[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<HttpResult<GateIoOrder>> CancelOrderAsync(
            string symbol,
            long? orderId = null,
            string? clientOrderId = null,
            SpotAccountType? accountType = null,
            CancellationToken ct = default)
        {
            var id = orderId?.ToString() ?? clientOrderId ?? throw new ArgumentException($"Either {nameof(orderId)} or {nameof(clientOrderId)} must be provided"); ;

            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency_pair", symbol);
            parameters.Add("account", accountType);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "/api/v4/spot/orders/" + id, GateIoExchange.RateLimiter.RestSpotOrderCancelation, 1, true);
            var result = await _baseClient.SendAsync<GateIoOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<HttpResult<GateIoUserTrade[]>> GetUserTradesAsync(
            string? symbol = null,
            long? orderId = null,
            int? limit = null,
            int? page = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            SpotAccountType? accountType = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency_pair", symbol);
            parameters.Add("order_id", orderId);
            parameters.Add("limit", limit);
            parameters.Add("page", page);
            parameters.Add("from", startTime);
            parameters.Add("to", endTime);
            parameters.Add("account", accountType);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/spot/my_trades", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync<GateIoUserTrade[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Orders After

        /// <inheritdoc />
        public async Task<HttpResult<GateIoCancelAfter>> CancelOrdersAfterAsync(
            TimeSpan cancelAfter,
            string? symbol = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("timeout", (int)cancelAfter.TotalSeconds);
            parameters.Add("currency_pair", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v4/spot/countdown_cancel_all", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync<GateIoCancelAfter>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Place Trigger Order

        /// <inheritdoc />
        public async Task<HttpResult<GateIoId>> PlaceTriggerOrderAsync(
            string symbol,
            OrderSide orderSide,
            NewOrderType orderType,
            TriggerType triggerType,
            decimal triggerPrice,
            TimeSpan expiration,
            decimal quantity,
            TriggerAccountType accountType,
            TimeInForce timeInForce,
            decimal? orderPrice = null,
            string? text = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("market", symbol);
            parameters.Add("trigger", new Dictionary<string, object>
            {
                { "price", triggerPrice.ToString(CultureInfo.InvariantCulture) },
                { "rule", EnumConverter.GetString(triggerType) },
                { "expiration", (int)expiration.TotalSeconds },
            });
            var order = new Parameters(GateIoExchange._parameterSerializationSettings);
            order.Add("type", orderType);
            order.Add("side", orderSide);
            order.Add("amount", quantity);
            order.Add("price", orderPrice);
            order.Add("account", accountType);
            order.Add("time_in_force", timeInForce);
            order.Add("text", text);
            parameters.Add("put", order);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v4/spot/price_orders", GateIoExchange.RateLimiter.RestSpotOrderPlacement, 1, true);
            return await _baseClient.SendAsync<GateIoId>(
                request, 
                parameters,
                ct, 
                1, 
                new Dictionary<string, string> {
                    { "X-Gate-Channel-Id", LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange) }
                },
                rateLimitKeySuffix: symbol).ConfigureAwait(false);
        }

        #endregion

        #region Get Trigger Orders

        /// <inheritdoc />
        public async Task<HttpResult<GateIoTriggerOrder[]>> GetTriggerOrdersAsync(
            bool open,
            string? symbol = null,
            TriggerAccountType? accountType = null,
            int? limit = null,
            int? offset = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("status", open ? "open" : "finished");
            parameters.Add("market", symbol);
            parameters.Add("account", accountType);
            parameters.Add("limit", limit);
            parameters.Add("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/spot/price_orders", GateIoExchange.RateLimiter.RestSpotOrderCancelation, 1, true);
            return await _baseClient.SendAsync<GateIoTriggerOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel All Trigger Orders

        /// <inheritdoc />
        public async Task<HttpResult<GateIoTriggerOrder[]>> CancelAllTriggerOrdersAsync(string? symbol = null, TriggerAccountType? accountType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("market", symbol);
            parameters.Add("account", accountType);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "/api/v4/spot/price_orders", GateIoExchange.RateLimiter.RestSpotOrderCancelation, 1, true);
            return await _baseClient.SendAsync<GateIoTriggerOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Trigger Order

        /// <inheritdoc />
        public async Task<HttpResult<GateIoTriggerOrder>> GetTriggerOrderAsync(long id, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/spot/price_orders/" + id, GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync<GateIoTriggerOrder>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Trigger Order

        /// <inheritdoc />
        public async Task<HttpResult<GateIoTriggerOrder>> CancelTriggerOrderAsync(long id, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "/api/v4/spot/price_orders/" + id, GateIoExchange.RateLimiter.RestSpotOrderCancelation, 1, true);
            return await _baseClient.SendAsync<GateIoTriggerOrder>(request, null, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
