using CryptoExchange.Net.Objects;
using GateIo.Net.Objects.Models;
using GateIo.Net.Enums;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GateIo.Net.Interfaces.Clients.AlphaApi;
using CryptoExchange.Net;

namespace GateIo.Net.Clients.AlphaApi
{
    /// <inheritdoc />
    internal class GateIoRestClientAlphaApiTrading : IGateIoRestClientAlphaApiTrading
    {
        private readonly GateIoRestClientAlphaApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        internal GateIoRestClientAlphaApiTrading(GateIoRestClientAlphaApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Quote

        /// <inheritdoc />
        public async Task<HttpResult<GateIoAlphaQuote>> GetQuoteAsync(
            string asset,
            OrderSide side,
            decimal quantity,
            GasMode gasMode,
            decimal? slippage = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("side", side);
            parameters.Add("amount", quantity);
            parameters.Add("gas_mode", gasMode);
            parameters.Add("slippage", slippage);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v4/alpha/quote", GateIoExchange.RateLimiter.RestAlpha, 1, true);
            return await _baseClient.SendAsync<GateIoAlphaQuote>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Place Order

        /// <inheritdoc />
        public async Task<HttpResult<GateIoAlphaOrderResult>> PlaceOrderAsync(
            string asset,
            OrderSide side,
            decimal quantity,
            GasMode gasMode,
            string quoteId,
            decimal? slippage = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("side", side);
            parameters.Add("amount", quantity);
            parameters.Add("quote_id", quoteId);
            parameters.Add("gas_mode", gasMode);
            parameters.Add("slippage", slippage);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v4/alpha/orders", GateIoExchange.RateLimiter.RestAlpha, 1, true);
            return await _baseClient.SendAsync<GateIoAlphaOrderResult>(
                request,
                parameters,
                ct,
                requestHeaders: new Dictionary<string, string> 
                { 
                    { "X-Gate-Channel-Id", LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.ExchangeName) }
                }).ConfigureAwait(false);
        }

        #endregion

        #region Get Orders

        /// <inheritdoc />
        public async Task<HttpResult<GateIoAlphaOrder[]>> GetOrdersAsync(
            string asset,
            OrderSide side,
            AlphaOrderStatus status,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? page = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("side", side);
            parameters.Add("status", status);
            parameters.Add("from", startTime, DateTimeSerialization.SecondsNumber);
            parameters.Add("to", endTime, DateTimeSerialization.SecondsNumber);
            parameters.Add("limit", limit);
            parameters.Add("page", page);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/alpha/orders", GateIoExchange.RateLimiter.RestAlpha, 1, true);
            return await _baseClient.SendAsync<GateIoAlphaOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<HttpResult<GateIoAlphaOrder>> GetOrderAsync(
            string orderId,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("order_id", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/alpha/order", GateIoExchange.RateLimiter.RestAlpha, 1, true);
            return await _baseClient.SendAsync<GateIoAlphaOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
