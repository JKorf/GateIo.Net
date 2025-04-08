using CryptoExchange.Net.Interfaces;
using System;

namespace GateIo.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// GateIo Spot API endpoints
    /// </summary>
    public interface IGateIoRestClientSpotApi : IRestApiClient, IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        /// <see cref="IGateIoRestClientSpotApiAccount"/>
        public IGateIoRestClientSpotApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        /// <see cref="IGateIoRestClientSpotApiExchangeData"/>
        public IGateIoRestClientSpotApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        /// <see cref="IGateIoRestClientSpotApiTrading"/>
        public IGateIoRestClientSpotApiTrading Trading { get; }

        /// <summary>
        /// Get the shared rest requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        IGateIoRestClientSpotApiShared SharedClient { get; }

    }
}
