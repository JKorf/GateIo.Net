using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Ticker info
    /// </summary>
    [SerializationModel]
    public record GateIoPerpTicker
    {
        /// <summary>
        /// ["<c>contract</c>"] Contract
        /// </summary>
        [JsonPropertyName("contract")]
        public string Contract { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>last</c>"] Last price
        /// </summary>
        [JsonPropertyName("last")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// ["<c>low_24h</c>"] 24h low price
        /// </summary>
        [JsonPropertyName("low_24h")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// ["<c>high_24h</c>"] 24h high price
        /// </summary>
        [JsonPropertyName("high_24h")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// ["<c>change_percentage</c>"] 24h Change percentage
        /// </summary>
        [JsonPropertyName("change_percentage")]
        public decimal ChangePercentage { get; set; }
        /// <summary>
        /// ["<c>total_size</c>"] Total contract size
        /// </summary>
        [JsonPropertyName("total_size")]
        public decimal TotalSize { get; set; }
        /// <summary>
        /// ["<c>volume_24h</c>"] 24h Volume
        /// </summary>
        [JsonPropertyName("volume_24h")]
        public decimal Volume { get; set; }
        /// <summary>
        /// ["<c>volume_24h_btc</c>"] 24h Volume in BTC
        /// </summary>
        [JsonPropertyName("volume_24h_btc")]
        public decimal VolumeBtc { get; set; }
        /// <summary>
        /// ["<c>volume_24h_usd</c>"] 24h Volume in USD
        /// </summary>
        [JsonPropertyName("volume_24h_usd")]
        public decimal VolumeUsd { get; set; }
        /// <summary>
        /// ["<c>volume_24h_base</c>"] 24h Base asset volume
        /// </summary>
        [JsonPropertyName("volume_24h_base")]
        public decimal BaseVolume { get; set; }
        /// <summary>
        /// ["<c>volume_24h_quote</c>"] 24h Quote asset volume
        /// </summary>
        [JsonPropertyName("volume_24h_quote")]
        public decimal QuoteVolume { get; set; }
        /// <summary>
        /// ["<c>volume_24h_settle</c>"] 24h Settle asset volume
        /// </summary>
        [JsonPropertyName("volume_24h_settle")]
        public decimal SettleVolume { get; set; }
        /// <summary>
        /// ["<c>mark_price</c>"] Mark price
        /// </summary>
        [JsonPropertyName("mark_price")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// ["<c>funding_rate</c>"] Funding rate
        /// </summary>
        [JsonPropertyName("funding_rate")]
        public decimal FundingRate { get; set; }
        /// <summary>
        /// ["<c>funding_rate_indicative</c>"] Funding rate indicative
        /// </summary>
        [JsonPropertyName("funding_rate_indicative")]
        public decimal IndicativeFundingRate { get; set; }
        /// <summary>
        /// ["<c>index_price</c>"] Index price
        /// </summary>
        [JsonPropertyName("index_price")]
        public decimal IndexPrice { get; set; }
        /// <summary>
        /// ["<c>highest_bid</c>"] Best ask price
        /// </summary>
        [JsonPropertyName("highest_bid")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>highest_size</c>"] Best ask price quantity
        /// </summary>
        [JsonPropertyName("highest_size")]
        public decimal BestBidQuantity { get; set; }
        /// <summary>
        /// ["<c>lowest_ask</c>"] Best bid price
        /// </summary>
        [JsonPropertyName("lowest_ask")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// ["<c>lowest_size</c>"] Best bid price quantity
        /// </summary>
        [JsonPropertyName("lowest_size")]
        public decimal BestAskQuantity { get; set; }
    }
}
