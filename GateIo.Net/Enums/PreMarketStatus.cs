using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Pre-Market trading status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PreMarketStatus>))]
    public enum PreMarketStatus
    {
        /// <summary>
        /// Normal
        /// </summary>
        [Map("normal")]
        Normal,
        /// <summary>
        /// Pre-Market
        /// </summary>
        [Map("pre-market")]
        PreMarket
    }
}
