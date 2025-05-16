using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// How a trigger order was finished
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TriggerFinishType>))]
    public enum TriggerFinishType
    {
        /// <summary>
        /// Filled
        /// </summary>
        [Map("succeeded")]
        Succeeeded,
        /// <summary>
        /// Manually canceled
        /// </summary>
        [Map("cancelled")]
        Canceled,
        /// <summary>
        /// Failed
        /// </summary>
        [Map("failed")]
        Failed,
        /// <summary>
        /// Expired
        /// </summary>
        [Map("expired")]
        Expired
    }
}
