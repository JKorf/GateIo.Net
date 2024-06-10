using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Trigger account type
    /// </summary>
    public enum TriggerAccountType
    {
        /// <summary>
        /// Normal spot
        /// </summary>
        [Map("normal")]
        Normal,
        /// <summary>
        /// Margin
        /// </summary>
        [Map("margin")]
        Margin,
        /// <summary>
        /// Cross margin
        /// </summary>
        [Map("cross_margin")]
        CrossMargin
    }
}
