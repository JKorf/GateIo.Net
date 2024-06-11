using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Internal
{
    internal class GateIoSocketLoginResponse
    {
        [JsonPropertyName("api_key")]
        public string ApiKey { get; set; } = string.Empty;
        [JsonPropertyName("uid")]
        public long UserId { get; set; }
    }
}
