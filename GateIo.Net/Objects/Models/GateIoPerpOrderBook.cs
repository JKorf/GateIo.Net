using CryptoExchange.Net.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Order book
    /// </summary>
    public record GateIoPerpOrderBook
    {
        /// <summary>
        /// Book sync id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }

        /// <summary>
        /// The timestamp the book was requested
        /// </summary>
        [JsonPropertyName("current")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// The timestamp the book was last updated
        /// </summary>
        [JsonPropertyName("update")]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// Asks list
        /// </summary>
        [JsonPropertyName("asks")]
        public IEnumerable<GateIoPerpOrderBookEntry> Asks { get; set; } = Array.Empty<GateIoPerpOrderBookEntry>();

        /// <summary>
        /// Bids list
        /// </summary>
        [JsonPropertyName("bids")]
        public IEnumerable<GateIoPerpOrderBookEntry> Bids { get; set; } = Array.Empty<GateIoPerpOrderBookEntry>();
    }

    /// <summary>
    /// Order book entry
    /// </summary>
    public record GateIoPerpOrderBookEntry : ISymbolOrderBookEntry
    {
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("s")]
        public decimal Quantity { get; set; }
    }
}
