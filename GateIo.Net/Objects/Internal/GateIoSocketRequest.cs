using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Internal
{
    internal class GateIoSocketRequest<T>
    {
        [JsonPropertyName("time")]
        public long Timestamp { get; set; }
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("channel")]
        public string Channel { get; set; } = string.Empty;
        [JsonPropertyName("event")]
        public string Event { get; set; } = string.Empty;
        [JsonPropertyName("payload"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Payload { get; set; }
    }

    internal class GateIoSocketAuthRequest<T> : GateIoSocketRequest<T>
    {
        [JsonPropertyName("auth"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public GateIoSocketAuth? Auth { get; set; }
    }

    internal class GateIoSocketAuth
    {
        [JsonPropertyName("method")]
        public string Method { get; set; } = string.Empty;
        [JsonPropertyName("KEY")]
        public string Key { get; set; } = string.Empty;
        [JsonPropertyName("SIGN")]
        public string Sign { get; set; } = string.Empty;
    }
}
