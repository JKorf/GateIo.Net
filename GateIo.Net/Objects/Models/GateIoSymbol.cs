using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Symbol info
    /// </summary>
    [SerializationModel]
    public record GateIoSymbol
    {
        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("id")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Base asset
        /// </summary>
        [JsonPropertyName("base")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// Base asset name
        /// </summary>
        [JsonPropertyName("base_name")]
        public string BaseAssetName { get; set; } = string.Empty;
        /// <summary>
        /// Quote asset
        /// </summary>
        [JsonPropertyName("quote")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// Quote asset name
        /// </summary>
        [JsonPropertyName("quote_name")]
        public string QuoteAssetName { get; set; } = string.Empty;
        /// <summary>
        /// Trade fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal TradeFee { get; set; }
        /// <summary>
        /// Min base asset order quantity
        /// </summary>
        [JsonPropertyName("min_base_amount")]
        public decimal MinBaseQuantity { get; set; }
        /// <summary>
        /// Min quote asset order quantity
        /// </summary>
        [JsonPropertyName("min_quote_amount")]
        public decimal MinQuoteQuantity { get; set; }
        /// <summary>
        /// Max base asset order quantity
        /// </summary>
        [JsonPropertyName("max_base_amount")]
        public decimal? MaxBaseQuantity { get; set; }
        /// <summary>
        /// Max quote asset order quantity
        /// </summary>
        [JsonPropertyName("max_quote_amount")]
        public decimal MaxQuoteQuantity { get; set; }
        /// <summary>
        /// Quantity decimal precision
        /// </summary>
        [JsonPropertyName("amount_precision")]
        public int QuantityPrecision { get; set; }
        /// <summary>
        /// Price decimal precision
        /// </summary>
        [JsonPropertyName("precision")]
        public int PricePrecision { get; set; }
        /// <summary>
        /// Trading status
        /// </summary>
        [JsonPropertyName("trade_status")]
        public SymbolStatus TradeStatus { get; set; }
        /// <summary>
        /// Sell start time
        /// </summary>
        [JsonPropertyName("sell_start")]
        public DateTime SellStart { get; set; }
        /// <summary>
        /// Buy start time
        /// </summary>
        [JsonPropertyName("buy_start")]
        public DateTime BuyStart { get; set; }
        /// <summary>
        /// Status of the market
        /// </summary>
        [JsonPropertyName("type")]
        public PreMarketStatus PreMarketStatus { get; set; }
        /// <summary>
        /// Time of delisting
        /// </summary>
        [JsonPropertyName("delisting_time")]
        public DateTime? DelistTime { get; set; }
        /// <summary>
        /// Trade link
        /// </summary>
        [JsonPropertyName("trade_url")]
        public string TradeUrl { get; set; } = string.Empty;
    }
}
