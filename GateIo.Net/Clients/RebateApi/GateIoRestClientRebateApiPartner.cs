using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using GateIo.Net.Interfaces.Clients.RebateApi;
using GateIo.Net.Objects.Models;

namespace GateIo.Net.Clients.RebateApi
{
    /// <inheritdoc />
    internal class GateIoRestClientRebateApiPartner : IGateIoRestClientRebateApiPartner
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly GateIoRestClientRebateApi _baseClient;

        internal GateIoRestClientRebateApiPartner(GateIoRestClientRebateApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoRebatePartnerSubordinateList>> GetSubordinatesAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v4/rebate/partner/sub_list", GateIoExchange.RateLimiter.RestOther, 1, true);
            return await _baseClient.SendAsync<GateIoRebatePartnerSubordinateList>(request, parameters, ct).ConfigureAwait(false);
        }
    }
}
