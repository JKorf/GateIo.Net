using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Order type
    /// </summary>
    public enum NewOrderType
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
        Market
    }
}
