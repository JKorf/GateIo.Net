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
        internal static GateIoRestOptions Default { get; set; } = new GateIoRestOptions()
        {
            Environment = GateIoEnvironment.Live,
            AutoTimestamp = true
        };

        /// <summary>
        /// ctor
        /// </summary>
        public GateIoRestOptions()
        {
            Default?.Set(this);
        }

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

        /// <summary>
        /// Rebate API options
        /// </summary>
        public RestApiOptions RebateOptions { get; private set; } = new RestApiOptions();

        internal GateIoRestOptions Set(GateIoRestOptions targetOptions)
        {
            targetOptions = base.Set<GateIoRestOptions>(targetOptions);
            targetOptions.BrokerId = BrokerId;
            targetOptions.SpotOptions = SpotOptions.Set(targetOptions.SpotOptions);
            targetOptions.PerpetualFuturesOptions = PerpetualFuturesOptions.Set(targetOptions.PerpetualFuturesOptions);
            targetOptions.RebateOptions = RebateOptions.Set(targetOptions.RebateOptions);
            return targetOptions;
        }
    }
}
