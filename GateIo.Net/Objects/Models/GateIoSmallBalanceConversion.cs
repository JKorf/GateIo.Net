using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Small balance conversion
    /// </summary>
    [SerializationModel]
    public record GateIoSmallBalanceConversion
    {
        /// <summary>
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>currency</c>"] Asset name
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>create_time</c>"] Create time
        /// </summary>
        [JsonPropertyName("create_time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] Input amount
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>gt_amount</c>"] Output GT
        /// </summary>
        [JsonPropertyName("gt_amount")]
        public decimal GtQuantity { get; set; }
    }
}
