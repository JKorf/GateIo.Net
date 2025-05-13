using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Balance update
    /// </summary>
    [SerializationModel]
    public record GateIoPerpBalanceUpdate
    {
        /// <summary>
        /// Balance after change
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
        /// <summary>
        /// Changed quantity
        /// </summary>
        [JsonPropertyName("change")]
        public decimal Change { get; set; }
        /// <summary>
        /// Text
        /// </summary>
        [JsonPropertyName("text")]
        public string? Text { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("time_ms")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Change type
        /// </summary>
        [JsonPropertyName("type")]
        public string ChangeType { get; set; } = string.Empty;
        /// <summary>
        /// User id
        /// </summary>
        [JsonPropertyName("user")]
        public long UserId { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
    }
}
