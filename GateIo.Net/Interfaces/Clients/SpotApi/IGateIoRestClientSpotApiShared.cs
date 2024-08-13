using CryptoExchange.Net.SharedApis.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GateIo.Net.Interfaces.Clients.SpotApi
{
    public interface IGateIoRestClientSpotApiShared :
        ITickerRestClient,
        ISpotSymbolRestClient,
        IKlineRestClient,
        ITradeRestClient,
        IBalanceRestClient
    {
    }
}
