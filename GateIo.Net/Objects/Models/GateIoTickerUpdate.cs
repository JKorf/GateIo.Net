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
        /// Symbol
        /// </summary>
        [JsonPropertyName("currency_pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Last trade price
        /// </summary>
        [JsonPropertyName("last")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// Best ask price
        /// </summary>
        [JsonPropertyName("lowest_ask")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// Best bid price
        /// </summary>
        [JsonPropertyName("highest_bid")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// Change percentage compared to 24h ago
        /// </summary>
        [JsonPropertyName("change_percentage")]
        public decimal ChangePercentage24h { get; set; }
        /// <summary>
        /// Volume in base asset
        /// </summary>
        [JsonPropertyName("base_volume")]
        public decimal BaseVolume { get; set; }
        /// <summary>
        /// Volume in quote asset
        /// </summary>
        [JsonPropertyName("quote_volume")]
        public decimal QuoteVolume { get; set; }
        /// <summary>
        /// 24h high price
        /// </summary>
        [JsonPropertyName("high_24h")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// 24h low price
        /// </summary>
        [JsonPropertyName("low_24h")]
        public decimal LowPrice { get; set; }
    }
}
