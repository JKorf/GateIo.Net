using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Kline interval
    /// </summary>
    public enum KlineInterval
    {
        /// <summary>
        /// 10 seconds
        /// </summary>
        [Map("10s")]
        TenSeconds,
        /// <summary>
        /// 1 minute
        /// </summary>
        [Map("1m")]
        OneMinute,
        /// <summary>
        /// 5 minutes
        /// </summary>
        [Map("5m")]
        FiveMinutes,
        /// <summary>
        /// 15 minutes
        /// </summary>
        [Map("15m")]
        FifteenMinutes,
        /// <summary>
        /// 30 minutes
        /// </summary>
        [Map("30m")]
        ThirtyMinutes,
        /// <summary>
        /// 1 hour
        /// </summary>
        [Map("1h")]
        OneHour,
        /// <summary>
        /// 4 hours
        /// </summary>
        [Map("4h")]
        FourHours,
        /// <summary>
        /// 8 hours
        /// </summary>
        [Map("8h")]
        EightHours,
        /// <summary>
        /// 1 day
        /// </summary>
        [Map("1d")]
        OneDay,
        /// <summary>
        /// 1 week
        /// </summary>
        [Map("7d")]
        OneWeek,
        /// <summary>
        /// 1 month
        /// </summary>
        [Map("30d")]
        OneMonth
    }
}
