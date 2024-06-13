using CryptoExchange.Net.Objects;
using GateIo.Net.Interfaces.Clients.PerpetualFuturesApi;
using GateIo.Net.Objects.Models;
using System.Collections.Generic;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GateIo.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    public class GateIoRestClientPerpetualFuturesApiAccount : IGateIoRestClientPerpetualFuturesApiAccount
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly GateIoRestClientPerpetualFuturesApi _baseClient;

        internal GateIoRestClientPerpetualFuturesApiAccount(GateIoRestClientPerpetualFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Risk Limit Tiers

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoFuturesAccount>> GetAccountAsync(
            string settlementAsset,
            CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/futures/{settlementAsset}/accounts", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoFuturesAccount> (request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Account Ledger

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<GateIoPerpLedgerEntry>>> GetLedgerAsync(string settlementAsset, string? contract = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, string? type = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("contract", contract);
            parameters.AddOptional("page", page);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("type", type);
            parameters.AddOptionalSeconds("from", startTime);
            parameters.AddOptionalSeconds("to", endTime);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/futures/{settlementAsset}/account_book", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<IEnumerable<GateIoPerpLedgerEntry>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Update Position Mode

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoFuturesAccount>> UpdatePositionModeAsync(string settlementAsset, bool dualMode, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("dual_mode", dualMode.ToString().ToLowerInvariant());
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/api/v4/futures/{settlementAsset}/dual_mode", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<GateIoFuturesAccount>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Trading Fees

        /// <inheritdoc />
        public async Task<WebCallResult<Dictionary<string, GateIoPerpFee>>> GetTradingFeeAsync(string settlementAsset, string? contract = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("contract", contract);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/futures/{settlementAsset}/fee", GateIoExchange.RateLimiter.RestFuturesOther, 1, true);
            return await _baseClient.SendAsync<Dictionary<string, GateIoPerpFee>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
