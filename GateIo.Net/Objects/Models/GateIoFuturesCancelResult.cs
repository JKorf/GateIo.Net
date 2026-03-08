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
        /// ["<c>user_id</c>"] User id
        /// </summary>
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }
        /// <summary>
        /// ["<c>id</c>"] Order id
        /// </summary>
        [JsonPropertyName("id")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>succeeded</c>"] Succeeded
        /// </summary>
        [JsonPropertyName("succeeded")]
        public bool Success { get; set; }
        /// <summary>
        /// ["<c>message</c>"] Message
        /// </summary>
        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}
