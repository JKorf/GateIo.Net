using System;
using System.Net.Http;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects.Options;
using GateIo.Net.Clients.FuturesApi;
using GateIo.Net.Clients.RebateApi;
using GateIo.Net.Clients.SpotApi;
using GateIo.Net.Interfaces.Clients;
using GateIo.Net.Interfaces.Clients.PerpetualFuturesApi;
using GateIo.Net.Interfaces.Clients.RebateApi;
using GateIo.Net.Interfaces.Clients.SpotApi;
using GateIo.Net.Objects.Options;
using Microsoft.Extensions.Logging;
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
        /// <inheritdoc />
        public IGateIoRestClientRebateApi RebateApi { get; }

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
            RebateApi = AddApiClient(new GateIoRestClientRebateApi(_logger, httpClient, this, options.Value));
        }

        #endregion

        /// <inheritdoc />
        public void SetOptions(UpdateOptions options)
        {
            SpotApi.SetOptions(options);
            PerpetualFuturesApi.SetOptions(options);
            RebateApi.SetOptions(options);
        }

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
            RebateApi.SetApiCredentials(credentials);
        }
    }
}
