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
    /// Quote status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<QuoteStatus>))]
    public enum QuoteStatus
    {
        /// <summary>
        /// Success
        /// </summary>
        [Map("0")]
        Success,
        /// <summary>
        /// Exceeds max value
        /// </summary>
        [Map("1")]
        ExceedsMaxValue,
        /// <summary>
        /// Below min value
        /// </summary>
        [Map("2")]
        BelowMinValue
    }
}
