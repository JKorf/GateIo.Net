using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Trigger order info
    /// </summary>
    [SerializationModel]
    public record GateIoTriggerOrder
    {
        /// <summary>
        /// Order info
        /// </summary>
        [JsonPropertyName("put")]
        public GateIoTriggerOrderOrder Order { get; set; } = null!;

        /// <summary>
        /// Order info
        /// </summary>
        [JsonPropertyName("trigger")]
        public GateIoTriggerOrderTrigger Trigger { get; set; } = null!;
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
        /// Symbol
        /// </summary>
        [JsonPropertyName("market")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Create time
        /// </summary>
        [JsonPropertyName("ctime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Trigger time
        /// </summary>
        [JsonPropertyName("ftime")]
        public DateTime? TriggerTime { get; set; }
        /// <summary>
        /// Id of the create order
        /// </summary>
        [JsonPropertyName("fired_order_id")]
        public long? TriggeredOrderId { get; set; }
        /// <summary>
        /// Trigger status
        /// </summary>
        [JsonPropertyName("status")]
        public TriggerOrderStatus? Status { get; set; }
        /// <summary>
        /// Additional info
        /// </summary>
        [JsonPropertyName("reason")]
        public string? Reason { get; set; }
    }

    /// <summary>
    /// Trigger order trigger info
    /// </summary>
    [SerializationModel]
    public record GateIoTriggerOrderTrigger
    {
        /// <summary>
        /// Trigger price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Trigger type
        /// </summary>
        [JsonPropertyName("rule")]
        public TriggerType TriggerType { get; set; }
        /// <summary>
        /// Expiration in seconds
        /// </summary>
        [JsonPropertyName("expiration")]
        public int Expiration { get; set; }
    }

    /// <summary>
    /// Order info
    /// </summary>
    [SerializationModel]
    public record GateIoTriggerOrderOrder
    {
        /// <summary>
        /// User defined text
        /// </summary>
        [JsonPropertyName("text")]
        public string? Text { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("type")]
        public NewOrderType Type { get; set; }
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Account type
        /// </summary>
        [JsonPropertyName("account")]
        public TriggerAccountType AccountType { get; set; }
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
        /// Order price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
    }
}
