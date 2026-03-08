using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Order book
    /// </summary>
    [SerializationModel]
    public record GateIoPerpOrderBook
    {
        /// <summary>
        /// ["<c>id</c>"] Book sync id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }

        /// <summary>
        /// ["<c>current</c>"] The timestamp the book was requested
        /// </summary>
        [JsonPropertyName("current")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// ["<c>update</c>"] The timestamp the book was last updated
        /// </summary>
        [JsonPropertyName("update")]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// ["<c>asks</c>"] Asks list
        /// </summary>
        [JsonPropertyName("asks")]
        public GateIoPerpOrderBookEntry[] Asks { get; set; } = Array.Empty<GateIoPerpOrderBookEntry>();

        /// <summary>
        /// ["<c>bids</c>"] Bids list
        /// </summary>
        [JsonPropertyName("bids")]
        public GateIoPerpOrderBookEntry[] Bids { get; set; } = Array.Empty<GateIoPerpOrderBookEntry>();
    }

    /// <summary>
    /// Order book entry
    /// </summary>
    [SerializationModel]
    public record GateIoPerpOrderBookEntry : ISymbolOrderBookEntry
    {
        /// <summary>
        /// ["<c>p</c>"] Price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>s</c>"] Quantity
        /// </summary>
        [JsonPropertyName("s")]
        public decimal Quantity { get; set; }
    }
}
