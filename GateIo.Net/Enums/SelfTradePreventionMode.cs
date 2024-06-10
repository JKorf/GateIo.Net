using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Self trade prevention mode
    /// </summary>
    public enum SelfTradePreventionMode
    {
        /// <summary>
        /// None
        /// </summary>
        [Map("", "-")]
        None,
        /// <summary>
        /// Cancel newest
        /// </summary>
        [Map("cn")]
        CancelNewest,
        /// <summary>
        /// Cancel oldest
        /// </summary>
        [Map("co")]
        CancelOldest,
        /// <summary>
        /// Cancel both
        /// </summary>
        [Map("cb")]
        CancelBoth
    }
}
