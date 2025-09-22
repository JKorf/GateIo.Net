namespace GateIo.Net.Objects
{
    /// <summary>
    /// Api addresses
    /// </summary>
    public class GateIoApiAddresses
    {
        /// <summary>
        /// The address used by the GateIoRestClient for the API
        /// </summary>
        public string RestClientAddress { get; set; } = "";
        /// <summary>
        /// The address used by the GateIoSocketClient for the spot websocket API
        /// </summary>
        public string SpotSocketClientAddress { get; set; } = "";
        /// <summary>
        /// The address used by the GateIoSocketClient for the futures websocket API
        /// </summary>
        public string FuturesSocketClientAddress { get; set; } = "";

        /// <summary>
        /// The default addresses to connect to the Gate.io API
        /// </summary>
        public static GateIoApiAddresses Default = new GateIoApiAddresses
        {
            RestClientAddress = "https://api.gateio.ws",
            SpotSocketClientAddress = "wss://api.gateio.ws",
            FuturesSocketClientAddress = "wss://fx-ws.gateio.ws"
        };

        /// <summary>
        /// Demo addresses
        /// </summary>
        public static GateIoApiAddresses Demo = new GateIoApiAddresses
        {
            RestClientAddress = "https://api-testnet.gateapi.io",
            SpotSocketClientAddress = "wss://ws-testnet.gate.com",
            FuturesSocketClientAddress = "wss://ws-testnet.gate.com"
        };
    }
}
