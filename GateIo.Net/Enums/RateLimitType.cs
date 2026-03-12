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
        /// ["<c>spot</c>"] Spot
        /// </summary>
        [Map("spot")]
        Spot,
        /// <summary>
        /// ["<c>futures</c>"] Futures
        /// </summary>
        [Map("futures")]
        Futures
    }
}
