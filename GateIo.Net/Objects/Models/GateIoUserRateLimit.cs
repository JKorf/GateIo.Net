using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net.Enums;
using System;
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
        /// ["<c>type</c>"] Type
        /// </summary>
        [JsonPropertyName("type")]
        public RateLimitType RateLimitType { get; set; }
        /// <summary>
        /// ["<c>tier</c>"] Tier
        /// </summary>
        [JsonPropertyName("tier")]
        public string Tier { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>ratio</c>"] Ratio
        /// </summary>
        [JsonPropertyName("ratio")]
        public decimal Ratio { get; set; }
        /// <summary>
        /// ["<c>main_ratio</c>"] Main ratio
        /// </summary>
        [JsonPropertyName("main_ratio")]
        public decimal MainRatio { get; set; }
        /// <summary>
        /// ["<c>updated_at</c>"] Update time
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }


}
