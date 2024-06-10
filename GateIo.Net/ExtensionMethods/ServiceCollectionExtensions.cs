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

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for DI
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the IGateIoClient and IGateIoSocketClient to the sevice collection so they can be injected
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="defaultRestOptionsDelegate">Set default options for the rest client</param>
        /// <param name="defaultSocketOptionsDelegate">Set default options for the socket client</param>
        /// <param name="socketClientLifeTime">The lifetime of the IGateIoSocketClient for the service collection. Defaults to Singleton.</param>
        /// <returns></returns>
        public static IServiceCollection AddGateIo(
            this IServiceCollection services,
            Action<GateIoRestOptions>? defaultRestOptionsDelegate = null,
            Action<GateIoSocketOptions>? defaultSocketOptionsDelegate = null,
            ServiceLifetime? socketClientLifeTime = null)
        {
            var restOptions = GateIoRestOptions.Default.Copy();

            if (defaultRestOptionsDelegate != null)
            {
                defaultRestOptionsDelegate(restOptions);
                GateIoRestClient.SetDefaultOptions(defaultRestOptionsDelegate);
            }

            if (defaultSocketOptionsDelegate != null)
                GateIoSocketClient.SetDefaultOptions(defaultSocketOptionsDelegate);

            services.AddHttpClient<IGateIoRestClient, GateIoRestClient>(options =>
            {
                options.Timeout = restOptions.RequestTimeout;
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new HttpClientHandler();
                if (restOptions.Proxy != null)
                {
                    handler.Proxy = new WebProxy
                    {
                        Address = new Uri($"{restOptions.Proxy.Host}:{restOptions.Proxy.Port}"),
                        Credentials = restOptions.Proxy.Password == null ? null : new NetworkCredential(restOptions.Proxy.Login, restOptions.Proxy.Password)
                    };
                }
                return handler;
            });

            services.AddTransient<ICryptoRestClient, CryptoRestClient>();
            services.AddSingleton<ICryptoSocketClient, CryptoSocketClient>();
            services.AddSingleton<IGateIoOrderBookFactory, GateIoOrderBookFactory>();
            services.AddTransient(x => x.GetRequiredService<IGateIoRestClient>().SpotApi.CommonSpotClient);
            if (socketClientLifeTime == null)
                services.AddSingleton<IGateIoSocketClient, GateIoSocketClient>();
            else
                services.Add(new ServiceDescriptor(typeof(IGateIoSocketClient), typeof(GateIoSocketClient), socketClientLifeTime.Value));
            return services;
        }
    }
}
