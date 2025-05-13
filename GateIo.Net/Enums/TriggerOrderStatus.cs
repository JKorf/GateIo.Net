using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Trigger order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TriggerOrderStatus>))]
    public enum TriggerOrderStatus
    {
        /// <summary>
        /// Active
        /// </summary>
        [Map("open")]
        Open,
        /// <summary>
        /// Canceled
        /// </summary>
        [Map("cancelled")]
        Canceled,
        /// <summary>
        /// Finished
        /// </summary>
        [Map("finish")]
        Finished,
        /// <summary>
        /// Failed to execute
        /// </summary>
        [Map("failed")]
        Failed,
        /// <summary>
        /// Trigger expired
        /// </summary>
        [Map("expired")]
        Expired
    }
}
