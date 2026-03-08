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
        /// ["<c>quote_id</c>"] Quote id
        /// </summary>
        [JsonPropertyName("quote_id")]
        public string QuoteId { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>min_amount</c>"] Min order quantity
        /// </summary>
        [JsonPropertyName("min_amount")]
        public decimal MinQuantity { get; set; }
        /// <summary>
        /// ["<c>max_amount</c>"] Max order quantity
        /// </summary>
        [JsonPropertyName("max_amount")]
        public string MaxQuantity { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>slippage</c>"] Slippage
        /// </summary>
        [JsonPropertyName("slippage")]
        public decimal Slippage { get; set; }
        /// <summary>
        /// ["<c>estimate_gas_fee_amount_usdt</c>"] Estimated gas USDT fee
        /// </summary>
        [JsonPropertyName("estimate_gas_fee_amount_usdt")]
        public string EstimatedGasFeeUsdt { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>order_fee</c>"] Order fee
        /// </summary>
        [JsonPropertyName("order_fee")]
        public string OrderFee { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>target_token_min_amount</c>"] Minimum received quantity
        /// </summary>
        [JsonPropertyName("target_token_min_amount")]
        public decimal MinReceiveQuantity { get; set; }
        /// <summary>
        /// ["<c>target_token_max_amount</c>"] Maximum received quantity
        /// </summary>
        [JsonPropertyName("target_token_max_amount")]
        public decimal MaxReceiveQuantity { get; set; }
        /// <summary>
        /// ["<c>error_type</c>"] Quote status
        /// </summary>
        [JsonPropertyName("error_type")]
        public QuoteStatus Status { get; set; }
    }
}
