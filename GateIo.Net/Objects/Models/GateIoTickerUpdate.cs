using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Ticker update
    /// </summary>
    [SerializationModel]
    public record GateIoTickerUpdate
    {
        /// <summary>
        /// ["<c>currency_pair</c>"] Symbol
        /// </summary>
        [JsonPropertyName("currency_pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>last</c>"] Last trade price
        /// </summary>
        [JsonPropertyName("last")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// ["<c>lowest_ask</c>"] Best ask price
        /// </summary>
        [JsonPropertyName("lowest_ask")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// ["<c>highest_bid</c>"] Best bid price
        /// </summary>
        [JsonPropertyName("highest_bid")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>change_percentage</c>"] Change percentage compared to 24h ago
        /// </summary>
        [JsonPropertyName("change_percentage")]
        public decimal ChangePercentage24h { get; set; }
        /// <summary>
        /// ["<c>base_volume</c>"] Volume in base asset
        /// </summary>
        [JsonPropertyName("base_volume")]
        public decimal BaseVolume { get; set; }
        /// <summary>
        /// ["<c>quote_volume</c>"] Volume in quote asset
        /// </summary>
        [JsonPropertyName("quote_volume")]
        public decimal QuoteVolume { get; set; }
        /// <summary>
        /// ["<c>high_24h</c>"] 24h high price
        /// </summary>
        [JsonPropertyName("high_24h")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// ["<c>low_24h</c>"] 24h low price
        /// </summary>
        [JsonPropertyName("low_24h")]
        public decimal LowPrice { get; set; }
    }
}
