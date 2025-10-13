using GateIo.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Order info
    /// </summary>
    public record GateIoAlphaOrder
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Order status
        /// </summary>
        [JsonPropertyName("status")]
        public AlphaOrderStatus Status { get; set; }
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Gas mode
        /// </summary>
        [JsonPropertyName("gas_mode")]
        public GasMode GasMode { get; set; }
        /// <summary>
        /// Create time
        /// </summary>
        [JsonPropertyName("create_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Quantity in USDT
        /// </summary>
        [JsonPropertyName("usdt_amount")]
        public decimal? QuantityUsdt { get; set; }
        /// <summary>
        /// Quantity in the asset
        /// </summary>
        [JsonPropertyName("currency_amount")]
        public decimal? QuantityAsset { get; set; }
        /// <summary>
        /// Network
        /// </summary>
        [JsonPropertyName("chain")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// Transaction hash
        /// </summary>
        [JsonPropertyName("tx_hash")]
        public string? TransactionHash { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Gas fee
        /// </summary>
        [JsonPropertyName("gas_fee")]
        public decimal? GasFee { get; set; }
        /// <summary>
        /// Transaction fee
        /// </summary>
        [JsonPropertyName("transaction_fee")]
        public decimal? TransactionFee { get; set; }
        /// <summary>
        /// Failed reason
        /// </summary>
        [JsonPropertyName("failed_reason")]
        public string? FailedReason { get; set; }
    }
}
