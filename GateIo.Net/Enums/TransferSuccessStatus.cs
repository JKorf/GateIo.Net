using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Transfr status
    /// </summary>
    public enum TransferSuccessStatus
    {
        /// <summary>
        /// Pending
        /// </summary>
        [Map("PENDING")]
        Pending,
        /// <summary>
        /// Success
        /// </summary>
        [Map("SUCCESS")]
        Success,
        /// <summary>
        /// Failed
        /// </summary>
        [Map("FAIL")]
        Failed,
        /// <summary>
        /// Partial success
        /// </summary>
        [Map("PARTIAL_SUCCESS")]
        PartialSuccess
    }
}
