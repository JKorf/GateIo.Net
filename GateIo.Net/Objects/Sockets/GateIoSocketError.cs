using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Sockets
{
    internal class GateIoSocketError
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
    }
}
