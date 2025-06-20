using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using GateIo.Net.Clients.RebateApi;
using GateIo.Net.Interfaces.Clients.RebateApi;
using GateIo.Net.Objects.Models;
using Microsoft.Extensions.Logging;

namespace GateIo.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class GateIoRestClientRebateApiExchangeData : IGateIoRestClientRebateApiExchangeData
    {
        private readonly GateIoRestClientRebateApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        internal GateIoRestClientRebateApiExchangeData(ILogger logger, GateIoRestClientRebateApi baseClient)
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
    }
}
