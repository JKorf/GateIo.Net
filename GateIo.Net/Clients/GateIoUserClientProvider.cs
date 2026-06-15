using GateIo.Net.Interfaces.Clients;
using GateIo.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Net.Http;
using CryptoExchange.Net.Clients;

namespace GateIo.Net.Clients
{
    /// <inheritdoc />
    public class GateIoUserClientProvider : UserClientProvider<
        IGateIoRestClient,
        IGateIoSocketClient,
        GateIoRestOptions,
        GateIoSocketOptions,
        GateIoCredentials,
        GateIoEnvironment
        >, IGateIoUserClientProvider
    {
        /// <inheritdoc />
        public override string ExchangeName => GateIoExchange.ExchangeName;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="optionsDelegate">Options to use for created clients</param>
        public GateIoUserClientProvider(Action<GateIoOptions>? optionsDelegate = null)
            : this(null, null, Options.Create(ApplyOptionsDelegate(optionsDelegate).Rest), Options.Create(ApplyOptionsDelegate(optionsDelegate).Socket))
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        public GateIoUserClientProvider(
            HttpClient? httpClient,
            ILoggerFactory? loggerFactory,
            IOptions<GateIoRestOptions> restOptions,
            IOptions<GateIoSocketOptions> socketOptions)
            : base(httpClient, loggerFactory, restOptions, socketOptions)
        {
        }

        /// <inheritdoc />
        protected override IGateIoRestClient ConstructRestClient(HttpClient client, ILoggerFactory? loggerFactory, IOptions<GateIoRestOptions> options) 
            => new GateIoRestClient(client, loggerFactory, options);
        /// <inheritdoc />
        protected override IGateIoSocketClient ConstructSocketClient(ILoggerFactory? loggerFactory, IOptions<GateIoSocketOptions> options)
            => new GateIoSocketClient(options, loggerFactory);
    }
}
