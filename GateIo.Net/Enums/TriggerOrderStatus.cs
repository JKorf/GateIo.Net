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
        /// ["<c>open</c>"] Active
        /// </summary>
        [Map("open")]
        Open,
        /// <summary>
        /// ["<c>cancelled</c>"] Canceled
        /// </summary>
        [Map("cancelled")]
        Canceled,
        /// <summary>
        /// ["<c>finish</c>"] Finished
        /// </summary>
        [Map("finish")]
        Finished,
        /// <summary>
        /// ["<c>failed</c>"] Failed to execute
        /// </summary>
        [Map("failed")]
        Failed,
        /// <summary>
        /// ["<c>expired</c>"] Trigger expired
        /// </summary>
        [Map("expired")]
        Expired
    }
}
