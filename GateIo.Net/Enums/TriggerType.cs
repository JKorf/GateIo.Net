using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Price trigger type
    /// </summary>
    public enum TriggerType
    {
        /// <summary>
        /// Trigger when price is higher or equal to trigger price
        /// </summary>
        [Map(">=", "1")]
        EqualOrHigher,
        /// <summary>
        /// Trigger when price is lower or equal to trigger price
        /// </summary>
        [Map("<=", "2")]
        EqualOrLower
    }
}
