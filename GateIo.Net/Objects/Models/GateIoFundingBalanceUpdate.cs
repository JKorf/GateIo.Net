using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Funding Balance update
    /// </summary>
    [SerializationModel]
    public record GateIoFundingBalanceUpdate
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
        /// ["<c>freeze</c>"] Frozen
        /// </summary>
        [JsonPropertyName("freeze")]
        public decimal Frozen { get; set; }
        /// <summary>
        /// ["<c>lent</c>"] Lent
        /// </summary>
        [JsonPropertyName("lent")]
        public decimal Lent { get; set; }
    }
}
