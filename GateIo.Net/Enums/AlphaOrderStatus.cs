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
    /// Order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AlphaOrderStatus>))]
    public enum AlphaOrderStatus
    {
        /// <summary>
        /// Processing
        /// </summary>
        [Map("1")]
        Processing,
        /// <summary>
        /// Successful
        /// </summary>
        [Map("2")]
        Successful,
        /// <summary>
        /// Failed
        /// </summary>
        [Map("3")]
        Failed,
        /// <summary>
        /// Cancelled
        /// </summary>
        [Map("4")]
        Cancelled,
        /// <summary>
        /// Buy order placed but transfer not completed
        /// </summary>
        [Map("5")]
        BuyPlacedNotTransferred,
        /// <summary>
        /// Canceled but not transferred yet
        /// </summary>
        [Map("6")]
        CanceledNotTransferred
    }
}
