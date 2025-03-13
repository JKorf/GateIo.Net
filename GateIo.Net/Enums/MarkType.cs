using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Mark type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarkType>))]
    public enum MarkType
    {
        /// <summary>
        /// Internal
        /// </summary>
        [Map("internal")]
        Internal,
        /// <summary>
        /// Index
        /// </summary>
        [Map("index")]
        Index
    }
}
