using GateIo.Net.Clients;
using GateIo.Net.Interfaces.Clients;

namespace CryptoExchange.Net.Interfaces
{
    /// <summary>
    /// Extensions for the ICryptoRestClient and ICryptoSocketClient interfaces
    /// </summary>
    public static class CryptoClientExtensions
    {
        /// <summary>
        /// Get the Gate REST Api client
        /// </summary>
        /// <param name="baseClient"></param>
        /// <returns></returns>
        public static IGateIoRestClient GateIo(this ICryptoRestClient baseClient) => baseClient.TryGet<IGateIoRestClient>(() => new GateIoRestClient());

        /// <summary>
        /// Get the Gate Websocket Api client
        /// </summary>
        /// <param name="baseClient"></param>
        /// <returns></returns>
        public static IGateIoSocketClient GateIo(this ICryptoSocketClient baseClient) => baseClient.TryGet<IGateIoSocketClient>(() => new GateIoSocketClient());
    }
}
