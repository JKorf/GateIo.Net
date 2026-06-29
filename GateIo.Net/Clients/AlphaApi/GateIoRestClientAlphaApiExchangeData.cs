using CryptoExchange.Net.Objects;
using GateIo.Net.Objects.Models;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GateIo.Net.Interfaces.Clients.AlphaApi;

namespace GateIo.Net.Clients.AlphaApi
{
    /// <inheritdoc />
    internal class GateIoRestClientAlphaApiExchangeData : IGateIoRestClientAlphaApiExchangeData
    {
        private readonly GateIoRestClientAlphaApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        internal GateIoRestClientAlphaApiExchangeData(GateIoRestClientAlphaApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Assets

        /// <inheritdoc />
        public async Task<HttpResult<GateIoAlphaAsset[]>> GetAssetsAsync(string? asset = null, int? page = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("page", page);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/alpha/currencies", GateIoExchange.RateLimiter.RestAlpha, 1, false);
            return await _baseClient.SendAsync<GateIoAlphaAsset[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Tickers

        /// <inheritdoc />
        public async Task<HttpResult<GateIoAlphaTicker[]>> GetTickersAsync(string? asset = null, int? page = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(GateIoExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("page", page);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v4/alpha/tickers", GateIoExchange.RateLimiter.RestAlpha, 1, false);
            return await _baseClient.SendAsync<GateIoAlphaTicker[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

    }
}
