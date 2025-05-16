using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Order action mode
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderActionMode>))]
    public enum OrderActionMode
    {
        /// <summary>
        /// Acknowledge, return only the most basic order info
        /// </summary>
        [Map("ACK")]
        Acknowledge,
        /// <summary>
        /// Result, return all but clearing info
        /// </summary>
        [Map("RESULT")]
        Result,
        /// <summary>
        /// Full order result
        /// </summary>
        [Map("FULL")]
        Full
    }
}
