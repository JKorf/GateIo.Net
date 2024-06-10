using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Order book update
    /// </summary>
    public record GateIoPerpOrderBookUpdate
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
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
        public IEnumerable<GateIoPerpOrderBookEntry> Bids { get; set; } = Array.Empty<GateIoPerpOrderBookEntry>();
        /// <summary>
        /// Updated asks
        /// </summary>
        [JsonPropertyName("a")]
        public IEnumerable<GateIoPerpOrderBookEntry> Asks { get; set; } = Array.Empty<GateIoPerpOrderBookEntry>();
    }
}
