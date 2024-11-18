using CryptoExchange.Net.Objects.Options;

namespace GateIo.Net.Objects.Options
{
    /// <summary>
    /// Options for the GateIoSocketClient
    /// </summary>
    public class GateIoSocketOptions : SocketExchangeOptions<GateIoEnvironment>
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        internal static GateIoSocketOptions Default { get; set; } = new GateIoSocketOptions()
        {
            Environment = GateIoEnvironment.Live,
            SocketSubscriptionsCombineTarget = 10,
            MaxSocketConnections = 300
        };

        /// <summary>
        /// ctor
        /// </summary>
        public GateIoSocketOptions()
        {
            Default?.Set(this);
        }

        /// <summary>
        /// Broker id
        /// </summary>
        public string? BrokerId { get; set; }

        /// <summary>
        /// Options for the Spot API
        /// </summary>
        public SocketApiOptions SpotOptions { get; private set; } = new SocketApiOptions()
        {
        };

        /// <summary>
        /// Options for the Perpetual Futures API
        /// </summary>
        public SocketApiOptions PerpetualFuturesOptions { get; private set; } = new SocketApiOptions();

        internal GateIoSocketOptions Set(GateIoSocketOptions targetOptions)
        {
            targetOptions = base.Set<GateIoSocketOptions>(targetOptions);
            targetOptions.BrokerId = BrokerId;
            targetOptions.SpotOptions = SpotOptions.Set(targetOptions.SpotOptions);
            targetOptions.PerpetualFuturesOptions = PerpetualFuturesOptions.Set(targetOptions.PerpetualFuturesOptions);
            return targetOptions;
        }
    }
}
