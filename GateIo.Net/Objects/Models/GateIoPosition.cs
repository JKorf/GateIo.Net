using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;
using GateIo.Net.Enums;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Position info
    /// </summary>
    [SerializationModel]
    public record GateIoPosition
    {
        /// <summary>
        /// User id
        /// </summary>
        [JsonPropertyName("user")]
        public long UserId { get; set; }
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
        /// Position value
        /// </summary>
        [JsonPropertyName("value")]
        public decimal PositionValue { get; set; }
        /// <summary>
        /// Margin
        /// </summary>
        [JsonPropertyName("margin")]
        public decimal Margin { get; set; }
        /// <summary>
        /// Initial margin
        /// </summary>
        [JsonPropertyName("initial_margin")]
        public decimal? InitialMargin { get; set; }
        /// <summary>
        /// Maintenance margin
        /// </summary>
        [JsonPropertyName("maintenance_margin")]
        public decimal? MaintenanceMargin { get; set; }
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
        /// Mark price
        /// </summary>
        [JsonPropertyName("mark_price")]
        public decimal? MarkPrice { get; set; }
        /// <summary>
        /// Unrealized profit and less
        /// </summary>
        [JsonPropertyName("unrealised_pnl")]
        public decimal? UnrealisedPnl { get; set; }
        /// <summary>
        /// Realized profit and less
        /// </summary>
        [JsonPropertyName("realised_pnl")]
        public decimal? RealisedPnl { get; set; }
        /// <summary>
        /// Realized profit and loss position
        /// </summary>
        [JsonPropertyName("pnl_pnl")]
        public decimal? RealisedPnlPosition { get; set; }
        /// <summary>
        /// Realized PNL - Funding Fees
        /// </summary>
        [JsonPropertyName("pnl_fund")]
        public decimal? RealisedPnlFundingFees { get; set; }
        /// <summary>
        /// Realized PNL - Transaction Fees
        /// </summary>
        [JsonPropertyName("pnl_fee")]
        public decimal? RealisedPnlFee { get; set; }
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
        /// Auto deleverage ranking
        /// </summary>
        [JsonPropertyName("adl_ranking")]
        public int? AdlRanking { get; set; }
        /// <summary>
        /// Open orders
        /// </summary>
        [JsonPropertyName("pending_orders")]
        public int? PendingOrders { get; set; }
        /// <summary>
        /// Position mode
        /// </summary>
        [JsonPropertyName("mode")]
        public PositionMode? PositionMode { get; set; }
        /// <summary>
        /// Update time
        /// </summary>
        [JsonPropertyName("update_time")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// Cross margin leverage
        /// </summary>
        [JsonPropertyName("cross_leverage_limit")]
        public decimal? CrossLeverageLimit { get; set; }
        /// <summary>
        /// Update id
        /// </summary>
        [JsonPropertyName("update_id")]
        public long? UpdateId { get; set; }
        /// <summary>
        /// Average maintenance margin rate
        /// </summary>
        [JsonPropertyName("average_maintenance_rate")]
        public decimal? AverageMaintenanceRate { get; set; }
        /// <summary>
        /// Close order
        /// </summary>
        [JsonPropertyName("close_order")]
        public GateIoPositionCloseOrder? CloseOrder { get; set; }
    }

    /// <summary>
    /// Close order info
    /// </summary>
    [SerializationModel]
    public record GateIoPositionCloseOrder
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("id")]
        public long? Id { get; set; }
        /// <summary>
        /// Close order price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// Is liquidation order
        /// </summary>
        [JsonPropertyName("is_liq")]
        public bool? IsLiquidation { get; set; }
    }
}
