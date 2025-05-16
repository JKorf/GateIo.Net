using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Trigger order type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TriggerOrderType>))]
    public enum TriggerOrderType
    {
        /// <summary>
        /// Order take-profit/stop-loss, close long position
        /// </summary>
        [Map("close-long-order")]
        CloseLongOrder,
        /// <summary>
        /// Order take-profit/stop-loss, close short position
        /// </summary>
        [Map("close-short-order")]
        CloseShortOrder,
        /// <summary>
        /// Position take-profit/stop-loss, close long position
        /// </summary>
        [Map("close-long-position")]
        CloseLongPosition,
        /// <summary>
        /// Position take-profit/stop-loss, close short position
        /// </summary>
        [Map("close-short-position")]
        CloseShortPosition,
        /// <summary>
        /// Position planned take-profit/stop-loss, close long position
        /// </summary>
        [Map("plan-close-long-position")]
        PlanCloseLongPosition,
        /// <summary>
        /// Position planned take-profit/stop-loss, close short position
        /// </summary>
        [Map("plan-close-short-position")]
        PlanCloseShortPosition
    }
}
