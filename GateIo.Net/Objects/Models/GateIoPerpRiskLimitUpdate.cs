using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Risk limit update
    /// </summary>
    [SerializationModel]
    public record GateIoPerpRiskLimitUpdate
    {
        /// <summary>
        /// Cancel orders
        /// </summary>
        [JsonPropertyName("cancel_orders")]
        public int CancelOrders { get; set; }
        /// <summary>
        /// Contract
        /// </summary>
        [JsonPropertyName("contract")]
        public string Contract { get; set; } = string.Empty;
        /// <summary>
        /// Max leverage
        /// </summary>
        [JsonPropertyName("leverage_max")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// Liquidation price
        /// </summary>
        [JsonPropertyName("liq_price")]
        public decimal LiquidationPrice { get; set; }
        /// <summary>
        /// Maintenance rate
        /// </summary>
        [JsonPropertyName("maintenance_rate")]
        public decimal MaintenanceRate { get; set; }
        /// <summary>
        /// Risk limit
        /// </summary>
        [JsonPropertyName("risk_limit")]
        public int RiskLimit { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("time_ms")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// User id
        /// </summary>
        [JsonPropertyName("user")]
        public long UserId { get; set; }
    }
}
