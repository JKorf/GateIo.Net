using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Business type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<BusinessType>))]
    public enum BusinessType
    {
        /// <summary>
        /// Margin account
        /// </summary>
        [Map("margin")]
        Margin,
        /// <summary>
        /// Unified account
        /// </summary>
        [Map("unified")]
        Unified
    }
}
