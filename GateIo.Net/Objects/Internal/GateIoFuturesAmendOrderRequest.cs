using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Internal
{
    internal class GateIoFuturesAmendOrderRequest
    {
        [JsonPropertyName("amend_text"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? AmendText { get; set; }
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        [JsonPropertyName("size"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int? Quantity { get; set; }
        [JsonPropertyName("price"), JsonConverter(typeof(DecimalStringWriterConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? Price { get; set; }
    }
}
