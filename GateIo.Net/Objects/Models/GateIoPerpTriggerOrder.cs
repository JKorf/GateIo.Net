using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Trigger order
    /// </summary>
    [SerializationModel]
    public record GateIoPerpTriggerOrder
    {
        /// <summary>
        /// ["<c>id</c>"] Trigger order id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>user</c>"] User id
        /// </summary>
        [JsonPropertyName("user")]
        public long UserId { get; set; }
        /// <summary>
        /// ["<c>create_time</c>"] Create time
        /// </summary>
        [JsonPropertyName("create_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>finish_time</c>"] Finish time
        /// </summary>
        [JsonPropertyName("finish_time")]
        public DateTime? FinishTime { get; set; }
        /// <summary>
        /// ["<c>trigger_time</c>"] Trigger time
        /// </summary>
        [JsonPropertyName("trigger_time")]
        public DateTime? TriggerTime { get; set; }
        /// <summary>
        /// ["<c>trade_id</c>"] Trade id
        /// </summary>
        [JsonPropertyName("trade_id")]
        public long? TradeId { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public FuturesTriggerOrderStatus Status { get; set; }
        /// <summary>
        /// ["<c>finish_as</c>"] Finish type
        /// </summary>
        [JsonPropertyName("finish_as")]
        public TriggerFinishType? FinishType { get; set; }
        /// <summary>
        /// ["<c>reason</c>"] Reason
        /// </summary>
        [JsonPropertyName("reason")]
        public string? Reason { get; set; }
        /// <summary>
        /// ["<c>reason_type</c>"] Reason type
        /// </summary>
        [JsonPropertyName("reason_type")]
        public string? ReasonType { get; set; }
        /// <summary>
        /// ["<c>order_type</c>"] Order type
        /// </summary>
        [JsonPropertyName("order_type")]
        public TriggerOrderType? OrderType { get; set; }
        /// <summary>
        /// ["<c>is_stop_order</c>"] Is stop order
        /// </summary>
        [JsonPropertyName("is_stop_order")]
        public bool IsStopOrder { get; set; }
        /// <summary>
        /// ["<c>in_dual_mode</c>"] Is in hedge mode
        /// </summary>
        [JsonPropertyName("in_dual_mode")]
        public bool InDualMode { get; set; }
        /// <summary>
        /// ["<c>me_order_id</c>"] Corresponding order ID for order take-profit/stop-loss orders
        /// </summary>
        [JsonPropertyName("me_order_id")]
        public long? TpslCorrespondingOrderId { get; set; }
        /// <summary>
        /// ["<c>parent_id</c>"] Parent order id
        /// </summary>
        [JsonPropertyName("parent_id")]
        public long? ParentId { get; set; }
        /// <summary>
        /// ["<c>stop_profit_price</c>"] Stop profit price
        /// </summary>
        [JsonPropertyName("stop_profit_price")]
        public decimal? StopProfitPrice { get; set; }
        /// <summary>
        /// ["<c>stop_loss_price</c>"] Stop loss price
        /// </summary>
        [JsonPropertyName("stop_loss_price")]
        public decimal? StopLossPrice { get; set; }
        /// <summary>
        /// ["<c>direction</c>"] Position side
        /// </summary>
        [JsonPropertyName("direction")]
        public PositionSide? PositionSide { get; set; }
        /// <summary>
        /// ["<c>filled_size</c>"] Quantity filled
        /// </summary>
        [JsonPropertyName("filled_size")]
        public decimal? QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>pos_margin_mode</c>"] Margin mode
        /// </summary>
        [JsonPropertyName("pos_margin_mode")]
        public MarginMode? MarginMode { get; set; }
        /// <summary>
        /// ["<c>position_mode</c>"] Position mode
        /// </summary>
        [JsonPropertyName("position_mode")]
        public PositionHoldMode? PositionMode { get; set; }
        /// <summary>
        /// ["<c>finish_as_text</c>"] Finish text
        /// </summary>
        [JsonPropertyName("finish_as_text")]
        public string? FinishText { get; set; }
        /// <summary>
        /// ["<c>is_splitting</c>"] Is splitting
        /// </summary>
        [JsonPropertyName("is_splitting")]
        public bool IsSplitting { get; set; }
        /// <summary>
        /// ["<c>text_output</c>"] Text output
        /// </summary>
        [JsonPropertyName("text_output")]
        public string? TextOutput { get; set; }
        /// <summary>
        /// ["<c>initial</c>"] Order info
        /// </summary>
        [JsonPropertyName("initial")]
        public GateIoPerpTriggerOrderInitial Order { get; set; } = null!;
        /// <summary>
        /// ["<c>trigger</c>"] Trigger info
        /// </summary>
        [JsonPropertyName("trigger")]
        public GateIoPerpTriggerOrderTrigger Trigger { get; set; } = null!;
    }

    /// <summary>
    /// Trigger info
    /// </summary>
    [SerializationModel]
    public record GateIoPerpTriggerOrderTrigger
    {
        /// <summary>
        /// ["<c>price_type</c>"] Price type
        /// </summary>
        [JsonPropertyName("price_type")]
        public PriceType PriceType { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>rule</c>"] Trigger type
        /// </summary>
        [JsonPropertyName("rule")]
        public TriggerType TriggerType { get; set; }
        /// <summary>
        /// ["<c>strategy_type</c>"] Strategy type
        /// </summary>
        [JsonPropertyName("strategy_type")]
        public int StrategyType { get; set; }
        /// <summary>
        /// ["<c>expiration</c>"] Expire time in seconds
        /// </summary>
        [JsonPropertyName("expiration")]
        public int Expiration { get; set; }
    }

    /// <summary>
    /// Order info
    /// </summary>
    [SerializationModel]
    public record GateIoPerpTriggerOrderInitial
    {
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
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// ["<c>is_close</c>"] Close position order
        /// </summary>
        [JsonPropertyName("is_close")]
        public bool ClosePosition { get; set; }
        /// <summary>
        /// ["<c>is_reduce_only</c>"] Reduce only order
        /// </summary>
        [JsonPropertyName("is_reduce_only")]
        public bool ReduceOnly { get; set; }
        /// <summary>
        /// ["<c>tif</c>"] Time in force
        /// </summary>
        [JsonPropertyName("tif")]
        public TimeInForce? TimeInForce { get; set; }
        /// <summary>
        /// ["<c>text</c>"] Text
        /// </summary>
        [JsonPropertyName("text")]
        public string? Text { get; set; }
        /// <summary>
        /// ["<c>auto_size</c>"] Close side
        /// </summary>
        [JsonPropertyName("auto_size")]
        public CloseSide? CloseSide { get; set; }
        /// <summary>
        /// ["<c>iceberg</c>"] Iceberg quantity
        /// </summary>
        [JsonPropertyName("iceberg")]
        public decimal? IcebergQuantity { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] Amount
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal? Amount { get; set; }
        /// <summary>
        /// ["<c>iceberg_amount</c>"] Iceberg amount
        /// </summary>
        [JsonPropertyName("iceberg_amount")]
        public decimal? IcebergAmount { get; set; }
    }
}
