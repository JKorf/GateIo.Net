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
        /// ["<c>0</c>"] Last trade price
        /// </summary>
        [Map("0")]
        LastTradePrice,
        /// <summary>
        /// ["<c>1</c>"] Mark price
        /// </summary>
        [Map("1")]
        MarkPrice,
        /// <summary>
        /// ["<c>2</c>"] Index price
        /// </summary>
        [Map("2")]
        IndexPrice
    }
}
