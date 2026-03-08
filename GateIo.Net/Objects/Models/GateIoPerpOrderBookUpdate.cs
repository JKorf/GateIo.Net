using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Order book update
    /// </summary>
    [SerializationModel]
    public record GateIoPerpOrderBookUpdate
    {
        /// <summary>
        /// ["<c>t</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>full</c>"] Whether this is a full snapshot
        /// </summary>
        [JsonPropertyName("full")]
        public bool Full { get; set; }
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
        /// ["<c>s</c>"] Contract
        /// </summary>
        [JsonPropertyName("s")]
        public string Contract { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>e</c>"] Event
        /// </summary>
        [JsonPropertyName("e")]
        public string Event { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>b</c>"] Updated bids
        /// </summary>
        [JsonPropertyName("b")]
        public GateIoPerpOrderBookEntry[] Bids { get; set; } = Array.Empty<GateIoPerpOrderBookEntry>();
        /// <summary>
        /// ["<c>a</c>"] Updated asks
        /// </summary>
        [JsonPropertyName("a")]
        public GateIoPerpOrderBookEntry[] Asks { get; set; } = Array.Empty<GateIoPerpOrderBookEntry>();
    }
}
