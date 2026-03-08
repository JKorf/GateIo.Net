using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Contract statistics
    /// </summary>
    [SerializationModel]
    public record GateIoPerpContractStats
    {
        /// <summary>
        /// ["<c>time</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>contract</c>"] Contract 
        /// </summary>
        [JsonPropertyName("contract")]
        public string? Contract { get; set; }
        /// <summary>
        /// ["<c>lsr_taker</c>"] Long/short account number ratio
        /// </summary>
        [JsonPropertyName("lsr_taker")]
        public decimal LongShortAccountRatio { get; set; }
        /// <summary>
        /// ["<c>lsr_account</c>"] Long/short taker size ratio
        /// </summary>
        [JsonPropertyName("lsr_account")]
        public decimal LongShortTakerRatio { get; set; }
        /// <summary>
        /// ["<c>long_liq_size</c>"] Long liquidation size
        /// </summary>
        [JsonPropertyName("long_liq_size")]
        public decimal LongLiquidationSize { get; set; }
        /// <summary>
        /// ["<c>short_liq_size</c>"] Short liquidation size
        /// </summary>
        [JsonPropertyName("short_liq_size")]
        public decimal ShotLiquidationSize { get; set; }
        /// <summary>
        /// ["<c>open_interest</c>"] Open interest
        /// </summary>
        [JsonPropertyName("open_interest")]
        public decimal OpenInterest { get; set; }
        /// <summary>
        /// ["<c>short_liq_usd</c>"] Short liquidation volume(quote currency)
        /// </summary>
        [JsonPropertyName("short_liq_usd")]
        public decimal ShortLiquidationUsd { get; set; }
        /// <summary>
        /// ["<c>mark_price</c>"] Mark price
        /// </summary>
        [JsonPropertyName("mark_price")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// ["<c>top_lsr_size</c>"] Top trader long/short position ratio
        /// </summary>
        [JsonPropertyName("top_lsr_size")]
        public decimal TopTraderLongShorPositionRatio { get; set; }
        /// <summary>
        /// ["<c>short_liq_amount</c>"] Short liquidation amount(base currency)
        /// </summary>
        [JsonPropertyName("short_liq_amount")]
        public decimal ShortLiquidationAmount { get; set; }
        /// <summary>
        /// ["<c>long_liq_amount</c>"] Long liquidation amount(base currency)
        /// </summary>
        [JsonPropertyName("long_liq_amount")]
        public decimal LongLiquidationAmount { get; set; }
        /// <summary>
        /// ["<c>open_interest_usd</c>"] Open interest volume(quote currency)
        /// </summary>
        [JsonPropertyName("open_interest_usd")]
        public decimal OpenInterestUsd { get; set; }
        /// <summary>
        /// ["<c>top_lsr_account</c>"] Top trader long/short account ratio
        /// </summary>
        [JsonPropertyName("top_lsr_account")]
        public decimal TopTraderLongShortAccountRatio { get; set; }
        /// <summary>
        /// ["<c>long_liq_usd</c>"] Long liquidation volume(quote currency)
        /// </summary>
        [JsonPropertyName("long_liq_usd")]
        public decimal LongLiquidationUsd { get; set; }
    }
}
