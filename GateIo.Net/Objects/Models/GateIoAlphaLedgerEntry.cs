using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Account ledger entry
    /// </summary>
    public record GateIoAlphaLedgerEntry
    {
        /// <summary>
        /// Entry id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Change quantity
        /// </summary>
        [JsonPropertyName("change")]
        public decimal Change { get; set; }
        /// <summary>
        /// Balance after change
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
    }
}
