using CryptoExchange.Net.Converters.SystemTextJson;
using System;
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
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>margin_tiers</c>"] Tiers
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
        /// ["<c>tier</c>"] Tier
        /// </summary>
        [JsonPropertyName("tier")]
        public string Tier { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>margin_rate</c>"] Margin rate
        /// </summary>
        [JsonPropertyName("margin_rate")]
        public decimal MarginRate { get; set; }
        /// <summary>
        /// ["<c>lower_limit</c>"] Lower volume limit
        /// </summary>
        [JsonPropertyName("lower_limit")]
        public decimal LowerLimit { get; set; }
        /// <summary>
        /// ["<c>upper_limit</c>"] Upper volume limit
        /// </summary>
        [JsonPropertyName("upper_limit")]
        public decimal? UpperLimit { get; set; }
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
    }
}
