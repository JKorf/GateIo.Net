using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;
using GateIo.Net.Enums;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Trade info
    /// </summary>
    [SerializationModel]
    public record GateIoTrade
    {
        /// <summary>
        /// Trade id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("currency_pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("create_time_ms")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Trade price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Sequence id
        /// </summary>
        [JsonPropertyName("sequence_id")]
        public decimal SequenceId { get; set; }
    }

    /// <summary>
    /// User trade info
    /// </summary>
    [SerializationModel]
    public record GateIoUserTrade: GateIoTrade
    {
        /// <summary>
        /// Role
        /// </summary>
        [JsonPropertyName("role")]
        public Role Role { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Trade fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Asset the fee is in
        /// </summary>
        [JsonPropertyName("fee_currency")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// Points used to deduct fee
        /// </summary>
        [JsonPropertyName("point_fee")]
        public decimal PointFee { get; set; }
        /// <summary>
        /// GT used to deduct fee.
        /// </summary>
        [JsonPropertyName("gt_fee")]
        public decimal GTFee { get; set; }
        /// <summary>
        /// The custom data that the user remarked when amending the order
        /// </summary>
        [JsonPropertyName("amend_text")]
        public string? AmendText { get; set; }
        /// <summary>
        /// User defined information
        /// </summary>
        [JsonPropertyName("text")]
        public string? Text { get; set; }
    }
}
