using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Options;
using GateIo.Net.Interfaces.Clients.PerpetualFuturesApi;
using GateIo.Net.Interfaces.Clients.SpotApi;

namespace GateIo.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Gate websocket API
    /// </summary>
    public interface IGateIoSocketClient : ISocketClient<GateIoCredentials>
    {
        /// <summary>
        /// Spot streams
        /// </summary>
        /// <see cref="IGateIoSocketClientSpotApi"/>
        IGateIoSocketClientSpotApi SpotApi { get; }
        /// <summary>
        /// Perpetual Futures streams
        /// </summary>
        /// <see cref="IGateIoSocketClientPerpetualFuturesApi"/>
        IGateIoSocketClientPerpetualFuturesApi PerpetualFuturesApi { get; }
    }
}
