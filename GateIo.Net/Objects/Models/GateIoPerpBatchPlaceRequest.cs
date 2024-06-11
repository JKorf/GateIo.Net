using GateIo.Net.Enums;
using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Collections.Generic;
using GateIo.Net.Objects.Internal;
using System;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Batch order placement item
    /// </summary>
    public record GateIoPerpBatchPlaceRequest
    {
        /// <summary>
        /// Contract
        /// </summary>
        [JsonPropertyName("contract")]
        public string Contract { get; set; } = string.Empty;
        /// <summary>
        /// Quantity, negaative for sell, positive for buy
        /// </summary>
        [JsonPropertyName("size")]
        public int Quantity { get; set; }
        /// <summary>
        /// Iceberg quantity
        /// </summary>
        [JsonPropertyName("iceberg")]
        public int IcebergQuantity { get; set; }
        /// <summary>
        /// Order price
        /// </summary>
        [JsonPropertyName("price"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? Price { get; set; }
        /// <summary>
        /// Time in force
        /// </summary>
        [JsonPropertyName("tif"), JsonConverter(typeof(EnumConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TimeInForce? TimeInForce { get; set; }
        /// <summary>
        /// Text
        /// </summary>
        [JsonPropertyName("text"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Text { get; set; }
        /// <summary>
        /// Close position
        /// </summary>
        [JsonPropertyName("close"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? ClosePosition { get; set; }
        /// <summary>
        /// Reduce only
        /// </summary>
        [JsonPropertyName("reduce_only"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? ReduceOnly { get; set; }
        /// <summary>
        /// Close side
        /// </summary>
        [JsonPropertyName("auto_size"), JsonConverter(typeof(EnumConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CloseSide? CloseSide { get; set; }
        /// <summary>
        /// Self trade prevention mode
        /// </summary>
        [JsonPropertyName("stp_act"), JsonConverter(typeof(EnumConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SelfTradePreventionMode? StpMode { get; set; }
    }
}
