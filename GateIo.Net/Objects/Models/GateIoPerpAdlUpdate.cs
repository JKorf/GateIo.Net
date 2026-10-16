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
    public record GateIoPerpAdlUpdate
    {
        /// <summary>
        /// ["<c>contract</c>"] Contract
        /// </summary>
        [JsonPropertyName("contract")]
        public string Contract { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>settle</c>"] Settlement asset
        /// </summary>
        [JsonPropertyName("settle")]
        public string Settle { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>state</c>"] Status
        /// </summary>
        [JsonPropertyName("state")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>update_time</c>"] Update timestamp
        /// </summary>
        [JsonPropertyName("update_time")]
        public DateTime UpdateTime { get; set; }

    }
}
