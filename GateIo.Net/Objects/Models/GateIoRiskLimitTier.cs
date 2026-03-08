using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Risk limit tier
    /// </summary>
    [SerializationModel]
    public record GateIoRiskLimitTier
    {
        /// <summary>
        /// ["<c>maintenance_rate</c>"] Maintenance rate
        /// </summary>
        [JsonPropertyName("maintenance_rate")]
        public decimal MaintenanceRate { get; set; }
        /// <summary>
        /// ["<c>tier</c>"] Tier
        /// </summary>
        [JsonPropertyName("tier")]
        public int Tier { get; set; }
        /// <summary>
        /// ["<c>initial_rate</c>"] Initial margin rate
        /// </summary>
        [JsonPropertyName("initial_rate")]
        public decimal InitialRate { get; set; }
        /// <summary>
        /// ["<c>leverage_max</c>"] Max leverage
        /// </summary>
        [JsonPropertyName("leverage_max")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// ["<c>risk_limit</c>"] Risk limit
        /// </summary>
        [JsonPropertyName("risk_limit")]
        public decimal RiskLimit { get; set; }
        /// <summary>
        /// ["<c>deduction</c>"] Maintenance margin quick calculation deduction
        /// </summary>
        [JsonPropertyName("deduction")]
        public decimal? Deduction { get; set; }
    }
}
