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
    /// Order info
    /// </summary>
    public record GateIoAlphaOrderResult
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Order status
        /// </summary>
        [JsonPropertyName("status")]
        public AlphaOrderStatus Status { get; set; }
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Gas mode
        /// </summary>
        [JsonPropertyName("gas_mode")]
        public GasMode GasMode { get; set; }
        /// <summary>
        /// Create time
        /// </summary>
        [JsonPropertyName("create_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// Token address
        /// </summary>
        [JsonPropertyName("token_address")]
        public string TokenAddress { get; set; } = string.Empty;
        /// <summary>
        /// Network
        /// </summary>
        [JsonPropertyName("chain")]
        public string Network { get; set; } = string.Empty;
    }
}
