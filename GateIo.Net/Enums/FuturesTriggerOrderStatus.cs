using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Trigger order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FuturesTriggerOrderStatus>))]
    public enum FuturesTriggerOrderStatus
    {
        /// <summary>
        /// ["<c>open</c>"] Active
        /// </summary>
        [Map("open")]
        Open,
        /// <summary>
        /// ["<c>finished</c>"] Finished
        /// </summary>
        [Map("finished")]
        Finished,
        /// <summary>
        ///  ["<c>inactive</c>"] Order is not active, only for close-long-order or close-short-order
        /// </summary>
        [Map("inactive")]
        Inactive,
        /// <summary>
        /// ["<c>invalid</c>"] Order is invalid, only for close-long-order or close-short-order
        /// </summary>
        [Map("invalid")]
        Invalid
    }
}
