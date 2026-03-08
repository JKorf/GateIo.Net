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
        /// ["<c>user_id</c>"] User id
        /// </summary>
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }
        /// <summary>
        /// ["<c>locked</c>"] Locked
        /// </summary>
        [JsonPropertyName("locked")]
        public bool Locked { get; set; }
        /// <summary>
        /// ["<c>total</c>"] Total value in USD
        /// </summary>
        [JsonPropertyName("total")]
        public decimal Total { get; set; }
        /// <summary>
        /// ["<c>borrowed</c>"] Borrowed value in USD
        /// </summary>
        [JsonPropertyName("borrowed")]
        public decimal Borrowed { get; set; }
        /// <summary>
        /// ["<c>total_initial_margin</c>"] Total initial margin
        /// </summary>
        [JsonPropertyName("total_initial_margin")]
        public decimal TotalInitialMargin { get; set; }
        /// <summary>
        /// ["<c>total_margin_balance</c>"] Total margin balance
        /// </summary>
        [JsonPropertyName("total_margin_balance")]
        public decimal TotalMarginBalance { get; set; }
        /// <summary>
        /// ["<c>total_maintenance_margin</c>"] Total maintenance margin
        /// </summary>
        [JsonPropertyName("total_maintenance_margin")]
        public decimal TotalMaintenanceMargin { get; set; }
        /// <summary>
        /// ["<c>total_initial_margin_rate</c>"] Total initial margin rate
        /// </summary>
        [JsonPropertyName("total_initial_margin_rate")]
        public decimal TotalInitialMarginRate { get; set; }
        /// <summary>
        /// ["<c>total_maintenance_margin_rate</c>"] Total maintenance margin rate
        /// </summary>
        [JsonPropertyName("total_maintenance_margin_rate")]
        public decimal TotalMaintenanceMarginRate { get; set; }
        /// <summary>
        /// ["<c>total_available_margin</c>"] Total available margin
        /// </summary>
        [JsonPropertyName("total_available_margin")]
        public decimal TotalAvailableMargin { get; set; }
        /// <summary>
        /// ["<c>unified_account_total</c>"] Unified account total
        /// </summary>
        [JsonPropertyName("unified_account_total")]
        public decimal UnifiedAccountTotal { get; set; }
        /// <summary>
        /// ["<c>unified_account_total_liab</c>"] Unified account total liabilities
        /// </summary>
        [JsonPropertyName("unified_account_total_liab")]
        public decimal UnifiedAccountTotalLiabilities { get; set; }
        /// <summary>
        /// ["<c>unified_account_total_equity</c>"] Unified account total equity
        /// </summary>
        [JsonPropertyName("unified_account_total_equity")]
        public decimal UnifiedAccountTotalEquity { get; set; }
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// ["<c>spot_order_loss</c>"] Total order loss, in USDT
        /// </summary>
        [JsonPropertyName("spot_order_loss")]
        public decimal TotalOrderLoss { get; set; }
        /// <summary>
        /// ["<c>spot_hedge</c>"] Spot hedging status
        /// </summary>
        [JsonPropertyName("spot_hedge")]
        public bool SpotHedge { get; set; }
        /// <summary>
        /// ["<c>use_funding</c>"] Whether to use funds as margin
        /// </summary>
        [JsonPropertyName("use_funding")]
        public bool? UseFunding { get; set; }
        /// <summary>
        /// ["<c>balances</c>"] Balances
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
        /// ["<c>available</c>"] Available quantity
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// ["<c>freeze</c>"] Frozen quantity
        /// </summary>
        [JsonPropertyName("freeze")]
        public decimal Frozen { get; set; }
        /// <summary>
        /// ["<c>borrowed</c>"] Borrowed quantity
        /// </summary>
        [JsonPropertyName("borrowed")]
        public decimal Borrowed { get; set; }
        /// <summary>
        /// ["<c>negative_liab</c>"] Negative liabilities
        /// </summary>
        [JsonPropertyName("negative_liab")]
        public decimal NegativeLiabilities { get; set; }
        /// <summary>
        /// ["<c>futures_pos_liab</c>"] Borrowing to open futures positions
        /// </summary>
        [JsonPropertyName("futures_pos_liab")]
        public decimal FuturesPositionLiabilities { get; set; }
        /// <summary>
        /// ["<c>equity</c>"] Equity
        /// </summary>
        [JsonPropertyName("equity")]
        public decimal Equity { get; set; }
        /// <summary>
        /// ["<c>total_freeze</c>"] Total frozen
        /// </summary>
        [JsonPropertyName("total_freeze")]
        public decimal TotalFrozen { get; set; }
        /// <summary>
        /// ["<c>total_liab</c>"] Total liabilities
        /// </summary>
        [JsonPropertyName("total_liab")]
        public decimal TotalLiabilities { get; set; }
        /// <summary>
        /// ["<c>spot_in_use</c>"] Spot hedging utilization
        /// </summary>
        [JsonPropertyName("spot_in_use")]
        public decimal SpotInUse { get; set; }
        /// <summary>
        /// ["<c>cross_balance</c>"] Cross margin balance
        /// </summary>
        [JsonPropertyName("cross_balance")]
        public decimal? CrossMarginBalance { get; set; }
        /// <summary>
        /// ["<c>iso_balance</c>"] Isolated margin balance
        /// </summary>
        [JsonPropertyName("iso_balance")]
        public decimal? IsolatedMarginBalance { get; set; }
        /// <summary>
        /// ["<c>im</c>"] Initial margin
        /// </summary>
        [JsonPropertyName("im")]
        public decimal? InitialMargin { get; set; }
        /// <summary>
        /// ["<c>mm</c>"] Maintenance margin
        /// </summary>
        [JsonPropertyName("mm")]
        public decimal? MaintenanceMargin { get; set; }
        /// <summary>
        /// ["<c>imr</c>"] Initial margin rate
        /// </summary>
        [JsonPropertyName("imr")]
        public decimal? InitialMarginRate { get; set; }
        /// <summary>
        /// ["<c>mmr</c>"] Maintenance margin rate
        /// </summary>
        [JsonPropertyName("mmr")]
        public decimal? MaintenanceMarginRate { get; set; }
        /// <summary>
        /// ["<c>margin_balance</c>"] Margin balance
        /// </summary>
        [JsonPropertyName("margin_balance")]
        public decimal? MarginBalance { get; set; }
        /// <summary>
        /// ["<c>available_margin</c>"] Available margin
        /// </summary>
        [JsonPropertyName("available_margin")]
        public decimal? AvailableMargin { get; set; }
    }
}
