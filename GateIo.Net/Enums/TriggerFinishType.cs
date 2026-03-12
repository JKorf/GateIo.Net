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
        /// ["<c>succeeded</c>"] Filled
        /// </summary>
        [Map("succeeded")]
        Succeeeded,
        /// <summary>
        /// ["<c>cancelled</c>"] Manually canceled
        /// </summary>
        [Map("cancelled")]
        Canceled,
        /// <summary>
        /// ["<c>failed</c>"] Failed
        /// </summary>
        [Map("failed")]
        Failed,
        /// <summary>
        /// ["<c>expired</c>"] Expired
        /// </summary>
        [Map("expired")]
        Expired
    }
}
