using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Funding account
    /// </summary>
    [SerializationModel]
    public record GateIoMarginFundingAccount
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>available</c>"] Available
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// ["<c>locked</c>"] Locked
        /// </summary>
        [JsonPropertyName("locked")]
        public decimal Locked { get; set; }
        /// <summary>
        /// ["<c>lent</c>"] Lent
        /// </summary>
        [JsonPropertyName("lent")]
        public decimal Lent { get; set; }
        /// <summary>
        /// ["<c>total_lent</c>"] Total lent
        /// </summary>
        [JsonPropertyName("total_lent")]
        public decimal TotalLent { get; set; }
    }
}
