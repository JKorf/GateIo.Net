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
        /// Timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Whether this is a full snapshot
        /// </summary>
        [JsonPropertyName("full")]
        public bool Full { get; set; }
        /// <summary>
        /// Update id
        /// </summary>
        [JsonPropertyName("u")]
        public long LastUpdateId { get; set; }
        /// <summary>
        /// Update id
        /// </summary>
        [JsonPropertyName("U")]
        public long FirstUpdateId { get; set; }
        /// <summary>
        /// Contract
        /// </summary>
        [JsonPropertyName("s")]
        public string Contract { get; set; } = string.Empty;
        /// <summary>
        /// Event
        /// </summary>
        [JsonPropertyName("e")]
        public string Event { get; set; } = string.Empty;
        /// <summary>
        /// Updated bids
        /// </summary>
        [JsonPropertyName("b")]
        public GateIoPerpOrderBookEntry[] Bids { get; set; } = Array.Empty<GateIoPerpOrderBookEntry>();
        /// <summary>
        /// Updated asks
        /// </summary>
        [JsonPropertyName("a")]
        public GateIoPerpOrderBookEntry[] Asks { get; set; } = Array.Empty<GateIoPerpOrderBookEntry>();
    }
}
