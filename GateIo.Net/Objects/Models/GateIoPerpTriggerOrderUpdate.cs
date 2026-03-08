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
        /// ["<c>me_order_id</c>"] Corresponding order ID of order take-profit/stop-loss.
        /// </summary>
        [JsonPropertyName("me_order_id")]
        public long? TriggerdOrderId { get; set; }
        /// <summary>
        /// ["<c>is_stop_order</c>"] Is stop order
        /// </summary>
        [JsonPropertyName("is_stop_order")]
        public bool IsStopOrder { get; set; }
        /// <summary>
        /// ["<c>name</c>"] Name
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }
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
        /// ["<c>order_type</c>"] Order type
        /// </summary>
        [JsonPropertyName("order_type")]
        public TriggerOrderType? OrderType { get; set; }
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
}
