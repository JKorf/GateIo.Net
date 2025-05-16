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
        /// Spot account
        /// </summary>
        [Map("spot")]
        Spot,
        /// <summary>
        /// Margin account
        /// </summary>
        [Map("margin")]
        Margin,
        /// <summary>
        /// Unified account
        /// </summary>
        [Map("unified")]
        Unified,
        /// <summary>
        /// Cross margin
        /// </summary>
        [Map("cross_margin")]
        CrossMargin
    }
}
