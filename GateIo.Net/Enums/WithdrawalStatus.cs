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
        /// ["<c>DONE</c>"] Done
        /// </summary>
        [Map("DONE")]
        Done,
        /// <summary>
        /// ["<c>CANCEL</c>"] Canceled
        /// </summary>
        [Map("CANCEL")]
        Canceled,
        /// <summary>
        /// ["<c>REQUEST</c>"] Requested
        /// </summary>
        [Map("REQUEST")]
        Requested,
        /// <summary>
        /// ["<c>MANUAL</c>"] Pending manual approval
        /// </summary>
        [Map("MANUAL")]
        PendingApproval,
        /// <summary>
        /// ["<c>BCODE</c>"] GateCode operation
        /// </summary>
        [Map("BCODE")]
        GateCode,
        /// <summary>
        /// ["<c>EXTPEND</c>"] Pending confirmation after sending
        /// </summary>
        [Map("EXTPEND")]
        PendingConfirmation,
        /// <summary>
        /// ["<c>FAIL</c>"] Failed confirmation
        /// </summary>
        [Map("FAIL")]
        FailedConfirmation,
        /// <summary>
        /// ["<c>INVALID</c>"] Invalid order
        /// </summary>
        [Map("INVALID")]
        Invalid,
        /// <summary>
        /// ["<c>VERIFY</c>"] Verifying
        /// </summary>
        [Map("VERIFY")]
        Verifying,
        /// <summary>
        /// ["<c>PROCES</c>"] Processing
        /// </summary>
        [Map("PROCES")]
        Processing,
        /// <summary>
        /// ["<c>PEND</c>"] Pending
        /// </summary>
        [Map("PEND")]
        Pending,
        /// <summary>
        /// ["<c>DMOVE</c>"] Requires manual approval
        /// </summary>
        [Map("DMOVE")]
        RequiresManualApproval,
        /// <summary>
        /// ["<c>REVIEW</c>"] Under review
        /// </summary>
        [Map("REVIEW")]
        Review,
        /// <summary>
        /// ["<c>TRACK</c>"] Waiting for confirmations
        /// </summary>
        [Map("TRACK")]
        Track,
        /// <summary>
        /// ["<c>BLOCKED</c>"] Rejected
        /// </summary>
        [Map("BLOCKED")]
        Blocked,
        /// <summary>
        /// ["<c>DEP_CREDITED</c>"] Deposit credited
        /// </summary>
        [Map("DEP_CREDITED")]
        Credited,
        /// <summary>
        /// ["<c>FINAL</c>"] Funds added to spot account
        /// </summary>
        [Map("FINAL")]
        Final
    }
}
