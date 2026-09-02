using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using GateIo.Net.Objects.Models;
using Microsoft.Extensions.Logging;
using GateIo.Net.Interfaces.Clients.PerpetualFuturesApi;
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
        public async Task<HttpResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/spot/time", GateIoExchange.RateLimiter.Public, 1);
            var result = await _baseClient.SendAsync<GateIoServerTime>(request, null, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<DateTime>(result);

            return HttpResult.Ok(result, result.Data.ServerTime);
        }

        #endregion

        #region Get Contracts

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPerpFuturesContract[]>> GetContractsAsync(string settlementAsset, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/contracts", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoPerpFuturesContract[]>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Contract

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPerpFuturesContract>> GetContractAsync(string settlementAsset, string contract, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/contracts/" + contract, GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoPerpFuturesContract>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Order Book

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPerpOrderBook>> GetOrderBookAsync(string settlementAsset, string contract, int? mergeDepth = null, int? depth = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("contract", contract);
            parameters.Add("interval", mergeDepth);
            parameters.Add("limit", depth);
            parameters.Add("with_id", true.ToString().ToLowerInvariant());
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/order_book", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoPerpOrderBook>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Trades

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPerpTrade[]>> GetTradesAsync(
            string settlementAsset, 
            string contract, 
            int? limit = null, 
            int? offset = null, 
            string? lastId = null, 
            DateTime? startTime = null, 
            DateTime? endTime = null, 
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("contract", contract);
            parameters.Add("limit", limit);
            parameters.Add("offset", offset);
            parameters.Add("last_id", lastId);
            parameters.Add("from", startTime, DateTimeSerialization.SecondsNumber);
            parameters.Add("to", endTime, DateTimeSerialization.SecondsNumber);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/trades", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoPerpTrade[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Klines

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPerpKline[]>> GetKlinesAsync(
            string settlementAsset,
            string contract,
            KlineInterval interval,
            int? limit = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("contract", contract);
            parameters.Add("interval", interval);
            parameters.Add("limit", limit);
            parameters.Add("from", startTime, DateTimeSerialization.SecondsNumber);
            parameters.Add("to", endTime, DateTimeSerialization.SecondsNumber);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/candlesticks", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoPerpKline[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Index Klines

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPerpIndexKline[]>> GetIndexKlinesAsync(
            string settlementAsset,
            string contract,
            KlineInterval interval,
            int? limit = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("contract", contract);
            parameters.Add("interval", interval);
            parameters.Add("limit", limit);
            parameters.Add("from", startTime, DateTimeSerialization.SecondsNumber);
            parameters.Add("to", endTime, DateTimeSerialization.SecondsNumber);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/premium_index", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoPerpIndexKline[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Tickers

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPerpTicker[]>> GetTickersAsync(
            string settlementAsset,
            string? contract = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("contract", contract);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/tickers", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoPerpTicker[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Funding Rate History

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPerpFundingRate[]>> GetFundingRateHistoryAsync(
            string settlementAsset,
            string contract,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("contract", contract);
            parameters.Add("from", startTime, DateTimeSerialization.SecondsNumber);
            parameters.Add("to", endTime, DateTimeSerialization.SecondsNumber);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/funding_rate", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoPerpFundingRate[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Insurance Balance History

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPerpInsurance[]>> GetInsuranceBalanceHistoryAsync(
            string settlementAsset,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/insurance", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoPerpInsurance[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Stats

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPerpContractStats[]>> GetContractStatsAsync(
            string settlementAsset,
            string contract,
            int? limit = null,
            DateTime? startTime = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("contract", contract);
            parameters.Add("limit", limit);
            parameters.Add("from", startTime, DateTimeSerialization.SecondsNumber);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/contract_stats", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoPerpContractStats[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Index Constituents

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPerpConstituent>> GetIndexConstituentsAsync(
            string settlementAsset,
            string contract,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/index_constituents/{contract}", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoPerpConstituent>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Liquidations

        /// <inheritdoc />
        public async Task<HttpResult<GateIoLiquidation[]>> GetLiquidationsAsync(
            string settlementAsset,
            string? contract = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("contract", contract);
            parameters.Add("limit", limit);
            parameters.Add("from", startTime, DateTimeSerialization.SecondsNumber);
            parameters.Add("to", endTime, DateTimeSerialization.SecondsNumber);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/liq_orders", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoLiquidation[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Risk Limit Tiers

        /// <inheritdoc />
        public async Task<HttpResult<GateIoRiskLimitTier[]>> GetRiskLimitTiersAsync(
            string settlementAsset,
            string contract,
            int? offset = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("contract", contract);
            parameters.Add("limit", limit);
            parameters.Add("offset", offset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/risk_limit_tiers", GateIoExchange.RateLimiter.Public, 1);
            return await _baseClient.SendAsync<GateIoRiskLimitTier[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
