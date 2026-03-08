using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;
using System;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Balance info
    /// </summary>
    [SerializationModel]
    public record GateIoBalance
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset name
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>available</c>"] Available quantity
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// ["<c>locked</c>"] Locked quantity
        /// </summary>
        [JsonPropertyName("locked")]
        public decimal Locked { get; set; }
        /// <summary>
        /// ["<c>update_id</c>"] Update id
        /// </summary>
        [JsonPropertyName("update_id")]
        public long UpdateId { get; set; }
        /// <summary>
        /// ["<c>refresh_time</c>"] Update time
        /// </summary>
        [JsonPropertyName("refresh_time")]
        public DateTime? UpdateTime { get; set; }
    }
}
