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
        /// ctor for DI, use <see cref="CreateCustom"/> for creating a custom environment
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public GateIoEnvironment() : base(TradeEnvironmentNames.Live)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        { }

        /// <summary>
        /// Get the GateIo environment by name
        /// </summary>
        public static GateIoEnvironment? GetEnvironmentByName(string? name)
         => name switch
         {
             TradeEnvironmentNames.Live => Live,
             TradeEnvironmentNames.Testnet => Demo,
             "" => Live,
             null => Live,
             _ => default
         };

        /// <summary>
        /// Available environment names
        /// </summary>
        /// <returns></returns>
        public static string[] All => [Live.Name, Demo.Name];

        /// <summary>
        /// Live environment
        /// </summary>
        public static GateIoEnvironment Live { get; }
            = new GateIoEnvironment(TradeEnvironmentNames.Live,
                                     GateIoApiAddresses.Default.RestClientAddress,
                                     GateIoApiAddresses.Default.SpotSocketClientAddress,
                                     GateIoApiAddresses.Default.FuturesSocketClientAddress);

        /// <summary>
        /// Demo environment
        /// </summary>
        public static GateIoEnvironment Demo { get; }
            = new GateIoEnvironment(TradeEnvironmentNames.Testnet,
                GateIoApiAddresses.Demo.RestClientAddress,
                GateIoApiAddresses.Demo.SpotSocketClientAddress,
                GateIoApiAddresses.Demo.FuturesSocketClientAddress);

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
