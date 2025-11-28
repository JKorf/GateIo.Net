using GateIo.Net.Objects.Sockets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Internal
{
    internal class GateIoSocketRequestResponse
    {
        [JsonPropertyName("header")]
        public GateIoSocketRequestResponseHeader Header { get; set; } = null!;
        [JsonPropertyName("request_id")]
        public string RequestId { get; set; } = string.Empty;
        [JsonPropertyName("ack")]
        public bool Acknowledge { get; set; }
    }

    internal class GateIoSocketRequestResponse<T> : GateIoSocketRequestResponse
    {
        [JsonPropertyName("data")]
        public GateIoQueryResponseData<T> Data { get; set; } = null!;
    }

    internal class GateIoSocketRequestResponseHeader
    {
        [JsonPropertyName("response_Time")]
        public DateTime ResponseTime { get; set; }
        [JsonPropertyName("status")]
        public int Status { get; set; }
        [JsonPropertyName("channel")]
        public string Channel { get; set; } = string.Empty;
        [JsonPropertyName("event")]
        public string Event { get; set; } = string.Empty;
        [JsonPropertyName("client_id")]
        public string ClientId { get; set; } = string.Empty;
    }

    internal class GateIoQueryResponseData<T>
    {
        [JsonPropertyName("result")]
        public T? Result { get; set; }

        [JsonPropertyName("errs")]
        public GateIoSocketRequestError? Error { get; set; }
    }
}
