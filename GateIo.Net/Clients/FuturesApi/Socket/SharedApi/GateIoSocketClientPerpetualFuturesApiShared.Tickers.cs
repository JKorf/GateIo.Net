using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using GateIo.Net.Enums;
using GateIo.Net.Interfaces.Clients.PerpetualFuturesApi;
using GateIo.Net.Objects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace GateIo.Net.Clients.FuturesApi
{
    internal partial class GateIoSocketClientPerpetualFuturesSharedApi
    {
        #region Subscribe To Ticker Updates

        async Task<WebSocketResult<UpdateSubscription>> ISubscribeTickerSocket.SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<DataEvent<SharedTicker>> handler, CancellationToken ct)
            => await SubscribeToTickerUpdatesAsync(request, x => handler(x.ToType<SharedTicker>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeTickerOptions SubscribeTickerOptions { get; } = new SubscribeTickerOptions(_exchangeName)
        {
            SupportsMultipleSymbols = true,
            ExchangeParameterRules = [
                ExchangeParameterRule.Required("SettleAsset", "Settlement asset, btc, usd or usdt", "usdt")
            ]
        };
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<DataEvent<SharedSpotTicker>> handler, CancellationToken ct)
        {
            var validationError = SubscribeTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)) : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await _api.SubscribeToTickerUpdatesAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, symbols, update =>
            {
                foreach (var data in update.Data)
                {
                    handler(update.ToType(
                        new SharedSpotTicker(
                            ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, data.Contract),
                            data.Contract,
                            data.LastPrice,
                            data.HighPrice,
                            data.LowPrice,
                            new SharedOrderQuantity(data.BaseVolume, data.QuoteVolume, data.Volume),                            
                            data.ChangePercentage24h)
                    {
                    }));
                }
            }, ct).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}
