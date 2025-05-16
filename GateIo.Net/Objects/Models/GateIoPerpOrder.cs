using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Futures order info
    /// </summary>
    [SerializationModel]
    public record GateIoPerpOrder
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// User id
        /// </summary>
        [JsonPropertyName("user")]
        public long UserId { get; set; }
        /// <summary>
        /// Contract
        /// </summary>
        [JsonPropertyName("contract")]
        public string Contract { get; set; } = string.Empty;
        /// <summary>
        /// Create time
        /// </summary>
        [JsonPropertyName("create_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("size")]
        public int Quantity { get; set; }
        /// <summary>
        /// Iceberg quantity
        /// </summary>
        [JsonPropertyName("iceberg")]
        public int? IcebergQuantity { get; set; }
        /// <summary>
        /// Open quantity
        /// </summary>
        [JsonPropertyName("left")]
        public int QuantityRemaining { get; set; }
        /// <summary>
        /// Order price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// Fill price
        /// </summary>
        [JsonPropertyName("fill_price")]
        public decimal? FillPrice { get; set; }
        /// <summary>
        /// Maker fee
        /// </summary>
        [JsonPropertyName("mkfr")]
        public decimal? MakerFee { get; set; }
        /// <summary>
        /// Taker fee
        /// </summary>
        [JsonPropertyName("tkfr")]
        public decimal? TakerFee { get; set; }
        /// <summary>
        /// Time in force
        /// </summary>
        [JsonPropertyName("tif")]
        public TimeInForce? TimeInForce { get; set; }
        /// <summary>
        /// Reference user ID
        /// </summary>
        [JsonPropertyName("refu")]
        public long? ReferenceUserId { get; set; }
        /// <summary>
        /// Is reduce only order
        /// </summary>
        [JsonPropertyName("is_reduce_only")]
        public bool IsReduceOnly { get; set; }
        /// <summary>
        /// Is close position order
        /// </summary>
        [JsonPropertyName("is_close")]
        public bool IsClose { get; set; }
        /// <summary>
        /// Is liquidation position order
        /// </summary>
        [JsonPropertyName("is_liq")]
        public bool IsLiquidation { get; set; }
        /// <summary>
        /// Text
        /// </summary>
        [JsonPropertyName("text")]
        public string? Text { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// Finish time
        /// </summary>
        [JsonPropertyName("finish_time")]
        public DateTime? FinishTime { get; set; }
        /// <summary>
        /// Finish type
        /// </summary>
        [JsonPropertyName("finish_as")]
        public OrderFinishType? FinishedAs { get; set; }
        /// <summary>
        /// STP group id
        /// </summary>
        [JsonPropertyName("stp_id")]
        public long? SelfTradePreventionId { get; set; }
        /// <summary>
        /// STP mode
        /// </summary>
        [JsonPropertyName("stp_act")]
        public SelfTradePreventionMode? SelfTradePreventionAction { get; set; }
        /// <summary>
        /// Amend text
        /// </summary>
        [JsonPropertyName("amend_text")]
        public string? AmendText { get; set; }

        /// <summary>
        /// Whether or not the requested operation succeeded
        /// </summary>
        [JsonPropertyName("succeeded")]
        public bool? Succeeded { get; set; }

        /// <summary>
        /// Error message if the requested operation failed
        /// </summary>
        [JsonPropertyName("label")]
        public string? ErrorMessage { get; set; }
    }
}
