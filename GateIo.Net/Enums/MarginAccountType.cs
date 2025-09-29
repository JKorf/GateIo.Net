using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Margin account type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarginAccountType>))]
    public enum MarginAccountType
    {
        /// <summary>
        /// Maintenance margin rate account
        /// </summary>
        [Map("mmr")]
        MaintenanceMarginRate,
        /// <summary>
        /// Risk rate account
        /// </summary>
        [Map("risk")]
        RiskRate,
        /// <summary>
        /// Inactive
        /// </summary>
        [Map("inactive")]
        Inactive
    }
}
