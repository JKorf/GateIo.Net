using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Order book update
    /// </summary>
    [SerializationModel]
    public record GateIoOrderBookUpdate
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
        public long LastUpdateId { get; set; }
        /// <summary>
        /// ["<c>U</c>"] Update id
        /// </summary>
        [JsonPropertyName("U")]
        public long FirstUpdateId { get; set; }
        /// <summary>
        /// ["<c>s</c>"] Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>e</c>"] Event
        /// </summary>
        [JsonPropertyName("e")]
        public string Event { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>b</c>"] Updated bids
        /// </summary>
        [JsonPropertyName("b")]
        public GateIoOrderBookEntry[] Bids { get; set; } = Array.Empty<GateIoOrderBookEntry>();
        /// <summary>
        /// ["<c>a</c>"] Updated asks
        /// </summary>
        [JsonPropertyName("a")]
        public GateIoOrderBookEntry[] Asks { get; set; } = Array.Empty<GateIoOrderBookEntry>();
    }
}
