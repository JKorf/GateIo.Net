using CryptoExchange.Net.Converters.SystemTextJson;
using System;
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
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>discount_tiers</c>"] Tiers
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
        /// ["<c>tier</c>"] Tier
        /// </summary>
        [JsonPropertyName("tier")]
        public string Tier { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>discount</c>"] Discount. 1 means full price
        /// </summary>
        [JsonPropertyName("discount")]
        public decimal Discount { get; set; }
        /// <summary>
        /// ["<c>lower_limit</c>"] Lower volume limit
        /// </summary>
        [JsonPropertyName("lower_limit")]
        public decimal LowerLimit { get; set; }
        /// <summary>
        /// ["<c>upper_limit</c>"] Upper volume limit. Note that this is a string as '+' is returned for the most upper tier
        /// </summary>
        [JsonPropertyName("upper_limit")]
        public string UpperLimit { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>leverage</c>"] Position leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }

    }
}
