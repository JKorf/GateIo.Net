using GateIo.Net.Enums;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Quote
    /// </summary>
    public record GateIoAlphaQuote
    {
        /// <summary>
        /// Quote id
        /// </summary>
        [JsonPropertyName("quote_id")]
        public string QuoteId { get; set; } = string.Empty;

        /// <summary>
        /// Min order quantity
        /// </summary>
        [JsonPropertyName("min_amount")]
        public decimal MinQuantity { get; set; }
        /// <summary>
        /// Max order quantity
        /// </summary>
        [JsonPropertyName("max_amount")]
        public string MaxQuantity { get; set; } = string.Empty;
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Slippage
        /// </summary>
        [JsonPropertyName("slippage")]
        public decimal Slippage { get; set; }
        /// <summary>
        /// Estimated gas USDT fee
        /// </summary>
        [JsonPropertyName("estimate_gas_fee_amount_usdt")]
        public string EstimatedGasFeeUsdt { get; set; } = string.Empty;
        /// <summary>
        /// Order fee
        /// </summary>
        [JsonPropertyName("order_fee")]
        public string OrderFee { get; set; } = string.Empty;
        /// <summary>
        /// Minimum received quantity
        /// </summary>
        [JsonPropertyName("target_token_min_amount")]
        public decimal MinReceiveQuantity { get; set; }
        /// <summary>
        /// Maximum received quantity
        /// </summary>
        [JsonPropertyName("target_token_max_amount")]
        public decimal MaxReceiveQuantity { get; set; }
        /// <summary>
        /// Quote status
        /// </summary>
        [JsonPropertyName("error_type")]
        public QuoteStatus Status { get; set; }
    }
}
