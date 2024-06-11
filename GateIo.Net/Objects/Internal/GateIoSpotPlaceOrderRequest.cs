using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Internal
{
    internal class GateIoSpotPlaceOrderRequest
    {
        [JsonPropertyName("text"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Text { get; set; }
        [JsonPropertyName("currency_pair")]
        public string Symbol { get; set; } = string.Empty;
        [JsonPropertyName("type"), JsonConverter(typeof(EnumConverter))]
        public NewOrderType OrderType { get; set; }
        [JsonPropertyName("side"), JsonConverter(typeof(EnumConverter))]
        public OrderSide Side { get; set; }
        [JsonPropertyName("amount"), JsonConverter(typeof(DecimalStringWriterConverter))]
        public decimal Quantity { get; set; }
        [JsonPropertyName("price"), JsonConverter(typeof(DecimalStringWriterConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? Price { get; set; }
        [JsonPropertyName("time_in_force"), JsonConverter(typeof(EnumConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public TimeInForce? TimeInForce { get; set; }
        [JsonPropertyName("account"), JsonConverter(typeof(EnumConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public SpotAccountType? AccountType { get; set; }
        [JsonPropertyName("iceberg"), JsonConverter(typeof(DecimalStringWriterConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? Iceberg { get; set; }
        [JsonPropertyName("auto_borrow"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool? AutoBorrow { get; set; }
        [JsonPropertyName("auto_repay"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool? AutoRepay { get; set; }
        [JsonPropertyName("stp_act"), JsonConverter(typeof(EnumConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public SelfTradePreventionMode? StpMode { get; set; }
    }
}
