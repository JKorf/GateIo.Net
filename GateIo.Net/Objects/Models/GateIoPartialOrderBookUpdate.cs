using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Order book update
    /// </summary>
    [SerializationModel]
    public record GateIoPartialOrderBookUpdate
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Update id
        /// </summary>
        [JsonPropertyName("lastUpdateId")]
        public long LastUpdateId { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Updated bids
        /// </summary>
        [JsonPropertyName("bids")]
        public GateIoOrderBookEntry[] Bids { get; set; } = Array.Empty<GateIoOrderBookEntry>();
        /// <summary>
        /// Updated asks
        /// </summary>
        [JsonPropertyName("asks")]
        public GateIoOrderBookEntry[] Asks { get; set; } = Array.Empty<GateIoOrderBookEntry>();
    }
}
