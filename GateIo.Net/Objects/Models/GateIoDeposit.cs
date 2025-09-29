using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;
using GateIo.Net.Enums;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Deposit info
    /// </summary>
    [SerializationModel]
    public record GateIoDeposit
    {
        /// <summary>
        /// Withdrawal id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Client specified id
        /// </summary>
        [JsonPropertyName("withdraw_order_id")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Address
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// Network
        /// </summary>
        [JsonPropertyName("chain")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// Memo
        /// </summary>
        [JsonPropertyName("memo")]
        public string? Memo { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonPropertyName("txid")]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// Deposit status
        /// </summary>
        [JsonPropertyName("status")]
        public WithdrawalStatus Status { get; set; }
    }
}
