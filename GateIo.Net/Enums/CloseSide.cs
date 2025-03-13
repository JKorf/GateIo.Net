using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Close side
    /// </summary>
    [JsonConverter(typeof(EnumConverter<CloseSide>))]
    public enum CloseSide
    {
        /// <summary>
        /// Close long
        /// </summary>
        [Map("close_long")]
        CloseLong,
        /// <summary>
        /// Close short
        /// </summary>
        [Map("close_short")]
        CloseShort
    }
}
