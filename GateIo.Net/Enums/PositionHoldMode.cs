using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Position mode
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PositionHoldMode>))]
    public enum PositionHoldMode
    {
        /// <summary>
        /// ["<c>single</c>"] Single Direction Position
        /// </summary>
        [Map("single")]
        Single,
        /// <summary>
        /// ["<c>dual</c>"] Dual Direction Position
        /// </summary>
        [Map("dual")]
        Dual,
        /// <summary>
        /// ["<c>dual_plus</c>"] Split Position
        /// </summary>
        [Map("dual_plus")]
        DualPlus
    }
}
