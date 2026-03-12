using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Gas mode
    /// </summary>
    [JsonConverter(typeof(EnumConverter<GasMode>))]
    public enum GasMode
    {
        /// <summary>
        /// ["<c>speed</c>"] Smart mode
        /// </summary>
        [Map("speed")]
        SmartMode,
        /// <summary>
        /// ["<c>custom</c>"] Custom
        /// </summary>
        [Map("custom")]
        Custom
    }
}
