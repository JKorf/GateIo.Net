using Microsoft.Extensions.Logging;
using System.Net.Http;
using System;
using CryptoExchange.Net.Authentication;
using GateIo.Net.Interfaces.Clients;
using GateIo.Net.Interfaces.Clients.SpotApi;
using GateIo.Net.Objects.Options;
using GateIo.Net.Clients.SpotApi;
using GateIo.Net.Clients.FuturesApi;
using CryptoExchange.Net.Clients;
using GateIo.Net.Interfaces.Clients.PerpetualFuturesApi;
using Microsoft.Extensions.Options;

namespace GateIo.Net.Clients
{
    /// <inheritdoc cref="IGateIoRestClient" />
    public class GateIoRestClient : BaseRestClient, IGateIoRestClient
    {
        #region Api clients

        /// <inheritdoc />
        public IGateIoRestClientSpotApi SpotApi { get; }
        /// <inheritdoc />
        public IGateIoRestClientPerpetualFuturesApi PerpetualFuturesApi { get; }

        #endregion

        #region constructor/destructor

        /// <summary>
        /// Create a new instance of the GateIoRestClient using provided options
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public GateIoRestClient(Action<GateIoRestOptions>? optionsDelegate = null)
            : this(null, null, Options.Create(ApplyOptionsDelegate(optionsDelegate)))
        {
        }

        /// <summary>
        /// Create a new instance of the GateIoRestClient using provided options
        /// </summary>
        /// <param name="options">Option configuration delegate</param>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="httpClient">Http client for this client</param>
        public GateIoRestClient(HttpClient? httpClient, ILoggerFactory? loggerFactory, IOptions<GateIoRestOptions> options) : base(loggerFactory, "GateIo")
        {
            Initialize(options.Value);

            SpotApi = AddApiClient(new GateIoRestClientSpotApi(_logger, httpClient, options.Value));
            PerpetualFuturesApi = AddApiClient(new GateIoRestClientPerpetualFuturesApi(_logger, httpClient, options.Value));
        }

        #endregion

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<GateIoRestOptions> optionsDelegate)
        {
            GateIoRestOptions.Default = ApplyOptionsDelegate(optionsDelegate);
        }

        /// <inheritdoc />
        public void SetApiCredentials(ApiCredentials credentials)
        {
            SpotApi.SetApiCredentials(credentials);
            PerpetualFuturesApi.SetApiCredentials(credentials);
        }
    }
}
