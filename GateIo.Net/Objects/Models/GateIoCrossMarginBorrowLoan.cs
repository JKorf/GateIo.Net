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
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>create_time</c>"] Create time
        /// </summary>
        [JsonPropertyName("create_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>update_time</c>"] Update time
        /// </summary>
        [JsonPropertyName("update_time")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>text</c>"] Text
        /// </summary>
        [JsonPropertyName("text")]
        public string? Text { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public BorrowStatus Status { get; set; }
        /// <summary>
        /// ["<c>repaid</c>"] Repaid
        /// </summary>
        [JsonPropertyName("repaid")]
        public decimal Repaid { get; set; }
        /// <summary>
        /// ["<c>repaid_interest</c>"] Repaid interest
        /// </summary>
        [JsonPropertyName("repaid_interest")]
        public decimal RepaidInterest { get; set; }
        /// <summary>
        /// ["<c>unpaid_interest</c>"] Unpaid interest
        /// </summary>
        [JsonPropertyName("unpaid_interest")]
        public decimal UnpaidInterest { get; set; }
    }
}
