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
        /// ["<c>entry_price</c>"] Entry price
        /// </summary>
        [JsonPropertyName("entry_price")]
        public decimal EntryPrice { get; set; }
        /// <summary>
        /// ["<c>fill_price</c>"] Order price
        /// </summary>
        [JsonPropertyName("fill_price")]
        public decimal FillPrice { get; set; }
        /// <summary>
        /// ["<c>order_id</c>"] Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>user</c>"] User id
        /// </summary>
        [JsonPropertyName("user")]
        public long UserId { get; set; }
        /// <summary>
        /// ["<c>cross_leverage_limit</c>"] Cross leverage limit
        /// </summary>
        [JsonPropertyName("cross_leverage_limit")]
        public decimal CrossLeverageLimit { get; set; }
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// ["<c>position_size</c>"] Position quantity
        /// </summary>
        [JsonPropertyName("position_size")]
        public int PositionQuantity { get; set; }
        /// <summary>
        /// ["<c>trade_size</c>"] Trade quantity
        /// </summary>
        [JsonPropertyName("trade_size")]
        public int TradeQuantity { get; set; }
    }
}
