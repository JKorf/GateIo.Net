using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Internal
{
    internal class GateIoSpotAmendOrderRequest
    {
        [JsonPropertyName("amend_text"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? AmendText { get; set; }
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        [JsonPropertyName("currency_pair")]
        public string Symbol { get; set; } = string.Empty;
        [JsonPropertyName("amount"), JsonConverter(typeof(DecimalStringWriterConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? Quantity { get; set; }
        [JsonPropertyName("price"), JsonConverter(typeof(DecimalStringWriterConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? Price { get; set; }
        [JsonPropertyName("account"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public SpotAccountType? AccountType { get; set; }
    }
}
