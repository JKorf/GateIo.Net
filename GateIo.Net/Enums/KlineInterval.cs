using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Kline interval
    /// </summary>
    [JsonConverter(typeof(EnumConverter<KlineInterval>))]
    public enum KlineInterval
    {
        /// <summary>
        /// ["<c>1s</c>"] 1 second
        /// </summary>
        [Map("1s")]
        OneSecond = 1,
        /// <summary>
        /// ["<c>10s</c>"] 10 seconds
        /// </summary>
        [Map("10s")]
        TenSeconds = 10,
        /// <summary>
        /// ["<c>1m</c>"] 1 minute
        /// </summary>
        [Map("1m")]
        OneMinute = 60,
        /// <summary>
        /// ["<c>5m</c>"] 5 minutes
        /// </summary>
        [Map("5m")]
        FiveMinutes = 60 * 5,
        /// <summary>
        /// ["<c>15m</c>"] 15 minutes
        /// </summary>
        [Map("15m")]
        FifteenMinutes = 60 * 15,
        /// <summary>
        /// ["<c>30m</c>"] 30 minutes
        /// </summary>
        [Map("30m")]
        ThirtyMinutes = 60 * 30,
        /// <summary>
        /// ["<c>1h</c>"] 1 hour
        /// </summary>
        [Map("1h")]
        OneHour = 60 * 60,
        /// <summary>
        /// ["<c>4h</c>"] 4 hours
        /// </summary>
        [Map("4h")]
        FourHours = 60 * 60 * 4,
        /// <summary>
        /// ["<c>8h</c>"] 8 hours
        /// </summary>
        [Map("8h")]
        EightHours = 60 * 60 * 8,
        /// <summary>
        /// ["<c>1d</c>"] 1 day
        /// </summary>
        [Map("1d")]
        OneDay = 60 * 60 * 24,
        /// <summary>
        /// ["<c>7d</c>"] 1 week
        /// </summary>
        [Map("7d")]
        OneWeek = 60 * 60 * 24 * 7,
        /// <summary>
        /// ["<c>30d</c>"] 1 month
        /// </summary>
        [Map("30d")]
        OneMonth = 60 * 60 * 24 * 30
    }
}
