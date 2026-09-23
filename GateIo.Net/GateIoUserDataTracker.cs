using GateIo.Net.Interfaces.Clients;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.UserData;
using CryptoExchange.Net.Trackers.UserData.Objects;
using Microsoft.Extensions.Logging;

namespace GateIo.Net
{
    /// <inheritdoc/>
    public class GateIoUserSpotDataTracker : UserSpotDataTracker
    {
        /// <summary>
        /// ctor
        /// </summary>
        public GateIoUserSpotDataTracker(
            ILogger<GateIoUserSpotDataTracker> logger,
            IGateIoRestClient restClient,
            IGateIoSocketClient socketClient,
            string? userIdentifier,
            SpotUserDataTrackerConfig? config) : base(
                logger,
                restClient.SpotApi.SharedApi,

                restClient.SpotApi.SharedApi,
                socketClient.SpotApi.SharedApi,

                restClient.SpotApi.SharedApi,
                restClient.SpotApi.SharedApi,
                socketClient.SpotApi.SharedApi,

                restClient.SpotApi.SharedApi,
                socketClient.SpotApi.SharedApi,
                userIdentifier,
                config ?? new SpotUserDataTrackerConfig())
        {
        }
    }

    /// <inheritdoc/>
    public class GateIoUserPerpetualFuturesDataTracker : UserFuturesDataTracker
    {
        /// <inheritdoc/>
        protected override bool WebsocketPositionUpdatesAreFullSnapshots => false;

        /// <summary>
        /// ctor
        /// </summary>
        public GateIoUserPerpetualFuturesDataTracker(
            ILogger<GateIoUserPerpetualFuturesDataTracker> logger,
            IGateIoRestClient restClient,
            IGateIoSocketClient socketClient,
            string? userIdentifier,
            FuturesUserDataTrackerConfig? config,
            ExchangeParameters? exchangeParameters) : base(logger,
                restClient.PerpetualFuturesApi.SharedApi,

                restClient.PerpetualFuturesApi.SharedApi,
                socketClient.PerpetualFuturesApi.SharedApi,

                restClient.PerpetualFuturesApi.SharedApi,
                restClient.PerpetualFuturesApi.SharedApi,
                socketClient.PerpetualFuturesApi.SharedApi,

                restClient.PerpetualFuturesApi.SharedApi,
                socketClient.PerpetualFuturesApi.SharedApi,

                restClient.PerpetualFuturesApi.SharedApi,
                socketClient.PerpetualFuturesApi.SharedApi,
                userIdentifier,
                config ?? new FuturesUserDataTrackerConfig(),
                exchangeParameters: exchangeParameters)
        {
        }
    }
}
