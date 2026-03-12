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
        /// ["<c>ACK</c>"] Acknowledge, return only the most basic order info
        /// </summary>
        [Map("ACK")]
        Acknowledge,
        /// <summary>
        /// ["<c>RESULT</c>"] Result, return all but clearing info
        /// </summary>
        [Map("RESULT")]
        Result,
        /// <summary>
        /// ["<c>FULL</c>"] Full order result
        /// </summary>
        [Map("FULL")]
        Full
    }
}
