using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Repay type
    /// </summary>
    public enum RepayType
    {
        /// <summary>
        /// No repay
        /// </summary>
        [Map("none")]
        None,
        /// <summary>
        /// Manual repayment
        /// </summary>
        [Map("manual_repay")]
        ManualRepay,
        /// <summary>
        /// Automatic repayment
        /// </summary>
        [Map("auto_repay")]
        AutoRepay,
        /// <summary>
        /// Automatic repayment after cancelation
        /// </summary>
        [Map("cancel_auto_repay")]
        CancelAutoRepay
    }
}
