using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Order update
    /// </summary>
    [SerializationModel]
    public record GateIoOrderUpdate
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
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
        /// Update time
        /// </summary>
        [JsonPropertyName("update_time_ms")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("currency_pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("type")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Account type
        /// </summary>
        [JsonPropertyName("account")]
        public SpotAccountType AccountType { get; set; }
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
        /// Time in force
        /// </summary>
        [JsonPropertyName("time_in_force")]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// Quantity still open
        /// </summary>
        [JsonPropertyName("left")]
        public decimal QuantityRemaining { get; set; }
        /// <summary>
        /// Quote asset quantity filled
        /// </summary>
        [JsonPropertyName("filled_total")]
        public decimal QuoteQuantityFilled { get; set; }
        /// <summary>
        /// Quantity filled
        /// </summary>
        [JsonPropertyName("filled_amount")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// Average fill price
        /// </summary>
        [JsonPropertyName("avg_deal_price")]
        public decimal? AveragePrice { get; set; }
        /// <summary>
        /// Fee paid
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Asset the fee is in
        /// </summary>
        [JsonPropertyName("fee_currency")]
        public string FeeAsset { get; set; } = string.Empty;
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
        /// Whether GT fee discount is used
        /// </summary>
        [JsonPropertyName("gt_discount")]
        public bool? GtDiscount { get; set; }
        /// <summary>
        /// Rebated fee
        /// </summary>
        [JsonPropertyName("rebated_fee")]
        public bool? RebatedFee { get; set; }
        /// <summary>
        /// Rebated fee currency unit
        /// </summary>
        [JsonPropertyName("rebated_fee_currency")]
        public string? RebateFeeAsset { get; set; }
        /// <summary>
        ///	Orders between users in the same SelfTradePreventionId group are not allowed to be self-traded
        /// </summary>
        [JsonPropertyName("stp_id")]
        public int? SelfTradePreventionId { get; set; }
        /// <summary>
        /// Self trade prevention mode
        /// </summary>
        [JsonPropertyName("stp_act")]
        public SelfTradePreventionMode SelfTradePreventionMode { get; set; }
        /// <summary>
        ///	User id
        /// </summary>
        [JsonPropertyName("user")]
        public long? UserId { get; set; }
        /// <summary>
        /// Trigger event
        /// </summary>
        [JsonPropertyName("event")]
        public OrderUpdateEvent Event { get; set; }
        /// <summary>
        /// Order finish type
        /// </summary>
        [JsonPropertyName("finish_as")]
        public OrderFinishType? FinishType { get; set; }
        /// <summary>
        /// Custom data that the user remarked when amending the order
        /// </summary>
        [JsonPropertyName("amend_text")]
        public string? AmendText { get; set; }
        /// <summary>
        /// Auto borrow
        /// </summary>
        [JsonPropertyName("auto_borrow")]
        public bool? AutoBorrow { get; set; }
        /// <summary>
        /// Auto repay
        /// </summary>
        [JsonPropertyName("auto_repay")]
        public bool? AutoRepay { get; set; }
    }
}
