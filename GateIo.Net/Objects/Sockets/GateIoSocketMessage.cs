using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Sockets
{
    internal class GateIoSocketMessage<T>
    {
        [JsonPropertyName("time_ms")]
        public DateTime Timestamp { get; set; }
        [JsonPropertyName("channel")]
        public string Channel { get; set; } = string.Empty;
        [JsonPropertyName("event")]
        public string Event { get; set; } = string.Empty;
        [JsonPropertyName("payload")]
        public IEnumerable<object>? Payload { get; set; }
        [JsonPropertyName("error")]
        public GateIoSocketError? Error { get; set; }
        [JsonPropertyName("result")]
        public T Result { get; set; } = default!;
    }

    internal class GateIoSocketResponse<T>: GateIoSocketMessage<T>
    {

        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}
