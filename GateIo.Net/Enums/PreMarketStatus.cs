using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Pre-Market trading status
    /// </summary>
    public enum PreMarketStatus
    {
        /// <summary>
        /// Normal
        /// </summary>
        [Map("normal")]
        Normal,
        /// <summary>
        /// Pre-Market
        /// </summary>
        [Map("pre-market")]
        PreMarket
    }
}
