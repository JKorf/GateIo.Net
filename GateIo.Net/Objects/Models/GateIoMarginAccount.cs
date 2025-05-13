using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Margin account
    /// </summary>
    [SerializationModel]
    public record GateIoMarginAccount
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("currency_pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Account is locked
        /// </summary>
        [JsonPropertyName("locked")]
        public bool Locked { get; set; }
        /// <summary>
        /// Current risk rate
        /// </summary>
        [JsonPropertyName("risk")]
        public decimal RiskRate { get; set; }
        /// <summary>
        /// Base asset
        /// </summary>
        [JsonPropertyName("base")]
        public GateIoMarginAccountAsset Base { get; set; } = null!;
        /// <summary>
        /// Quote asset
        /// </summary>
        [JsonPropertyName("quote")]
        public GateIoMarginAccountAsset Quote { get; set; } = null!;
    }

    /// <summary>
    /// Margin account asset
    /// </summary>
    [SerializationModel]
    public record GateIoMarginAccountAsset
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Available quantity
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// Locked quantity
        /// </summary>
        [JsonPropertyName("locked")]
        public decimal Locked { get; set; }
        /// <summary>
        /// Borrowed quantity
        /// </summary>
        [JsonPropertyName("borrowed")]
        public decimal Borrowed { get; set; }
        /// <summary>
        /// Interest quantity
        /// </summary>
        [JsonPropertyName("interest")]
        public decimal Interest { get; set; }
    }
}
