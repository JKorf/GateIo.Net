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
        public SubscribePositionOptions SubscribePositionOptions { get; } = new SubscribePositionOptions(_exchangeName, true)
        {
            ExchangeParameterRules = [
                ExchangeParameterRule.Required("SettleAsset", "Settlement asset, btc, usd or usdt", "usdt"),
                ExchangeParameterRule.Required("UserId", "The user id of the current API credentials", 123123123L)
            ]
        };
        #region Subscribe To Position Updates

        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(SubscribePositionRequest request, Action<DataEvent<SharedPosition[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribePositionOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToPositionUpdatesAsync(
                ExchangeParameters.GetValue<long>(request.ExchangeParameters, Exchange, "UserId")!,
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                update => handler(update.ToType<SharedPosition[]>(update.Data.Select(x => 
                    new SharedPosition(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Contract), 
                        x.Contract, 
                        new SharedOrderQuantity(contractQuantity: Math.Abs(x.Size)), 
                        update.DataTime ?? update.ReceiveTime)
                    {
                        AverageOpenPrice = x.EntryPrice == 0 ? null : x.EntryPrice,
                        PositionMode = x.PositionMode == Enums.PositionMode.Single ? SharedPositionMode.OneWay : SharedPositionMode.HedgeMode,
                        PositionSide = x.PositionMode == Enums.PositionMode.Single ? (x.Size > 0 ? SharedPositionSide.Long : SharedPositionSide.Short) : x.PositionMode == Enums.PositionMode.DualShort ? SharedPositionSide.Short : SharedPositionSide.Long,
                        LiquidationPrice = x.LiquidationPrice == 0 ? null : x.LiquidationPrice,
                        Leverage = x.Leverage == 0 ? null : x.Leverage
                    }).ToArray())),
                ct: ct).ConfigureAwait(false);

            return result;
        }

        #endregion

    }
}
