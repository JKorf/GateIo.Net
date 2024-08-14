using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis.Enums;
using CryptoExchange.Net.SharedApis.Interfaces.Socket;
using CryptoExchange.Net.SharedApis.Models.Socket;
using CryptoExchange.Net.SharedApis.RequestModels;
using CryptoExchange.Net.SharedApis.ResponseModels;
using CryptoExchange.Net.SharedApis.SubscribeModels;
using GateIo.Net.Interfaces.Clients.SpotApi;
using GateIo.Net.Objects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var result = await SubscribeToTickerUpdatesAsync(symbol, update => handler(update.As(new SharedTicker(update.Data.Symbol, update.Data.LastPrice, update.Data.HighPrice, update.Data.LowPrice))), ct).ConfigureAwait(false);

            return result;
        }

        async Task<CallResult<UpdateSubscription>> ITradeSocketClient.SubscribeToTradeUpdatesAsync(TradeSubscribeRequest request, Action<DataEvent<IEnumerable<SharedTrade>>> handler, CancellationToken ct)
        {
            var symbol = FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType);
            var result = await SubscribeToTradeUpdatesAsync(symbol, update => handler(update.As<IEnumerable<SharedTrade>>(new[] { new SharedTrade(update.Data.Price, update.Data.Quantity, update.Data.CreateTime) })), ct).ConfigureAwait(false);

            return result;
        }

        async Task<CallResult<UpdateSubscription>> IBookTickerSocketClient.SubscribeToBookTickerUpdatesAsync(BookTickerSubscribeRequest request, Action<DataEvent<SharedBookTicker>> handler, CancellationToken ct)
        {
            var symbol = FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType);
            var result = await SubscribeToBookTickerUpdatesAsync(symbol, update => handler(update.As(new SharedBookTicker(update.Data.BestAskPrice, update.Data.BestAskQuantity, update.Data.BestBidPrice, update.Data.BestBidQuantity))), ct).ConfigureAwait(false);

            return result;
        }

        async Task<CallResult<UpdateSubscription>> IBalanceSocketClient.SubscribeToBalanceUpdatesAsync(SharedRequest request, Action<DataEvent<IEnumerable<SharedBalance>>> handler, CancellationToken ct)
        {
            var result = await SubscribeToBalanceUpdatesAsync(
                update => handler(update.As(update.Data.Select(x => new SharedBalance(x.Asset, x.Available, x.Total)))),
                ct: ct).ConfigureAwait(false);

            return result;
        }

        async Task<CallResult<UpdateSubscription>> ISpotOrderSocketClient.SubscribeToOrderUpdatesAsync(SharedRequest request, Action<DataEvent<IEnumerable<SharedSpotOrder>>> handler, CancellationToken ct)
        {
            var result = await SubscribeToOrderUpdatesAsync(
                update => handler(update.As<IEnumerable<SharedSpotOrder>>(update.Data.Select(x =>
                    new SharedSpotOrder(
                        x.Symbol,
                        x.Id.ToString(),
                        x.OrderType == Enums.OrderType.Limit ? CryptoExchange.Net.SharedApis.Enums.SharedOrderType.Limit : x.OrderType == Enums.OrderType.Market ? CryptoExchange.Net.SharedApis.Enums.SharedOrderType.Market : CryptoExchange.Net.SharedApis.Enums.SharedOrderType.Other,
                        x.Side == Enums.OrderSide.Buy ? CryptoExchange.Net.SharedApis.Enums.SharedOrderSide.Buy : CryptoExchange.Net.SharedApis.Enums.SharedOrderSide.Sell,
                        GetOrderStatus(x),
                        x.CreateTime)
                    {
                        ClientOrderId = x.Text,
                        Quantity = x.Quantity,
                        QuantityFilled = x.Quantity - x.QuantityRemaining,
                        QuoteQuantityFilled = x.QuoteQuantityFilled,
                        UpdateTime = x.UpdateTime,
                        Price = x.Price,
                        AveragePrice = x.AveragePrice,
                        Fee = x.Fee,
                        FeeAsset = x.FeeAsset,
                        TimeInForce = x.TimeInForce == Enums.TimeInForce.ImmediateOrCancel ? CryptoExchange.Net.SharedApis.Enums.SharedTimeInForce.ImmediateOrCancel : x.TimeInForce == Enums.TimeInForce.FillOrKill ? CryptoExchange.Net.SharedApis.Enums.SharedTimeInForce.FillOrKill : x.TimeInForce == Enums.TimeInForce.GoodTillCancel ? CryptoExchange.Net.SharedApis.Enums.SharedTimeInForce.GoodTillCanceled : null
                    }
                ))),
                ct: ct).ConfigureAwait(false);

            return result;
        }

        async Task<CallResult<UpdateSubscription>> ISpotUserTradeSocketClient.SubscribeToUserTradeUpdatesAsync(SharedRequest request, Action<DataEvent<IEnumerable<SharedUserTrade>>> handler, CancellationToken ct)
        {
            var result = await SubscribeToUserTradeUpdatesAsync(
                update => handler(update.As(update.Data.Select(x =>
                    new SharedUserTrade(
                        x.Symbol,
                        x.OrderId.ToString(),
                        x.Id.ToString(),
                        x.Quantity,
                        x.Price!.Value,
                        x.CreateTime)
                    {
                        Role = x.Role == Enums.Role.Maker ? SharedRole.Maker : SharedRole.Taker,
                        Fee = x.Fee,
                        FeeAsset = x.FeeAsset
                    }
                ))),
                ct: ct).ConfigureAwait(false);

            return result;
        }

        private SharedOrderStatus GetOrderStatus(GateIoOrderUpdate update)
        {
            if (update.QuantityRemaining == 0)
                return SharedOrderStatus.Filled;

            if (update.Event != "finish")
            {
                return SharedOrderStatus.Open;
            }
            else
            {
                if (update.QuantityRemaining != 0)
                    return SharedOrderStatus.Canceled;

                return SharedOrderStatus.Filled;
            }
        }
    }
}
