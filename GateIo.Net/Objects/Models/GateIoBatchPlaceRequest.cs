using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net.Enums;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Batch order placement request
    /// </summary>
    [SerializationModel]
    public record GateIoBatchPlaceRequest
    {
        /// <summary>
        /// The symbol the order is on
        /// </summary>
        [JsonPropertyName("currency_pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Text
        /// </summary>
        [JsonPropertyName("text")]
        public string? Text { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("type")]
        public NewOrderType Type { get; set; }
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Time in force
        /// </summary>
        [JsonPropertyName("time_in_force")]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount"), JsonConverter(typeof(DecimalStringWriterConverter))]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Order price
        /// </summary>
        [JsonPropertyName("price"), JsonConverter(typeof(DecimalStringWriterConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? Price { get; set; }
        /// <summary>
        /// Iceberg quantity
        /// </summary>
        [JsonPropertyName("iceberg"), JsonConverter(typeof(DecimalStringWriterConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? IcebergQuantity { get; set; }
        /// <summary>
        /// The type of account
        /// </summary>
        [JsonPropertyName("account")]
        public SpotAccountType AccountType { get; set; }
        /// <summary>
        /// Auto borrow
        /// </summary>
        [JsonPropertyName("auto_borrow"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? AutoBorrow { get; set; }
        /// <summary>
        /// Auto repay
        /// </summary>
        [JsonPropertyName("auto_repay"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? AutoRepay { get; set; }
        /// <summary>
        /// STP mode
        /// </summary>
        [JsonPropertyName("stp_act"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SelfTradePreventionMode? SelfTradePreventionMode { get; set; }
    }
}
