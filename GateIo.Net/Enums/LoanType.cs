using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Loan type
    /// </summary>
    public enum LoanType
    {
        /// <summary>
        /// Platform
        /// </summary>
        [Map("platform")]
        Platform,
        /// <summary>
        /// Margin
        /// </summary>
        [Map("margin")]
        Margin
    }
}
