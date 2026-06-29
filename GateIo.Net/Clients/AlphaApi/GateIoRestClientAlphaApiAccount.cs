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
        public async Task<HttpResult<GateIoAlphaAccount[]>> GetAccountInfoAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/alpha/accounts", GateIoExchange.RateLimiter.RestAlpha, 1, true);
            return await _baseClient.SendAsync<GateIoAlphaAccount[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Ledger

        /// <inheritdoc />
        public async Task<HttpResult<GateIoAlphaLedgerEntry[]>> GetLedgerAsync(
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? page = null,
            int? limit = null, 
            CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("from", startTime, DateTimeSerialization.SecondsNumber);
            parameters.Add("to", endTime, DateTimeSerialization.SecondsNumber);
            parameters.Add("page", page);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/alpha/account_book", GateIoExchange.RateLimiter.RestAlpha, 1, true);
            return await _baseClient.SendAsync<GateIoAlphaLedgerEntry[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
