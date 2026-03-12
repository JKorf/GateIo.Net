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
        /// ["<c>0</c>"] Success
        /// </summary>
        [Map("0")]
        Success,
        /// <summary>
        /// ["<c>1</c>"] Exceeds max value
        /// </summary>
        [Map("1")]
        ExceedsMaxValue,
        /// <summary>
        /// ["<c>2</c>"] Below min value
        /// </summary>
        [Map("2")]
        BelowMinValue
    }
}
