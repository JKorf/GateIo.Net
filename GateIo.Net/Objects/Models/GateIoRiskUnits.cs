using CryptoExchange.Net.Converters.SystemTextJson;
using System;
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
        /// ["<c>user_id</c>"] User id
        /// </summary>
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }
        /// <summary>
        /// ["<c>spot_hedge</c>"] Spot hedge status
        /// </summary>
        [JsonPropertyName("spot_hedge")]
        public bool SpotHedge { get; set; }
        /// <summary>
        /// ["<c>risk_units</c>"] Risk units
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
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>spot_in_use</c>"] Spot hedging usage
        /// </summary>
        [JsonPropertyName("spot_in_use")]
        public decimal SpotInUse { get; set; }
        /// <summary>
        /// ["<c>maintain_margin</c>"] Maintenance margin
        /// </summary>
        [JsonPropertyName("maintain_margin")]
        public decimal MaintenanceMargin { get; set; }
        /// <summary>
        /// ["<c>initial_margin</c>"] Initial margin
        /// </summary>
        [JsonPropertyName("initial_margin")]
        public decimal InitialMargin { get; set; }
        /// <summary>
        /// ["<c>delta</c>"] Delta
        /// </summary>
        [JsonPropertyName("delta")]
        public decimal Delta { get; set; }
        /// <summary>
        /// ["<c>gamma</c>"] Gamma
        /// </summary>
        [JsonPropertyName("gamma")]
        public decimal Gamma { get; set; }
        /// <summary>
        /// ["<c>theta</c>"] Theta
        /// </summary>
        [JsonPropertyName("theta")]
        public decimal Theta { get; set; }
        /// <summary>
        /// ["<c>vega</c>"] Vega
        /// </summary>
        [JsonPropertyName("vega")]
        public decimal Vega { get; set; }
    }
}
