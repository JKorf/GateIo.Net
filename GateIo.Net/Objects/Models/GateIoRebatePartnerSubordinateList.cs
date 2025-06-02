using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Partner subordinate list
    /// </summary>
    public record GateIoRebatePartnerSubordinateList
    {
        /// <summary>
        /// Total
        /// </summary>
        [JsonPropertyName("total")]
        public long Total { get; set; }

        /// <summary>
        /// List
        /// </summary>
        [JsonPropertyName("list")]
        public List<GateIoRebatePartnerSubordinate>? List { get; set; }
    }
}
