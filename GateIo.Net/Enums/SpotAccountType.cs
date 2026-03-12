using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Spot account type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SpotAccountType>))]
    public enum SpotAccountType
    {
        /// <summary>
        /// ["<c>spot</c>"] Spot account
        /// </summary>
        [Map("spot")]
        Spot,
        /// <summary>
        /// ["<c>margin</c>"] Margin account
        /// </summary>
        [Map("margin")]
        Margin,
        /// <summary>
        /// ["<c>unified</c>"] Unified account
        /// </summary>
        [Map("unified")]
        Unified,
        /// <summary>
        /// ["<c>cross_margin</c>"] Cross margin
        /// </summary>
        [Map("cross_margin")]
        CrossMargin
    }
}
