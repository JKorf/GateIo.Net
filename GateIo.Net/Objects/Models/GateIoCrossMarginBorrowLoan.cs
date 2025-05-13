using System;
using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net.Enums;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Cross margin borrow loan
    /// </summary>
    [SerializationModel]
    public record GateIoCrossMarginBorrowLoan
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Create time
        /// </summary>
        [JsonPropertyName("create_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Update time
        /// </summary>
        [JsonPropertyName("update_time")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Text
        /// </summary>
        [JsonPropertyName("text")]
        public string? Text { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public BorrowStatus Status { get; set; }
        /// <summary>
        /// Repaid
        /// </summary>
        [JsonPropertyName("repaid")]
        public decimal Repaid { get; set; }
        /// <summary>
        /// Repaid interest
        /// </summary>
        [JsonPropertyName("repaid_interest")]
        public decimal RepaidInterest { get; set; }
        /// <summary>
        /// Unpaid interest
        /// </summary>
        [JsonPropertyName("unpaid_interest")]
        public decimal UnpaidInterest { get; set; }
    }
}
