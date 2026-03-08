using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Get GT deduction status
    /// </summary>
    [SerializationModel]
    public record GateIoGTDeducationStatus
    {
        /// <summary>
        /// ["<c>enabled</c>"] Is enabled
        /// </summary>
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }
    }
}
