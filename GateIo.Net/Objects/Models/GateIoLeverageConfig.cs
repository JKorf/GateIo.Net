using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// ["<c>current_leverage</c>"] Current leverage
        /// </summary>
        [JsonPropertyName("current_leverage")]
        public decimal CurrentLeverage { get; set; }
        /// <summary>
        /// ["<c>min_leverage</c>"] Min leverage
        /// </summary>
        [JsonPropertyName("min_leverage")]
        public decimal MinLeverage { get; set; }
        /// <summary>
        /// ["<c>max_leverage</c>"] Max leverage
        /// </summary>
        [JsonPropertyName("max_leverage")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// ["<c>debit</c>"] Debit
        /// </summary>
        [JsonPropertyName("debit")]
        public decimal Debit { get; set; }
        /// <summary>
        /// ["<c>available_margin</c>"] Available margin
        /// </summary>
        [JsonPropertyName("available_margin")]
        public decimal AvailableMargin { get; set; }
        /// <summary>
        /// ["<c>borrowable</c>"] Borrowable
        /// </summary>
        [JsonPropertyName("borrowable")]
        public decimal Borrowable { get; set; }
        /// <summary>
        /// ["<c>except_leverage_borrowable</c>"] Except leverage borrowable
        /// </summary>
        [JsonPropertyName("except_leverage_borrowable")]
        public decimal ExceptLeverageBorrowable { get; set; }
    }


}
