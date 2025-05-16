using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Order type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderType>))]
    public enum OrderType
    {
        /// <summary>
        /// LImit order
        /// </summary>
        [Map("limit")]
        Limit,
        /// <summary>
        /// Market order
        /// </summary>
        [Map("market")]
        Market,
        /// <summary>
        /// Limit repay
        /// </summary>
        [Map("limit_repay")]
        LimitRepay,
        /// <summary>
        /// Market repay
        /// </summary>
        [Map("market_repay")]
        MarketRepay,
        /// <summary>
        /// Limit borrow
        /// </summary>
        [Map("limit_borrow")]
        LimitBorrow,
        /// <summary>
        /// Market borrow
        /// </summary>
        [Map("market_borrow")]
        MarketBorrow,
        /// <summary>
        /// Limit borrow/repay
        /// </summary>
        [Map("limit_borrow_repay")]
        LimitBorrowRepay
    }
}
