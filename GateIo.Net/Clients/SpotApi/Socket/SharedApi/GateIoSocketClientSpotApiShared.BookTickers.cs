using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using GateIo.Net.Clients.FuturesApi;
using GateIo.Net.Enums;
using GateIo.Net.Interfaces.Clients.SpotApi;
using GateIo.Net.Objects.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GateIo.Net.Clients.SpotApi
{
    internal partial class GateIoSocketClientSpotSharedApi
    {

        public SubscribeBookTickerOptions SubscribeBookTickerOptions { get; } = new SubscribeBookTickerOptions(_exchangeName, false)
        {
            SupportsMultipleSymbols = true
        };
        #region Subscribe To Book Ticker Updates

        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(SubscribeBookTickerRequest request, Action<DataEvent<SharedBookTicker>> handler, CancellationToken ct)
        {
            var validationError = SubscribeBookTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            // USDT_USD returns error..
            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Where(x => x.SymbolName != "USDT_USD").Select(x => x.GetSymbol(FormatSymbol)) : [request.Symbol!.GetSymbol(FormatSymbol)];
            if (!symbols.Any())
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, ArgumentError.Invalid(nameof(SubscribeTradeRequest.Symbol), "Invalid symbol(s)"));

            var result = await _api.SubscribeToBookTickerUpdatesAsync(symbols, update => handler(
                update.ToType(
                    new SharedBookTicker(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.Symbol),
                        update.Data.Symbol,
                        update.Data.BestAskPrice,
                        new SharedOrderQuantity(update.Data.BestAskQuantity), 
                        update.Data.BestBidPrice,
                        new SharedOrderQuantity(update.Data.BestBidQuantity)))), ct).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}
