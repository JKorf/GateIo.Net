using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Saved address
    /// </summary>
    [SerializationModel]
    public record GateIoSavedAddress
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Address
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// Network
        /// </summary>
        [JsonPropertyName("chain")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Tag
        /// </summary>
        [JsonPropertyName("tag")]
        public string Tag { get; set; } = string.Empty;
        /// <summary>
        /// Is verified
        /// </summary>
        [JsonPropertyName("verified")]
        public bool Verified { get; set; }
    }
}
