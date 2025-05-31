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
        public async Task<WebCallResult<GateIoFuturesAccount>> GetAccountAsync(
            string settlementAsset,
            CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/accounts", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoFuturesAccount> (request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Account Ledger

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPerpLedgerEntry[]>> GetLedgerAsync(string settlementAsset, string? contract = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, string? type = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("contract", contract);
            parameters.AddOptional("page", page);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("type", type);
            parameters.AddOptionalSeconds("from", startTime);
            parameters.AddOptionalSeconds("to", endTime);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/account_book", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPerpLedgerEntry[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Update Position Mode

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoFuturesAccount>> UpdatePositionModeAsync(string settlementAsset, bool dualMode, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("dual_mode", dualMode.ToString().ToLowerInvariant());
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/dual_mode", GateIoExchange.RateLimiter.RestFuturesOther, 1, true, parameterPosition: HttpMethodParameterPosition.InUri);
            return await _baseClient.SendAsync<GateIoFuturesAccount>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Margin Mode

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoPosition[]>> SetMarginModeAsync(string settlementAsset, string contract, MarginMode marginMode, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contract", contract);
            parameters.AddEnum("mode", marginMode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/positions/cross_mode", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoPosition[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Trading Fees

        /// <inheritdoc />
        public async Task<WebCallResult<Dictionary<string, GateIoPerpFee>>> GetTradingFeeAsync(string settlementAsset, string? contract = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("contract", contract);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/futures/{settlementAsset.ToLowerInvariant()}/fee", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<Dictionary<string, GateIoPerpFee>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
