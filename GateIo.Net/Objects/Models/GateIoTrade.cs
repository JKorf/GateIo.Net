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
        /// ["<c>id</c>"] Trade id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>currency_pair</c>"] Symbol
        /// </summary>
        [JsonPropertyName("currency_pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>create_time_ms</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("create_time_ms")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>side</c>"] Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Trade price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>sequence_id</c>"] Sequence id
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
        /// ["<c>role</c>"] Role
        /// </summary>
        [JsonPropertyName("role")]
        public Role Role { get; set; }
        /// <summary>
        /// ["<c>order_id</c>"] Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fee</c>"] Trade fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>fee_currency</c>"] Asset the fee is in
        /// </summary>
        [JsonPropertyName("fee_currency")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>point_fee</c>"] Points used to deduct fee
        /// </summary>
        [JsonPropertyName("point_fee")]
        public decimal PointFee { get; set; }
        /// <summary>
        /// ["<c>gt_fee</c>"] GT used to deduct fee.
        /// </summary>
        [JsonPropertyName("gt_fee")]
        public decimal GTFee { get; set; }
        /// <summary>
        /// ["<c>amend_text</c>"] The custom data that the user remarked when amending the order
        /// </summary>
        [JsonPropertyName("amend_text")]
        public string? AmendText { get; set; }
        /// <summary>
        /// ["<c>text</c>"] User defined information
        /// </summary>
        [JsonPropertyName("text")]
        public string? Text { get; set; }
        /// <summary>
        /// ["<c>deal</c>"] Total executed value
        /// </summary>
        [JsonPropertyName("deal")]
        public decimal Value { get; set; }
    }
}
