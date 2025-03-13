using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Internal
{
    internal class GateIoSpotCancelAllOrderRequest
    {
        [JsonPropertyName("currency_pair")]
        public string Symbol { get; set; } = string.Empty;
        [JsonPropertyName("side"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Side { get; set; }
        [JsonPropertyName("account_Type"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public SpotAccountType? AccountType { get; set; }
    }
}
