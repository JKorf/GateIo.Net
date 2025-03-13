using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Cancel result
    /// </summary>
    [SerializationModel]
    public record GateIoFuturesCancelResult
    {
        /// <summary>
        /// User id
        /// </summary>
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("id")]
        public long OrderId { get; set; }
        /// <summary>
        /// Succeeded
        /// </summary>
        [JsonPropertyName("succeeded")]
        public bool Success { get; set; }
        /// <summary>
        /// Message
        /// </summary>
        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}
