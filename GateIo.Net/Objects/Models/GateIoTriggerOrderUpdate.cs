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
        /// Symbol
        /// </summary>
        [JsonPropertyName("market")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// User id
        /// </summary>
        [JsonPropertyName("uid")]
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Base asset
        /// </summary>
        [JsonPropertyName("currency_type")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// Quote asset
        /// </summary>
        [JsonPropertyName("exchange_type")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// Reason
        /// </summary>
        [JsonPropertyName("reason")]
        public string Reason { get; set; } = string.Empty;
        /// <summary>
        /// Error message
        /// </summary>
        [JsonPropertyName("err_msg")]
        public string ErrorMessage { get; set; } = string.Empty;
        /// <summary>
        /// Id of the order which was placed after triggering
        /// </summary>
        [JsonPropertyName("fired_order_id")]
        public long FiredOrderId { get; set; }
        /// <summary>
        /// Instant cancel
        /// </summary>
        [JsonPropertyName("instant_cancel")]
        public bool InstantCancel { get; set; }
        /// <summary>
        /// Trigger price
        /// </summary>
        [JsonPropertyName("trigger_price")]
        public decimal TriggerPrice { get; set; }
        /// <summary>
        /// Trigger type
        /// </summary>
        [JsonPropertyName("trigger_rule")]
        public TriggerType TriggerType { get; set; }
        /// <summary>
        /// Expiration in seconds
        /// </summary>
        [JsonPropertyName("trigger_expiration")]
        public int Expiration { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Source
        /// </summary>
        [JsonPropertyName("source")]
        public string Source { get; set; } = string.Empty;
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("order_type")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// Side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// Engine type
        /// </summary>
        [JsonPropertyName("engine_type")]
        public string EngineType { get; set; } = string.Empty;
        /// <summary>
        /// Is stop order
        /// </summary>
        [JsonPropertyName("is_stop_order")]
        public bool IsStopOrder { get; set; }
        /// <summary>
        /// Stop trigger price
        /// </summary>
        [JsonPropertyName("stop_trigger_price")]
        public decimal? StopTriggerPrice { get; set; }
        /// <summary>
        /// Stop trigger rule
        /// </summary>
        [JsonPropertyName("stop_trigger_rule")]
        public string? StopTriggerRule { get; set; }
        /// <summary>
        /// Stop price
        /// </summary>
        [JsonPropertyName("stop_price")]
        public decimal? StopPrice { get; set; }
        /// <summary>
        /// Create time
        /// </summary>
        [JsonPropertyName("ctime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Fire time
        /// </summary>
        [JsonPropertyName("ftime")]
        public DateTime? FireTime { get; set; }
    }
}
