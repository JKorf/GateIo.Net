using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Insurance
    /// </summary>
    [SerializationModel]
    public record GateIoPerpInsurance
    {
        /// <summary>
        /// ["<c>t</c>"] Time
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>b</c>"] Funding rate
        /// </summary>
        [JsonPropertyName("b")]
        public decimal Insurance { get; set; }
    }
}
