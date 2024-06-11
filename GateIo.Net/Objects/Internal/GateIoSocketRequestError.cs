using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Sockets
{
    internal class GateIoSocketRequestError
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("label")]
        public string Label { get; set; } = string.Empty;
    }
}
