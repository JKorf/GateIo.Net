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
        /// ["<c>user_id</c>"] User Id
        /// </summary>
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }

        /// <summary>
        /// ["<c>user_join_time</c>"] User join time
        /// </summary>
        [JsonPropertyName("user_join_time")]
        public DateTime UserJoinTime { get; set; }

        /// <summary>
        /// ["<c>type</c>"] Type
        /// </summary>
        [JsonPropertyName("type")]
        public SubordinateType Type { get; set; }
    }
}
