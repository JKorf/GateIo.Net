using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Leverage config
    /// </summary>
    [SerializationModel]
    public record GateIoLeverageConfig
    {
        /// <summary>
        /// Current leverage
        /// </summary>
        [JsonPropertyName("current_leverage")]
        public decimal CurrentLeverage { get; set; }
        /// <summary>
        /// Min leverage
        /// </summary>
        [JsonPropertyName("min_leverage")]
        public decimal MinLeverage { get; set; }
        /// <summary>
        /// Max leverage
        /// </summary>
        [JsonPropertyName("max_leverage")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// Debit
        /// </summary>
        [JsonPropertyName("debit")]
        public decimal Debit { get; set; }
        /// <summary>
        /// Available margin
        /// </summary>
        [JsonPropertyName("available_margin")]
        public decimal AvailableMargin { get; set; }
        /// <summary>
        /// Borrowable
        /// </summary>
        [JsonPropertyName("borrowable")]
        public decimal Borrowable { get; set; }
        /// <summary>
        /// Except leverage borrowable
        /// </summary>
        [JsonPropertyName("except_leverage_borrowable")]
        public decimal ExceptLeverageBorrowable { get; set; }
    }


}
