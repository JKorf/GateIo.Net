using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Time in force
    /// </summary>
    public enum TimeInForce
    {
        /// <summary>
        /// Good till canceled
        /// </summary>
        [Map("gtc")]
        GoodTillCancel,
        /// <summary>
        /// Immediate or cancel
        /// </summary>
        [Map("ioc")]
        ImmediateOrCancel,
        /// <summary>
        /// Post only order
        /// </summary>
        [Map("poc")]
        PendingOrCancel,
        /// <summary>
        /// Fill or kill
        /// </summary>
        [Map("fok")]
        FillOrKill
    }
}
