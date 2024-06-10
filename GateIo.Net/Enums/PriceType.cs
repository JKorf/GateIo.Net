using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Price type
    /// </summary>
    public enum PriceType
    {
        /// <summary>
        /// Last trade price
        /// </summary>
        [Map("0")]
        LastTradePrice,
        /// <summary>
        /// Mark price
        /// </summary>
        [Map("1")]
        MarkPrice,
        /// <summary>
        /// Index price
        /// </summary>
        [Map("2")]
        IndexPrice
    }
}
