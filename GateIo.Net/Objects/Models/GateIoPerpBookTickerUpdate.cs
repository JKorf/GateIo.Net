using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Book ticker update
    /// </summary>
    [SerializationModel]
    public record GateIoPerpBookTickerUpdate
    {
        /// <summary>
        /// ["<c>t</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>u</c>"] Update id
        /// </summary>
        [JsonPropertyName("u")]
        public long UpdateId { get; set; }
        /// <summary>
        /// ["<c>s</c>"] Contract
        /// </summary>
        [JsonPropertyName("s")]
        public string Contract { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>b</c>"] Best bid price
        /// </summary>
        [JsonPropertyName("b")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>B</c>"] Best bid quantity
        /// </summary>
        [JsonPropertyName("B")]
        public int BestBidQuantity { get; set; }
        /// <summary>
        /// ["<c>a</c>"] Best ask price
        /// </summary>
        [JsonPropertyName("a")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// ["<c>A</c>"] Best ask quantity
        /// </summary>
        [JsonPropertyName("A")]
        public int BestAskQuantity { get; set; }
    }
}
