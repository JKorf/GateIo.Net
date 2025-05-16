using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Orders for a symbol
    /// </summary>
    [SerializationModel]
    public record GateIoSymbolOrders
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("currency_pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Total count
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }
        /// <summary>
        /// Orders
        /// </summary>
        [JsonPropertyName("orders")]
        public GateIoOrder[] Orders { get; set; } = Array.Empty<GateIoOrder>();
    }
}
