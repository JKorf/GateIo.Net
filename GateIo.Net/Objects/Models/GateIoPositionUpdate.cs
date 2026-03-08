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
        /// ["<c>user</c>"] User id
        /// </summary>
        [JsonPropertyName("user")]
        public long UserId { get; set; }
        /// <summary>
        /// ["<c>update_id</c>"] Update id
        /// </summary>
        [JsonPropertyName("update_id")]
        public long UpdateId { get; set; }
        /// <summary>
        /// ["<c>contract</c>"] Contract
        /// </summary>
        [JsonPropertyName("contract")]
        public string Contract { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>size</c>"] Position size
        /// </summary>
        [JsonPropertyName("size")]
        public long Size { get; set; }
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// ["<c>risk_limit</c>"] Risk limit
        /// </summary>
        [JsonPropertyName("risk_limit")]
        public decimal RiskLimit { get; set; }
        /// <summary>
        /// ["<c>leverage_max</c>"] Max leverage
        /// </summary>
        [JsonPropertyName("leverage_max")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// ["<c>maintenance_rate</c>"] Maintenance rate
        /// </summary>
        [JsonPropertyName("maintenance_rate")]
        public decimal MaintenanceRate { get; set; }
        /// <summary>
        /// ["<c>margin</c>"] Margin
        /// </summary>
        [JsonPropertyName("margin")]
        public decimal Margin { get; set; }
        /// <summary>
        /// ["<c>entry_price</c>"] Entry price
        /// </summary>
        [JsonPropertyName("entry_price")]
        public decimal? EntryPrice { get; set; }
        /// <summary>
        /// ["<c>liq_price</c>"] Liquidation price
        /// </summary>
        [JsonPropertyName("liq_price")]
        public decimal? LiquidationPrice { get; set; }
        /// <summary>
        /// ["<c>realised_pnl</c>"] Realized profit and less
        /// </summary>
        [JsonPropertyName("realised_pnl")]
        public decimal? RealisedPnl { get; set; }
        /// <summary>
        /// ["<c>history_pnl</c>"] History realized PNL
        /// </summary>
        [JsonPropertyName("history_pnl")]
        public decimal? HistoryPnl { get; set; }
        /// <summary>
        /// ["<c>last_close_pnl</c>"] Last close pnl
        /// </summary>
        [JsonPropertyName("last_close_pnl")]
        public decimal? LastClosePnl { get; set; }
        /// <summary>
        /// ["<c>realised_point</c>"] Realized POINT PNL
        /// </summary>
        [JsonPropertyName("realised_point")]
        public decimal? RealsedPoinPnl { get; set; }
        /// <summary>
        /// ["<c>history_point</c>"] History realized POINT PNL
        /// </summary>
        [JsonPropertyName("history_point")]
        public decimal? HistoryPoinPnl { get; set; }
        /// <summary>
        /// ["<c>mode</c>"] Position mode
        /// </summary>
        [JsonPropertyName("mode")]
        public PositionMode? PositionMode { get; set; }
        /// <summary>
        /// ["<c>cross_leverage_limit</c>"] Cross margin leverage
        /// </summary>
        [JsonPropertyName("cross_leverage_limit")]
        public decimal? CrossLeverageLimit { get; set; }
    }
}
