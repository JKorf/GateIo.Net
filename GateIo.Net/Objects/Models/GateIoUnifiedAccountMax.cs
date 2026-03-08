using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Max borrowable info
    /// </summary>
    [SerializationModel]
    public record GateIoUnifiedAccountMax
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>currency_pair</c>"] Symbol
        /// </summary>
        [JsonPropertyName("currency_pair")]
        public string? Symbol { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] Max borrowable
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal MaxBorrowable { get; set; }
    }
}
