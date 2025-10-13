using CryptoExchange.Net.Objects;
using GateIo.Net.Interfaces.Clients.SpotApi;
using GateIo.Net.Objects.Models;
using GateIo.Net.Enums;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.RateLimiting.Guards;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Linq;
using GateIo.Net.Interfaces.Clients.AlphaApi;

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
        public async Task<WebCallResult<GateIoAlphaQuote>> GetQuoteAsync(
            string asset,
            OrderSide side,
            decimal quantity,
            GasMode gasMode,
            decimal? slippage = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency", asset);
            parameters.AddEnum("side", side);
            parameters.AddString("amount", quantity);
            parameters.AddEnum("gas_mode", gasMode);
            parameters.AddOptionalString("slippage", slippage);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v4/alpha/quote", GateIoExchange.RateLimiter.RestAlpha, 1, true);
            return await _baseClient.SendAsync<GateIoAlphaQuote>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Place Order

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoAlphaOrderResult>> PlaceOrderAsync(
            string asset,
            OrderSide side,
            decimal quantity,
            GasMode gasMode,
            string quoteId,
            decimal? slippage = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency", asset);
            parameters.AddEnum("side", side);
            parameters.AddString("amount", quantity);
            parameters.Add("quote_id", quoteId);
            parameters.AddEnum("gas_mode", gasMode);
            parameters.AddOptionalString("slippage", slippage);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v4/alpha/orders", GateIoExchange.RateLimiter.RestAlpha, 1, true);
            return await _baseClient.SendAsync<GateIoAlphaOrderResult>(request, parameters, ct, requestHeaders: new Dictionary<string, string> { { "X-Gate-Channel-Id", _baseClient._brokerId } }).ConfigureAwait(false);
        }

        #endregion

        #region Get Orders

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoAlphaOrder[]>> GetOrdersAsync(
            string asset,
            OrderSide side,
            AlphaOrderStatus status,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? page = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency", asset);
            parameters.AddEnum("side", side);
            parameters.AddEnum("status", status);
            parameters.AddOptionalSeconds("from", startTime);
            parameters.AddOptionalSeconds("to", endTime);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("page", page);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/alpha/orders", GateIoExchange.RateLimiter.RestAlpha, 1, true);
            return await _baseClient.SendAsync<GateIoAlphaOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoAlphaOrder>> GetOrderAsync(
            string orderId,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("order_id", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/alpha/order", GateIoExchange.RateLimiter.RestAlpha, 1, true);
            return await _baseClient.SendAsync<GateIoAlphaOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
