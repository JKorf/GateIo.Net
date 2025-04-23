using CryptoExchange.Net.SharedApis;

namespace GateIo.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Shared interface for Perpetual futures rest API usage
    /// </summary>
    public interface IGateIoRestClientPerpetualFuturesApiShared :
        IBalanceRestClient,
        IFuturesTickerRestClient,
        IFuturesSymbolRestClient,
        IFuturesOrderRestClient,
        IKlineRestClient,
        IIndexPriceKlineRestClient,
        IRecentTradeRestClient,
        ITradeHistoryRestClient,
        ILeverageRestClient,
        IOrderBookRestClient,
        IOpenInterestRestClient,
        IFundingRateRestClient,
        IPositionModeRestClient,
        IPositionHistoryRestClient,
        IFeeRestClient,
        IFuturesOrderClientIdRestClient,
        IFuturesTriggerOrderRestClient,
        IFuturesTpSlRestClient,
        IBookTickerRestClient
    {
    }
}
