using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

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
