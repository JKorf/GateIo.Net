using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Order info
    /// </summary>
    [SerializationModel]
    public record GateIoOrder
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("currency_pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// User defined text
        /// </summary>
        [JsonPropertyName("text")]
        public string? Text { get; set; }
        /// <summary>
        /// Custom data that the user remarked when amending the order
        /// </summary>
        [JsonPropertyName("amend_text")]
        public string? AmendText { get; set; }
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonPropertyName("create_time_ms")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Last update time
        /// </summary>
        [JsonPropertyName("update_time_ms")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// Order status
        /// </summary>
        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("type")]
        public OrderType Type { get; set; }
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
        /// Time in force
        /// </summary>
        [JsonPropertyName("time_in_force")]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Quantity remaining
        /// </summary>
        [JsonPropertyName("left")]
        public decimal QuantityRemaining { get; set; }
        /// <summary>
        /// Quantity filled
        /// </summary>
        [JsonPropertyName("filled_amount")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// Quote quantity filled
        /// </summary>
        [JsonPropertyName("filled_total")]
        public decimal QuoteQuantityFilled { get; set; }
        /// <summary>
        /// Order price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// Average fill price
        /// </summary>
        [JsonPropertyName("avg_deal_price")]
        public decimal? AveragePrice { get; set; }
        /// <summary>
        /// Fee paid
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal? Fee { get; set; }
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
        /// GT used to deduct maker fee
        /// </summary>
        [JsonPropertyName("gt_maker_fee")]
        public decimal? GtMakerFee { get; set; }
        /// <summary>
        /// GT used to deduct taker fee
        /// </summary>
        [JsonPropertyName("gt_taker_fee")]
        public decimal? GtTakerFee { get; set; }
        /// <summary>
        /// Whether GT fee discount is used
        /// </summary>
        [JsonPropertyName("gt_discount")]
        public bool? GtDiscount { get; set; }
        /// <summary>
        /// Rebated fee
        /// </summary>
        [JsonPropertyName("rebated_fee")]
        public decimal? RebatedFee { get; set; }
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
        /// Order finish type
        /// </summary>
        [JsonPropertyName("finish_as")]
        public OrderFinishType? FinishType { get; set; }
        /// <summary>
        /// Iceberg quantity
        /// </summary>
        [JsonPropertyName("iceberg")]
        public decimal? IcebergQuantity { get; set; }
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

    /// <summary>
    /// Order operation result
    /// </summary>
    [SerializationModel]
    public record GateIoOrderOperation : GateIoOrder
    {
        /// <summary>
        /// Whether the operation succeeded
        /// </summary>
        [JsonPropertyName("succeeded")]
        public bool Succeeded { get; set; }

        /// <summary>
        /// Error code when operation failed
        /// </summary>
        [JsonPropertyName("label")]
        public string? ErrorCode { get; set; }
        /// <summary>
        /// Error message when operation failed
        /// </summary>
        [JsonPropertyName("message")]
        public string? ErrorMessage { get; set; }
    }
}
