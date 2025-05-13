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
        /// Trigger order id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// User id
        /// </summary>
        [JsonPropertyName("user")]
        public long UserId { get; set; }
        /// <summary>
        /// Create time
        /// </summary>
        [JsonPropertyName("create_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Finish time
        /// </summary>
        [JsonPropertyName("finish_time")]
        public DateTime? FinishTime { get; set; }
        /// <summary>
        /// Trade id
        /// </summary>
        [JsonPropertyName("trade_id")]
        public long? TradeId { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public FuturesTriggerOrderStatus Status { get; set; }
        /// <summary>
        /// Finish type
        /// </summary>
        [JsonPropertyName("finish_as")]
        public TriggerFinishType? FinishType { get; set; }
        /// <summary>
        /// Reason
        /// </summary>
        [JsonPropertyName("reason")]
        public string? Reason { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("order_type")]
        public TriggerOrderType? OrderType { get; set; }
        /// <summary>
        /// Order info
        /// </summary>
        [JsonPropertyName("initial")]
        public GateIoPerpTriggerOrderInitial Order { get; set; } = null!;
        /// <summary>
        /// Trigger info
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
        /// Price type
        /// </summary>
        [JsonPropertyName("price_type")]
        public PriceType PriceType { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Trigger type
        /// </summary>
        [JsonPropertyName("rule")]
        public TriggerType TriggerType { get; set; }
        /// <summary>
        /// Expire time in seconds
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
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// Close position order
        /// </summary>
        [JsonPropertyName("is_close")]
        public bool ClosePosition { get; set; }
        /// <summary>
        /// Reduce only order
        /// </summary>
        [JsonPropertyName("is_reduce_only")]
        public bool ReduceOnly { get; set; }
        /// <summary>
        /// Time in force
        /// </summary>
        [JsonPropertyName("tif")]
        public TimeInForce? TimeInForce { get; set; }
        /// <summary>
        /// Text
        /// </summary>
        [JsonPropertyName("text")]
        public string? Text { get; set; }
        /// <summary>
        /// Close side
        /// </summary>
        [JsonPropertyName("auto_size")]
        public CloseSide? CloseSide { get; set; }
    }
}
