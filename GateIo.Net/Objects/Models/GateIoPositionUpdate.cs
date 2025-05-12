using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;
using GateIo.Net.Enums;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Position update
    /// </summary>
    [SerializationModel]
    public record GateIoPositionUpdate
    {
        /// <summary>
        /// User id
        /// </summary>
        [JsonPropertyName("user")]
        public long UserId { get; set; }
        /// <summary>
        /// Update id
        /// </summary>
        [JsonPropertyName("update_id")]
        public long UpdateId { get; set; }
        /// <summary>
        /// Contract
        /// </summary>
        [JsonPropertyName("contract")]
        public string Contract { get; set; } = string.Empty;
        /// <summary>
        /// Position size
        /// </summary>
        [JsonPropertyName("size")]
        public long Size { get; set; }
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// Risk limit
        /// </summary>
        [JsonPropertyName("risk_limit")]
        public decimal RiskLimit { get; set; }
        /// <summary>
        /// Max leverage
        /// </summary>
        [JsonPropertyName("leverage_max")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// Maintenance rate
        /// </summary>
        [JsonPropertyName("maintenance_rate")]
        public decimal MaintenanceRate { get; set; }
        /// <summary>
        /// Margin
        /// </summary>
        [JsonPropertyName("margin")]
        public decimal Margin { get; set; }
        /// <summary>
        /// Entry price
        /// </summary>
        [JsonPropertyName("entry_price")]
        public decimal? EntryPrice { get; set; }
        /// <summary>
        /// Liquidation price
        /// </summary>
        [JsonPropertyName("liq_price")]
        public decimal? LiquidationPrice { get; set; }
        /// <summary>
        /// Realized profit and less
        /// </summary>
        [JsonPropertyName("realised_pnl")]
        public decimal? RealisedPnl { get; set; }
        /// <summary>
        /// History realized PNL
        /// </summary>
        [JsonPropertyName("history_pnl")]
        public decimal? HistoryPnl { get; set; }
        /// <summary>
        /// Last close pnl
        /// </summary>
        [JsonPropertyName("last_close_pnl")]
        public decimal? LastClosePnl { get; set; }
        /// <summary>
        /// Realized POINT PNL
        /// </summary>
        [JsonPropertyName("realised_point")]
        public decimal? RealsedPoinPnl { get; set; }
        /// <summary>
        /// History realized POINT PNL
        /// </summary>
        [JsonPropertyName("history_point")]
        public decimal? HistoryPoinPnl { get; set; }
        /// <summary>
        /// Position mode
        /// </summary>
        [JsonPropertyName("mode")]
        public PositionMode? PositionMode { get; set; }
        /// <summary>
        /// Cross margin leverage
        /// </summary>
        [JsonPropertyName("cross_leverage_limit")]
        public decimal? CrossLeverageLimit { get; set; }
    }
}
