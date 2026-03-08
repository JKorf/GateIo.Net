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
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>user</c>"] User id
        /// </summary>
        [JsonPropertyName("user")]
        public long UserId { get; set; }
        /// <summary>
        /// ["<c>contract</c>"] Contract
        /// </summary>
        [JsonPropertyName("contract")]
        public string Contract { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>create_time</c>"] Create time
        /// </summary>
        [JsonPropertyName("create_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>size</c>"] Quantity
        /// </summary>
        [JsonPropertyName("size")]
        public int Quantity { get; set; }
        /// <summary>
        /// ["<c>iceberg</c>"] Iceberg quantity
        /// </summary>
        [JsonPropertyName("iceberg")]
        public int? IcebergQuantity { get; set; }
        /// <summary>
        /// ["<c>left</c>"] Open quantity
        /// </summary>
        [JsonPropertyName("left")]
        public int QuantityRemaining { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Order price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// ["<c>fill_price</c>"] Fill price
        /// </summary>
        [JsonPropertyName("fill_price")]
        public decimal? FillPrice { get; set; }
        /// <summary>
        /// ["<c>mkfr</c>"] Maker fee
        /// </summary>
        [JsonPropertyName("mkfr")]
        public decimal? MakerFee { get; set; }
        /// <summary>
        /// ["<c>tkfr</c>"] Taker fee
        /// </summary>
        [JsonPropertyName("tkfr")]
        public decimal? TakerFee { get; set; }
        /// <summary>
        /// ["<c>tif</c>"] Time in force
        /// </summary>
        [JsonPropertyName("tif")]
        public TimeInForce? TimeInForce { get; set; }
        /// <summary>
        /// ["<c>refu</c>"] Reference user ID
        /// </summary>
        [JsonPropertyName("refu")]
        public long? ReferenceUserId { get; set; }
        /// <summary>
        /// ["<c>is_reduce_only</c>"] Is reduce only order
        /// </summary>
        [JsonPropertyName("is_reduce_only")]
        public bool IsReduceOnly { get; set; }
        /// <summary>
        /// ["<c>is_close</c>"] Is close position order
        /// </summary>
        [JsonPropertyName("is_close")]
        public bool IsClose { get; set; }
        /// <summary>
        /// ["<c>is_liq</c>"] Is liquidation position order
        /// </summary>
        [JsonPropertyName("is_liq")]
        public bool IsLiquidation { get; set; }
        /// <summary>
        /// ["<c>text</c>"] Text
        /// </summary>
        [JsonPropertyName("text")]
        public string? Text { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// ["<c>finish_time</c>"] Finish time
        /// </summary>
        [JsonPropertyName("finish_time")]
        public DateTime? FinishTime { get; set; }
        /// <summary>
        /// ["<c>finish_as</c>"] Finish type
        /// </summary>
        [JsonPropertyName("finish_as")]
        public OrderFinishType? FinishedAs { get; set; }
        /// <summary>
        /// ["<c>stp_id</c>"] STP group id
        /// </summary>
        [JsonPropertyName("stp_id")]
        public long? SelfTradePreventionId { get; set; }
        /// <summary>
        /// ["<c>stp_act</c>"] STP mode
        /// </summary>
        [JsonPropertyName("stp_act")]
        public SelfTradePreventionMode? SelfTradePreventionAction { get; set; }
        /// <summary>
        /// ["<c>amend_text</c>"] Amend text
        /// </summary>
        [JsonPropertyName("amend_text")]
        public string? AmendText { get; set; }

        /// <summary>
        /// ["<c>succeeded</c>"] Whether or not the requested operation succeeded
        /// </summary>
        [JsonPropertyName("succeeded")]
        public bool? Succeeded { get; set; }

        /// <summary>
        /// ["<c>label</c>"] Error message if the requested operation failed
        /// </summary>
        [JsonPropertyName("label")]
        public string? ErrorMessage { get; set; }
    }
}
