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
        /// Smart mode
        /// </summary>
        [Map("speed")]
        SmartMode,
        /// <summary>
        /// Custom
        /// </summary>
        [Map("custom")]
        Custom
    }
}
