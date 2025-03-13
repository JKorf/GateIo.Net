using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Cancel after results
    /// </summary>
    [SerializationModel]
    public record GateIoCancelAfter
    {
        /// <summary>
        /// Time the cancellation is triggered
        /// </summary>
        [JsonPropertyName("triggerTime")]
        public DateTime TriggerTime { get; set; }
    }
}
