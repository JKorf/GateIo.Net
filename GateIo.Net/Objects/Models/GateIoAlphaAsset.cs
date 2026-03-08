using GateIo.Net.Enums;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Asset info
    /// </summary>
    public record GateIoAlphaAsset
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset name
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>name</c>"] Asset full name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>chain</c>"] Network of the asset
        /// </summary>
        [JsonPropertyName("chain")]
        public string? Network { get; set; }
        /// <summary>
        /// ["<c>address</c>"] Token address
        /// </summary>
        [JsonPropertyName("address")]
        public string? Address { get; set; }
        /// <summary>
        /// ["<c>precision</c>"] Price precision
        /// </summary>
        [JsonPropertyName("precision")]
        public int PricePrecision { get; set; }
        /// <summary>
        /// ["<c>amount_precision</c>"] Quantity precision
        /// </summary>
        [JsonPropertyName("amount_precision")]
        public int QuantityPrecision { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Asset status
        /// </summary>
        [JsonPropertyName("status")]
        public AlphaAssetStatus Status { get; set; }
    }
}
