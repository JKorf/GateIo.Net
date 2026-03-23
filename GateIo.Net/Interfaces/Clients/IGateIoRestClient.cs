using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Options;
using GateIo.Net.Interfaces.Clients.PerpetualFuturesApi;
using GateIo.Net.Interfaces.Clients.RebateApi;
using GateIo.Net.Interfaces.Clients.SpotApi;

namespace GateIo.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Gate Rest API.
    /// </summary>
    public interface IGateIoRestClient : IRestClient<GateIoCredentials>
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
        /// Alpha API endpoints
        /// </summary>
        /// <see cref="IGateIoRestClientAlphaApi"/>
        IGateIoRestClientAlphaApi AlphaApi { get; }
    }
}
