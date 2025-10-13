using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Price ticker
    /// </summary>
    public record GateIoAlphaTicker
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Last trade price
        /// </summary>
        [JsonPropertyName("last")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// Change percentage
        /// </summary>
        [JsonPropertyName("change")]
        public decimal ChangePercentage { get; set; }
        /// <summary>
        /// Trade volume in last 24h
        /// </summary>
        [JsonPropertyName("volume")]
        public decimal Volume { get; set; }
        /// <summary>
        /// Market cap
        /// </summary>
        [JsonPropertyName("market_cap")]
        public decimal MarketCap { get; set; }
    }
}
