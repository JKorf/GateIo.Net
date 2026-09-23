using CryptoExchange.Net.SharedApis;
using GateIo.Net.Interfaces.Clients;
using GateIo.Net.Interfaces.Clients.PerpetualFuturesApi;
using GateIo.Net.Interfaces.Clients.SpotApi;
using GateIo.Net.Objects.Options;
using Microsoft.Extensions.Options;

namespace GateIo.Net.Clients
{
    /// <inheritdoc />
    public class GateIoSharedApiClient : SharedApiClientBase, IGateIoSharedApiClient
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
            IGateIoSocketClient socketClient,
            IOptions<GateIoOptions> options)
            : base(options.Value.SharedApi.PreferredTransport,
                    restClient.SpotApi.SharedApi,
                    restClient.PerpetualFuturesApi.SharedApi,
                    socketClient.SpotApi.SharedApi,
                    socketClient.PerpetualFuturesApi.SharedApi
                  )
        {
            SpotRest = restClient.SpotApi.SharedApi;
            PerpetualFuturesRest = restClient.PerpetualFuturesApi.SharedApi;
            SpotSocket = socketClient.SpotApi.SharedApi;
            PerpetualFuturesSocket = socketClient.PerpetualFuturesApi.SharedApi;
        }
    }
}
