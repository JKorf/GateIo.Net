using CryptoExchange.Net.Converters.SystemTextJson;
using System;
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
        /// ["<c>currency_pair</c>"] Symbol
        /// </summary>
        [JsonPropertyName("currency_pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>total</c>"] Total count
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }
        /// <summary>
        /// ["<c>orders</c>"] Orders
        /// </summary>
        [JsonPropertyName("orders")]
        public GateIoOrder[] Orders { get; set; } = Array.Empty<GateIoOrder>();
    }
}
