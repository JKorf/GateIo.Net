using CryptoExchange.Net.Objects;
using GateIo.Net.Objects.Models;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GateIo.Net.Interfaces.Clients.AlphaApi;

namespace GateIo.Net.Clients.AlphaApi
{
    /// <inheritdoc />
    internal class GateIoRestClientAlphaApiAccount : IGateIoRestClientAlphaApiAccount
    {
        private readonly GateIoRestClientAlphaApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        internal GateIoRestClientAlphaApiAccount(GateIoRestClientAlphaApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Account Info

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoAlphaAccount[]>> GetAccountInfoAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/alpha/accounts", GateIoExchange.RateLimiter.RestAlpha, 1, true);
            return await _baseClient.SendAsync<GateIoAlphaAccount[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Ledger

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoAlphaLedgerEntry[]>> GetLedgerAsync(
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? page = null,
            int? limit = null, 
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalSeconds("from", startTime);
            parameters.AddOptionalSeconds("to", endTime);
            parameters.AddOptional("page", page);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/alpha/account_book", GateIoExchange.RateLimiter.RestAlpha, 1, true);
            return await _baseClient.SendAsync<GateIoAlphaLedgerEntry[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
