using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using Microsoft.Extensions.Logging;
using System;
using GateIo.Net.Clients.FuturesApi;
using GateIo.Net.Clients.SpotApi;
using GateIo.Net.Interfaces.Clients;
using GateIo.Net.Interfaces.Clients.SpotApi;
using GateIo.Net.Objects.Options;
using GateIo.Net.Interfaces.Clients.PerpetualFuturesApi;

namespace GateIo.Net.Clients
{
    /// <inheritdoc cref="IGateIoSocketClient" />
    public class GateIoSocketClient : BaseSocketClient, IGateIoSocketClient
    {
        #region fields
        #endregion

        #region Api clients

        /// <inheritdoc />
        public IGateIoSocketClientSpotApi SpotApi { get; set; }

        /// <inheritdoc />
        public IGateIoSocketClientPerpetualFuturesApi PerpetualFuturesApi { get; set; }

        #endregion

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of GateIoSocketClient
        /// </summary>
        /// <param name="loggerFactory">The logger factory</param>
        public GateIoSocketClient(ILoggerFactory? loggerFactory = null) : this((x) => { }, loggerFactory)
        {
        }

        /// <summary>
        /// Create a new instance of GateIoSocketClient
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public GateIoSocketClient(Action<GateIoSocketOptions> optionsDelegate) : this(optionsDelegate, null)
        {
        }

        /// <summary>
        /// Create a new instance of GateIoSocketClient
        /// </summary>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public GateIoSocketClient(Action<GateIoSocketOptions>? optionsDelegate, ILoggerFactory? loggerFactory = null) : base(loggerFactory, "GateIo")
        {
            var options = GateIoSocketOptions.Default.Copy();
            optionsDelegate?.Invoke(options);
            Initialize(options);

            SpotApi = AddApiClient(new GateIoSocketClientSpotApi(_logger, options));
            PerpetualFuturesApi = AddApiClient(new GateIoSocketClientPerpetualFuturesApi(_logger, options));
        }
        #endregion

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<GateIoSocketOptions> optionsDelegate)
        {
            var options = GateIoSocketOptions.Default.Copy();
            optionsDelegate(options);
            GateIoSocketOptions.Default = options;
        }

        /// <inheritdoc />
        public void SetApiCredentials(ApiCredentials credentials)
        {
            SpotApi.SetApiCredentials(credentials);
            PerpetualFuturesApi.SetApiCredentials(credentials);
        }
    }
}
