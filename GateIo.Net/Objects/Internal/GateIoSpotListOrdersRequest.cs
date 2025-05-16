using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Internal
{
    internal class GateIoSpotListOrdersRequest
    {
        [JsonPropertyName("currency_pair"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Symbol { get; set; }
        [JsonPropertyName("status"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Status { get; set; }
        [JsonPropertyName("page"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int? Page { get; set; }
        [JsonPropertyName("limit"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int? Limit { get; set; }
        [JsonPropertyName("from"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public long? From { get; set; }
        [JsonPropertyName("to"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public long? To { get; set; }
        [JsonPropertyName("account"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public SpotAccountType? Account { get; set; }
        [JsonPropertyName("side"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Side { get; set; }
    }
}
