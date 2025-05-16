using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using GateIo.Net.Converters;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Order book
    /// </summary>
    [SerializationModel]
    public record GateIoOrderBook
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
        public GateIoOrderBookEntry[] Asks { get; set; } = Array.Empty<GateIoOrderBookEntry>();

        /// <summary>
        /// Bids list
        /// </summary>
        [JsonPropertyName("bids")]
        public GateIoOrderBookEntry[] Bids { get; set; } = Array.Empty<GateIoOrderBookEntry>();
    }

    /// <summary>
    /// Order book entry
    /// </summary>
    [JsonConverter(typeof(ArrayConverter<GateIoOrderBookEntry>))]
    [SerializationModel]
    public record GateIoOrderBookEntry : ISymbolOrderBookEntry
    {
        /// <summary>
        /// Price
        /// </summary>
        [ArrayProperty(0)]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [ArrayProperty(1)]
        public decimal Quantity { get; set; }
    }
}
