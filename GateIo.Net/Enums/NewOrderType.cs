using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Order type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<NewOrderType>))]
    public enum NewOrderType
    {
        /// <summary>
        /// LImit order
        /// </summary>
        [Map("limit")]
        Limit,
        /// <summary>
        /// Market order
        /// </summary>
        [Map("market")]
        Market
    }
}
