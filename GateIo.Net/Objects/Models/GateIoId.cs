using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Id reference
    /// </summary>
    public record GateIoId
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}
