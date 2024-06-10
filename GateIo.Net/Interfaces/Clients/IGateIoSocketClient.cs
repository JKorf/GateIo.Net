using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using GateIo.Net.Interfaces.Clients.PerpetualFuturesApi;
using GateIo.Net.Interfaces.Clients.SpotApi;

namespace GateIo.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Gate.io websocket API
    /// </summary>
    public interface IGateIoSocketClient : ISocketClient
    {
        /// <summary>
        /// Spot streams
        /// </summary>
        IGateIoSocketClientSpotApi SpotApi { get; }
        /// <summary>
        /// Perpetual Futures streams
        /// </summary>
        IGateIoSocketClientPerpetualFuturesApi PerpetualFuturesApi { get; }

        /// <summary>
        /// Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.
        /// </summary>
        /// <param name="credentials">The credentials to set</param>
        void SetApiCredentials(ApiCredentials credentials);
    }
}
