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
        public int Quantity { get; set; }
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
    }
}
