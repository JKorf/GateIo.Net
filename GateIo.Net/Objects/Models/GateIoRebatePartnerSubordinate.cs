using System;
using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net.Enums;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Partner subordinate
    /// </summary>
    [SerializationModel]
    public record GateIoRebatePartnerSubordinate
    {
        /// <summary>
        /// User Id
        /// </summary>
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }

        /// <summary>
        /// User join time
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
