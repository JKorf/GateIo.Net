using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Loan type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<LoanType>))]
    public enum LoanType
    {
        /// <summary>
        /// Platform
        /// </summary>
        [Map("platform")]
        Platform,
        /// <summary>
        /// Margin
        /// </summary>
        [Map("margin")]
        Margin
    }
}
