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
        /// ["<c>order_id</c>"] Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>id</c>"] Trade id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>id_market</c>"] Market unique id
        /// </summary>
        [JsonPropertyName("id_market")]
        public long MarketId { get; set; }
        /// <summary>
        /// ["<c>text</c>"] Text
        /// </summary>
        [JsonPropertyName("text")]
        public string? Text { get; set; }
        /// <summary>
        /// ["<c>create_time_ms</c>"] Create time
        /// </summary>
        [JsonPropertyName("create_time_ms")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>currency_pair</c>"] Symbol
        /// </summary>
        [JsonPropertyName("currency_pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>role</c>"] Role
        /// </summary>
        [JsonPropertyName("role")]
        public Role Role { get; set; }
        /// <summary>
        /// ["<c>side</c>"] Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] Order quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Order price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// ["<c>fee</c>"] Fee paid
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>fee_currency</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("fee_currency")]
        public string? FeeAsset { get; set; }
        /// <summary>
        /// ["<c>point_fee</c>"] Points used to deduct fee
        /// </summary>
        [JsonPropertyName("point_fee")]
        public decimal? PointFee { get; set; }
        /// <summary>
        /// ["<c>gt_fee</c>"] GT used to deduct fee
        /// </summary>
        [JsonPropertyName("gt_fee")]
        public decimal? GtFee { get; set; }
        /// <summary>
        ///	["<c>user_id</c>"] User id
        /// </summary>
        [JsonPropertyName("user_id")]
        public long? UserId { get; set; }
    }
}
