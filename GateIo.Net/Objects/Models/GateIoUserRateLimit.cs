using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// User rate limits
    /// </summary>
    [SerializationModel]
    public record GateIoUserRateLimit
    {
        /// <summary>
        /// Type
        /// </summary>
        [JsonPropertyName("type")]
        public RateLimitType RateLimitType { get; set; }
        /// <summary>
        /// Tier
        /// </summary>
        [JsonPropertyName("tier")]
        public string Tier { get; set; } = string.Empty;
        /// <summary>
        /// Ratio
        /// </summary>
        [JsonPropertyName("ratio")]
        public decimal Ratio { get; set; }
        /// <summary>
        /// Main ratio
        /// </summary>
        [JsonPropertyName("main_ratio")]
        public decimal MainRatio { get; set; }
        /// <summary>
        /// Update time
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }


}
