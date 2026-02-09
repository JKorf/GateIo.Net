using GateIo.Net.Interfaces.Clients;
using GateIo.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Net.Http;

namespace GateIo.Net.Clients
{
    /// <inheritdoc />
    public class GateIoUserClientProvider : IGateIoUserClientProvider
    {
        private ConcurrentDictionary<string, IGateIoRestClient> _restClients = new ConcurrentDictionary<string, IGateIoRestClient>();
        private ConcurrentDictionary<string, IGateIoSocketClient> _socketClients = new ConcurrentDictionary<string, IGateIoSocketClient>();

        private readonly IOptions<GateIoRestOptions> _restOptions;
        private readonly IOptions<GateIoSocketOptions> _socketOptions;
        private readonly HttpClient _httpClient;
        private readonly ILoggerFactory? _loggerFactory;

        /// <inheritdoc />
        public string ExchangeName => GateIoExchange.ExchangeName;

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
        {
            _httpClient = httpClient ?? new HttpClient();
            _loggerFactory = loggerFactory;
            _restOptions = restOptions;
            _socketOptions = socketOptions;
        }

        /// <inheritdoc />
        public void InitializeUserClient(string userIdentifier, ApiCredentials credentials, GateIoEnvironment? environment = null)
        {
            CreateRestClient(userIdentifier, credentials, environment);
            CreateSocketClient(userIdentifier, credentials, environment);
        }

        /// <inheritdoc />
        public void ClearUserClients(string userIdentifier)
        {
            _restClients.TryRemove(userIdentifier, out _);
            _socketClients.TryRemove(userIdentifier, out _);
        }

        /// <inheritdoc />
        public IGateIoRestClient GetRestClient(string userIdentifier, ApiCredentials? credentials = null, GateIoEnvironment? environment = null)
        {
            if (!_restClients.TryGetValue(userIdentifier, out var client) || client.Disposed)
                client = CreateRestClient(userIdentifier, credentials, environment);

            return client;
        }

        /// <inheritdoc />
        public IGateIoSocketClient GetSocketClient(string userIdentifier, ApiCredentials? credentials = null, GateIoEnvironment? environment = null)
        {
            if (!_socketClients.TryGetValue(userIdentifier, out var client) || client.Disposed)
                client = CreateSocketClient(userIdentifier, credentials, environment);

            return client;
        }

        private IGateIoRestClient CreateRestClient(string userIdentifier, ApiCredentials? credentials, GateIoEnvironment? environment)
        {
            var clientRestOptions = SetRestEnvironment(environment);
            var client = new GateIoRestClient(_httpClient, _loggerFactory, clientRestOptions);
            if (credentials != null)
            {
                client.SetApiCredentials(credentials);
                _restClients.TryAdd(userIdentifier, client);
            }
            return client;
        }

        private IGateIoSocketClient CreateSocketClient(string userIdentifier, ApiCredentials? credentials, GateIoEnvironment? environment)
        {
            var clientSocketOptions = SetSocketEnvironment(environment);
            var client = new GateIoSocketClient(clientSocketOptions!, _loggerFactory);
            if (credentials != null)
            {
                client.SetApiCredentials(credentials);
                _socketClients.TryAdd(userIdentifier, client);
            }
            return client;
        }

        private IOptions<GateIoRestOptions> SetRestEnvironment(GateIoEnvironment? environment)
        {
            if (environment == null)
                return _restOptions;

            var newRestClientOptions = new GateIoRestOptions();
            var restOptions = _restOptions.Value.Set(newRestClientOptions);
            newRestClientOptions.Environment = environment;
            return Options.Create(newRestClientOptions);
        }

        private IOptions<GateIoSocketOptions> SetSocketEnvironment(GateIoEnvironment? environment)
        {
            if (environment == null)
                return _socketOptions;

            var newSocketClientOptions = new GateIoSocketOptions();
            var restOptions = _socketOptions.Value.Set(newSocketClientOptions);
            newSocketClientOptions.Environment = environment;
            return Options.Create(newSocketClientOptions);
        }

        private static T ApplyOptionsDelegate<T>(Action<T>? del) where T : new()
        {
            var opts = new T();
            del?.Invoke(opts);
            return opts;
        }
    }
}
