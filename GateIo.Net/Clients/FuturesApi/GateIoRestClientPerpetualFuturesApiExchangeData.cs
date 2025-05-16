using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using GateIo.Net.Objects.Models;
using Microsoft.Extensions.Logging;
using GateIo.Net.Interfaces.Clients.PerpetualFuturesApi;
using System.Collections.Generic;
using GateIo.Net.Enums;

namespace GateIo.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    internal class GateIoRestClientPerpetualFuturesApiExchangeData : IGateIoRestClientPerpetualFuturesApiExchangeData
    {
        private readonly ILogger _logger;

        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly GateIoRestClientPerpetualFuturesApi _baseClient;

        internal GateIoRestClientPerpetualFuturesApiExchangeData(ILogger logger, GateIoRestClientPerpetualFuturesApi baseClient)
        {
            _logger = logger;
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

        #region Get Contracts

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPerpFuturesContract[]>> GetContractsAsync(string settlementAsset, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/contracts", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoPerpFuturesContract[]>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Contract

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPerpFuturesContract>> GetContractAsync(string settlementAsset, string contract, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/contracts/" + contract, GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoPerpFuturesContract>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Order Book

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPerpOrderBook>> GetOrderBookAsync(string settlementAsset, string contract, int? mergeDepth = null, int? depth = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contract", contract);
            parameters.AddOptional("interval", mergeDepth);
            parameters.AddOptional("limit", depth);
            parameters.AddOptional("with_id", true.ToString().ToLowerInvariant());
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/order_book", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoPerpOrderBook>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Trades

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPerpTrade[]>> GetTradesAsync(
            string settlementAsset, 
            string contract, 
            int? limit = null, 
            int? offset = null, 
            string? lastId = null, 
            DateTime? startTime = null, 
            DateTime? endTime = null, 
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contract", contract);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("offset", offset);
            parameters.AddOptional("last_id", lastId);
            parameters.AddOptionalSeconds("from", startTime);
            parameters.AddOptionalSeconds("to", endTime);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/trades", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoPerpTrade[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Klines

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPerpKline[]>> GetKlinesAsync(
            string settlementAsset,
            string contract,
            KlineInterval interval,
            int? limit = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contract", contract);
            parameters.AddEnum("interval", interval);
            parameters.AddOptional("limit", limit);
            parameters.AddOptionalSeconds("from", startTime);
            parameters.AddOptionalSeconds("to", endTime);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/candlesticks", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoPerpKline[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Index Klines

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPerpIndexKline[]>> GetIndexKlinesAsync(
            string settlementAsset,
            string contract,
            KlineInterval interval,
            int? limit = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contract", contract);
            parameters.AddEnum("interval", interval);
            parameters.AddOptional("limit", limit);
            parameters.AddOptionalSeconds("from", startTime);
            parameters.AddOptionalSeconds("to", endTime);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/premium_index", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoPerpIndexKline[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Tickers

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPerpTicker[]>> GetTickersAsync(
            string settlementAsset,
            string? contract = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("contract", contract);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/tickers", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoPerpTicker[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Funding Rate History

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPerpFundingRate[]>> GetFundingRateHistoryAsync(
            string settlementAsset,
            string contract,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contract", contract);
            parameters.AddOptionalSeconds("from", startTime);
            parameters.AddOptionalSeconds("to", endTime);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/funding_rate", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoPerpFundingRate[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Insurance Balance History

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPerpInsurance[]>> GetInsuranceBalanceHistoryAsync(
            string settlementAsset,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/insurance", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoPerpInsurance[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Stats

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPerpContractStats[]>> GetContractStatsAsync(
            string settlementAsset,
            string contract,
            int? limit = null,
            DateTime? startTime = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contract", contract);
            parameters.AddOptional("limit", limit);
            parameters.AddOptionalSeconds("from", startTime);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/contract_stats", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoPerpContractStats[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Index Constituents

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPerpConstituent>> GetIndexConstituentsAsync(
            string settlementAsset,
            string contract,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/index_constituents/{contract}", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoPerpConstituent>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Liquidations

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoLiquidation[]>> GetLiquidationsAsync(
            string settlementAsset,
            string? contract = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("contract", contract);
            parameters.AddOptional("limit", limit);
            parameters.AddOptionalSeconds("from", startTime);
            parameters.AddOptionalSeconds("to", endTime);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/liq_orders", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoLiquidation[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Risk Limit Tiers

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoRiskLimitTier[]>> GetRiskLimitTiersAsync(
            string settlementAsset,
            string contract,
            int? offset = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contract", contract);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/risk_limit_tiers", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoRiskLimitTier[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
