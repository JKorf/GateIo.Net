using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Risk units
    /// </summary>
    [SerializationModel]
    public record GateIoRiskUnits
    {
        /// <summary>
        /// User id
        /// </summary>
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }
        /// <summary>
        /// Spot hedge status
        /// </summary>
        [JsonPropertyName("spot_hedge")]
        public bool SpotHedge { get; set; }
        /// <summary>
        /// Risk units
        /// </summary>
        [JsonPropertyName("risk_units")]
        public GateIoRiskUnitsDetails[] RiskUnits { get; set; } = Array.Empty<GateIoRiskUnitsDetails>();
    }

    /// <summary>
    /// Risk unit details
    /// </summary>
    [SerializationModel]
    public record GateIoRiskUnitsDetails
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Spot hedging usage
        /// </summary>
        [JsonPropertyName("spot_in_use")]
        public decimal SpotInUse { get; set; }
        /// <summary>
        /// Maintenance margin
        /// </summary>
        [JsonPropertyName("maintain_margin")]
        public decimal MaintenanceMargin { get; set; }
        /// <summary>
        /// Initial margin
        /// </summary>
        [JsonPropertyName("initial_margin")]
        public decimal InitialMargin { get; set; }
        /// <summary>
        /// Delta
        /// </summary>
        [JsonPropertyName("delta")]
        public decimal Delta { get; set; }
        /// <summary>
        /// Gamma
        /// </summary>
        [JsonPropertyName("gamma")]
        public decimal Gamma { get; set; }
        /// <summary>
        /// Theta
        /// </summary>
        [JsonPropertyName("theta")]
        public decimal Theta { get; set; }
        /// <summary>
        /// Vega
        /// </summary>
        [JsonPropertyName("vega")]
        public decimal Vega { get; set; }
    }
}
