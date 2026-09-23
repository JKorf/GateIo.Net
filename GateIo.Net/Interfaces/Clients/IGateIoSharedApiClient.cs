using CryptoExchange.Net.SharedApis;
using GateIo.Net.Interfaces.Clients.PerpetualFuturesApi;
using GateIo.Net.Interfaces.Clients.SpotApi;

namespace GateIo.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for the shared REST and WebSocket API implementations of Gate.io
    /// </summary>
    public interface IGateIoSharedApiClient : ISharedApiClientBase
    {
        /// <summary>
        /// Spot REST shared API implementations
        /// </summary>
        IGateIoRestClientSpotSharedApi SpotRest { get; }

        /// <summary>
        /// Perpetual Futures REST shared API implementations
        /// </summary>
        IGateIoRestClientPerpetualFuturesSharedApi PerpetualFuturesRest { get; }

        /// <summary>
        /// Spot WebSocket shared API implementations
        /// </summary>
        IGateIoSocketClientSpotSharedApi SpotSocket { get; }

        /// <summary>
        /// Perpetual Futures WebSocket shared API implementations
        /// </summary>
        IGateIoSocketClientPerpetualFuturesSharedApi PerpetualFuturesSocket { get; }
    }
}
