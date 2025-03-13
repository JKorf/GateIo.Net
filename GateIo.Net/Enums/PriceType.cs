using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Price type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PriceType>))]
    public enum PriceType
    {
        /// <summary>
        /// Last trade price
        /// </summary>
        [Map("0")]
        LastTradePrice,
        /// <summary>
        /// Mark price
        /// </summary>
        [Map("1")]
        MarkPrice,
        /// <summary>
        /// Index price
        /// </summary>
        [Map("2")]
        IndexPrice
    }
}
