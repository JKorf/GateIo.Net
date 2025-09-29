using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Withdrawal status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<WithdrawalStatus>))]
    public enum WithdrawalStatus
    {
        /// <summary>
        /// Done
        /// </summary>
        [Map("DONE")]
        Done,
        /// <summary>
        /// Canceled
        /// </summary>
        [Map("CANCEL")]
        Canceled,
        /// <summary>
        /// Requested
        /// </summary>
        [Map("REQUEST")]
        Requested,
        /// <summary>
        /// Pending manual approval
        /// </summary>
        [Map("MANUAL")]
        PendingApproval,
        /// <summary>
        /// GateCode operation
        /// </summary>
        [Map("BCODE")]
        GateCode,
        /// <summary>
        /// Pending confirmation after sending
        /// </summary>
        [Map("EXTPEND")]
        PendingConfirmation,
        /// <summary>
        /// Failed confirmation
        /// </summary>
        [Map("FAIL")]
        FailedConfirmation,
        /// <summary>
        /// Invalid order
        /// </summary>
        [Map("INVALID")]
        Invalid,
        /// <summary>
        /// Verifying
        /// </summary>
        [Map("VERIFY")]
        Verifying,
        /// <summary>
        /// Processing
        /// </summary>
        [Map("PROCES")]
        Processing,
        /// <summary>
        /// Pending
        /// </summary>
        [Map("PEND")]
        Pending,
        /// <summary>
        /// Requires manual approval
        /// </summary>
        [Map("DMOVE")]
        RequiresManualApproval,
        /// <summary>
        /// Under review
        /// </summary>
        [Map("REVIEW")]
        Review,
        /// <summary>
        /// Waiting for confirmations
        /// </summary>
        [Map("TRACK")]
        Track
    }
}
