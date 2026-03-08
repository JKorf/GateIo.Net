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
        /// ["<c>currency_pair</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("currency_pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>last</c>"] Last traded price
        /// </summary>
        [JsonPropertyName("last")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// ["<c>lowest_ask</c>"] Best ask price
        /// </summary>
        [JsonPropertyName("lowest_ask")]
        public decimal? BestAskPrice { get; set; }
        /// <summary>
        /// ["<c>lowest_size</c>"] Best ask quantity
        /// </summary>
        [JsonPropertyName("lowest_size")]
        public decimal? BestAskQuantity { get; set; }
        /// <summary>
        /// ["<c>highest_bid</c>"] Best bid price
        /// </summary>
        [JsonPropertyName("highest_bid")]
        public decimal? BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>highest_size</c>"] Best bid quantity
        /// </summary>
        [JsonPropertyName("highest_size")]
        public decimal? BestBidQuantity { get; set; }
        /// <summary>
        /// ["<c>change_percentage</c>"] Change percentage last 24h
        /// </summary>
        [JsonPropertyName("change_percentage")]
        public decimal ChangePercentage24h { get; set; }
        /// <summary>
        /// ["<c>change_utc0</c>"] Change percentage UTC+0 timezone
        /// </summary>
        [JsonPropertyName("change_utc0")]
        public decimal ChangePercentageUtc0 { get; set; }
        /// <summary>
        /// ["<c>change_utc8</c>"] Change percentage UTC+8 timezone
        /// </summary>
        [JsonPropertyName("change_utc8")]
        public decimal ChangePercentageUtc8 { get; set; }
        /// <summary>
        /// ["<c>base_volume</c>"] Volume last 24h in the base asset
        /// </summary>
        [JsonPropertyName("base_volume")]
        public decimal BaseVolume { get; set; }
        /// <summary>
        /// ["<c>quote_volume</c>"] Volume last 24h in the quote asset
        /// </summary>
        [JsonPropertyName("quote_volume")]
        public decimal QuoteVolume { get; set; }
        /// <summary>
        /// ["<c>high_24h</c>"] Highest price in last 24h
        /// </summary>
        [JsonPropertyName("high_24h")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// ["<c>low_24h</c>"] Lowest price in last 24h
        /// </summary>
        [JsonPropertyName("low_24h")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// ["<c>etf_net_value</c>"] ETF Net value
        /// </summary>
        [JsonPropertyName("etf_net_value")]
        public decimal EtfNetValue { get; set; }
        /// <summary>
        /// ["<c>etf_pre_net_value</c>"] ETF previous net value
        /// </summary>
        [JsonPropertyName("etf_pre_net_value")]
        public decimal? EtfPrevNetValue { get; set; }
        /// <summary>
        /// ["<c>etf_pre_timestamp</c>"] ETF previous rebalance time
        /// </summary>
        [JsonPropertyName("etf_pre_timestamp")]
        public DateTime? EtfPrevRebalanceTime { get; set; }
        /// <summary>
        /// ["<c>etf_leverage</c>"] ETF current leverage
        /// </summary>
        [JsonPropertyName("etf_leverage")]
        public decimal? EtfLeverage { get; set; }
    }
}
