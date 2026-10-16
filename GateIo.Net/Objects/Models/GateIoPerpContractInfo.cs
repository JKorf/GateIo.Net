using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Contract info
    /// </summary>
    public record GateIoPerpContractInfo
    {
        /// <summary>
        /// ["<c>contract</c>"] Contract
        /// </summary>
        [JsonPropertyName("contract")]
        public string Contract { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quanto_multiplier</c>"] Quanto multiplier
        /// </summary>
        [JsonPropertyName("quanto_multiplier")]
        public decimal QuantoMultiplier { get; set; }
        /// <summary>
        /// ["<c>order_price_round</c>"] Order price precision
        /// </summary>
        [JsonPropertyName("order_price_round")]
        public decimal OrderPricePrecision { get; set; }
        /// <summary>
        /// ["<c>mark_price_round</c>"] Mark price precision
        /// </summary>
        [JsonPropertyName("mark_price_round")]
        public decimal MarkPricePrecision { get; set; }        
        /// <summary>
        /// ["<c>risk_limit_tiers</c>"] Risk limit tiers
        /// </summary>
        [JsonPropertyName("risk_limit_tiers")]
        public GateIoRiskLimitTier[]? RiskLimitTiers { get; set; }
    }
}
