using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Contract info
    /// </summary>
    [SerializationModel]
    public record GateIoPerpFuturesContract
    {
        /// <summary>
        /// Contract name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonPropertyName("type")]
        public ContractType Type { get; set; }
        /// <summary>
        /// Multiplier used in converting from invoicing to settlement asset
        /// </summary>
        [JsonPropertyName("quanto_multiplier")]
        public decimal Multiplier { get; set; }
        /// <summary>
        /// Referral fee rate discount
        /// </summary>
        [JsonPropertyName("ref_discount_rate")]
        public decimal ReferalDiscount { get; set; }
        /// <summary>
        /// Deviation between order price and current index price
        /// </summary>
        [JsonPropertyName("order_price_deviate")]
        public decimal OrderPriceDeviation { get; set; }
        /// <summary>
        /// Maintenance rate of margin
        /// </summary>
        [JsonPropertyName("maintenance_rate")]
        public decimal MaintenanceRate { get; set; }
        /// <summary>
        /// Mark type
        /// </summary>
        [JsonPropertyName("mark_type")]
        public MarkType MarkType { get; set; }
        /// <summary>
        /// Mark price
        /// </summary>
        [JsonPropertyName("mark_price")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// Last price
        /// </summary>
        [JsonPropertyName("last_price")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// Index price
        /// </summary>
        [JsonPropertyName("index_price")]
        public decimal IndexPrice { get; set; }
        /// <summary>
        /// Funding rate indicative 
        /// </summary>
        [JsonPropertyName("funding_rate_indicative")]
        public decimal FundingRateIndicative { get; set; }
        /// <summary>
        /// Minimum mark price increment
        /// </summary>
        [JsonPropertyName("mark_price_round")]
        public decimal MarkPriceStep { get; set; }
        /// <summary>
        /// Funding offset
        /// </summary>
        [JsonPropertyName("funding_offset")]
        public decimal FundingOffset { get; set; }
        /// <summary>
        /// Delisting
        /// </summary>
        [JsonPropertyName("in_delisting")]
        public bool Delisting { get; set; }
        /// <summary>
        /// Interest rate
        /// </summary>
        [JsonPropertyName("interest_rate")]
        public decimal InterestRate { get; set; }
        /// <summary>
        /// Minimum order price increment
        /// </summary>
        [JsonPropertyName("order_price_round")]
        public decimal OrderPriceStep { get; set; }
        /// <summary>
        /// Minimum order quantity
        /// </summary>
        [JsonPropertyName("order_size_min")]
        public decimal MinOrderQuantity { get; set; }
        /// <summary>
        /// Referral fee rate discount
        /// </summary>
        [JsonPropertyName("ref_rebate_rate")]
        public decimal ReferalRebateRate { get; set; }
        /// <summary>
        /// Funding application interval, unit in seconds
        /// </summary>
        [JsonPropertyName("funding_interval")]
        public int? FundingInterval { get; set; }
        /// <summary>
        /// Min leverage
        /// </summary>
        [JsonPropertyName("leverage_min")]
        public decimal MinLeverage { get; set; }
        /// <summary>
        /// Max leverage
        /// </summary>
        [JsonPropertyName("leverage_max")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// Maker fee rate
        /// </summary>
        [JsonPropertyName("maker_fee_rate")]
        public decimal MakerFeeRate { get; set; }
        /// <summary>
        /// Taker fee rate
        /// </summary>
        [JsonPropertyName("taker_fee_rate")]
        public decimal TakerFeeRate { get; set; }
        /// <summary>
        /// Funding rate
        /// </summary>
        [JsonPropertyName("funding_rate")]
        public decimal FundingRate { get; set; }
        /// <summary>
        /// Max order quantity
        /// </summary>
        [JsonPropertyName("order_size_max")]
        public decimal MaxOrderQuantity { get; set; }
        /// <summary>
        /// Next funding time
        /// </summary>
        [JsonPropertyName("funding_next_apply")]
        public DateTime NextFundingTime { get; set; }
        /// <summary>
        /// Config change time
        /// </summary>
        [JsonPropertyName("config_change_time")]
        public DateTime ConfigChangeTime { get; set; }
        /// <summary>
        /// Short users
        /// </summary>
        [JsonPropertyName("short_users")]
        public int ShortUsers { get; set; }
        /// <summary>
        /// Historical accumulated trade size
        /// </summary>
        [JsonPropertyName("trade_size")]
        public decimal TotalTradeSize { get; set; }
        /// <summary>
        /// Current total long position size
        /// </summary>
        [JsonPropertyName("position_size")]
        public decimal PositionSize { get; set; }
        /// <summary>
        /// Long users
        /// </summary>
        [JsonPropertyName("long_users")]
        public int LongUsers { get; set; }
        /// <summary>
        /// Funding impact value
        /// </summary>
        [JsonPropertyName("funding_impact_value")]
        public decimal FundingImpactValue { get; set; }
        /// <summary>
        /// Maximum number of open orders
        /// </summary>
        [JsonPropertyName("orders_limit")]
        public int MaxOrders { get; set; }
        /// <summary>
        /// Last trade id
        /// </summary>
        [JsonPropertyName("trade_id")]
        public long CurrentTradeId { get; set; }
        /// <summary>
        /// Last book sequence id
        /// </summary>
        [JsonPropertyName("orderbook_id")]
        public long CurrentOrderbookId { get; set; }
        /// <summary>
        /// Whether bonus is enabled
        /// </summary>
        [JsonPropertyName("enable_bonus")]
        public bool BonusEnabled { get; set; }
        /// <summary>
        /// Whether portfolio margin account is enabled
        /// </summary>
        [JsonPropertyName("enable_credit")]
        public bool CreditEnabled { get; set; }
        /// <summary>
        /// Create time
        /// </summary>
        [JsonPropertyName("create_time")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// The factor for the maximum of the funding rate.
        /// </summary>
        [JsonPropertyName("funding_cap_ratio")]
        public decimal FundingCapRatio { get; set; }
        /// <summary>
        /// Voucher leverage
        /// </summary>
        [JsonPropertyName("voucher_leverage")]
        public decimal VoucherLeverage { get; set; }
    }
}
