using CryptoExchange.Net.Objects;
using GateIo.Net.Interfaces.Clients.PerpetualFuturesApi;
using GateIo.Net.Objects.Models;
using System.Collections.Generic;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GateIo.Net.Enums;

namespace GateIo.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    internal class GateIoRestClientPerpetualFuturesApiAccount : IGateIoRestClientPerpetualFuturesApiAccount
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly GateIoRestClientPerpetualFuturesApi _baseClient;

        internal GateIoRestClientPerpetualFuturesApiAccount(GateIoRestClientPerpetualFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Account

        /// <inheritdoc />
        public async Task<HttpResult<GateIoFuturesAccount>> GetAccountAsync(
            string settlementAsset,
            CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/accounts", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoFuturesAccount> (request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Account Ledger

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPerpLedgerEntry[]>> GetLedgerAsync(string settlementAsset, string? contract = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, string? type = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("contract", contract);
            parameters.Add("page", page);
            parameters.Add("limit", limit);
            parameters.Add("type", type);
            parameters.Add("from", startTime, DateTimeSerialization.SecondsNumber);
            parameters.Add("to", endTime, DateTimeSerialization.SecondsNumber);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/account_book", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPerpLedgerEntry[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Update Position Mode

        /// <inheritdoc />
        public async Task<HttpResult<GateIoFuturesAccount>> UpdatePositionModeAsync(string settlementAsset, bool dualMode, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("dual_mode", dualMode.ToString().ToLowerInvariant());
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/dual_mode", GateIoExchange.RateLimiter.RestFuturesOther, 1, true, parameterPosition: HttpMethodParameterPosition.InUri);
            return await _baseClient.SendAsync<GateIoFuturesAccount>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Margin Mode

        /// <inheritdoc />
        public async Task<HttpResult<GateIoPosition>> SetMarginModeAsync(string settlementAsset, string contract, MarginMode marginMode, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings) { { "contract", contract } };
            parameters.Add("mode", marginMode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/positions/cross_mode", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPosition>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Trading Fees

        /// <inheritdoc />
        public async Task<HttpResult<Dictionary<string, GateIoPerpFee>>> GetTradingFeeAsync(string settlementAsset, string? contract = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("contract", contract);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/fee", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<Dictionary<string, GateIoPerpFee>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

    }
}
