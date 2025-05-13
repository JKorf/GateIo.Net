using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// User liquidation info
    /// </summary>
    [SerializationModel]
    public record GateIoPerpLiquidation
    {
        /// <summary>
        /// User id
        /// </summary>
        [JsonPropertyName("user")]
        public long? UserId { get; set; }
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
        public int Quantity { get; set; }
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// Margin
        /// </summary>
        [JsonPropertyName("margin")]
        public decimal Margin { get; set; }
        /// <summary>
        /// Entry price
        /// </summary>
        [JsonPropertyName("entry_price")]
        public decimal EntryPrice { get; set; }
        /// <summary>
        /// Liquidation price
        /// </summary>
        [JsonPropertyName("liq_price")]
        public decimal LiquidationPrice { get; set; }
        /// <summary>
        /// Mark price
        /// </summary>
        [JsonPropertyName("mark_price")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public long OrderId { get; set; }
        /// <summary>
        /// Order price
        /// </summary>
        [JsonPropertyName("order_price")]
        public decimal OrderPrice { get; set; }
        /// <summary>
        /// Order price
        /// </summary>
        [JsonPropertyName("fill_price")]
        public decimal FillPrice { get; set; }
        /// <summary>
        /// Left
        /// </summary>
        [JsonPropertyName("left")]
        public decimal QuantityRemaining { get; set; }
    }
}
