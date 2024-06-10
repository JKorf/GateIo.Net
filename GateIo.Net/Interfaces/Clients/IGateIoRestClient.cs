using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using GateIo.Net.Interfaces.Clients.PerpetualFuturesApi;
using GateIo.Net.Interfaces.Clients.SpotApi;

namespace GateIo.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Gate.io Rest API. 
    /// </summary>
    public interface IGateIoRestClient : IRestClient
    {
        /// <summary>
        /// Spot API endpoints
        /// </summary>
        IGateIoRestClientSpotApi SpotApi { get; }
        /// <summary>
        /// Perpetual Futures API endpoints
        /// </summary>
        IGateIoRestClientPerpetualFuturesApi PerpetualFuturesApi { get; }

        /// <summary>
        /// Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.
        /// </summary>
        /// <param name="credentials">The credentials to set</param>
        void SetApiCredentials(ApiCredentials credentials);
    }
}
