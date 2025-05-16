using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Symbol
        /// </summary>
        [JsonPropertyName("currency_pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Min borrow quantity in the base asset
        /// </summary>
        [JsonPropertyName("base_min_borrow_amount")]
        public decimal BaseAssetMinBorrowQuantity { get; set; }
        /// <summary>
        /// Min borrow quantity in the quote asset
        /// </summary>
        [JsonPropertyName("quote_min_borrow_amount")]
        public decimal QuoteAssetMinBorrowQuantity { get; set; }
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
    }
}
