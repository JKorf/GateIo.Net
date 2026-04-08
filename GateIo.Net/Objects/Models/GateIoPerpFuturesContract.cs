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
        /// ["<c>name</c>"] Contract name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>type</c>"] Contract type
        /// </summary>
        [JsonPropertyName("type")]
        public ContractType Type { get; set; }
        /// <summary>
        /// ["<c>contract_type</c>"] Contract target type
        /// </summary>
        [JsonPropertyName("contract_type")]
        public ContractTargetType? ContractType { get; set; }
        /// <summary>
        /// ["<c>quanto_multiplier</c>"] Multiplier used in converting from invoicing to settlement asset
        /// </summary>
        [JsonPropertyName("quanto_multiplier")]
        public decimal Multiplier { get; set; }
        /// <summary>
        /// ["<c>ref_discount_rate</c>"] Referral fee rate discount
        /// </summary>
        [JsonPropertyName("ref_discount_rate")]
        public decimal ReferalDiscount { get; set; }
        /// <summary>
        /// ["<c>order_price_deviate</c>"] Deviation between order price and current index price
        /// </summary>
        [JsonPropertyName("order_price_deviate")]
        public decimal OrderPriceDeviation { get; set; }
        /// <summary>
        /// ["<c>maintenance_rate</c>"] Maintenance rate of margin
        /// </summary>
        [JsonPropertyName("maintenance_rate")]
        public decimal MaintenanceRate { get; set; }
        /// <summary>
        /// ["<c>mark_type</c>"] Mark type
        /// </summary>
        [JsonPropertyName("mark_type")]
        public MarkType MarkType { get; set; }
        /// <summary>
        /// ["<c>mark_price</c>"] Mark price
        /// </summary>
        [JsonPropertyName("mark_price")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// ["<c>last_price</c>"] Last price
        /// </summary>
        [JsonPropertyName("last_price")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// ["<c>index_price</c>"] Index price
        /// </summary>
        [JsonPropertyName("index_price")]
        public decimal IndexPrice { get; set; }
        /// <summary>
        /// ["<c>funding_rate_indicative</c>"] Funding rate indicative 
        /// </summary>
        [JsonPropertyName("funding_rate_indicative")]
        public decimal FundingRateIndicative { get; set; }
        /// <summary>
        /// ["<c>mark_price_round</c>"] Minimum mark price increment
        /// </summary>
        [JsonPropertyName("mark_price_round")]
        public decimal MarkPriceStep { get; set; }
        /// <summary>
        /// ["<c>funding_offset</c>"] Funding offset
        /// </summary>
        [JsonPropertyName("funding_offset")]
        public decimal FundingOffset { get; set; }
        /// <summary>
        /// ["<c>in_delisting</c>"] Delisting
        /// </summary>
        [JsonPropertyName("in_delisting")]
        public bool Delisting { get; set; }
        /// <summary>
        /// ["<c>interest_rate</c>"] Interest rate
        /// </summary>
        [JsonPropertyName("interest_rate")]
        public decimal InterestRate { get; set; }
        /// <summary>
        /// ["<c>order_price_round</c>"] Minimum order price increment
        /// </summary>
        [JsonPropertyName("order_price_round")]
        public decimal OrderPriceStep { get; set; }
        /// <summary>
        /// ["<c>order_size_min</c>"] Minimum order quantity
        /// </summary>
        [JsonPropertyName("order_size_min")]
        public decimal MinOrderQuantity { get; set; }
        /// <summary>
        /// ["<c>ref_rebate_rate</c>"] Referral fee rate discount
        /// </summary>
        [JsonPropertyName("ref_rebate_rate")]
        public decimal ReferalRebateRate { get; set; }
        /// <summary>
        /// ["<c>funding_interval</c>"] Funding application interval, unit in seconds
        /// </summary>
        [JsonPropertyName("funding_interval")]
        public int? FundingInterval { get; set; }
        /// <summary>
        /// ["<c>leverage_min</c>"] Min leverage
        /// </summary>
        [JsonPropertyName("leverage_min")]
        public decimal MinLeverage { get; set; }
        /// <summary>
        /// ["<c>leverage_max</c>"] Max leverage
        /// </summary>
        [JsonPropertyName("leverage_max")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// ["<c>maker_fee_rate</c>"] Maker fee rate
        /// </summary>
        [JsonPropertyName("maker_fee_rate")]
        public decimal MakerFeeRate { get; set; }
        /// <summary>
        /// ["<c>taker_fee_rate</c>"] Taker fee rate
        /// </summary>
        [JsonPropertyName("taker_fee_rate")]
        public decimal TakerFeeRate { get; set; }
        /// <summary>
        /// ["<c>funding_rate</c>"] Funding rate
        /// </summary>
        [JsonPropertyName("funding_rate")]
        public decimal FundingRate { get; set; }
        /// <summary>
        /// ["<c>order_size_max</c>"] Max order quantity
        /// </summary>
        [JsonPropertyName("order_size_max")]
        public decimal MaxOrderQuantity { get; set; }
        /// <summary>
        /// ["<c>funding_next_apply</c>"] Next funding time
        /// </summary>
        [JsonPropertyName("funding_next_apply")]
        public DateTime NextFundingTime { get; set; }
        /// <summary>
        /// ["<c>config_change_time</c>"] Config change time
        /// </summary>
        [JsonPropertyName("config_change_time")]
        public DateTime ConfigChangeTime { get; set; }
        /// <summary>
        /// ["<c>short_users</c>"] Short users
        /// </summary>
        [JsonPropertyName("short_users")]
        public int ShortUsers { get; set; }
        /// <summary>
        /// ["<c>trade_size</c>"] Historical accumulated trade size
        /// </summary>
        [JsonPropertyName("trade_size")]
        public decimal TotalTradeSize { get; set; }
        /// <summary>
        /// ["<c>position_size</c>"] Current total long position size
        /// </summary>
        [JsonPropertyName("position_size")]
        public decimal PositionSize { get; set; }
        /// <summary>
        /// ["<c>long_users</c>"] Long users
        /// </summary>
        [JsonPropertyName("long_users")]
        public int LongUsers { get; set; }
        /// <summary>
        /// ["<c>funding_impact_value</c>"] Funding impact value
        /// </summary>
        [JsonPropertyName("funding_impact_value")]
        public decimal FundingImpactValue { get; set; }
        /// <summary>
        /// ["<c>orders_limit</c>"] Maximum number of open orders
        /// </summary>
        [JsonPropertyName("orders_limit")]
        public int MaxOrders { get; set; }
        /// <summary>
        /// ["<c>trade_id</c>"] Last trade id
        /// </summary>
        [JsonPropertyName("trade_id")]
        public long CurrentTradeId { get; set; }
        /// <summary>
        /// ["<c>orderbook_id</c>"] Last book sequence id
        /// </summary>
        [JsonPropertyName("orderbook_id")]
        public long CurrentOrderbookId { get; set; }
        /// <summary>
        /// ["<c>enable_bonus</c>"] Whether bonus is enabled
        /// </summary>
        [JsonPropertyName("enable_bonus")]
        public bool BonusEnabled { get; set; }
        /// <summary>
        /// ["<c>enable_credit</c>"] Whether portfolio margin account is enabled
        /// </summary>
        [JsonPropertyName("enable_credit")]
        public bool CreditEnabled { get; set; }
        /// <summary>
        /// ["<c>create_time</c>"] Create time
        /// </summary>
        [JsonPropertyName("create_time")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// ["<c>funding_cap_ratio</c>"] The factor for the maximum of the funding rate.
        /// </summary>
        [JsonPropertyName("funding_cap_ratio")]
        public decimal FundingCapRatio { get; set; }
        /// <summary>
        /// ["<c>voucher_leverage</c>"] Voucher leverage
        /// </summary>
        [JsonPropertyName("voucher_leverage")]
        public decimal VoucherLeverage { get; set; }
        /// <summary>
        /// ["<c>funding_rate_limit</c>"] Upper and lower limits of funding rate
        /// </summary>
        [JsonPropertyName("funding_rate_limit")]
        public decimal FundingRateLimit { get; set; }
        /// <summary>
        /// ["<c>market_order_slip_ratio</c>"] Max slippage ratio for market orders
        /// </summary>
        [JsonPropertyName("market_order_slip_ratio")]
        public decimal MarketOrderMaxSlippageRatio { get; set; }
        /// <summary>
        /// ["<c>market_order_size_max</c>"] Max quantity for market orders
        /// </summary>
        [JsonPropertyName("market_order_size_max")]
        public decimal MarketOrderMaxQuantity { get; set; }
    }
}
