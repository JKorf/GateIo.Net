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
        /// ["<c>balance</c>"] Balance after change
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
        /// <summary>
        /// ["<c>change</c>"] Changed quantity
        /// </summary>
        [JsonPropertyName("change")]
        public decimal Change { get; set; }
        /// <summary>
        /// ["<c>text</c>"] Text
        /// </summary>
        [JsonPropertyName("text")]
        public string? Text { get; set; }
        /// <summary>
        /// ["<c>time_ms</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("time_ms")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Change type
        /// </summary>
        [JsonPropertyName("type")]
        public string ChangeType { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>user</c>"] User id
        /// </summary>
        [JsonPropertyName("user")]
        public long UserId { get; set; }
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
    }
}
