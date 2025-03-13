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
    public record GateIoPerpPositionCloseUpdate
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("time_ms")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Profit and loss
        /// </summary>
        [JsonPropertyName("pnl")]
        public decimal ProfitAndLoss { get; set; }
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
        /// User id
        /// </summary>
        [JsonPropertyName("user")]
        public long UserId { get; set; }
    }
}
