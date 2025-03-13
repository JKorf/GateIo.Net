using System;
using System.Collections.Generic;
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
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/spot/time", GateIoExchange.RateLimiter.Public, 1);
            var result = await _baseClient.SendAsync<GateIoServerTime>(request, null, ct).ConfigureAwait(false);
            return result.As(result.Data.ServerTime);
        }

        #endregion

        #region Get Assets

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoAsset[]>> GetAssetsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/spot/currencies", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoAsset[]>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Asset

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoAsset>> GetAssetAsync(string asset, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/spot/currencies/{asset}", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoAsset>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Symbol

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoSymbol>> GetSymbolAsync(string symbol, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/spot/currency_pairs/{symbol}", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoSymbol>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Symbols

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoSymbol[]>> GetSymbolsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/spot/currency_pairs", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoSymbol[]>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Tickers

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoTicker[]>> GetTickersAsync(string? symbol = null, string? timezone = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency_pair", symbol);
            parameters.AddOptional("timezone", timezone);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/spot/tickers", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoTicker[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Order Book

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoOrderBook>> GetOrderBookAsync(string symbol, int? mergeDepth = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency_pair", symbol);
            parameters.AddOptional("interval", mergeDepth);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("with_id", true.ToString().ToLowerInvariant());
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/spot/order_book", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoOrderBook>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Trades

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoTrade[]>> GetTradesAsync(string symbol, int? limit = null, string? lastId = null, bool? reverse = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency_pair", symbol);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("last_id", lastId);
            parameters.AddOptional("reverse", reverse == null ? null : reverse == true ? "true" : "false");
            parameters.AddOptionalSeconds("from", startTime);
            parameters.AddOptionalSeconds("to", endTime);
            parameters.AddOptional("page", page);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/spot/trades", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoTrade[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Klines

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoKline[]>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency_pair", symbol);
            parameters.AddEnum("interval", interval);
            parameters.AddOptional("limit", limit);
            parameters.AddOptionalSeconds("from", startTime);
            parameters.AddOptionalSeconds("to", endTime);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/spot/candlesticks", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoKline[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Networks

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoNetwork[]>> GetNetworksAsync(string asset, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/wallet/currency_chains", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoNetwork[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Discount Tiers

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoDiscountTier[]>> GetDiscountTiersAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/unified/currency_discount_tiers", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoDiscountTier[]>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Loan Margin Tiers

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoLoanMarginTier[]>> GetLoanMarginTiersAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/unified/loan_margin_tiers", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoLoanMarginTier[]>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Asset

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoCrossMarginAsset>> GetCrossMarginAssetAsync(string asset, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/margin/cross/currencies/" + asset, GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoCrossMarginAsset>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Assets

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoCrossMarginAsset[]>> GetCrossMarginAssetsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/margin/cross/currencies", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoCrossMarginAsset[]>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Lending Symbols

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoLendingSymbol[]>> GetLendingSymbolsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/margin/uni/currency_pairs", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoLendingSymbol[]>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Lending Symbol

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoLendingSymbol>> GetLendingSymbolAsync(string symbol, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/margin/uni/currency_pairs/" + symbol, GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoLendingSymbol>(request, null, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
