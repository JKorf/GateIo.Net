using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Margin tier
    /// </summary>
    [SerializationModel]
    public record GateIoLoanMarginTier
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Tiers
        /// </summary>
        [JsonPropertyName("margin_tiers")]
        public GateIoLoanMarginTierEntry[] MarginTiers { get; set; } = Array.Empty<GateIoLoanMarginTierEntry>();
    }
    
    /// <summary>
    /// Margin tier entry
    /// </summary>
    [SerializationModel]
    public record GateIoLoanMarginTierEntry
    {
        /// <summary>
        /// Tier
        /// </summary>
        [JsonPropertyName("tier")]
        public string Tier { get; set; } = string.Empty;
        /// <summary>
        /// Margin rate
        /// </summary>
        [JsonPropertyName("margin_rate")]
        public decimal MarginRate { get; set; }
        /// <summary>
        /// Lower volume limit
        /// </summary>
        [JsonPropertyName("lower_limit")]
        public decimal LowerLimit { get; set; }
        /// <summary>
        /// Upper volume limit
        /// </summary>
        [JsonPropertyName("upper_limit")]
        public decimal? UpperLimit { get; set; }
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
    }
}
