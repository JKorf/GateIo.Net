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
        /// ["<c>put</c>"] Order info
        /// </summary>
        [JsonPropertyName("put")]
        public GateIoTriggerOrderOrder Order { get; set; } = null!;

        /// <summary>
        /// ["<c>trigger</c>"] Order info
        /// </summary>
        [JsonPropertyName("trigger")]
        public GateIoTriggerOrderTrigger Trigger { get; set; } = null!;
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
        /// ["<c>market</c>"] Symbol
        /// </summary>
        [JsonPropertyName("market")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>ctime</c>"] Create time
        /// </summary>
        [JsonPropertyName("ctime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>ftime</c>"] Trigger time
        /// </summary>
        [JsonPropertyName("ftime")]
        public DateTime? TriggerTime { get; set; }
        /// <summary>
        /// ["<c>fired_order_id</c>"] Id of the create order
        /// </summary>
        [JsonPropertyName("fired_order_id")]
        public long? TriggeredOrderId { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Trigger status
        /// </summary>
        [JsonPropertyName("status")]
        public TriggerOrderStatus? Status { get; set; }
        /// <summary>
        /// ["<c>reason</c>"] Additional info
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
        /// ["<c>price</c>"] Trigger price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>rule</c>"] Trigger type
        /// </summary>
        [JsonPropertyName("rule")]
        public TriggerType TriggerType { get; set; }
        /// <summary>
        /// ["<c>expiration</c>"] Expiration in seconds
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
        /// ["<c>text</c>"] User defined text
        /// </summary>
        [JsonPropertyName("text")]
        public string? Text { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Order type
        /// </summary>
        [JsonPropertyName("type")]
        public NewOrderType Type { get; set; }
        /// <summary>
        /// ["<c>side</c>"] Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>account</c>"] Account type
        /// </summary>
        [JsonPropertyName("account")]
        public TriggerAccountType AccountType { get; set; }
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
        /// ["<c>price</c>"] Order price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
    }
}
