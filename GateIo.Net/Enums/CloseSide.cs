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
        /// ["<c>close_long</c>"] Close long
        /// </summary>
        [Map("close_long")]
        CloseLong,
        /// <summary>
        /// ["<c>close_short</c>"] Close short
        /// </summary>
        [Map("close_short")]
        CloseShort
    }
}
