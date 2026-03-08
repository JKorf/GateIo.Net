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
        /// ["<c>user</c>"] User id
        /// </summary>
        [JsonPropertyName("user")]
        public long UserId { get; set; }
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
        /// ["<c>trade_max_size</c>"] Maximum position
        /// </summary>
        [JsonPropertyName("trade_max_size")]
        public long MaxTradeSize { get; set; }
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
        /// ["<c>value</c>"] Position value
        /// </summary>
        [JsonPropertyName("value")]
        public decimal PositionValue { get; set; }
        /// <summary>
        /// ["<c>margin</c>"] Margin
        /// </summary>
        [JsonPropertyName("margin")]
        public decimal Margin { get; set; }
        /// <summary>
        /// ["<c>initial_margin</c>"] Initial margin
        /// </summary>
        [JsonPropertyName("initial_margin")]
        public decimal? InitialMargin { get; set; }
        /// <summary>
        /// ["<c>maintenance_margin</c>"] Maintenance margin
        /// </summary>
        [JsonPropertyName("maintenance_margin")]
        public decimal? MaintenanceMargin { get; set; }
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
        /// ["<c>mark_price</c>"] Mark price
        /// </summary>
        [JsonPropertyName("mark_price")]
        public decimal? MarkPrice { get; set; }
        /// <summary>
        /// ["<c>unrealised_pnl</c>"] Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("unrealised_pnl")]
        public decimal? UnrealisedPnl { get; set; }
        /// <summary>
        /// ["<c>realised_pnl</c>"] Realized profit and loss
        /// </summary>
        [JsonPropertyName("realised_pnl")]
        public decimal? RealisedPnl { get; set; }
        /// <summary>
        /// ["<c>pnl_pnl</c>"] Realized profit and loss position
        /// </summary>
        [JsonPropertyName("pnl_pnl")]
        public decimal? RealisedPnlPosition { get; set; }
        /// <summary>
        /// ["<c>pnl_fund</c>"] Realized PNL - Funding Fees
        /// </summary>
        [JsonPropertyName("pnl_fund")]
        public decimal? RealisedPnlFundingFees { get; set; }
        /// <summary>
        /// ["<c>pnl_fee</c>"] Realized PNL - Transaction Fees
        /// </summary>
        [JsonPropertyName("pnl_fee")]
        public decimal? RealisedPnlFee { get; set; }
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
        public decimal? RealisedPointPnl { get; set; }
        /// <summary>
        /// ["<c>history_point</c>"] History realized POINT PNL
        /// </summary>
        [JsonPropertyName("history_point")]
        public decimal? HistoryPointPnl { get; set; }
        /// <summary>
        /// ["<c>adl_ranking</c>"] Auto deleverage ranking
        /// </summary>
        [JsonPropertyName("adl_ranking")]
        public int? AdlRanking { get; set; }
        /// <summary>
        /// ["<c>pending_orders</c>"] Open orders
        /// </summary>
        [JsonPropertyName("pending_orders")]
        public int? PendingOrders { get; set; }
        /// <summary>
        /// ["<c>mode</c>"] Position mode
        /// </summary>
        [JsonPropertyName("mode")]
        public PositionMode? PositionMode { get; set; }
        /// <summary>
        /// ["<c>open_time</c>"] First open time
        /// </summary>
        [JsonPropertyName("open_time")]
        public DateTime? OpenTime { get; set; }
        /// <summary>
        /// ["<c>update_time</c>"] Update time
        /// </summary>
        [JsonPropertyName("update_time")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// ["<c>cross_leverage_limit</c>"] Cross margin leverage
        /// </summary>
        [JsonPropertyName("cross_leverage_limit")]
        public decimal? CrossLeverageLimit { get; set; }
        /// <summary>
        /// ["<c>update_id</c>"] Update id
        /// </summary>
        [JsonPropertyName("update_id")]
        public long? UpdateId { get; set; }
        /// <summary>
        /// ["<c>average_maintenance_rate</c>"] Average maintenance margin rate
        /// </summary>
        [JsonPropertyName("average_maintenance_rate")]
        public decimal? AverageMaintenanceRate { get; set; }
        /// <summary>
        /// ["<c>close_order</c>"] Close order
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
        /// ["<c>id</c>"] Order id
        /// </summary>
        [JsonPropertyName("id")]
        public long? Id { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Close order price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// ["<c>is_liq</c>"] Is liquidation order
        /// </summary>
        [JsonPropertyName("is_liq")]
        public bool? IsLiquidation { get; set; }
    }
}
