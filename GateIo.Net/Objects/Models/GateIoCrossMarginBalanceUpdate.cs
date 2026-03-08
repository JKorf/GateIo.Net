using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Cross Margin Balance update
    /// </summary>
    [SerializationModel]
    public record GateIoCrossMarginBalanceUpdate
    {
        /// <summary>
        /// ["<c>timestamp_ms</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("timestamp_ms")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>user</c>"] User id
        /// </summary>
        [JsonPropertyName("user")]
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>change</c>"] Change
        /// </summary>
        [JsonPropertyName("change")]
        public decimal Change { get; set; }
        /// <summary>
        /// ["<c>total</c>"] Total
        /// </summary>
        [JsonPropertyName("total")]
        public decimal Total { get; set; }
        /// <summary>
        /// ["<c>available</c>"] Available
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// ["<c>freeze</c>"] Frozen
        /// </summary>
        [JsonPropertyName("freeze")]
        public decimal Frozen { get; set; }
        /// <summary>
        /// ["<c>freeze_change</c>"] Change in frozen quantity
        /// </summary>
        [JsonPropertyName("freeze_change")]
        public decimal FrozenChange { get; set; }
        /// <summary>
        /// ["<c>change_type</c>"] Change type
        /// </summary>
        [JsonPropertyName("change_type")]
        public BalanceChangeType ChangeType { get; set; }
    }
}
