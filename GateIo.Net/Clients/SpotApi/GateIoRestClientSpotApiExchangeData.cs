using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using GateIo.Net.Interfaces.Clients.SpotApi;
using GateIo.Net.Objects.Models;
using GateIo.Net.Enums;

namespace GateIo.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class GateIoRestClientSpotApiExchangeData : IGateIoRestClientSpotApiExchangeData
    {
        private readonly GateIoRestClientSpotApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        internal GateIoRestClientSpotApiExchangeData(ILogger logger, GateIoRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Server Time

        /// <inheritdoc />
        public async Task<HttpResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/spot/time", GateIoExchange.RateLimiter.Public, 1);
            var result = await _baseClient.SendAsync<GateIoServerTime>(request, null, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<DateTime>(result);

            return HttpResult.Ok(result, result.Data.ServerTime);
        }

        #endregion

        #region Get Assets

        /// <inheritdoc />
        public async Task<HttpResult<GateIoAsset[]>> GetAssetsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/spot/currencies", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoAsset[]>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Asset

        /// <inheritdoc />
        public async Task<HttpResult<GateIoAsset>> GetAssetAsync(string asset, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/spot/currencies/{asset}", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoAsset>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Symbol

        /// <inheritdoc />
        public async Task<HttpResult<GateIoSymbol>> GetSymbolAsync(string symbol, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/spot/currency_pairs/{symbol}", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoSymbol>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Symbols

        /// <inheritdoc />
        public async Task<HttpResult<GateIoSymbol[]>> GetSymbolsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/spot/currency_pairs", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoSymbol[]>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Tickers

        /// <inheritdoc />
        public async Task<HttpResult<GateIoTicker[]>> GetTickersAsync(string? symbol = null, string? timezone = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency_pair", symbol);
            parameters.Add("timezone", timezone);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/spot/tickers", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoTicker[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Order Book

        /// <inheritdoc />
        public async Task<HttpResult<GateIoOrderBook>> GetOrderBookAsync(string symbol, int? mergeDepth = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency_pair", symbol);
            parameters.Add("interval", mergeDepth);
            parameters.Add("limit", limit);
            parameters.Add("with_id", true.ToString().ToLowerInvariant());
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/spot/order_book", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoOrderBook>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Trades

        /// <inheritdoc />
        public async Task<HttpResult<GateIoTrade[]>> GetTradesAsync(string symbol, int? limit = null, string? lastId = null, bool? reverse = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency_pair", symbol);
            parameters.Add("limit", limit);
            parameters.Add("last_id", lastId);
            parameters.Add("reverse", reverse == null ? null : reverse == true ? "true" : "false");
            parameters.Add("from", startTime);
            parameters.Add("to", endTime);
            parameters.Add("page", page);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/spot/trades", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoTrade[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Klines

        /// <inheritdoc />
        public async Task<HttpResult<GateIoKline[]>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency_pair", symbol);
            parameters.Add("interval", interval);
            parameters.Add("limit", limit);
            parameters.Add("from", startTime);
            parameters.Add("to", endTime);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/spot/candlesticks", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoKline[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Networks

        /// <inheritdoc />
        public async Task<HttpResult<GateIoNetwork[]>> GetNetworksAsync(string asset, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/wallet/currency_chains", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoNetwork[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Discount Tiers

        /// <inheritdoc />
        public async Task<HttpResult<GateIoDiscountTier[]>> GetDiscountTiersAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/unified/currency_discount_tiers", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoDiscountTier[]>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Loan Margin Tiers

        /// <inheritdoc />
        public async Task<HttpResult<GateIoLoanMarginTier[]>> GetLoanMarginTiersAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/unified/loan_margin_tiers", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoLoanMarginTier[]>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Asset

        /// <inheritdoc />
        public async Task<HttpResult<GateIoCrossMarginAsset>> GetCrossMarginAssetAsync(string asset, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/margin/cross/currencies/" + asset, GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoCrossMarginAsset>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Assets

        /// <inheritdoc />
        public async Task<HttpResult<GateIoCrossMarginAsset[]>> GetCrossMarginAssetsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/margin/cross/currencies", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoCrossMarginAsset[]>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Lending Symbols

        /// <inheritdoc />
        public async Task<HttpResult<GateIoLendingSymbol[]>> GetLendingSymbolsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/margin/uni/currency_pairs", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoLendingSymbol[]>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Lending Symbol

        /// <inheritdoc />
        public async Task<HttpResult<GateIoLendingSymbol>> GetLendingSymbolAsync(string symbol, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/margin/uni/currency_pairs/" + symbol, GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoLendingSymbol>(request, null, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
