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
        /// 1 second
        /// </summary>
        [Map("1s")]
        OneSecond = 1,
        /// <summary>
        /// 10 seconds
        /// </summary>
        [Map("10s")]
        TenSeconds = 10,
        /// <summary>
        /// 1 minute
        /// </summary>
        [Map("1m")]
        OneMinute = 60,
        /// <summary>
        /// 5 minutes
        /// </summary>
        [Map("5m")]
        FiveMinutes = 60 * 5,
        /// <summary>
        /// 15 minutes
        /// </summary>
        [Map("15m")]
        FifteenMinutes = 60 * 15,
        /// <summary>
        /// 30 minutes
        /// </summary>
        [Map("30m")]
        ThirtyMinutes = 60 * 30,
        /// <summary>
        /// 1 hour
        /// </summary>
        [Map("1h")]
        OneHour = 60 * 60,
        /// <summary>
        /// 4 hours
        /// </summary>
        [Map("4h")]
        FourHours = 60 * 60 * 4,
        /// <summary>
        /// 8 hours
        /// </summary>
        [Map("8h")]
        EightHours = 60 * 60 * 8,
        /// <summary>
        /// 1 day
        /// </summary>
        [Map("1d")]
        OneDay = 60 * 60 * 24,
        /// <summary>
        /// 1 week
        /// </summary>
        [Map("7d")]
        OneWeek = 60 * 60 * 24 * 7,
        /// <summary>
        /// 1 month
        /// </summary>
        [Map("30d")]
        OneMonth = 60 * 60 * 24 * 30
    }
}
