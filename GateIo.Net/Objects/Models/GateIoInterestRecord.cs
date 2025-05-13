using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Interest record
    /// </summary>
    [SerializationModel]
    public record GateIoInterestRecord
    {
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public bool Success { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("currency_pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Actual interest rate
        /// </summary>
        [JsonPropertyName("actual_rate")]
        public decimal ActualRate { get; set; }
        /// <summary>
        /// Interest
        /// </summary>
        [JsonPropertyName("interest")]
        public decimal Interest { get; set; }
        /// <summary>
        /// Loan type
        /// </summary>
        [JsonPropertyName("type")]
        public LoanType Type { get; set; }
        /// <summary>
        /// Create time
        /// </summary>
        [JsonPropertyName("create_time")]
        public DateTime Timestamp { get; set; }
    }
}
