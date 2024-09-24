﻿using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using GateIo.Net.Interfaces.Clients.PerpetualFuturesApi;
using GateIo.Net.Objects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GateIo.Net.Clients.FuturesApi
{
    internal partial class GateIoSocketClientPerpetualFuturesApi: IGateIoSocketClientPerpetualFuturesApiShared
    {
        public string Exchange => "GateIo";
        public TradingMode[] SupportedTradingModes { get; } = new[] { TradingMode.PerpetualLinear, TradingMode.PerpetualInverse };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();

        #region Ticker client
        EndpointOptions<SubscribeTickerRequest> ITickerSocketClient.SubscribeTickerOptions { get; } = new EndpointOptions<SubscribeTickerRequest>(false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeResult<UpdateSubscription>> ITickerSocketClient.SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<ExchangeEvent<SharedSpotTicker>> handler, CancellationToken ct)
        {
            var validationError = ((ITickerSocketClient)this).SubscribeTickerOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var result = await SubscribeToTickerUpdatesAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, symbol, update =>
            {
                var data = update.Data.First();
                handler(update.AsExchangeEvent(Exchange, new SharedSpotTicker(symbol, data.LastPrice, data.HighPrice, data.LowPrice, data.BaseVolume, data.ChangePercentage24h)));
            }, ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Trade client

        EndpointOptions<SubscribeTradeRequest> ITradeSocketClient.SubscribeTradeOptions { get; } = new EndpointOptions<SubscribeTradeRequest>(false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeResult<UpdateSubscription>> ITradeSocketClient.SubscribeToTradeUpdatesAsync(SubscribeTradeRequest request, Action<ExchangeEvent<IEnumerable<SharedTrade>>> handler, CancellationToken ct)
        {
            var validationError = ((ITradeSocketClient)this).SubscribeTradeOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var result = await SubscribeToTradeUpdatesAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, symbol, update => handler(update.AsExchangeEvent<IEnumerable<SharedTrade>>(Exchange, update.Data.Select(x => new SharedTrade(Math.Abs(x.Quantity), x.Price, x.CreateTime)).ToArray())), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Book Ticker client

        EndpointOptions<SubscribeBookTickerRequest> IBookTickerSocketClient.SubscribeBookTickerOptions { get; } = new EndpointOptions<SubscribeBookTickerRequest>(false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeResult<UpdateSubscription>> IBookTickerSocketClient.SubscribeToBookTickerUpdatesAsync(SubscribeBookTickerRequest request, Action<ExchangeEvent<SharedBookTicker>> handler, CancellationToken ct)
        {
            var validationError = ((IBookTickerSocketClient)this).SubscribeBookTickerOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var result = await SubscribeToBookTickerUpdatesAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, symbol, update => handler(update.AsExchangeEvent(Exchange, new SharedBookTicker(update.Data.BestAskPrice, update.Data.BestAskQuantity, update.Data.BestBidPrice, update.Data.BestBidQuantity))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Kline client
        SubscribeKlineOptions IKlineSocketClient.SubscribeKlineOptions { get; } = new SubscribeKlineOptions(false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeResult<UpdateSubscription>> IKlineSocketClient.SubscribeToKlineUpdatesAsync(SubscribeKlineRequest request, Action<ExchangeEvent<SharedKline>> handler, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeResult<UpdateSubscription>(Exchange, new ArgumentError("Interval not supported"));

            var validationError = ((IKlineSocketClient)this).SubscribeKlineOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var result = await SubscribeToKlineUpdatesAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, symbol, interval, update => {
                foreach (var item in update.Data)
                    handler(update.AsExchangeEvent(Exchange, new SharedKline(item.OpenTime, item.ClosePrice, item.HighPrice, item.LowPrice, item.OpenPrice, item.QuoteVolume)));
                }, ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Order Book client
        SubscribeOrderBookOptions IOrderBookSocketClient.SubscribeOrderBookOptions { get; } = new SubscribeOrderBookOptions(false, new[] { 5, 10, 20, 50, 100 })
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeResult<UpdateSubscription>> IOrderBookSocketClient.SubscribeToOrderBookUpdatesAsync(SubscribeOrderBookRequest request, Action<ExchangeEvent<SharedOrderBook>> handler, CancellationToken ct)
        {
            var validationError = ((IOrderBookSocketClient)this).SubscribeOrderBookOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var result = await SubscribeToOrderBookUpdatesAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, symbol, request.Limit ?? 20, 100, update => handler(update.AsExchangeEvent(Exchange, new SharedOrderBook(update.Data.Asks, update.Data.Bids))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Balance client
        EndpointOptions<SubscribeBalancesRequest> IBalanceSocketClient.SubscribeBalanceOptions { get; } = new EndpointOptions<SubscribeBalancesRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt"),
                new ParameterDescription("UserId", typeof(long), "The user id of the current API credentials", 123123123L)
            }
        };
        async Task<ExchangeResult<UpdateSubscription>> IBalanceSocketClient.SubscribeToBalanceUpdatesAsync(SubscribeBalancesRequest request, Action<ExchangeEvent<IEnumerable<SharedBalance>>> handler, CancellationToken ct)
        {
            var validationError = ((IBalanceSocketClient)this).SubscribeBalanceOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var result = await SubscribeToBalanceUpdatesAsync(
                ExchangeParameters.GetValue<long>(request.ExchangeParameters, Exchange, "UserId")!,
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                update => handler(update.AsExchangeEvent<IEnumerable<SharedBalance>>(Exchange, update.Data.Select(x => new SharedBalance(x.Asset, x.Balance, x.Balance)).ToArray())),
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Futures Order client

        EndpointOptions<SubscribeFuturesOrderRequest> IFuturesOrderSocketClient.SubscribeFuturesOrderOptions { get; } = new EndpointOptions<SubscribeFuturesOrderRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt"),
                new ParameterDescription("UserId", typeof(long), "The user id of the current API credentials", 123123123L)
            }
        };
        async Task<ExchangeResult<UpdateSubscription>> IFuturesOrderSocketClient.SubscribeToFuturesOrderUpdatesAsync(SubscribeFuturesOrderRequest request, Action<ExchangeEvent<IEnumerable<SharedFuturesOrder>>> handler, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderSocketClient)this).SubscribeFuturesOrderOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);
            var result = await SubscribeToOrderUpdatesAsync(
                ExchangeParameters.GetValue<long>(request.ExchangeParameters, Exchange, "UserId")!,
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                update => handler(update.AsExchangeEvent<IEnumerable<SharedFuturesOrder>>(Exchange, update.Data.Select(x =>
                    new SharedFuturesOrder(
                        x.Contract,
                        x.Id.ToString(),
                        ParseOrderType(x),
                        x.Quantity > 0 ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        x.Status == Enums.OrderStatus.Open ? SharedOrderStatus.Open : x.FinishedAs == Enums.OrderFinishType.Filled ? SharedOrderStatus.Filled : SharedOrderStatus.Canceled,
                        x.CreateTime)
                    {
                        ClientOrderId = x.Text,
                        Quantity = Math.Abs(x.Quantity),
                        QuantityFilled = Math.Abs(x.Quantity) - x.QuantityRemaining,
                        UpdateTime = x.FinishTime ?? x.CreateTime,
                        OrderPrice = x.Price,
                        AveragePrice = x.FillPrice == 0 ? null : x.FillPrice,
                        ReduceOnly = x.IsReduceOnly,
                        TimeInForce = x.TimeInForce == Enums.TimeInForce.ImmediateOrCancel ? SharedTimeInForce.ImmediateOrCancel : x.TimeInForce == Enums.TimeInForce.FillOrKill ? SharedTimeInForce.FillOrKill : x.TimeInForce == Enums.TimeInForce.GoodTillCancel ? SharedTimeInForce.GoodTillCanceled : null
                    }
                ).ToArray())),
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }

        private SharedOrderType ParseOrderType(GateIoPerpOrder update)
        {
            if (update.Price == 0)
                return SharedOrderType.Market;

            return SharedOrderType.Limit;
        }
        #endregion

        #region User Trade client
        EndpointOptions<SubscribeUserTradeRequest> IUserTradeSocketClient.SubscribeUserTradeOptions { get; } = new EndpointOptions<SubscribeUserTradeRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt"),
                new ParameterDescription("UserId", typeof(long), "The user id of the current API credentials", 123123123L)
            }
        };
        async Task<ExchangeResult<UpdateSubscription>> IUserTradeSocketClient.SubscribeToUserTradeUpdatesAsync(SubscribeUserTradeRequest request, Action<ExchangeEvent<IEnumerable<SharedUserTrade>>> handler, CancellationToken ct)
        {
            var validationError = ((IUserTradeSocketClient)this).SubscribeUserTradeOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var result = await SubscribeToUserTradeUpdatesAsync(
                ExchangeParameters.GetValue<long>(request.ExchangeParameters, Exchange, "UserId")!,
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                update => handler(update.AsExchangeEvent<IEnumerable<SharedUserTrade>>(Exchange, update.Data.Select(x =>
                    new SharedUserTrade(
                        x.Contract,
                        x.OrderId.ToString(),
                        x.Id.ToString(),
                        x.Quantity > 0 ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        Math.Abs(x.Quantity),
                        x.Price,
                        x.CreateTime)
                    {
                        Role = x.Role == Enums.Role.Maker ? SharedRole.Maker : SharedRole.Taker,
                        Fee = x.Fee
                    }
                ).ToArray())),
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Position client
        EndpointOptions<SubscribePositionRequest> IPositionSocketClient.SubscribePositionOptions { get; } = new EndpointOptions<SubscribePositionRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt"),
                new ParameterDescription("UserId", typeof(long), "The user id of the current API credentials", 123123123L)
            }
        };
        async Task<ExchangeResult<UpdateSubscription>> IPositionSocketClient.SubscribeToPositionUpdatesAsync(SubscribePositionRequest request, Action<ExchangeEvent<IEnumerable<SharedPosition>>> handler, CancellationToken ct)
        {
            var validationError = ((IPositionSocketClient)this).SubscribePositionOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var result = await SubscribeToPositionUpdatesAsync(
                ExchangeParameters.GetValue<long>(request.ExchangeParameters, Exchange, "UserId")!,
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                update => handler(update.AsExchangeEvent<IEnumerable<SharedPosition>>(Exchange, update.Data.Select(x => new SharedPosition(x.Contract, Math.Abs(x.Size), update.Timestamp)
                {
                    AverageOpenPrice = x.EntryPrice,
                    PositionSide = x.PositionMode == Enums.PositionMode.Single ? (x.Size > 0 ? SharedPositionSide.Long : SharedPositionSide.Short) : x.PositionMode == Enums.PositionMode.DualShort ? SharedPositionSide.Short : SharedPositionSide.Long,
                    LiquidationPrice = x.LiquidationPrice,
                    Leverage = x.Leverage
                }).ToArray())),
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }

        #endregion
    }
}
