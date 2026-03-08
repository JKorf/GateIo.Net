using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Trigger order update
    /// </summary>
    [SerializationModel]
    public record GateIoTriggerOrderUpdate
    {
        /// <summary>
        /// ["<c>market</c>"] Symbol
        /// </summary>
        [JsonPropertyName("market")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>uid</c>"] User id
        /// </summary>
        [JsonPropertyName("uid")]
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>currency_type</c>"] Base asset
        /// </summary>
        [JsonPropertyName("currency_type")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>exchange_type</c>"] Quote asset
        /// </summary>
        [JsonPropertyName("exchange_type")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>reason</c>"] Reason
        /// </summary>
        [JsonPropertyName("reason")]
        public string Reason { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>err_msg</c>"] Error message
        /// </summary>
        [JsonPropertyName("err_msg")]
        public string ErrorMessage { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fired_order_id</c>"] Id of the order which was placed after triggering
        /// </summary>
        [JsonPropertyName("fired_order_id")]
        public long FiredOrderId { get; set; }
        /// <summary>
        /// ["<c>instant_cancel</c>"] Instant cancel
        /// </summary>
        [JsonPropertyName("instant_cancel")]
        public bool InstantCancel { get; set; }
        /// <summary>
        /// ["<c>trigger_price</c>"] Trigger price
        /// </summary>
        [JsonPropertyName("trigger_price")]
        public decimal TriggerPrice { get; set; }
        /// <summary>
        /// ["<c>trigger_rule</c>"] Trigger type
        /// </summary>
        [JsonPropertyName("trigger_rule")]
        public TriggerType TriggerType { get; set; }
        /// <summary>
        /// ["<c>trigger_expiration</c>"] Expiration in seconds
        /// </summary>
        [JsonPropertyName("trigger_expiration")]
        public int Expiration { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>source</c>"] Source
        /// </summary>
        [JsonPropertyName("source")]
        public string Source { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>order_type</c>"] Order type
        /// </summary>
        [JsonPropertyName("order_type")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// ["<c>side</c>"] Side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// ["<c>engine_type</c>"] Engine type
        /// </summary>
        [JsonPropertyName("engine_type")]
        public string EngineType { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>is_stop_order</c>"] Is stop order
        /// </summary>
        [JsonPropertyName("is_stop_order")]
        public bool IsStopOrder { get; set; }
        /// <summary>
        /// ["<c>stop_trigger_price</c>"] Stop trigger price
        /// </summary>
        [JsonPropertyName("stop_trigger_price")]
        public decimal? StopTriggerPrice { get; set; }
        /// <summary>
        /// ["<c>stop_trigger_rule</c>"] Stop trigger rule
        /// </summary>
        [JsonPropertyName("stop_trigger_rule")]
        public string? StopTriggerRule { get; set; }
        /// <summary>
        /// ["<c>stop_price</c>"] Stop price
        /// </summary>
        [JsonPropertyName("stop_price")]
        public decimal? StopPrice { get; set; }
        /// <summary>
        /// ["<c>ctime</c>"] Create time
        /// </summary>
        [JsonPropertyName("ctime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>ftime</c>"] Fire time
        /// </summary>
        [JsonPropertyName("ftime")]
        public DateTime? FireTime { get; set; }
    }
}
