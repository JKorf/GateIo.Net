using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarginMode>))]
    public enum MarginMode
    {
        /// <summary>
        /// ["<c>CROSS</c>"] Cross margin
        /// </summary>
        [Map("CROSS")]
        Cross,
        /// <summary>
        /// ["<c>ISOLATED</c>"] Isolated margin
        /// </summary>
        [Map("ISOLATED")]
        Isolated
    }
}
