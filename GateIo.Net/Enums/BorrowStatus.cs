using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Borrow loan status
    /// </summary>
    public enum BorrowStatus
    {
        /// <summary>
        /// Failed to borrow
        /// </summary>
        [Map("1")]
        Failed,
        /// <summary>
        /// Borrowed but not repaid
        /// </summary>
        [Map("2")]
        Borrowed,
        /// <summary>
        /// Repaid
        /// </summary>
        [Map("3")]
        Repaid
    }
}
