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
        public static GateIoSocketOptions Default { get; set; } = new GateIoSocketOptions()
        {
            Environment = GateIoEnvironment.Live,
            SocketSubscriptionsCombineTarget = 10,
            MaxSocketConnections = 300
        };

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

        internal GateIoSocketOptions Copy()
        {
            var options = Copy<GateIoSocketOptions>();
            options.SpotOptions = SpotOptions.Copy<SocketApiOptions>();
            options.PerpetualFuturesOptions = PerpetualFuturesOptions.Copy<SocketApiOptions>();
            return options;
        }
    }
}
