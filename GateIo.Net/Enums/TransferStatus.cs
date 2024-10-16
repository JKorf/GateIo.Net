﻿using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Transfer status
    /// </summary>
    public enum TransferStatus
    {
        /// <summary>
        /// Creating
        /// </summary>
        [Map("CREATING")]
        Creating,
        /// <summary>
        /// Waiting for receiving (please contact the other party to accept the transfer on the gate official website)
        /// </summary>
        [Map("PENDING")]
        Pending,
        /// <summary>
        /// Cancelling
        /// </summary>
        [Map("CANCELLING")]
        Cancelling,
        /// <summary>
        /// Revoked
        /// </summary>
        [Map("CANCELLED")]
        Cancelled,
        /// <summary>
        /// Rejection
        /// </summary>
        [Map("REFUSING")]
        Refusing,
        /// <summary>
        /// Rejected
        /// </summary>
        [Map("REFUSED")]
        Refused,
        /// <summary>
        /// Receiving
        /// </summary>
        [Map("RECEIVING")]
        Receiving,
        /// <summary>
        /// Success
        /// </summary>
        [Map("RECEIVED")]
        Received,
    }

}
