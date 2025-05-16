using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Cancel result info
    /// </summary>
    [SerializationModel]
    public record GateIoCancelResult
    {
        /// <summary>
        /// Symbol 
        /// </summary>
        [JsonPropertyName("currency_pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Order info
        /// </summary>
        [JsonPropertyName("text")]
        public string? Text { get; set; }

        /// <summary>
        /// Whether the operation succeeded
        /// </summary>
        [JsonPropertyName("succeeded")]
        public bool Succeeded { get; set; }

        /// <summary>
        /// Error code when operation failed
        /// </summary>
        [JsonPropertyName("label")]
        public string? ErrorCode { get; set; }
        /// <summary>
        /// Error message when operation failed
        /// </summary>
        [JsonPropertyName("message")]
        public string? ErrorMessage { get; set; }
    }
}
