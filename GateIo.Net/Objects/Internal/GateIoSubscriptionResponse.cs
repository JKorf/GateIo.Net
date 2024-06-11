using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Internal
{
    internal class GateIoSubscriptionResponse
    {
        public bool Success => string.Equals(Status, "success");

        [JsonPropertyName("success")]
        public string Status { get; set; } = string.Empty;
    }
}
