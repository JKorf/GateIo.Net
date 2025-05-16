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
        /// Time
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Funding rate
        /// </summary>
        [JsonPropertyName("b")]
        public decimal Insurance { get; set; }
    }
}
