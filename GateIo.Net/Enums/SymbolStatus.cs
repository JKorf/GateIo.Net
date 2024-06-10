using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Symbol trade status
    /// </summary>
    public enum SymbolStatus
    {
        /// <summary>
        /// Not tradeable
        /// </summary>
        [Map("untradable")]
        Untradable,
        /// <summary>
        /// Only buyable
        /// </summary>
        [Map("buyable")]
        Buyable,
        /// <summary>
        /// Only sellable
        /// </summary>
        [Map("sellable")]
        Sellable,
        /// <summary>
        /// Can be bought and sold
        /// </summary>
        [Map("tradable")]
        Tradable,
    }
}
