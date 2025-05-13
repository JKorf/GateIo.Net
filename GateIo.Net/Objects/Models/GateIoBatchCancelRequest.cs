using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net.Enums;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Batch order cancellation request
    /// </summary>
    [SerializationModel]
    public record GateIoBatchCancelRequest
    {
        /// <summary>
        /// The symbol the order is on
        /// </summary>
        [JsonPropertyName("currency_pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The order id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// The type of account
        /// </summary>
        [JsonPropertyName("account"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SpotAccountType? AccountType { get; set; }
    }
}
