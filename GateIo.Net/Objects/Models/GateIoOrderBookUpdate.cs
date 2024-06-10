using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Order book update
    /// </summary>
    public record GateIoOrderBookUpdate
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
        /// Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Event
        /// </summary>
        [JsonPropertyName("e")]
        public string Event { get; set; } = string.Empty;
        /// <summary>
        /// Updated bids
        /// </summary>
        [JsonPropertyName("b")]
        public IEnumerable<GateIoOrderBookEntry> Bids { get; set; } = Array.Empty<GateIoOrderBookEntry>();
        /// <summary>
        /// Updated asks
        /// </summary>
        [JsonPropertyName("a")]
        public IEnumerable<GateIoOrderBookEntry> Asks { get; set; } = Array.Empty<GateIoOrderBookEntry>();
    }
}
