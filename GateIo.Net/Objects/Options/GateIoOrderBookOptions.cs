﻿using CryptoExchange.Net.Objects.Options;
using System;

namespace GateIo.Net.Objects.Options
{
    /// <summary>
    /// Options for the GateIo SymbolOrderBook
    /// </summary>
    public class GateIoOrderBookOptions : OrderBookOptions
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        public static GateIoOrderBookOptions Default { get; set; } = new GateIoOrderBookOptions();

        /// <summary>
        /// The top amount of results to keep in sync. If for example limit=10 is used, the order book will contain the 10 best bids and 10 best asks. Leaving this null will sync the full order book
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// Update interval in milliseconds, either 100 or 1000. Defaults to 1000
        /// </summary>
        public int? UpdateInterval { get; set; }

        /// <summary>
        /// After how much time we should consider the connection dropped if no data is received for this time after the initial subscriptions
        /// </summary>
        public TimeSpan? InitialDataTimeout { get; set; }

        internal GateIoOrderBookOptions Copy()
        {
            var result = Copy<GateIoOrderBookOptions>();
            result.Limit = Limit;
            result.UpdateInterval = UpdateInterval;
            result.InitialDataTimeout = InitialDataTimeout;
            return result;
        }
    }
}
