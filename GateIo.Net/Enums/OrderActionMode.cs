using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Order action mode
    /// </summary>
    public enum OrderActionMode
    {
        /// <summary>
        /// Acknowledge, return only the most basic order info
        /// </summary>
        [Map("ACK")]
        Acknowledge,
        /// <summary>
        /// Result, return all but clearing info
        /// </summary>
        [Map("RESULT")]
        Result,
        /// <summary>
        /// Full order result
        /// </summary>
        [Map("FULL")]
        Full
    }
}
