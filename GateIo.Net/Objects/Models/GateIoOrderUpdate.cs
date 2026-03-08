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
        /// ["<c>id</c>"] Order id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
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
        /// ["<c>update_time_ms</c>"] Update time
        /// </summary>
        [JsonPropertyName("update_time_ms")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// ["<c>currency_pair</c>"] Symbol
        /// </summary>
        [JsonPropertyName("currency_pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>type</c>"] Order type
        /// </summary>
        [JsonPropertyName("type")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// ["<c>side</c>"] Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>account</c>"] Account type
        /// </summary>
        [JsonPropertyName("account")]
        public SpotAccountType AccountType { get; set; }
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
        /// ["<c>time_in_force</c>"] Time in force
        /// </summary>
        [JsonPropertyName("time_in_force")]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// ["<c>left</c>"] Quantity still open
        /// </summary>
        [JsonPropertyName("left")]
        public decimal QuantityRemaining { get; set; }
        /// <summary>
        /// ["<c>filled_total</c>"] Quote asset quantity filled
        /// </summary>
        [JsonPropertyName("filled_total")]
        public decimal QuoteQuantityFilled { get; set; }
        /// <summary>
        /// ["<c>filled_amount</c>"] Quantity filled
        /// </summary>
        [JsonPropertyName("filled_amount")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>avg_deal_price</c>"] Average fill price
        /// </summary>
        [JsonPropertyName("avg_deal_price")]
        public decimal? AveragePrice { get; set; }
        /// <summary>
        /// ["<c>fee</c>"] Fee paid
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>fee_currency</c>"] Asset the fee is in
        /// </summary>
        [JsonPropertyName("fee_currency")]
        public string FeeAsset { get; set; } = string.Empty;
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
        /// ["<c>gt_discount</c>"] Whether GT fee discount is used
        /// </summary>
        [JsonPropertyName("gt_discount")]
        public bool? GtDiscount { get; set; }
        /// <summary>
        /// ["<c>rebated_fee</c>"] Rebated fee
        /// </summary>
        [JsonPropertyName("rebated_fee")]
        public bool? RebatedFee { get; set; }
        /// <summary>
        /// ["<c>rebated_fee_currency</c>"] Rebated fee currency unit
        /// </summary>
        [JsonPropertyName("rebated_fee_currency")]
        public string? RebateFeeAsset { get; set; }
        /// <summary>
        ///	["<c>stp_id</c>"] Orders between users in the same SelfTradePreventionId group are not allowed to be self-traded
        /// </summary>
        [JsonPropertyName("stp_id")]
        public int? SelfTradePreventionId { get; set; }
        /// <summary>
        /// ["<c>stp_act</c>"] Self trade prevention mode
        /// </summary>
        [JsonPropertyName("stp_act")]
        public SelfTradePreventionMode SelfTradePreventionMode { get; set; }
        /// <summary>
        ///	["<c>user</c>"] User id
        /// </summary>
        [JsonPropertyName("user")]
        public long? UserId { get; set; }
        /// <summary>
        /// ["<c>event</c>"] Trigger event
        /// </summary>
        [JsonPropertyName("event")]
        public OrderUpdateEvent Event { get; set; }
        /// <summary>
        /// ["<c>finish_as</c>"] Order finish type
        /// </summary>
        [JsonPropertyName("finish_as")]
        public OrderFinishType? FinishType { get; set; }
        /// <summary>
        /// ["<c>amend_text</c>"] Custom data that the user remarked when amending the order
        /// </summary>
        [JsonPropertyName("amend_text")]
        public string? AmendText { get; set; }
        /// <summary>
        /// ["<c>auto_borrow</c>"] Auto borrow
        /// </summary>
        [JsonPropertyName("auto_borrow")]
        public bool? AutoBorrow { get; set; }
        /// <summary>
        /// ["<c>auto_repay</c>"] Auto repay
        /// </summary>
        [JsonPropertyName("auto_repay")]
        public bool? AutoRepay { get; set; }
    }
}
