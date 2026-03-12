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
        /// ["<c>close-long-order</c>"] Order take-profit/stop-loss, close long position
        /// </summary>
        [Map("close-long-order")]
        CloseLongOrder,
        /// <summary>
        /// ["<c>close-short-order</c>"] Order take-profit/stop-loss, close short position
        /// </summary>
        [Map("close-short-order")]
        CloseShortOrder,
        /// <summary>
        /// ["<c>close-long-position</c>"] Position take-profit/stop-loss, close long position
        /// </summary>
        [Map("close-long-position")]
        CloseLongPosition,
        /// <summary>
        /// ["<c>close-short-position</c>"] Position take-profit/stop-loss, close short position
        /// </summary>
        [Map("close-short-position")]
        CloseShortPosition,
        /// <summary>
        /// ["<c>plan-close-long-position</c>"] Position planned take-profit/stop-loss, close long position
        /// </summary>
        [Map("plan-close-long-position")]
        PlanCloseLongPosition,
        /// <summary>
        /// ["<c>plan-close-short-position</c>"] Position planned take-profit/stop-loss, close short position
        /// </summary>
        [Map("plan-close-short-position")]
        PlanCloseShortPosition
    }
}
