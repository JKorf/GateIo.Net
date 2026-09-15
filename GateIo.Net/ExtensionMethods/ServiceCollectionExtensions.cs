using CryptoExchange.Net;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.SharedApis;
using GateIo.Net;
using GateIo.Net.Clients;
using GateIo.Net.Interfaces;
using GateIo.Net.Interfaces.Clients;
using GateIo.Net.Objects.Options;
using GateIo.Net.SymbolOrderBooks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for DI
    /// </summary>
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// Add services such as the IGateIoRestClient and IGateIoSocketClient. Configures the services based on the provided configuration.<br />
        /// See <see href="https://github.com/JKorf/GateIo.Net/blob/main/Examples/example-config.json" /> for an example of how to set up the configuration.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="configuration">The configuration(section) containing the options</param>
        /// <returns></returns>
        public static IServiceCollection AddGateIo(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = GateIoOptions.CreateFromConfiguration(configuration);
            services.AddSingleton(Options.Options.Create(options.Rest));
            services.AddSingleton(Options.Options.Create(options.Socket));
            services.AddSingleton(Options.Options.Create(options));

            return AddGateIoCore(services, options.SocketClientLifeTime);
        }

        /// <summary>
        /// Add services such as the IGateIoRestClient and IGateIoSocketClient. Services will be configured based on the provided options.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="optionsDelegate">Set options for the GateIo services</param>
        /// <returns></returns>
        public static IServiceCollection AddGateIo(
            this IServiceCollection services,
            Action<GateIoOptions>? optionsDelegate = null)
        {
            var options = GateIoOptions.Create(optionsDelegate);
            services.AddSingleton(Options.Options.Create(options.Rest));
            services.AddSingleton(Options.Options.Create(options.Socket));
            services.AddSingleton(Options.Options.Create(options));

            return AddGateIoCore(services, options.SocketClientLifeTime);
        }

        private static IServiceCollection AddGateIoCore(
            this IServiceCollection services,
            ServiceLifetime? socketClientLifeTime = null)
        {
            services.AddHttpClient<IGateIoRestClient, GateIoRestClient>((client, serviceProvider) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<GateIoRestOptions>>().Value;
                client.Timeout = options.RequestTimeout;
                return new GateIoRestClient(client, serviceProvider.GetRequiredService<ILoggerFactory>(), serviceProvider.GetRequiredService<IOptions<GateIoRestOptions>>());
            }).ConfigurePrimaryHttpMessageHandler((serviceProvider) => {
                var options = serviceProvider.GetRequiredService<IOptions<GateIoRestOptions>>().Value;
                return LibraryHelpers.CreateHttpClientMessageHandler(options);
            }).SetHandlerLifetime(Timeout.InfiniteTimeSpan);
            services.Add(new ServiceDescriptor(typeof(IGateIoSocketClient), x => { return new GateIoSocketClient(x.GetRequiredService<IOptions<GateIoSocketOptions>>(), x.GetRequiredService<ILoggerFactory>()); }, socketClientLifeTime ?? ServiceLifetime.Singleton));

            services.AddTransient<IGateIoOrderBookFactory, GateIoOrderBookFactory>();
            services.AddTransient<IGateIoTrackerFactory, GateIoTrackerFactory>();
            services.AddTransient<ITrackerFactory, GateIoTrackerFactory>();
            services.AddSingleton<IGateIoUserClientProvider, GateIoUserClientProvider>(x =>
            new GateIoUserClientProvider(
                x.GetRequiredService<IHttpClientFactory>().CreateClient(typeof(IGateIoRestClient).Name),
                x.GetRequiredService<ILoggerFactory>(),
                x.GetRequiredService<IOptions<GateIoRestOptions>>(),
                x.GetRequiredService<IOptions<GateIoSocketOptions>>()));

            services.RegisterSharedRestInterfaces(x => x.GetRequiredService<IGateIoRestClient>().SpotApi.SharedClient);
            services.RegisterSharedSocketInterfaces(x => x.GetRequiredService<IGateIoSocketClient>().SpotApi.SharedClient);
            services.RegisterSharedRestInterfaces(x => x.GetRequiredService<IGateIoRestClient>().PerpetualFuturesApi.SharedClient);
            services.RegisterSharedSocketInterfaces(x => x.GetRequiredService<IGateIoSocketClient>().PerpetualFuturesApi.SharedClient);

            services.RegisterSharedApiClient<
                IGateIoSharedApiClient,
                GateIoSharedApiClient>(sharedApis => sharedApis
                    .Add(client => client.SpotRest)
                    .Add(client => client.SpotSocket)
                    .Add(client => client.PerpetualFuturesRest)
                    .Add(client => client.PerpetualFuturesSocket)
                    );

            return services;
        }
    }
}
