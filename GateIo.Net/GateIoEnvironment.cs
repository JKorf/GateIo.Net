using CryptoExchange.Net.Objects;
using GateIo.Net.Objects;

namespace GateIo.Net
{
    /// <summary>
    /// GateIo environments
    /// </summary>
    public class GateIoEnvironment : TradeEnvironment
    {
        /// <summary>
        /// Rest API address
        /// </summary>
        public string RestClientAddress { get; }

        /// <summary>
        /// Socket API spot address
        /// </summary>
        public string SpotSocketClientAddress { get; }

        /// <summary>
        /// Socket API futures address
        /// </summary>
        public string FuturesSocketClientAddress { get; }

        internal GateIoEnvironment(
            string name,
            string restAddress,
            string spotWebsocketAddress,
            string futuresWebsocketAddress) :
            base(name)
        {
            RestClientAddress = restAddress;
            SpotSocketClientAddress = spotWebsocketAddress;
            FuturesSocketClientAddress = futuresWebsocketAddress;
        }

        /// <summary>
        /// Live environment
        /// </summary>
        public static GateIoEnvironment Live { get; }
            = new GateIoEnvironment(TradeEnvironmentNames.Live,
                                     GateIoApiAddresses.Default.RestClientAddress,
                                     GateIoApiAddresses.Default.SpotSocketClientAddress,
                                     GateIoApiAddresses.Default.FuturesSocketClientAddress);

        /// <summary>
        /// Create a custom environment
        /// </summary>
        /// <param name="name"></param>
        /// <param name="spotRestAddress"></param>
        /// <param name="spotSocketStreamsAddress"></param>
        /// <param name="futuresSocketStreamsAddress"></param>
        /// <returns></returns>
        public static GateIoEnvironment CreateCustom(
                        string name,
                        string spotRestAddress,
                        string spotSocketStreamsAddress,
                        string futuresSocketStreamsAddress)
            => new GateIoEnvironment(name, spotRestAddress, spotSocketStreamsAddress, futuresSocketStreamsAddress);
    }
}
