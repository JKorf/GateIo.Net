using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;
using GateIo.Net.Enums;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// User trade updates
    /// </summary>
    [SerializationModel]
    public record GateIoUserTradeUpdate
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Trade id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// Market unique id
        /// </summary>
        [JsonPropertyName("id_market")]
        public long MarketId { get; set; }
        /// <summary>
        /// Text
        /// </summary>
        [JsonPropertyName("text")]
        public string? Text { get; set; }
        /// <summary>
        /// Create time
        /// </summary>
        [JsonPropertyName("create_time_ms")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("currency_pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Role
        /// </summary>
        [JsonPropertyName("role")]
        public Role Role { get; set; }
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Order quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Order price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// Fee paid
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonPropertyName("fee_currency")]
        public string? FeeAsset { get; set; }
        /// <summary>
        /// Points used to deduct fee
        /// </summary>
        [JsonPropertyName("point_fee")]
        public decimal? PointFee { get; set; }
        /// <summary>
        /// GT used to deduct fee
        /// </summary>
        [JsonPropertyName("gt_fee")]
        public decimal? GtFee { get; set; }
        /// <summary>
        ///	User id
        /// </summary>
        [JsonPropertyName("user_id")]
        public long? UserId { get; set; }
    }
}
