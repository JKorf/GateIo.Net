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
        /// Active
        /// </summary>
        [Map("open")]
        Open,
        /// <summary>
        /// Finished
        /// </summary>
        [Map("finished")]
        Finished,
        /// <summary>
        ///  Order is not active, only for close-long-order or close-short-order
        /// </summary>
        [Map("inactive")]
        Inactive,
        /// <summary>
        /// Order is invalid, only for close-long-order or close-short-order
        /// </summary>
        [Map("invalid")]
        Invalid
    }
}
