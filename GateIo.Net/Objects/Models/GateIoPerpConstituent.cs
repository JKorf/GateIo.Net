using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Index constituents
    /// </summary>
    [SerializationModel]
    public record GateIoPerpConstituent
    {
        /// <summary>
        /// Index name
        /// </summary>
        [JsonPropertyName("index")]
        public string Index { get; set; } = string.Empty;
        /// <summary>
        /// Constituents
        /// </summary>
        [JsonPropertyName("constituents")]
        public GateIoPerpConstituentReference[] Constituents { get; set; } = Array.Empty<GateIoPerpConstituentReference>();
    }
    
    /// <summary>
    /// Reference
    /// </summary>
    [SerializationModel]
    public record GateIoPerpConstituentReference
    {
        /// <summary>
        /// Exchange
        /// </summary>
        [JsonPropertyName("exchange")]
        public string Exchange { get; set; } = string.Empty;
        /// <summary>
        /// Symbols
        /// </summary>
        [JsonPropertyName("symbols")]
        public string[] Symbols { get; set; } = Array.Empty<string>();
    }
}
