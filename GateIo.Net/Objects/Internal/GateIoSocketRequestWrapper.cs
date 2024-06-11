using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Internal
{
    internal class GateIoSocketRequestWrapper
    {
        [JsonPropertyName("req_id")]
        public string Id { get; set; } = string.Empty;
    }

    internal class GateIoSocketRequestWrapper<T> : GateIoSocketRequestWrapper
    {
        [JsonPropertyName("req_param")]
        public T? Parameters { get; set; }
    }
}
