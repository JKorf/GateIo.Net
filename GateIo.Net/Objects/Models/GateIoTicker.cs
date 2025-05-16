using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Asset info
    /// </summary>
    [SerializationModel]
    public record GateIoTicker
    {
        /// <summary>
        /// Symbol name
        /// </summary>
        [JsonPropertyName("currency_pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Last traded price
        /// </summary>
        [JsonPropertyName("last")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// Best ask price
        /// </summary>
        [JsonPropertyName("lowest_ask")]
        public decimal? BestAskPrice { get; set; }
        /// <summary>
        /// Best ask quantity
        /// </summary>
        [JsonPropertyName("lowest_size")]
        public decimal? BestAskQuantity { get; set; }
        /// <summary>
        /// Best bid price
        /// </summary>
        [JsonPropertyName("highest_bid")]
        public decimal? BestBidPrice { get; set; }
        /// <summary>
        /// Best bid quantity
        /// </summary>
        [JsonPropertyName("highest_size")]
        public decimal? BestBidQuantity { get; set; }
        /// <summary>
        /// Change percentage last 24h
        /// </summary>
        [JsonPropertyName("change_percentage")]
        public decimal ChangePercentage24h { get; set; }
        /// <summary>
        /// Change percentage UTC+0 timezone
        /// </summary>
        [JsonPropertyName("change_utc0")]
        public decimal ChangePercentageUtc0 { get; set; }
        /// <summary>
        /// Change percentage UTC+8 timezone
        /// </summary>
        [JsonPropertyName("change_utc8")]
        public decimal ChangePercentageUtc8 { get; set; }
        /// <summary>
        /// Volume last 24h in the base asset
        /// </summary>
        [JsonPropertyName("base_volume")]
        public decimal BaseVolume { get; set; }
        /// <summary>
        /// Volume last 24h in the quote asset
        /// </summary>
        [JsonPropertyName("quote_volume")]
        public decimal QuoteVolume { get; set; }
        /// <summary>
        /// Highest price in last 24h
        /// </summary>
        [JsonPropertyName("high_24h")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// Lowest price in last 24h
        /// </summary>
        [JsonPropertyName("low_24h")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// ETF Net value
        /// </summary>
        [JsonPropertyName("etf_net_value")]
        public decimal EtfNetValue { get; set; }
        /// <summary>
        /// ETF previous net value
        /// </summary>
        [JsonPropertyName("etf_pre_net_value")]
        public decimal? EtfPrevNetValue { get; set; }
        /// <summary>
        /// ETF previous rebalance time
        /// </summary>
        [JsonPropertyName("etf_pre_timestamp")]
        public DateTime? EtfPrevRebalanceTime { get; set; }
        /// <summary>
        /// ETF current leverage
        /// </summary>
        [JsonPropertyName("etf_leverage")]
        public decimal? EtfLeverage { get; set; }
    }
}
