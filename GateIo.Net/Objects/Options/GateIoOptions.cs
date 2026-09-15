using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects.Options;
using Microsoft.Extensions.Configuration;
using System;
using CryptoExchange.Net.SharedApis;

namespace GateIo.Net.Objects.Options
{
    /// <summary>
    /// GateIo options
    /// </summary>
    public class GateIoOptions : LibraryOptions<GateIoRestOptions, GateIoSocketOptions, GateIoCredentials, GateIoEnvironment>
    {
        /// <summary>
        /// Options for Shared API usage
        /// </summary>
        public SharedApiOptions SharedApi { get; set; } = new();
        /// <summary>
        /// Create GateIoOptions instance using the provided configuration action
        /// </summary>
        public static GateIoOptions Create(Action<GateIoOptions>? configure = null)
        {
            var options = CreateUnconfigured();
            configure?.Invoke(options);
            return Normalize(options);
        }

        /// <summary>
        /// Create GateIoOptions using the provided IConfiguration
        /// </summary>
        public static GateIoOptions CreateFromConfiguration(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            var options = CreateUnconfigured();
            try
            {
                configuration.Bind(options);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Invalid GateIo configuration provided", ex);
            }

            if (options.Environment != null)
                options.Environment = GateIoEnvironment.GetEnvironmentByName(options.Environment.Name) ?? options.Environment;
            if (options.Rest?.Environment != null)
                options.Rest.Environment = GateIoEnvironment.GetEnvironmentByName(options.Rest.Environment.Name) ?? options.Rest.Environment;
            if (options.Socket?.Environment != null)
                options.Socket.Environment = GateIoEnvironment.GetEnvironmentByName(options.Socket.Environment.Name) ?? options.Socket.Environment;

            return Normalize(options);
        }

        private static GateIoOptions CreateUnconfigured()
        {
            var options = new GateIoOptions();
            options.Rest.Environment = null!;
            options.Socket.Environment = null!;
            return options;
        }

        private static GateIoOptions Normalize(GateIoOptions options)
        {
            if (options.Rest == null)
                throw new ArgumentException("REST options cannot be null", nameof(options));
            if (options.Socket == null)
                throw new ArgumentException("Socket options cannot be null", nameof(options));

            options.Rest.Environment ??= options.Environment ?? GateIoEnvironment.Live;
            options.Rest.ApiCredentials ??= options.ApiCredentials;
            options.Socket.Environment ??= options.Environment ?? GateIoEnvironment.Live;
            options.Socket.ApiCredentials ??= options.ApiCredentials;
            return options;
        }
    }
}
