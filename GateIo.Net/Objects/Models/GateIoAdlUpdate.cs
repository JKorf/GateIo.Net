using GateIo.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// ADL update
    /// </summary>
    public record GateIoAdlUpdate
    {
        /// <summary>
        /// ["<c>contract</c>"] Contract name
        /// </summary>
        [JsonPropertyName("contract")]
        public string Contract { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>mode</c>"] Position mode
        /// </summary>
        [JsonPropertyName("mode")]
        public PositionMode PositionMode { get; set; }
        /// <summary>
        /// ["<c>rank_division</c>"] Rank
        /// </summary>
        [JsonPropertyName("rank_division")]
        public int Rank { get; set; }
        /// <summary>
        /// ["<c>time_ms</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("time_ms")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>user_id</c>"] User id
        /// </summary>
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }
    }
}
