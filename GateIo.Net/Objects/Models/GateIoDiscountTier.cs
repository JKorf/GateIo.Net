using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Discount tier
    /// </summary>
    [SerializationModel]
    public record GateIoDiscountTier
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// Tiers
        /// </summary>
        [JsonPropertyName("discount_tiers")]
        public GateIoDiscountTierEntry[] Tiers { get; set; } = Array.Empty<GateIoDiscountTierEntry>();
    }

    /// <summary>
    /// Discount tier item
    /// </summary>
    [SerializationModel]
    public record GateIoDiscountTierEntry
    {
        /// <summary>
        /// Tier
        /// </summary>
        [JsonPropertyName("tier")]
        public string Tier { get; set; } = string.Empty;
        /// <summary>
        /// Discount. 1 means full price
        /// </summary>
        [JsonPropertyName("discount")]
        public decimal Discount { get; set; }
        /// <summary>
        /// Lower volume limit
        /// </summary>
        [JsonPropertyName("lower_limit")]
        public decimal LowerLimit { get; set; }
        /// <summary>
        /// Upper volume limit. Note that this is a string as '+' is returned for the most upper tier
        /// </summary>
        [JsonPropertyName("upper_limit")]
        public string UpperLimit { get; set; } = string.Empty;
        /// <summary>
        /// Position leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }

    }
}
