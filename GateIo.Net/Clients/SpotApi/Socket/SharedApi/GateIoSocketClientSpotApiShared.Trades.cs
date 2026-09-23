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

        public SubscribeTradeOptions SubscribeTradeOptions { get; } = new SubscribeTradeOptions(_exchangeName, false)
        {
            SupportsMultipleSymbols = true
        };
        #region Subscribe To Trade Updates

        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(SubscribeTradeRequest request, Action<DataEvent<SharedTrade[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeTradeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            // USDT_USD returns error..
            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Where(x => x.SymbolName != "USDT_USD").Select(x => x.GetSymbol(FormatSymbol)) : [request.Symbol!.GetSymbol(FormatSymbol)];
            if (!symbols.Any())
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, ArgumentError.Invalid(nameof(SubscribeTradeRequest.Symbol), "Invalid symbol(s)"));
            
            var result = await _api.SubscribeToTradeUpdatesAsync(symbols, update => handler(update.ToType<SharedTrade[]>(new[] { 
                new SharedTrade(ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.Symbol), update.Data.Symbol, new SharedOrderQuantity(update.Data.Quantity), update.Data.Price, update.Data.CreateTime){
                Side = update.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
            } })), ct).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}
