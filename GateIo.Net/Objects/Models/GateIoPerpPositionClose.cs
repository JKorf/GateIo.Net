using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Position close info
    /// </summary>
    [SerializationModel]
    public record GateIoPerpPositionClose
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Profit and loss
        /// </summary>
        [JsonPropertyName("pnl")]
        public decimal ProfitAndLoss { get; set; }
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
        /// Position side
        /// </summary>
        [JsonPropertyName("side")]
        public PositionSide Side { get; set; }
        /// <summary>
        /// Contract
        /// </summary>
        [JsonPropertyName("contract")]
        public string Contract { get; set; } = string.Empty;
        /// <summary>
        /// Text
        /// </summary>
        [JsonPropertyName("text")]
        public string? Text { get; set; }
        /// <summary>
        /// Max Trade Size
        /// </summary>
        [JsonPropertyName("max_size")]
        public decimal? MaxSize { get; set; }
        /// <summary>
        /// Accumelated size
        /// </summary>
        [JsonPropertyName("accum_size")]
        public decimal? AccumelatedSize { get; set; }
        /// <summary>
        /// First opening time
        /// </summary>
        [JsonPropertyName("first_open_time")]
        public DateTime? FirstOpenTime { get; set; }
        /// <summary>
        /// When 'side' is 'long,' it indicates the opening average price; when 'side' is 'short,' it indicates the closing average price.
        /// </summary>
        [JsonPropertyName("long_price")]
        public decimal LongPrice { get; set; }
        /// <summary>
        /// When 'side' is 'long,' it indicates the opening average price; when 'side' is 'short,' it indicates the closing average price
        /// </summary>
        [JsonPropertyName("short_price")]
        public decimal ShortPrice { get; set; }
    }
}
