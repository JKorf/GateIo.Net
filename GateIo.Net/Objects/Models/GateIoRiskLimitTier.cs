using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Risk limit tier
    /// </summary>
    public record GateIoRiskLimitTier
    {
        /// <summary>
        /// Maintenance rate
        /// </summary>
        [JsonPropertyName("maintenance_rate")]
        public decimal MaintenanceRate { get; set; }
        /// <summary>
        /// Tier
        /// </summary>
        [JsonPropertyName("tier")]
        public int Tier { get; set; }
        /// <summary>
        /// Initial margin rate
        /// </summary>
        [JsonPropertyName("initial_rate")]
        public decimal InitialRate { get; set; }
        /// <summary>
        /// Max leverage
        /// </summary>
        [JsonPropertyName("leverage_max")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// Risk limit
        /// </summary>
        [JsonPropertyName("risk_limit")]
        public decimal RiskLimit { get; set; }
    }
}
