using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Position mode
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PositionMode>))]
    public enum PositionMode
    {
        /// <summary>
        /// ["<c>single</c>"] Single
        /// </summary>
        [Map("single")]
        Single,
        /// <summary>
        /// ["<c>dual_long</c>"] Dual long mode
        /// </summary>
        [Map("dual_long")]
        DualLong,
        /// <summary>
        /// ["<c>dual_short</c>"] Dual short mode
        /// </summary>
        [Map("dual_short")]
        DualShort
    }
}
