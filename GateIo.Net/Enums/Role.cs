using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Role
    /// </summary>
    public enum Role
    {
        /// <summary>
        /// Taker
        /// </summary>
        [Map("taker")]
        Taker,
        /// <summary>
        /// Maker
        /// </summary>
        [Map("maker")]
        Maker
    }
}
