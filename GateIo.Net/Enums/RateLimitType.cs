using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Rate limit type
    /// </summary>
    public enum RateLimitType
    {
        /// <summary>
        /// Spot
        /// </summary>
        [Map("spot")]
        Spot,
        /// <summary>
        /// Futures
        /// </summary>
        [Map("futures")]
        Futures
    }
}
