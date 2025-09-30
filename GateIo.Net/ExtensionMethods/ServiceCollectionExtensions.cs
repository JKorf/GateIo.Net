using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Interfaces;
using System;
using System.Net;
using System.Net.Http;
using GateIo.Net.Clients;
using GateIo.Net.Interfaces;
using GateIo.Net.Interfaces.Clients;
using GateIo.Net.Objects.Options;
using GateIo.Net.SymbolOrderBooks;
using CryptoExchange.Net;
using GateIo.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for DI
    /// </summary>
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// Add services such as the IGateIoRestClient and IGateIoSocketClient. Configures the services based on the provided configuration.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="configuration">The configuration(section) containing the options</param>
        /// <returns></returns>
        public static IServiceCollection AddGateIo(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = new GateIoOptions();
            // Reset environment so we know if they're overridden
            options.Rest.Environment = null!;
            options.Socket.Environment = null!;
            configuration.Bind(options);

            if (options.Rest == null || options.Socket == null)
                throw new ArgumentException("Options null");

            var restEnvName = options.Rest.Environment?.Name ?? options.Environment?.Name ?? GateIoEnvironment.Live.Name;
            var socketEnvName = options.Socket.Environment?.Name ?? options.Environment?.Name ?? GateIoEnvironment.Live.Name;
            options.Rest.Environment = GateIoEnvironment.GetEnvironmentByName(restEnvName) ?? options.Rest.Environment!;
            options.Rest.ApiCredentials = options.Rest.ApiCredentials ?? options.ApiCredentials;
            options.Socket.Environment = GateIoEnvironment.GetEnvironmentByName(socketEnvName) ?? options.Socket.Environment!;
            options.Socket.ApiCredentials = options.Socket.ApiCredentials ?? options.ApiCredentials;


            services.AddSingleton(x => Options.Options.Create(options.Rest));
            services.AddSingleton(x => Options.Options.Create(options.Socket));

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
            var options = new GateIoOptions();
            // Reset environment so we know if they're overridden
            options.Rest.Environment = null!;
            options.Socket.Environment = null!;
            optionsDelegate?.Invoke(options);
            if (options.Rest == null || options.Socket == null)
                throw new ArgumentException("Options null");

            options.Rest.Environment = options.Rest.Environment ?? options.Environment ?? GateIoEnvironment.Live;
            options.Rest.ApiCredentials = options.Rest.ApiCredentials ?? options.ApiCredentials;
            options.Socket.Environment = options.Socket.Environment ?? options.Environment ?? GateIoEnvironment.Live;
            options.Socket.ApiCredentials = options.Socket.ApiCredentials ?? options.ApiCredentials;

            services.AddSingleton(x => Options.Options.Create(options.Rest));
            services.AddSingleton(x => Options.Options.Create(options.Socket));

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
                return LibraryHelpers.CreateHttpClientMessageHandler(options.Proxy, options.HttpKeepAliveInterval);
            });
            services.Add(new ServiceDescriptor(typeof(IGateIoSocketClient), x => { return new GateIoSocketClient(x.GetRequiredService<IOptions<GateIoSocketOptions>>(), x.GetRequiredService<ILoggerFactory>()); }, socketClientLifeTime ?? ServiceLifetime.Singleton));

            services.AddTransient<ICryptoRestClient, CryptoRestClient>();
            services.AddSingleton<ICryptoSocketClient, CryptoSocketClient>();
            services.AddTransient<IGateIoOrderBookFactory, GateIoOrderBookFactory>();
            services.AddTransient<IGateIoTrackerFactory, GateIoTrackerFactory>();
            services.AddTransient<ITrackerFactory, GateIoTrackerFactory>();
            services.AddSingleton<IGateIoUserClientProvider, GateIoUserClientProvider>(x =>
            new GateIoUserClientProvider(
                x.GetRequiredService<HttpClient>(),
                x.GetRequiredService<ILoggerFactory>(),
                x.GetRequiredService<IOptions<GateIoRestOptions>>(),
                x.GetRequiredService<IOptions<GateIoSocketOptions>>()));

            services.RegisterSharedRestInterfaces(x => x.GetRequiredService<IGateIoRestClient>().SpotApi.SharedClient);
            services.RegisterSharedSocketInterfaces(x => x.GetRequiredService<IGateIoSocketClient>().SpotApi.SharedClient);
            services.RegisterSharedRestInterfaces(x => x.GetRequiredService<IGateIoRestClient>().PerpetualFuturesApi.SharedClient);
            services.RegisterSharedSocketInterfaces(x => x.GetRequiredService<IGateIoSocketClient>().PerpetualFuturesApi.SharedClient);

            return services;
        }
    }
}
