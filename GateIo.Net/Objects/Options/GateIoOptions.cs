using CryptoExchange.Net.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GateIo.Net.Objects.Options
{
    /// <summary>
    /// GateIo options
    /// </summary>
    public class GateIoOptions
    {
        /// <summary>
        /// Rest client options
        /// </summary>
        public GateIoRestOptions Rest { get; set; } = new GateIoRestOptions();

        /// <summary>
        /// Socket client options
        /// </summary>
        public GateIoSocketOptions Socket { get; set; } = new GateIoSocketOptions();

        /// <summary>
        /// Trade environment. Contains info about URL's to use to connect to the API. Use `GateIoEnvironment` to swap environment, for example `Environment = GateIoEnvironment.Live`
        /// </summary>
        public GateIoEnvironment? Environment { get; set; }

        /// <summary>
        /// The api credentials used for signing requests.
        /// </summary>
        public ApiCredentials? ApiCredentials { get; set; }

        /// <summary>
        /// The DI service lifetime for the IGateIoSocketClient
        /// </summary>
        public ServiceLifetime? SocketClientLifeTime { get; set; }
    }
}
