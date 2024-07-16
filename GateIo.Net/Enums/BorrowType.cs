using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Borrow type
    /// </summary>
    public enum BorrowType
    {
        /// <summary>
        /// Manual borrow
        /// </summary>
        [Map("manual_borrow")]
        ManualBorrow,
        /// <summary>
        /// Auto borrow
        /// </summary>
        [Map("auto_borrow")]
        AutoBorrow,
    }
}
