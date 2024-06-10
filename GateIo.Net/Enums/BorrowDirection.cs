using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Borrow direction
    /// </summary>
    public enum BorrowDirection
    {
        /// <summary>
        /// Borrow
        /// </summary>
        [Map("borrow")]
        Borrow,
        /// <summary>
        /// Repay
        /// </summary>
        [Map("repay")]
        Repay
    }
}
