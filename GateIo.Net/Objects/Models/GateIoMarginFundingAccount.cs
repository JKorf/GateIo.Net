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
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Available
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// Locked
        /// </summary>
        [JsonPropertyName("locked")]
        public decimal Locked { get; set; }
        /// <summary>
        /// Lent
        /// </summary>
        [JsonPropertyName("lent")]
        public decimal Lent { get; set; }
        /// <summary>
        /// Total lent
        /// </summary>
        [JsonPropertyName("total_lent")]
        public decimal TotalLent { get; set; }
    }
}
