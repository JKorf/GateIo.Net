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
        /// ["<c>limit</c>"] LImit order
        /// </summary>
        [Map("limit")]
        Limit,
        /// <summary>
        /// ["<c>market</c>"] Market order
        /// </summary>
        [Map("market")]
        Market,
        /// <summary>
        /// ["<c>limit_repay</c>"] Limit repay
        /// </summary>
        [Map("limit_repay")]
        LimitRepay,
        /// <summary>
        /// ["<c>market_repay</c>"] Market repay
        /// </summary>
        [Map("market_repay")]
        MarketRepay,
        /// <summary>
        /// ["<c>limit_borrow</c>"] Limit borrow
        /// </summary>
        [Map("limit_borrow")]
        LimitBorrow,
        /// <summary>
        /// ["<c>market_borrow</c>"] Market borrow
        /// </summary>
        [Map("market_borrow")]
        MarketBorrow,
        /// <summary>
        /// ["<c>limit_borrow_repay</c>"] Limit borrow/repay
        /// </summary>
        [Map("limit_borrow_repay")]
        LimitBorrowRepay
    }
}
