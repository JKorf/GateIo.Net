using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Account type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AccountType>))]
    public enum AccountType
    {
        /// <summary>
        /// Spot
        /// </summary>
        [Map("spot")]
        Spot,
        /// <summary>
        /// Margin
        /// </summary>
        [Map("margin")]
        Margin,
        /// <summary>
        /// Perpetual futures
        /// </summary>
        [Map("futures")]
        PerpertualFutures,
        /// <summary>
        /// Delivery futures
        /// </summary>
        [Map("delivery")]
        DeliveryFutures,
        /// <summary>
        /// Cross margin
        /// </summary>
        [Map("cross_margin")]
        CrossMargin,
        /// <summary>
        /// Options
        /// </summary>
        [Map("options")]
        Options
    }
}
