using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Refund status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<RefundStatus>))]
    public enum RefundStatus
    {
        /// <summary>
        /// ["<c>REFUNDING</c>"] Refund in progress
        /// </summary>
        [Map("REFUNDING")]
        Refunding,
        /// <summary>
        /// ["<c>REFUNDED</c>"] Refund completed
        /// </summary>
        [Map("REFUNDED")]
        Refunded,
        /// <summary>
        /// ["<c>REFUND_FAILED</c>"] Refund failed
        /// </summary>
        [Map("REFUND_FAILED")]
        RefundFailed,
        /// <summary>
        /// ["<c>REJECTED</c>"] Refund rejected
        /// </summary>
        [Map("REJECTED")]
        Rejected,
    }

}
