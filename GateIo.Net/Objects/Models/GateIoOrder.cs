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
        /// ["<c>id</c>"] Order id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>currency_pair</c>"] Symbol
        /// </summary>
        [JsonPropertyName("currency_pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>text</c>"] User defined text
        /// </summary>
        [JsonPropertyName("text")]
        public string? Text { get; set; }
        /// <summary>
        /// ["<c>amend_text</c>"] Custom data that the user remarked when amending the order
        /// </summary>
        [JsonPropertyName("amend_text")]
        public string? AmendText { get; set; }
        /// <summary>
        /// ["<c>create_time_ms</c>"] Creation time
        /// </summary>
        [JsonPropertyName("create_time_ms")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>update_time_ms</c>"] Last update time
        /// </summary>
        [JsonPropertyName("update_time_ms")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Order status
        /// </summary>
        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Order type
        /// </summary>
        [JsonPropertyName("type")]
        public OrderType Type { get; set; }
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
        /// ["<c>time_in_force</c>"] Time in force
        /// </summary>
        [JsonPropertyName("time_in_force")]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>left</c>"] Quantity remaining
        /// </summary>
        [JsonPropertyName("left")]
        public decimal QuantityRemaining { get; set; }
        /// <summary>
        /// ["<c>filled_amount</c>"] Quantity filled
        /// </summary>
        [JsonPropertyName("filled_amount")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>filled_total</c>"] Quote quantity filled
        /// </summary>
        [JsonPropertyName("filled_total")]
        public decimal QuoteQuantityFilled { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Order price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// ["<c>avg_deal_price</c>"] Average fill price
        /// </summary>
        [JsonPropertyName("avg_deal_price")]
        public decimal? AveragePrice { get; set; }
        /// <summary>
        /// ["<c>fee</c>"] Fee paid
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal? Fee { get; set; }
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
        /// ["<c>gt_maker_fee</c>"] GT used to deduct maker fee
        /// </summary>
        [JsonPropertyName("gt_maker_fee")]
        public decimal? GtMakerFee { get; set; }
        /// <summary>
        /// ["<c>gt_taker_fee</c>"] GT used to deduct taker fee
        /// </summary>
        [JsonPropertyName("gt_taker_fee")]
        public decimal? GtTakerFee { get; set; }
        /// <summary>
        /// ["<c>gt_discount</c>"] Whether GT fee discount is used
        /// </summary>
        [JsonPropertyName("gt_discount")]
        public bool? GtDiscount { get; set; }
        /// <summary>
        /// ["<c>rebated_fee</c>"] Rebated fee
        /// </summary>
        [JsonPropertyName("rebated_fee")]
        public decimal? RebatedFee { get; set; }
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
        /// ["<c>finish_as</c>"] Order finish type
        /// </summary>
        [JsonPropertyName("finish_as")]
        public OrderFinishType? FinishType { get; set; }
        /// <summary>
        /// ["<c>iceberg</c>"] Iceberg quantity
        /// </summary>
        [JsonPropertyName("iceberg")]
        public decimal? IcebergQuantity { get; set; }
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

    /// <summary>
    /// Order operation result
    /// </summary>
    [SerializationModel]
    public record GateIoOrderOperation : GateIoOrder
    {
        /// <summary>
        /// ["<c>succeeded</c>"] Whether the operation succeeded
        /// </summary>
        [JsonPropertyName("succeeded")]
        public bool Succeeded { get; set; }

        /// <summary>
        /// ["<c>label</c>"] Error code when operation failed
        /// </summary>
        [JsonPropertyName("label")]
        public string? ErrorCode { get; set; }
        /// <summary>
        /// ["<c>message</c>"] Error message when operation failed
        /// </summary>
        [JsonPropertyName("message")]
        public string? ErrorMessage { get; set; }
    }
}
