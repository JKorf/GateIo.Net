using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.Klines;
using CryptoExchange.Net.Trackers.Trades;
using CryptoExchange.Net.Trackers.UserData.Interfaces;
using CryptoExchange.Net.Trackers.UserData.Objects;
using GateIo.Net.Clients;
using GateIo.Net.Interfaces;
using GateIo.Net.Interfaces.Clients;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;

namespace GateIo.Net
{
    /// <inheritdoc />
    public class GateIoTrackerFactory : IGateIoTrackerFactory
    {
        private readonly IServiceProvider? _serviceProvider;

        /// <summary>
        /// ctor
        /// </summary>
        public GateIoTrackerFactory()
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public GateIoTrackerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public bool CanCreateKlineTracker(SharedSymbol symbol, SharedKlineInterval interval)
        {
            var client = (_serviceProvider?.GetRequiredService<IGateIoSocketClient>() ?? new GateIoSocketClient());
            SubscribeKlineOptions klineOptions = symbol.TradingMode == TradingMode.Spot ? client.SpotApi.SharedClient.SubscribeKlineOptions : client.PerpetualFuturesApi.SharedClient.SubscribeKlineOptions;
            return klineOptions.IsSupported(interval);
        }

        /// <inheritdoc />
        public bool CanCreateTradeTracker(SharedSymbol symbol) => true;

        /// <inheritdoc />
        public IKlineTracker CreateKlineTracker(SharedSymbol symbol, SharedKlineInterval interval, int? limit = null, TimeSpan? period = null)
        {
            var restClient = _serviceProvider?.GetRequiredService<IGateIoRestClient>() ?? new GateIoRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IGateIoSocketClient>() ?? new GateIoSocketClient();

            IKlineRestClient sharedRestClient;
            IKlineSocketClient sharedSocketClient;
            if (symbol.TradingMode == TradingMode.Spot)
            {
                sharedRestClient = restClient.SpotApi.SharedClient;
                sharedSocketClient = socketClient.SpotApi.SharedClient;
            }
            else
            {
                sharedRestClient = restClient.PerpetualFuturesApi.SharedClient;
                sharedSocketClient = socketClient.PerpetualFuturesApi.SharedClient;
            }

            return new KlineTracker(
                _serviceProvider?.GetRequiredService<ILoggerFactory>().CreateLogger(restClient.Exchange),
                sharedRestClient,
                sharedSocketClient,
                symbol,
                interval,
                limit,
                period
                );
        }
        /// <inheritdoc />
        public ITradeTracker CreateTradeTracker(SharedSymbol symbol, int? limit = null, TimeSpan? period = null)
        {
            var restClient = _serviceProvider?.GetRequiredService<IGateIoRestClient>() ?? new GateIoRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IGateIoSocketClient>() ?? new GateIoSocketClient();

            IRecentTradeRestClient? sharedRestClient;
            ITradeSocketClient sharedSocketClient;
            if (symbol.TradingMode == TradingMode.Spot)
            {
                sharedRestClient = restClient.SpotApi.SharedClient;
                sharedSocketClient = socketClient.SpotApi.SharedClient;
            }
            else
            {
                sharedRestClient = restClient.PerpetualFuturesApi.SharedClient;
                sharedSocketClient = socketClient.PerpetualFuturesApi.SharedClient;
            }

            return new TradeTracker(
                _serviceProvider?.GetRequiredService<ILoggerFactory>().CreateLogger(restClient.Exchange),
                sharedRestClient,
                null,
                sharedSocketClient,
                symbol,
                limit,
                period
                );
        }

        /// <inheritdoc />
        public IUserSpotDataTracker CreateUserSpotDataTracker(SpotUserDataTrackerConfig config)
        {
            var restClient = _serviceProvider?.GetRequiredService<IGateIoRestClient>() ?? new GateIoRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IGateIoSocketClient>() ?? new GateIoSocketClient();
            return new GateIoUserSpotDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<GateIoUserSpotDataTracker>>() ?? new NullLogger<GateIoUserSpotDataTracker>(),
                restClient,
                socketClient,
                null,
                config
                );
        }

        /// <inheritdoc />
        public IUserSpotDataTracker CreateUserSpotDataTracker(string userIdentifier, SpotUserDataTrackerConfig config, ApiCredentials credentials, GateIoEnvironment? environment = null)
        {
            var clientProvider = _serviceProvider?.GetRequiredService<IGateIoUserClientProvider>() ?? new GateIoUserClientProvider();
            var restClient = clientProvider.GetRestClient(userIdentifier, credentials, environment);
            var socketClient = clientProvider.GetSocketClient(userIdentifier, credentials, environment);
            return new GateIoUserSpotDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<GateIoUserSpotDataTracker>>() ?? new NullLogger<GateIoUserSpotDataTracker>(),
                restClient,
                socketClient,
                userIdentifier,
                config
                );
        }

        /// <inheritdoc />
        public IUserFuturesDataTracker CreateUserFuturesDataTracker(FuturesUserDataTrackerConfig config)
        {
            var restClient = _serviceProvider?.GetRequiredService<IGateIoRestClient>() ?? new GateIoRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IGateIoSocketClient>() ?? new GateIoSocketClient();
            return new GateIoUserFuturesDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<GateIoUserFuturesDataTracker>>() ?? new NullLogger<GateIoUserFuturesDataTracker>(),
                restClient,
                socketClient,
                null,
                config
                );
        }

        /// <inheritdoc />
        public IUserFuturesDataTracker CreateUserFuturesDataTracker(string userIdentifier, FuturesUserDataTrackerConfig config, ApiCredentials credentials, GateIoEnvironment? environment = null)
        {
            var clientProvider = _serviceProvider?.GetRequiredService<IGateIoUserClientProvider>() ?? new GateIoUserClientProvider();
            var restClient = clientProvider.GetRestClient(userIdentifier, credentials, environment);
            var socketClient = clientProvider.GetSocketClient(userIdentifier, credentials, environment);
            return new GateIoUserFuturesDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<GateIoUserFuturesDataTracker>>() ?? new NullLogger<GateIoUserFuturesDataTracker>(),
                restClient,
                socketClient,
                userIdentifier,
                config
                );
        }
    }
}
