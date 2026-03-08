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
        /// ["<c>currency_pair</c>"] Symbol
        /// </summary>
        [JsonPropertyName("currency_pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>locked</c>"] Account is locked
        /// </summary>
        [JsonPropertyName("locked")]
        public bool Locked { get; set; }
        /// <summary>
        /// ["<c>risk</c>"] Current risk rate
        /// </summary>
        [JsonPropertyName("risk")]
        public decimal? RiskRate { get; set; }
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal? Leverage { get; set; }
        /// <summary>
        /// ["<c>mmr</c>"] Maintenance margin rate
        /// </summary>
        [JsonPropertyName("mmr")]
        public decimal? MaintenanceMarginRate { get; set; }
        /// <summary>
        /// ["<c>account_type</c>"] Account type
        /// </summary>
        [JsonPropertyName("account_type")]
        public MarginAccountType AccountType { get; set; }
        /// <summary>
        /// ["<c>base</c>"] Base asset
        /// </summary>
        [JsonPropertyName("base")]
        public GateIoMarginAccountAsset Base { get; set; } = null!;
        /// <summary>
        /// ["<c>quote</c>"] Quote asset
        /// </summary>
        [JsonPropertyName("quote")]
        public GateIoMarginAccountAsset Quote { get; set; } = null!;
    }
}
