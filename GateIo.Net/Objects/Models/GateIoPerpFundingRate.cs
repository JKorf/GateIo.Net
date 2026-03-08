using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Funding rate
    /// </summary>
    [SerializationModel]
    public record GateIoPerpFundingRate
    {
        /// <summary>
        /// ["<c>t</c>"] Time
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>r</c>"] Funding rate
        /// </summary>
        [JsonPropertyName("r")]
        public decimal FundingRate { get; set; }
    }
}
