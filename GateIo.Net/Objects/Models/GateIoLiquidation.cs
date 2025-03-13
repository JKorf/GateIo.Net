using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Liquidation
    /// </summary>
    [SerializationModel]
    public record GateIoLiquidation
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Contract
        /// </summary>
        [JsonPropertyName("contract")]
        public string Contract { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Number of forced liquidation orders
        /// </summary>
        [JsonPropertyName("order_size")]
        public decimal NumberOfOrders { get; set; }
        /// <summary>
        /// Order price
        /// </summary>
        [JsonPropertyName("order_price")]
        public decimal OrderPrice { get; set; }
        /// <summary>
        /// Fill price
        /// </summary>
        [JsonPropertyName("fill_price")]
        public decimal FillPrice { get; set; }
        /// <summary>
        /// Left
        /// </summary>
        [JsonPropertyName("left")]
        public decimal Left { get; set; }
    }
}
