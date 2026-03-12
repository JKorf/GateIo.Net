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
        /// ["<c>spot</c>"] Spot
        /// </summary>
        [Map("spot")]
        Spot,
        /// <summary>
        /// ["<c>margin</c>"] Margin
        /// </summary>
        [Map("margin")]
        Margin,
        /// <summary>
        /// ["<c>futures</c>"] Perpetual futures
        /// </summary>
        [Map("futures")]
        PerpertualFutures,
        /// <summary>
        /// ["<c>delivery</c>"] Delivery futures
        /// </summary>
        [Map("delivery")]
        DeliveryFutures,
        /// <summary>
        /// ["<c>cross_margin</c>"] Cross margin
        /// </summary>
        [Map("cross_margin")]
        CrossMargin,
        /// <summary>
        /// ["<c>options</c>"] Options
        /// </summary>
        [Map("options")]
        Options
    }
}
