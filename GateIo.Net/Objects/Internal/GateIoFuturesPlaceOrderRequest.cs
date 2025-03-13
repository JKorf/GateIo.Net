using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using GateIo.Net.Enums;

namespace GateIo.Net.Objects.Internal
{
    internal class GateIoFuturesPlaceOrderRequest
    {
        [JsonPropertyName("contract")]
        public string Contract { get; set; } = string.Empty;
        [JsonPropertyName("size")]
        public int Quantity { get; set; }
        [JsonPropertyName("price"), JsonConverter(typeof(DecimalStringWriterConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? Price { get; set; }
        [JsonPropertyName("iceberg"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int? Iceberg { get; set; }
        [JsonPropertyName("close"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool? Close { get; set; }
        [JsonPropertyName("reduce_only"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool? ReduceOnly { get; set; }
        [JsonPropertyName("time_in_force"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public TimeInForce? TimeInForce { get; set; }
        [JsonPropertyName("text"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Text { get; set; }
        [JsonPropertyName("auto_size"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public CloseSide? CloseSide { get; set; }
        [JsonPropertyName("stp_act"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public SelfTradePreventionMode? StpMode { get; set; }
    }
}
