using CryptoExchange.Net.Converters.SystemTextJson;
using System;
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
        /// ["<c>t</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>lastUpdateId</c>"] Update id
        /// </summary>
        [JsonPropertyName("lastUpdateId")]
        public long LastUpdateId { get; set; }
        /// <summary>
        /// ["<c>s</c>"] Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>bids</c>"] Updated bids
        /// </summary>
        [JsonPropertyName("bids")]
        public GateIoOrderBookEntry[] Bids { get; set; } = Array.Empty<GateIoOrderBookEntry>();
        /// <summary>
        /// ["<c>asks</c>"] Updated asks
        /// </summary>
        [JsonPropertyName("asks")]
        public GateIoOrderBookEntry[] Asks { get; set; } = Array.Empty<GateIoOrderBookEntry>();
    }
}
