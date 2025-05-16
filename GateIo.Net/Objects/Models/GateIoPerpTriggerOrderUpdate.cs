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
    public record GateIoPerpTriggerOrderUpdate
    {
        /// <summary>
        /// Corresponding order ID of order take-profit/stop-loss.
        /// </summary>
        [JsonPropertyName("me_order_id")]
        public long? TriggerdOrderId { get; set; }
        /// <summary>
        /// Is stop order
        /// </summary>
        [JsonPropertyName("is_stop_order")]
        public bool IsStopOrder { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }
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
}
