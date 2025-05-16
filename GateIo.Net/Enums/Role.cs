using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Role
    /// </summary>
    [JsonConverter(typeof(EnumConverter<Role>))]
    public enum Role
    {
        /// <summary>
        /// Taker
        /// </summary>
        [Map("taker")]
        Taker,
        /// <summary>
        /// Maker
        /// </summary>
        [Map("maker")]
        Maker
    }
}
