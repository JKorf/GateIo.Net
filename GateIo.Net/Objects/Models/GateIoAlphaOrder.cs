using GateIo.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Order info
    /// </summary>
    public record GateIoAlphaOrder
    {
        /// <summary>
        /// ["<c>order_id</c>"] Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] Order status
        /// </summary>
        [JsonPropertyName("status")]
        public AlphaOrderStatus Status { get; set; }
        /// <summary>
        /// ["<c>side</c>"] Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>gas_mode</c>"] Gas mode
        /// </summary>
        [JsonPropertyName("gas_mode")]
        public GasMode GasMode { get; set; }
        /// <summary>
        /// ["<c>create_time</c>"] Create time
        /// </summary>
        [JsonPropertyName("create_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>usdt_amount</c>"] Quantity in USDT
        /// </summary>
        [JsonPropertyName("usdt_amount")]
        public decimal? QuantityUsdt { get; set; }
        /// <summary>
        /// ["<c>currency_amount</c>"] Quantity in the asset
        /// </summary>
        [JsonPropertyName("currency_amount")]
        public decimal? QuantityAsset { get; set; }
        /// <summary>
        /// ["<c>chain</c>"] Network
        /// </summary>
        [JsonPropertyName("chain")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>tx_hash</c>"] Transaction hash
        /// </summary>
        [JsonPropertyName("tx_hash")]
        public string? TransactionHash { get; set; }
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>gas_fee</c>"] Gas fee
        /// </summary>
        [JsonPropertyName("gas_fee")]
        public decimal? GasFee { get; set; }
        /// <summary>
        /// ["<c>transaction_fee</c>"] Transaction fee
        /// </summary>
        [JsonPropertyName("transaction_fee")]
        public decimal? TransactionFee { get; set; }
        /// <summary>
        /// ["<c>failed_reason</c>"] Failed reason
        /// </summary>
        [JsonPropertyName("failed_reason")]
        public string? FailedReason { get; set; }
    }
}
