using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Business type
    /// </summary>
    public enum BusinessType
    {
        /// <summary>
        /// Margin account
        /// </summary>
        [Map("margin")]
        Margin,
        /// <summary>
        /// Unified account
        /// </summary>
        [Map("unified")]
        Unified
    }
}
