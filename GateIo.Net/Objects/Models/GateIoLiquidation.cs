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
        /// ["<c>time</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>contract</c>"] Contract
        /// </summary>
        [JsonPropertyName("contract")]
        public string Contract { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>size</c>"] Quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>order_size</c>"] Number of forced liquidation orders
        /// </summary>
        [JsonPropertyName("order_size")]
        public decimal NumberOfOrders { get; set; }
        /// <summary>
        /// ["<c>order_price</c>"] Order price
        /// </summary>
        [JsonPropertyName("order_price")]
        public decimal OrderPrice { get; set; }
        /// <summary>
        /// ["<c>fill_price</c>"] Fill price
        /// </summary>
        [JsonPropertyName("fill_price")]
        public decimal FillPrice { get; set; }
        /// <summary>
        /// ["<c>left</c>"] Left
        /// </summary>
        [JsonPropertyName("left")]
        public decimal Left { get; set; }
    }
}
