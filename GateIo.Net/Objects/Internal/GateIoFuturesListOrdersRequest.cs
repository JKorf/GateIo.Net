using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Internal
{
    internal class GateIoFuturesListOrdersRequest
    {
        [JsonPropertyName("contract"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Contract { get; set; }
        [JsonPropertyName("status"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Status { get; set; }
        [JsonPropertyName("limit"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int? Limit { get; set; }
        [JsonPropertyName("offset"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int? Offset { get; set; }
        [JsonPropertyName("last_id"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? LastId { get; set; }
    }
}
