using CryptoExchange.Net.Converters.SystemTextJson;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Unified account info
    /// </summary>
    [SerializationModel]
    public record GateIoUnifiedAccountInfo
    {
        /// <summary>
        /// User id
        /// </summary>
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }
        /// <summary>
        /// Locked
        /// </summary>
        [JsonPropertyName("locked")]
        public bool Locked { get; set; }
        /// <summary>
        /// Total value in USD
        /// </summary>
        [JsonPropertyName("total")]
        public decimal Total { get; set; }
        /// <summary>
        /// Borrowed value in USD
        /// </summary>
        [JsonPropertyName("borrowed")]
        public decimal Borrowed { get; set; }
        /// <summary>
        /// Total initial margin
        /// </summary>
        [JsonPropertyName("total_initial_margin")]
        public decimal TotalInitialMargin { get; set; }
        /// <summary>
        /// Total margin balance
        /// </summary>
        [JsonPropertyName("total_margin_balance")]
        public decimal TotalMarginBalance { get; set; }
        /// <summary>
        /// Total maintenance margin
        /// </summary>
        [JsonPropertyName("total_maintenance_margin")]
        public decimal TotalMaintenanceMargin { get; set; }
        /// <summary>
        /// Total initial margin rate
        /// </summary>
        [JsonPropertyName("total_initial_margin_rate")]
        public decimal TotalInitialMarginRate { get; set; }
        /// <summary>
        /// Total maintenance margin rate
        /// </summary>
        [JsonPropertyName("total_maintenance_margin_rate")]
        public decimal TotalMaintenanceMarginRate { get; set; }
        /// <summary>
        /// Total available margin
        /// </summary>
        [JsonPropertyName("total_available_margin")]
        public decimal TotalAvailableMargin { get; set; }
        /// <summary>
        /// Unified account total
        /// </summary>
        [JsonPropertyName("unified_account_total")]
        public decimal UnifiedAccountTotal { get; set; }
        /// <summary>
        /// Unified account total liabilities
        /// </summary>
        [JsonPropertyName("unified_account_total_liab")]
        public decimal UnifiedAccountTotalLiabilities { get; set; }
        /// <summary>
        /// Unified account total equity
        /// </summary>
        [JsonPropertyName("unified_account_total_equity")]
        public decimal UnifiedAccountTotalEquity { get; set; }
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// Total order loss, in USDT
        /// </summary>
        [JsonPropertyName("spot_order_loss")]
        public decimal TotalOrderLoss { get; set; }
        /// <summary>
        /// Spot hedging status
        /// </summary>
        [JsonPropertyName("spot_hedge")]
        public bool SpotHedge { get; set; }
        /// <summary>
        /// Whether to use funds as margin
        /// </summary>
        [JsonPropertyName("use_funding")]
        public bool? UseFunding { get; set; }
        /// <summary>
        /// Balances
        /// </summary>
        [JsonPropertyName("balances")]
        public Dictionary<string, GateIoUnifiedAccountBalance> Balances { get; set; } = new Dictionary<string, GateIoUnifiedAccountBalance>();
    }

    /// <summary>
    /// Unified account balance
    /// </summary>
    [SerializationModel]
    public record GateIoUnifiedAccountBalance
    {
        /// <summary>
        /// Available quantity
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// Frozen quantity
        /// </summary>
        [JsonPropertyName("freeze")]
        public decimal Frozen { get; set; }
        /// <summary>
        /// Borrowed quantity
        /// </summary>
        [JsonPropertyName("borrowed")]
        public decimal Borrowed { get; set; }
        /// <summary>
        /// Negative liabilities
        /// </summary>
        [JsonPropertyName("negative_liab")]
        public decimal NegativeLiabilities { get; set; }
        /// <summary>
        /// Borrowing to open futures positions
        /// </summary>
        [JsonPropertyName("futures_pos_liab")]
        public decimal FuturesPositionLiabilities { get; set; }
        /// <summary>
        /// Equity
        /// </summary>
        [JsonPropertyName("equity")]
        public decimal Equity { get; set; }
        /// <summary>
        /// Total frozen
        /// </summary>
        [JsonPropertyName("total_freeze")]
        public decimal TotalFrozen { get; set; }
        /// <summary>
        /// Total liabilities
        /// </summary>
        [JsonPropertyName("total_liab")]
        public decimal TotalLiabilities { get; set; }
        /// <summary>
        /// Spot hedging utilization
        /// </summary>
        [JsonPropertyName("spot_in_use")]
        public decimal SpotInUse { get; set; }
        /// <summary>
        /// Cross margin balance
        /// </summary>
        [JsonPropertyName("cross_balance")]
        public decimal? CrossMarginBalance { get; set; }
        /// <summary>
        /// Isolated margin balance
        /// </summary>
        [JsonPropertyName("iso_balance")]
        public decimal? IsolatedMarginBalance { get; set; }
        /// <summary>
        /// Initial margin
        /// </summary>
        [JsonPropertyName("im")]
        public decimal? InitialMargin { get; set; }
        /// <summary>
        /// Maintenance margin
        /// </summary>
        [JsonPropertyName("mm")]
        public decimal? MaintenanceMargin { get; set; }
        /// <summary>
        /// Initial margin rate
        /// </summary>
        [JsonPropertyName("imr")]
        public decimal? InitialMarginRate { get; set; }
        /// <summary>
        /// Maintenance margin rate
        /// </summary>
        [JsonPropertyName("mmr")]
        public decimal? MaintenanceMarginRate { get; set; }
        /// <summary>
        /// Margin balance
        /// </summary>
        [JsonPropertyName("margin_balance")]
        public decimal? MarginBalance { get; set; }
        /// <summary>
        /// Available margin
        /// </summary>
        [JsonPropertyName("available_margin")]
        public decimal? AvailableMargin { get; set; }
    }
}
