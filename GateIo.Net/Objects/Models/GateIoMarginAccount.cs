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
        public decimal RiskRate { get; set; }
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

    /// <summary>
    /// Margin account asset
    /// </summary>
    [SerializationModel]
    public record GateIoMarginAccountAsset
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>available</c>"] Available quantity
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// ["<c>locked</c>"] Locked quantity
        /// </summary>
        [JsonPropertyName("locked")]
        public decimal Locked { get; set; }
        /// <summary>
        /// ["<c>borrowed</c>"] Borrowed quantity
        /// </summary>
        [JsonPropertyName("borrowed")]
        public decimal Borrowed { get; set; }
        /// <summary>
        /// ["<c>interest</c>"] Interest quantity
        /// </summary>
        [JsonPropertyName("interest")]
        public decimal Interest { get; set; }
    }
}
