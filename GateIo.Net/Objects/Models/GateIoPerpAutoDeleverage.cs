using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Auto deleverage
    /// </summary>
    [SerializationModel]
    public record GateIoPerpAutoDeleverage
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
        /// Entry price
        /// </summary>
        [JsonPropertyName("entry_price")]
        public decimal EntryPrice { get; set; }
        /// <summary>
        /// Order price
        /// </summary>
        [JsonPropertyName("fill_price")]
        public decimal FillPrice { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public long OrderId { get; set; }
        /// <summary>
        /// User id
        /// </summary>
        [JsonPropertyName("user")]
        public long UserId { get; set; }
        /// <summary>
        /// Cross leverage limit
        /// </summary>
        [JsonPropertyName("cross_leverage_limit")]
        public decimal CrossLeverageLimit { get; set; }
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// Position quantity
        /// </summary>
        [JsonPropertyName("position_size")]
        public int PositionQuantity { get; set; }
        /// <summary>
        /// Trade quantity
        /// </summary>
        [JsonPropertyName("trade_size")]
        public int TradeQuantity { get; set; }
    }
}
