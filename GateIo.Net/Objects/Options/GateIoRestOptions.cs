using CryptoExchange.Net.Objects.Options;

namespace GateIo.Net.Objects.Options
{
    /// <summary>
    /// Options for the GateIoRestClient
    /// </summary>
    public class GateIoRestOptions : RestExchangeOptions<GateIoEnvironment>
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        public static GateIoRestOptions Default { get; set; } = new GateIoRestOptions()
        {
            Environment = GateIoEnvironment.Live,
            AutoTimestamp = true
        };

        /// <summary>
        /// Broker id
        /// </summary>
        public string? BrokerId { get; set; }

        /// <summary>
        /// Spot API options
        /// </summary>
        public RestApiOptions SpotOptions { get; private set; } = new RestApiOptions
        {
        };

        /// <summary>
        /// Futures API options
        /// </summary>
        public RestApiOptions PerpetualFuturesOptions { get; private set; } = new RestApiOptions();

        internal GateIoRestOptions Copy()
        {
            var options = Copy<GateIoRestOptions>();
            options.BrokerId = BrokerId;
            options.SpotOptions = SpotOptions.Copy<RestApiOptions>();
            options.PerpetualFuturesOptions = PerpetualFuturesOptions.Copy<RestApiOptions>();
            return options;
        }
    }
}
