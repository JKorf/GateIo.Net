using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Transferable
    /// </summary>
    [SerializationModel]
    public record GateIoMarginMaxTransferable
    {
        /// <summary>
        /// Asset 
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("currency_pair")]
        public string? Symbol { get; set; }
        /// <summary>
        /// Max transferable
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal MaxTransferable { get; set; }
    }
}
