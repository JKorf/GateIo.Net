using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Order status
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// Open
        /// </summary>
        [Map("open")]
        Open,
        /// <summary>
        /// Closed
        /// </summary>
        [Map("closed", "finished")]
        Closed,
        /// <summary>
        /// Cancelled
        /// </summary>
        [Map("cancelled")]
        Canceled
    }
}
