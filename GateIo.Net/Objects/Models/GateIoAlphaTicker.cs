using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Price ticker
    /// </summary>
    public record GateIoAlphaTicker
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>last</c>"] Last trade price
        /// </summary>
        [JsonPropertyName("last")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// ["<c>change</c>"] Change percentage
        /// </summary>
        [JsonPropertyName("change")]
        public decimal ChangePercentage { get; set; }
        /// <summary>
        /// ["<c>volume</c>"] Trade volume in last 24h
        /// </summary>
        [JsonPropertyName("volume")]
        public decimal Volume { get; set; }
        /// <summary>
        /// ["<c>market_cap</c>"] Market cap
        /// </summary>
        [JsonPropertyName("market_cap")]
        public decimal MarketCap { get; set; }
    }
}
