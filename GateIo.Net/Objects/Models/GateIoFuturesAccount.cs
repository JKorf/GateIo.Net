using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Futures account
    /// </summary>
    [SerializationModel]
    public record GateIoFuturesAccount
    {
        /// <summary>
        /// ["<c>user</c>"] User id
        /// </summary>
        [JsonPropertyName("user")]
        public long UserId { get; set; }
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>total</c>"] The balance after the user's accumulated deposit, withdraw, profit and loss
        /// </summary>
        [JsonPropertyName("total")]
        public decimal Total { get; set; }
        /// <summary>
        /// ["<c>unrealised_pnl</c>"] Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("unrealised_pnl")]
        public decimal UnrealisedPnl { get; set; }
        /// <summary>
        /// ["<c>position_margin</c>"] Position margin
        /// </summary>
        [JsonPropertyName("position_margin")]
        public decimal PositionMargin { get; set; }
        /// <summary>
        /// ["<c>order_margin</c>"] Order margin
        /// </summary>
        [JsonPropertyName("order_margin")]
        public decimal OrderMargin { get; set; }
        /// <summary>
        /// ["<c>available</c>"] Available
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// ["<c>point</c>"] Point quantity
        /// </summary>
        [JsonPropertyName("point")]
        public decimal PointQuantity { get; set; }
        /// <summary>
        /// ["<c>bonus</c>"] Perpetual Contract Bonus
        /// </summary>
        [JsonPropertyName("bonus")]
        public decimal Bonus { get; set; }
        /// <summary>
        /// ["<c>in_dual_mode</c>"] Dual mode
        /// </summary>
        [JsonPropertyName("in_dual_mode")]
        public bool DualMode { get; set; }
        /// <summary>
        /// ["<c>enable_evolved_classic</c>"] Classic account margin mode
        /// </summary>
        [JsonPropertyName("enable_evolved_classic")]
        public bool ClassicMarginMode { get; set; }
        /// <summary>
        /// ["<c>cross_initial_margin</c>"] Cross initial margin
        /// </summary>
        [JsonPropertyName("cross_initial_margin")]
        public decimal? CrossInitialMargin { get; set; }
        /// <summary>
        /// ["<c>cross_maintenance_margin</c>"] Cross maintenance margin
        /// </summary>
        [JsonPropertyName("cross_maintenance_margin")]
        public decimal? CrossMaintenanceMargin { get; set; }
        /// <summary>
        /// ["<c>cross_order_margin</c>"] Cross order margin
        /// </summary>
        [JsonPropertyName("cross_order_margin")]
        public decimal? CrossOrderMargin { get; set; }
        /// <summary>
        /// ["<c>cross_unrealised_pnl</c>"] Cross unrealized profit and loss
        /// </summary>
        [JsonPropertyName("cross_unrealised_pnl")]
        public decimal? CrossUnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>isolated_position_margin</c>"] Isolated position margin
        /// </summary>
        [JsonPropertyName("isolated_position_margin")]
        public decimal? IsolatedPositionMargin { get; set; }
        /// <summary>
        /// ["<c>cross_available</c>"] Cross available
        /// </summary>
        [JsonPropertyName("cross_available")]
        public decimal? CrossAvailable { get; set; }
        /// <summary>
        /// ["<c>cross_margin_balance</c>"] Cross margin balance
        /// </summary>
        [JsonPropertyName("cross_margin_balance")]
        public decimal? CrossMarginBalance { get; set; }
        /// <summary>
        /// ["<c>cross_mmr</c>"] Cross maintenance margin ratio
        /// </summary>
        [JsonPropertyName("cross_mmr")]
        public decimal? CrossMaintenanceMarginRatio { get; set; }
        /// <summary>
        /// ["<c>cross_imr</c>"] Cross initial margin rate
        /// </summary>
        [JsonPropertyName("cross_imr")]
        public decimal? CrossInitialMarginRate { get; set; }
        /// <summary>
        /// ["<c>history</c>"] Total history
        /// </summary>
        [JsonPropertyName("history")]
        public GateIoFuturesAccountHistory Totals { get; set; } = null!;
        /// <summary>
        /// ["<c>enable_tiered_mm</c>"] Whether to enable tiered maintenance margin calculation
        /// </summary>
        [JsonPropertyName("enable_tiered_mm")]
        public bool EnableTieredMaintenanceMargin { get; set; }
    }

    /// <summary>
    /// Account history
    /// </summary>
    [SerializationModel]
    public record GateIoFuturesAccountHistory
    {
        /// <summary>
        /// ["<c>dnw</c>"] Total amount of deposit and withdraw
        /// </summary>
        [JsonPropertyName("dnw")]
        public decimal DepositWithdrawalQuantity { get; set; }
        /// <summary>
        /// ["<c>pnl</c>"] Total Profit and loss
        /// </summary>
        [JsonPropertyName("pnl")]
        public decimal TotalProfitAndLoss { get; set; }
        /// <summary>
        /// ["<c>fee</c>"] Total amount of fee paid
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal TotalFee { get; set; }
        /// <summary>
        /// ["<c>refr</c>"] Total amount of referrer rebates
        /// </summary>
        [JsonPropertyName("refr")]
        public decimal TotalReferenceRebate { get; set; }
        /// <summary>
        /// ["<c>fund</c>"] Total amount of funding costs
        /// </summary>
        [JsonPropertyName("fund")]
        public decimal TotalFundingCosts { get; set; }
        /// <summary>
        /// ["<c>point_dnw</c>"] Total amount of point deposit and withdraw
        /// </summary>
        [JsonPropertyName("point_dnw")]
        public decimal TotalPointDepositWithdrawal { get; set; }
        /// <summary>
        /// ["<c>point_fee</c>"] Total amount of point fee
        /// </summary>
        [JsonPropertyName("point_fee")]
        public decimal TotalPointFee { get; set; }
        /// <summary>
        /// ["<c>point_refr</c>"] Total amountof point referrer rebates
        /// </summary>
        [JsonPropertyName("point_refr")]
        public decimal TotalPointReferenceRebate { get; set; }
        /// <summary>
        /// ["<c>bonus_dnw</c>"] Total amount of perpetual contract bonus transfer
        /// </summary>
        [JsonPropertyName("bonus_dnw")]
        public decimal TotalBonusDepositAndWithdrawal { get; set; }
        /// <summary>
        /// ["<c>bonus_offset</c>"] Total amount of perpetual contract bonus deduction
        /// </summary>
        [JsonPropertyName("bonus_offset")]
        public decimal TotalBonusDeduction { get; set; }
    }
}
