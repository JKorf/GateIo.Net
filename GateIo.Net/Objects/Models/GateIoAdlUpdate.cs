using GateIo.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// ADL update
    /// </summary>
    public record GateIoAdlUpdate
    {
        /// <summary>
        /// Contract name
        /// </summary>
        [JsonPropertyName("contract")]
        public string Contract { get; set; } = string.Empty;
        /// <summary>
        /// Position mode
        /// </summary>
        [JsonPropertyName("mode")]
        public PositionMode PositionMode { get; set; }
        /// <summary>
        /// Rank
        /// </summary>
        [JsonPropertyName("rank_division")]
        public int Rank { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("time_ms")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// User id
        /// </summary>
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }
    }
}
