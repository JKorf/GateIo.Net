using CryptoExchange.Net.Objects;
using GateIo.Net.Interfaces.Clients.SpotApi;
using GateIo.Net.Objects.Models;
using GateIo.Net.Enums;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.RateLimiting.Guards;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Linq;
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
        public async Task<WebCallResult<GateIoAlphaAsset[]>> GetAssetsAsync(string? asset = null, int? page = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);
            parameters.AddOptional("page", page);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/alpha/currencies", GateIoExchange.RateLimiter.RestAlpha, 1, false);
            return await _baseClient.SendAsync<GateIoAlphaAsset[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Tickers

        /// <inheritdoc />
        public async Task<WebCallResult<GateIoAlphaTicker[]>> GetTickersAsync(string? asset = null, int? page = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);
            parameters.AddOptional("page", page);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v4/alpha/tickers", GateIoExchange.RateLimiter.RestAlpha, 1, false);
            return await _baseClient.SendAsync<GateIoAlphaTicker[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

    }
}
