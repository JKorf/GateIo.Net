using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Order book update
    /// </summary>
    [SerializationModel]
    public record GateIoPerpOrderBookV2Update
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
        /// ["<c>s</c>"] Topic
        /// </summary>
        [JsonPropertyName("s")]
        public string Topic { get; set; } = string.Empty;
        /// <summary>
        /// The contract
        /// </summary>
        public string Contract => Topic.Split('.')[1];

        /// <summary>
        /// The depth
        /// </summary>
        public string Depth => Topic.Split('.')[2];
        /// <summary>
        /// ["<c>b</c>"] Updated bids
        /// </summary>
        [JsonPropertyName("b")]
        public GateIoPerpOrderBookV2Entry[] Bids { get; set; } = Array.Empty<GateIoPerpOrderBookV2Entry>();
        /// <summary>
        /// ["<c>a</c>"] Updated asks
        /// </summary>
        [JsonPropertyName("a")]
        public GateIoPerpOrderBookV2Entry[] Asks { get; set; } = Array.Empty<GateIoPerpOrderBookV2Entry>();
    }

    /// <summary>
    /// Order book entry
    /// </summary>
    [SerializationModel]
    [JsonConverter(typeof(ArrayConverter<GateIoPerpOrderBookV2Entry>))]
    public record GateIoPerpOrderBookV2Entry : ISymbolOrderBookEntry
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
