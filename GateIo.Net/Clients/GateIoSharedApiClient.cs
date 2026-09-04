using GateIo.Net.Interfaces.Clients;
using GateIo.Net.Interfaces.Clients.PerpetualFuturesApi;
using GateIo.Net.Interfaces.Clients.SpotApi;

namespace GateIo.Net.Clients
{
    /// <inheritdoc />
    public class GateIoSharedApiClient : IGateIoSharedApiClient
    {
        /// <inheritdoc />
        public IGateIoRestClientSpotSharedApi SpotRest { get; }
        /// <inheritdoc />
        public IGateIoRestClientPerpetualFuturesSharedApi PerpetualFuturesRest { get; }
        /// <inheritdoc />
        public IGateIoSocketClientSpotSharedApi SpotSocket { get; }
        /// <inheritdoc />
        public IGateIoSocketClientPerpetualFuturesSharedApi PerpetualFuturesSocket { get; }

        /// <summary>
        /// ctor
        /// </summary>
        public GateIoSharedApiClient(
            IGateIoRestClient restClient,
            IGateIoSocketClient socketClient)
        {
            SpotRest = restClient.SpotApi.SharedApi;
            PerpetualFuturesRest = restClient.PerpetualFuturesApi.SharedApi;
            SpotSocket = socketClient.SpotApi.SharedApi;
            PerpetualFuturesSocket = socketClient.PerpetualFuturesApi.SharedApi;
        }
    }
}
