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
        public async Task<WebCallResult<GateIoOrder>> PlaceOrderAsync(
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
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency_pair", symbol);
            parameters.AddEnum("type", type);
            parameters.AddEnum("side", side);
            parameters.AddString("amount", quantity);
            parameters.AddOptionalString("price", price);
            parameters.AddOptionalString("iceberg", icebergQuantity);
            parameters.AddOptionalEnum("account", accountType);
            parameters.AddOptionalEnum("time_in_force", timeInForce);
            parameters.AddOptionalEnum("stp_act", selfTradePreventionMode);
            parameters.AddOptional("auto_borrow", autoBorrow);
            parameters.AddOptional("auto_repay", autoRepay);
            parameters.AddOptional("text", text);
            parameters.AddOptionalEnum("action_mode", actionMode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v4/spot/orders", GateIoExchange.RateLimiter.RestSpotOrderPlacement, 1, true);
            var result = await _baseClient.SendAsync<GateIoOrder>(request, parameters, ct, 1, new Dictionary<string, string> { { "X-Gate-Channel-Id", _baseClient._brokerId } }, rateLimitKeySuffix: symbol).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Multiple Orders

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoOrderOperation[]>> PlaceMultipleOrderAsync(
            IEnumerable<GateIoBatchPlaceRequest> orders,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.SetBody(orders.ToArray());
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v4/spot/batch_orders", GateIoExchange.RateLimiter.RestSpotOrderPlacement, 1, true);
            var result = await _baseClient.SendAsync<GateIoOrderOperation[]>(request, parameters, ct, 1, new Dictionary<string, string> { { "X-Gate-Channel-Id", _baseClient._brokerId } }).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoSymbolOrders[]>> GetOpenOrdersAsync(
            int? page = null,
            int? limit = null,
            SpotAccountType? accountType = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("page", page);
            parameters.AddOptional("limit", limit);
            parameters.AddOptionalEnum("account", accountType);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/spot/open_orders", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync<GateIoSymbolOrders[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Orders

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoOrder[]>> GetOrdersAsync(
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
            var parameters = new ParameterCollection();
            parameters.Add("status", open ? "open" : "finished");
            parameters.AddOptional("currency_pair", symbol);
            parameters.AddOptional("page", page);
            parameters.AddOptional("limit", limit);
            parameters.AddOptionalEnum("account", accountType);
            parameters.AddOptionalEnum("side", side);
            parameters.AddOptionalSeconds("from", startTime);
            parameters.AddOptionalSeconds("to", endTime);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/spot/orders", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync<GateIoOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoOrder>> GetOrderAsync(
            string symbol,
            long? orderId = null,
            string? clientOrderId = null,
            SpotAccountType? accountType = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency_pair", symbol);
            parameters.AddOptionalEnum("account", accountType);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/spot/orders/" + (orderId?.ToString() ?? clientOrderId), GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync<GateIoOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel All Orders

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoOrderOperation[]>> CancelAllOrdersAsync(
            string symbol,
            OrderSide? side = null,
            SpotAccountType? accountType = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency_pair", symbol);
            parameters.AddOptionalEnum("side", side);
            parameters.AddOptionalEnum("account", accountType);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, "/api/v4/spot/orders", GateIoExchange.RateLimiter.RestSpotOrderCancelation, 1, true);
            var result = await _baseClient.SendAsync<GateIoOrderOperation[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Orders

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoCancelResult[]>> CancelOrdersAsync(
            IEnumerable<GateIoBatchCancelRequest> orders,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.SetBody(orders.ToArray());
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v4/spot/cancel_batch_orders", GateIoExchange.RateLimiter.RestSpotOrderCancelation, 1, true);
            var result = await _baseClient.SendAsync<GateIoCancelResult[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Edit Order

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoOrder>> EditOrderAsync(
            string symbol,
            long? orderId = null,
            string? clientOrderId = null,
            decimal? price = null,
            decimal? quantity = null,
            string? amendText = null,
            SpotAccountType? accountType = null,
            CancellationToken ct = default)
        {
            var id = orderId?.ToString() ?? clientOrderId;

            var queryParameters = new ParameterCollection();
            queryParameters.Add("currency_pair", symbol);

            var bodyParameters = new ParameterCollection();
            bodyParameters.AddOptionalString("price", price);
            bodyParameters.AddOptionalString("amount", quantity);
            bodyParameters.AddOptional("amend_text", amendText);
            bodyParameters.AddOptionalEnum("account", accountType);
            var request = _definitions.GetOrCreate(new HttpMethod("Patch"), "/api/v4/spot/orders/" + id, GateIoExchange.RateLimiter.RestSpotOrderPlacement, 1, true);
            return await _baseClient.SendAsync<GateIoOrder>(request, queryParameters, bodyParameters, ct, rateLimitKeySuffix: symbol).ConfigureAwait(false);
        }

        #endregion

        #region Edit Multiple Orders

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoOrderOperation[]>> EditMultipleOrderAsync(
            IEnumerable<GateIoBatchEditRequest> orders,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.SetBody(orders.ToArray());
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v4/spot/amend_batch_orders", GateIoExchange.RateLimiter.RestSpotOrderPlacement, 1, true);
            return await _baseClient.SendAsync<GateIoOrderOperation[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoOrder>> CancelOrderAsync(
            string symbol,
            long? orderId = null,
            string? clientOrderId = null,
            SpotAccountType? accountType = null,
            CancellationToken ct = default)
        {
            var id = orderId?.ToString() ?? clientOrderId ?? throw new ArgumentException($"Either {nameof(orderId)} or {nameof(clientOrderId)} must be provided"); ;

            var parameters = new ParameterCollection();
            parameters.Add("currency_pair", symbol);
            parameters.AddOptionalEnum("account", accountType);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, "/api/v4/spot/orders/" + id, GateIoExchange.RateLimiter.RestSpotOrderCancelation, 1, true);
            var result = await _baseClient.SendAsync<GateIoOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoUserTrade[]>> GetUserTradesAsync(
            string? symbol = null,
            long? orderId = null,
            int? limit = null,
            int? page = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            SpotAccountType? accountType = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency_pair", symbol);
            parameters.AddOptional("order_id", orderId);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("page", page);
            parameters.AddOptionalSeconds("from", startTime);
            parameters.AddOptionalSeconds("to", endTime);
            parameters.AddOptionalEnum("account", accountType);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/spot/my_trades", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync<GateIoUserTrade[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Orders After

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoCancelAfter>> CancelOrdersAfterAsync(
            TimeSpan cancelAfter,
            string? symbol = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("timeout", (int)cancelAfter.TotalSeconds);
            parameters.AddOptional("currency_pair", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v4/spot/countdown_cancel_all", GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync<GateIoCancelAfter>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Place Trigger Order

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoId>> PlaceTriggerOrderAsync(
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
            var parameters = new ParameterCollection();
            parameters.Add("market", symbol);
            parameters.Add("trigger", new Dictionary<string, object>
            {
                { "price", triggerPrice.ToString(CultureInfo.InvariantCulture) },
                { "rule", EnumConverter.GetString(triggerType) },
                { "expiration", (int)expiration.TotalSeconds },
            });
            var order = new ParameterCollection();
            order.AddEnum("type", orderType);
            order.AddEnum("side", orderSide);
            order.AddString("amount", quantity);
            order.AddOptionalString("price", orderPrice);
            order.AddEnum("account", accountType);
            order.AddEnum("time_in_force", timeInForce);
            order.AddOptional("text", text);
            parameters.Add("put", order);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v4/spot/price_orders", GateIoExchange.RateLimiter.RestSpotOrderPlacement, 1, true);
            return await _baseClient.SendAsync<GateIoId>(request, parameters, ct, 1, new Dictionary<string, string> { { "X-Gate-Channel-Id", _baseClient._brokerId } }, rateLimitKeySuffix: symbol).ConfigureAwait(false);
        }

        #endregion

        #region Get Trigger Orders

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoTriggerOrder[]>> GetTriggerOrdersAsync(
            bool open,
            string? symbol = null,
            TriggerAccountType? accountType = null,
            int? limit = null,
            int? offset = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("status", open ? "open" : "finished");
            parameters.AddOptional("market", symbol);
            parameters.AddOptionalEnum("account", accountType);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/spot/price_orders", GateIoExchange.RateLimiter.RestSpotOrderCancelation, 1, true);
            return await _baseClient.SendAsync<GateIoTriggerOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel All Trigger Orders

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoTriggerOrder[]>> CancelAllTriggerOrdersAsync(string? symbol = null, TriggerAccountType? accountType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("market", symbol);
            parameters.AddOptionalEnum("account", accountType);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, "/api/v4/spot/price_orders", GateIoExchange.RateLimiter.RestSpotOrderCancelation, 1, true);
            return await _baseClient.SendAsync<GateIoTriggerOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Trigger Order

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoTriggerOrder>> GetTriggerOrderAsync(long id, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/spot/price_orders/" + id, GateIoExchange.RateLimiter.RestSpotOther, 1, true);
            return await _baseClient.SendAsync<GateIoTriggerOrder>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Trigger Order

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoTriggerOrder>> CancelTriggerOrderAsync(long id, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Delete, "/api/v4/spot/price_orders/" + id, GateIoExchange.RateLimiter.RestSpotOrderCancelation, 1, true);
            return await _baseClient.SendAsync<GateIoTriggerOrder>(request, null, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
