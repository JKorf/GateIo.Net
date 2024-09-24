using CryptoExchange.Net.SharedApis;

namespace GateIo.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Shared interface for Spot socket API usage
    /// </summary>
    public interface IGateIoSocketClientSpotApiShared :
        ITickerSocketClient,
        ITradeSocketClient,
        IBookTickerSocketClient,
        IKlineSocketClient,
        IOrderBookSocketClient,
        IBalanceSocketClient,
        IUserTradeSocketClient,
        ISpotOrderSocketClient
    {
    }
}
