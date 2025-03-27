using System;
using System.Text.Json.Serialization;
using GateIo.Net.Enums;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Partner subordinate
    /// </summary>
    public record GateIoRebatePartnerSubordinate
    {
        /// <summary>
        /// UserID
        /// </summary>
        [JsonPropertyName("user_id")]
        public long UserID { get; set; }

        /// <summary>
        /// UserJoinTime
        /// </summary>
        [JsonPropertyName("user_join_time")]
        public DateTime UserJoinTime { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        [JsonPropertyName("type")]
        public SubordinateType Type { get; set; }
    }
}
