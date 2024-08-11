using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis.Interfaces;
using CryptoExchange.Net.SharedApis.RequestModels;
using CryptoExchange.Net.SharedApis.ResponseModels;
using CryptoExchange.Net.SharedApis.SubscribeModels;
using GateIo.Net.Interfaces.Clients.SpotApi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GateIo.Net.Clients.SpotApi
{
    internal partial class GateIoSocketClientSpotApi : IGateIoSocketClientSpotApiShared
    {
        public string Exchange => "GateIo";

        async Task<CallResult<UpdateSubscription>> ITickerSocketClient.SubscribeToTickerUpdatesAsync(TickerSubscribeRequest request, Action<DataEvent<SharedTicker>> handler, CancellationToken ct)
        {
            var symbol = FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType);
            var result = await SubscribeToTickerUpdatesAsync(symbol, update => handler(update.As(new SharedTicker
            {
                Symbol = symbol,
                HighPrice = update.Data.HighPrice,
                LastPrice = update.Data.LastPrice,
                LowPrice = update.Data.LowPrice
            })), ct).ConfigureAwait(false);

            return result;
        }
    }
}
