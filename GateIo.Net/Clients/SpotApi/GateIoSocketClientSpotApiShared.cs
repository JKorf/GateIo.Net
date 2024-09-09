﻿using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis.Enums;
using CryptoExchange.Net.SharedApis.Interfaces.Socket;
using CryptoExchange.Net.SharedApis.Models;
using CryptoExchange.Net.SharedApis.Models.FilterOptions;
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
        public string Exchange => GateIoExchange.ExchangeName;
        public ApiType[] SupportedApiTypes { get; } = new[] { ApiType.Spot };

        #region Ticker client
        SubscriptionOptions<SubscribeTickerRequest> ITickerSocketClient.SubscribeTickerOptions { get; } = new SubscriptionOptions<SubscribeTickerRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> ITickerSocketClient.SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<ExchangeEvent<SharedSpotTicker>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ITickerSocketClient)this).SubscribeTickerOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType));
            var result = await SubscribeToTickerUpdatesAsync(symbol, update => handler(update.AsExchangeEvent(Exchange, new SharedSpotTicker(update.Data.Symbol, update.Data.LastPrice, update.Data.HighPrice, update.Data.LowPrice, update.Data.BaseVolume))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Trade client

        SubscriptionOptions<SubscribeTradeRequest> ITradeSocketClient.SubscribeTradeOptions { get; } = new SubscriptionOptions<SubscribeTradeRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> ITradeSocketClient.SubscribeToTradeUpdatesAsync(SubscribeTradeRequest request, Action<ExchangeEvent<IEnumerable<SharedTrade>>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ITradeSocketClient)this).SubscribeTradeOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType));
            var result = await SubscribeToTradeUpdatesAsync(symbol, update => handler(update.AsExchangeEvent<IEnumerable<SharedTrade>>(Exchange, new[] { new SharedTrade(update.Data.Price, update.Data.Quantity, update.Data.CreateTime) })), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Book Ticker client

        SubscriptionOptions<SubscribeBookTickerRequest> IBookTickerSocketClient.SubscribeBookTickerOptions { get; } = new SubscriptionOptions<SubscribeBookTickerRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> IBookTickerSocketClient.SubscribeToBookTickerUpdatesAsync(SubscribeBookTickerRequest request, Action<ExchangeEvent<SharedBookTicker>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IBookTickerSocketClient)this).SubscribeBookTickerOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType));
            var result = await SubscribeToBookTickerUpdatesAsync(symbol, update => handler(update.AsExchangeEvent(Exchange, new SharedBookTicker(update.Data.BestAskPrice, update.Data.BestAskQuantity, update.Data.BestBidPrice, update.Data.BestBidQuantity))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Kline client
        SubscribeKlineOptions IKlineSocketClient.SubscribeKlineOptions { get; } = new SubscribeKlineOptions(false);
        async Task<ExchangeResult<UpdateSubscription>> IKlineSocketClient.SubscribeToKlineUpdatesAsync(SubscribeKlineRequest request, Action<ExchangeEvent<SharedKline>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeResult<UpdateSubscription>(Exchange, new ArgumentError("Interval not supported"));

            var validationError = ((IKlineSocketClient)this).SubscribeKlineOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType));
            var result = await SubscribeToKlineUpdatesAsync(symbol, interval, update => handler(update.AsExchangeEvent(Exchange, new SharedKline(update.Data.OpenTime, update.Data.ClosePrice, update.Data.HighPrice, update.Data.LowPrice, update.Data.OpenPrice, update.Data.BaseVolume))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Order Book client
        SubscribeOrderBookOptions IOrderBookSocketClient.SubscribeOrderBookOptions { get; } = new SubscribeOrderBookOptions(false, new[] { 5, 10, 20, 50, 100 });
        async Task<ExchangeResult<UpdateSubscription>> IOrderBookSocketClient.SubscribeToOrderBookUpdatesAsync(SubscribeOrderBookRequest request, Action<ExchangeEvent<SharedOrderBook>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IOrderBookSocketClient)this).SubscribeOrderBookOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType));
            var result = await SubscribeToPartialOrderBookUpdatesAsync(symbol, request.Limit ?? 20, 100, update => handler(update.AsExchangeEvent(Exchange, new SharedOrderBook(update.Data.Asks, update.Data.Bids))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Balance client
        SubscriptionOptions IBalanceSocketClient.SubscribeBalanceOptions { get; } = new SubscriptionOptions("SubscribeBalanceRequest", false);
        async Task<ExchangeResult<UpdateSubscription>> IBalanceSocketClient.SubscribeToBalanceUpdatesAsync(ApiType apiType, Action<ExchangeEvent<IEnumerable<SharedBalance>>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var result = await SubscribeToBalanceUpdatesAsync(
                update => handler(update.AsExchangeEvent(Exchange, update.Data.Select(x => new SharedBalance(x.Asset, x.Available, x.Total)))),
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Spot Order client

        SubscriptionOptions ISpotOrderSocketClient.SubscribeSpotOrderOptions { get; } = new SubscriptionOptions("SubscribeSpotOrderRequest", false);
        async Task<ExchangeResult<UpdateSubscription>> ISpotOrderSocketClient.SubscribeToSpotOrderUpdatesAsync(Action<ExchangeEvent<IEnumerable<SharedSpotOrder>>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ISpotOrderSocketClient)this).SubscribeSpotOrderOptions.ValidateRequest(Exchange, exchangeParameters, ApiType.Spot, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);
            var result = await SubscribeToOrderUpdatesAsync(
                update => handler(update.AsExchangeEvent(Exchange, update.Data.Select(x =>
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

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region User Trade client
        SubscriptionOptions IUserTradeSocketClient.SubscribeUserTradeOptions { get; } = new SubscriptionOptions("SubscribeUserTradeRequest", false);
        async Task<ExchangeResult<UpdateSubscription>> IUserTradeSocketClient.SubscribeToUserTradeUpdatesAsync(ApiType apiType, Action<ExchangeEvent<IEnumerable<SharedUserTrade>>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IUserTradeSocketClient)this).SubscribeUserTradeOptions.ValidateRequest(Exchange, exchangeParameters, apiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var result = await SubscribeToUserTradeUpdatesAsync(
                update => handler(update.AsExchangeEvent(Exchange, update.Data.Select(x =>
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

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

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
