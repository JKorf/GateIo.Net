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
        /// ["<c>cancel_orders</c>"] Cancel orders
        /// </summary>
        [JsonPropertyName("cancel_orders")]
        public int CancelOrders { get; set; }
        /// <summary>
        /// ["<c>contract</c>"] Contract
        /// </summary>
        [JsonPropertyName("contract")]
        public string Contract { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>leverage_max</c>"] Max leverage
        /// </summary>
        [JsonPropertyName("leverage_max")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// ["<c>liq_price</c>"] Liquidation price
        /// </summary>
        [JsonPropertyName("liq_price")]
        public decimal LiquidationPrice { get; set; }
        /// <summary>
        /// ["<c>maintenance_rate</c>"] Maintenance rate
        /// </summary>
        [JsonPropertyName("maintenance_rate")]
        public decimal MaintenanceRate { get; set; }
        /// <summary>
        /// ["<c>risk_limit</c>"] Risk limit
        /// </summary>
        [JsonPropertyName("risk_limit")]
        public int RiskLimit { get; set; }
        /// <summary>
        /// ["<c>time_ms</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("time_ms")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>user</c>"] User id
        /// </summary>
        [JsonPropertyName("user")]
        public long UserId { get; set; }
    }
}
