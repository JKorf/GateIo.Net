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
        /// Single
        /// </summary>
        [Map("single")]
        Single,
        /// <summary>
        /// Dual long mode
        /// </summary>
        [Map("dual_long")]
        DualLong,
        /// <summary>
        /// Dual short mode
        /// </summary>
        [Map("dual_short")]
        DualShort
    }
}
