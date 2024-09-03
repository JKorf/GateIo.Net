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
        public IGateIoRestClientPerpetualFuturesApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market data
        /// </summary>
        public IGateIoRestClientPerpetualFuturesApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        public IGateIoRestClientPerpetualFuturesApiTrading Trading { get; }

        public IGateIoRestClientPerpetualFuturesApiShared SharedClient { get; }
    }
}
