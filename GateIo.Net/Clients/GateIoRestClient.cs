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
        public GateIoRestClient(Action<GateIoRestOptions>? optionsDelegate = null) : this(null, null, optionsDelegate)
        {
        }

        /// <summary>
        /// Create a new instance of the GateIoRestClient using provided options
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="httpClient">Http client for this client</param>
        public GateIoRestClient(HttpClient? httpClient, ILoggerFactory? loggerFactory, Action<GateIoRestOptions>? optionsDelegate = null) : base(loggerFactory, "GateIo")
        {
            var options = GateIoRestOptions.Default.Copy();
            if (optionsDelegate != null)
                optionsDelegate(options);
            Initialize(options);

            SpotApi = AddApiClient(new GateIoRestClientSpotApi(_logger, httpClient, options));
            PerpetualFuturesApi = AddApiClient(new GateIoRestClientPerpetualFuturesApi(_logger, httpClient, options));
        }

        #endregion

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<GateIoRestOptions> optionsDelegate)
        {
            var options = GateIoRestOptions.Default.Copy();
            optionsDelegate(options);
            GateIoRestOptions.Default = options;
        }

        /// <inheritdoc />
        public void SetApiCredentials(ApiCredentials credentials)
        {
            SpotApi.SetApiCredentials(credentials);
            PerpetualFuturesApi.SetApiCredentials(credentials);
        }
    }
}
