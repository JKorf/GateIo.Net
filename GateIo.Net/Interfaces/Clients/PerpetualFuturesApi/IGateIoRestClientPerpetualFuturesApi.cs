using CryptoExchange.Net.Interfaces;
using GateIo.Net.Interfaces.Clients.SpotApi;
using System;

namespace GateIo.Net.Interfaces.Clients.PerpetualFuturesApi
{
    /// <summary>
    /// GateIo futures API endpoints
    /// </summary>
    public interface IGateIoRestClientPerpetualFuturesApi : IRestApiClient, IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        /// <see cref="IGateIoRestClientPerpetualFuturesApiAccount"/>
        public IGateIoRestClientPerpetualFuturesApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market data
        /// </summary>
        /// <see cref="IGateIoRestClientPerpetualFuturesApiExchangeData"/>
        public IGateIoRestClientPerpetualFuturesApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        /// <see cref="IGateIoRestClientPerpetualFuturesApiTrading"/>
        public IGateIoRestClientPerpetualFuturesApiTrading Trading { get; }

        /// <summary>
        /// Get the shared rest requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        public IGateIoRestClientPerpetualFuturesApiShared SharedClient { get; }
    }
}
