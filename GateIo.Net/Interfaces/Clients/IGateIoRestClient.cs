using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects.Options;
using GateIo.Net.Interfaces.Clients.PerpetualFuturesApi;
using GateIo.Net.Interfaces.Clients.RebateApi;
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
        /// <see cref="IGateIoRestClientSpotApi"/>
        IGateIoRestClientSpotApi SpotApi { get; }
        /// <summary>
        /// Perpetual Futures API endpoints
        /// </summary>
        /// <see cref="IGateIoRestClientPerpetualFuturesApi"/>
        IGateIoRestClientPerpetualFuturesApi PerpetualFuturesApi { get; }
        /// <summary>
        /// Rebate API endpoints
        /// </summary>
        /// <see cref="IGateIoRestClientRebateApi"/>
        IGateIoRestClientRebateApi RebateApi { get; }

        /// <summary>
        /// Update specific options
        /// </summary>
        /// <param name="options">Options to update. Only specific options are changeable after the client has been created</param>
        void SetOptions(UpdateOptions options);

        /// <summary>
        /// Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.
        /// </summary>
        /// <param name="credentials">The credentials to set</param>
        void SetApiCredentials(ApiCredentials credentials);
    }
}
