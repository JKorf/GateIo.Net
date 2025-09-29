using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net.Enums;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Margin account
    /// </summary>
    [SerializationModel]
    public record GateIoIsolatedMarginAccount
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
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal? Leverage { get; set; }
        /// <summary>
        /// Maintenance margin rate
        /// </summary>
        [JsonPropertyName("mmr")]
        public decimal? MaintenanceMarginRate { get; set; }
        /// <summary>
        /// Account type
        /// </summary>
        [JsonPropertyName("account_type")]
        public MarginAccountType AccountType { get; set; }
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
}
