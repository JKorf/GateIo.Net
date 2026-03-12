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
        /// ["<c>mmr</c>"] Maintenance margin rate account
        /// </summary>
        [Map("mmr")]
        MaintenanceMarginRate,
        /// <summary>
        /// ["<c>risk</c>"] Risk rate account
        /// </summary>
        [Map("risk")]
        RiskRate,
        /// <summary>
        /// ["<c>inactive</c>"] Inactive
        /// </summary>
        [Map("inactive")]
        Inactive
    }
}
