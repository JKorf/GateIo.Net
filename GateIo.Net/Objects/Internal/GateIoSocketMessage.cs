using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Internal
{
    internal class GateIoSocketMessage
    {
        [JsonPropertyName("time_ms")]
        public DateTime Timestamp { get; set; }
        [JsonPropertyName("channel")]
        public string Channel { get; set; } = string.Empty;
        [JsonPropertyName("event")]
        public string Event { get; set; } = string.Empty;
        [JsonPropertyName("payload")]
        public object[]? Payload { get; set; }
        [JsonPropertyName("error")]
        public GateIoSocketError? Error { get; set; }
    }

    internal class GateIoSocketMessage<T> : GateIoSocketMessage
    {
        [JsonPropertyName("result")]
        public T Result { get; set; } = default!;
    }

    internal class GateIoSocketResponse<T> : GateIoSocketMessage<T>
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}
