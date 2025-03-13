using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Small balance info
    /// </summary>
    [SerializationModel]
    public record GateIoSmallBalance
    {
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Available balance
        /// </summary>
        [JsonPropertyName("available_balance")]
        public decimal AvailableBalance { get; set; }
        /// <summary>
        /// Estimated value in BTC
        /// </summary>
        [JsonPropertyName("estimated_as_btc")]
        public decimal BtcValue { get; set; }
        /// <summary>
        /// Estimated value in GT
        /// </summary>
        [JsonPropertyName("convertible_to_gt")]
        public decimal GtValue { get; set; }
    }
}
