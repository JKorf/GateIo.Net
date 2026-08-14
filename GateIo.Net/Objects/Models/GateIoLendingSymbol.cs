using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Lending symbol
    /// </summary>
    [SerializationModel]
    public record GateIoLendingSymbol
    {
        /// <summary>
        /// ["<c>currency_pair</c>"] Symbol
        /// </summary>
        [JsonPropertyName("currency_pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>base_min_borrow_amount</c>"] Min borrow quantity in the base asset
        /// </summary>
        [JsonPropertyName("base_min_borrow_amount")]
        public decimal BaseAssetMinBorrowQuantity { get; set; }
        /// <summary>
        /// ["<c>quote_min_borrow_amount</c>"] Min borrow quantity in the quote asset
        /// </summary>
        [JsonPropertyName("quote_min_borrow_amount")]
        public decimal QuoteAssetMinBorrowQuantity { get; set; }
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }

        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>delisted_time</c>"] Delist time
        /// </summary>
        [JsonPropertyName("delisted_time")]
        public DateTime? DelistTime { get; set; } 
    }
}
