using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Balance update
    /// </summary>
    [SerializationModel]
    public record GateIoBalanceUpdate
	{
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("timestamp_ms")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// User id
        /// </summary>
        [JsonPropertyName("user")]
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Change
        /// </summary>
        [JsonPropertyName("change")]
        public decimal Change { get; set; }
        /// <summary>
        /// Total
        /// </summary>
        [JsonPropertyName("total")]
        public decimal Total { get; set; }
        /// <summary>
        /// Available
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// Frozen
        /// </summary>
        [JsonPropertyName("freeze")]
        public decimal Frozen { get; set; }
        /// <summary>
        /// Change in frozen quantity
        /// </summary>
        [JsonPropertyName("freeze_change")]
        public decimal FrozenChange { get; set; }
        /// <summary>
        /// Change type
        /// </summary>
        [JsonPropertyName("change_type")]
        public BalanceChangeType ChangeType { get; set; }
    }
}
