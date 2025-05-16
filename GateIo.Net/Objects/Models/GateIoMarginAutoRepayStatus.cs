using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Repay status
    /// </summary>
    [SerializationModel]
    public record GateIoMarginAutoRepayStatus
    {
        /// <summary>
        /// Status, on or off
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
    }
}
