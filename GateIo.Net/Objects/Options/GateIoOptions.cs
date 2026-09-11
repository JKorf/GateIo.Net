using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects.Options;
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
    }
}
