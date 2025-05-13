using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Rate limit type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<RateLimitType>))]
    public enum RateLimitType
    {
        /// <summary>
        /// Spot
        /// </summary>
        [Map("spot")]
        Spot,
        /// <summary>
        /// Futures
        /// </summary>
        [Map("futures")]
        Futures
    }
}
