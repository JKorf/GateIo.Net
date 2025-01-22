using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Transaction type
    /// </summary>
    public enum TransactionType
    {
        /// <summary>
        /// Deposit
        /// </summary>
        [Map("deposit")]
        Deposit,
        /// <summary>
        /// Withdrawal
        /// </summary>
        [Map("withdraw")]
        Withdrawal
    }
}
