using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis.Enums;
using CryptoExchange.Net.SharedApis.Interfaces.Socket;
using CryptoExchange.Net.SharedApis.Interfaces.Socket.Futures;
using CryptoExchange.Net.SharedApis.Models;
using CryptoExchange.Net.SharedApis.Models.FilterOptions;
using CryptoExchange.Net.SharedApis.Models.Socket;
using CryptoExchange.Net.SharedApis.RequestModels;
using CryptoExchange.Net.SharedApis.ResponseModels;
using CryptoExchange.Net.SharedApis.SubscribeModels;
using GateIo.Net.Interfaces.Clients.PerpetualFuturesApi;
using GateIo.Net.Interfaces.Clients.SpotApi;
using GateIo.Net.Objects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GateIo.Net.Clients.FuturesApi
{
    internal partial class GateIoSocketClientPerpetualFuturesApi: IGateIoSocketClientPerpetualFuturesApiShared
    {
        public string Exchange => "GateIo";
        public ApiType[] SupportedApiTypes { get; } = new[] { ApiType.PerpetualLinear, ApiType.PerpetualInverse };

        #region Ticker client
        SubscriptionOptions<SubscribeTickerRequest> ITickerSocketClient.SubscribeTickerOptions { get; } = new SubscriptionOptions<SubscribeTickerRequest>(false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeResult<UpdateSubscription>> ITickerSocketClient.SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<ExchangeEvent<SharedSpotTicker>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ITickerSocketClient)this).SubscribeTickerOptions.ValidateRequest(Exchange, request, exchangeParameters, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var result = await SubscribeToTickerUpdatesAsync(exchangeParameters!.GetValue<string>(Exchange, "SettleAsset")!, symbol, update =>
            {
                var data = update.Data.First();
                handler(update.AsExchangeEvent(Exchange, new SharedSpotTicker(symbol, data.LastPrice, data.HighPrice, data.LowPrice, data.BaseVolume)));
            }, ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Trade client

        SubscriptionOptions<SubscribeTradeRequest> ITradeSocketClient.SubscribeTradeOptions { get; } = new SubscriptionOptions<SubscribeTradeRequest>(false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeResult<UpdateSubscription>> ITradeSocketClient.SubscribeToTradeUpdatesAsync(SubscribeTradeRequest request, Action<ExchangeEvent<IEnumerable<SharedTrade>>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ITradeSocketClient)this).SubscribeTradeOptions.ValidateRequest(Exchange, request, exchangeParameters, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var result = await SubscribeToTradeUpdatesAsync(exchangeParameters!.GetValue<string>(Exchange, "SettleAsset")!, symbol, update => handler(update.AsExchangeEvent<IEnumerable<SharedTrade>>(Exchange, update.Data.Select(x => new SharedTrade(x.Price, x.Quantity, x.CreateTime)))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Book Ticker client

        SubscriptionOptions<SubscribeBookTickerRequest> IBookTickerSocketClient.SubscribeBookTickerOptions { get; } = new SubscriptionOptions<SubscribeBookTickerRequest>(false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeResult<UpdateSubscription>> IBookTickerSocketClient.SubscribeToBookTickerUpdatesAsync(SubscribeBookTickerRequest request, Action<ExchangeEvent<SharedBookTicker>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IBookTickerSocketClient)this).SubscribeBookTickerOptions.ValidateRequest(Exchange, request, exchangeParameters, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var result = await SubscribeToBookTickerUpdatesAsync(exchangeParameters!.GetValue<string>(Exchange, "SettleAsset")!, symbol, update => handler(update.AsExchangeEvent(Exchange, new SharedBookTicker(update.Data.BestAskPrice, update.Data.BestAskQuantity, update.Data.BestBidPrice, update.Data.BestBidQuantity))), ct).ConfigureAwait(false);

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
        async Task<ExchangeResult<UpdateSubscription>> IKlineSocketClient.SubscribeToKlineUpdatesAsync(SubscribeKlineRequest request, Action<ExchangeEvent<SharedKline>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeResult<UpdateSubscription>(Exchange, new ArgumentError("Interval not supported"));

            var validationError = ((IKlineSocketClient)this).SubscribeKlineOptions.ValidateRequest(Exchange, request, exchangeParameters, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var result = await SubscribeToKlineUpdatesAsync(exchangeParameters!.GetValue<string>(Exchange, "SettleAsset")!, symbol, interval, update => {
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
        async Task<ExchangeResult<UpdateSubscription>> IOrderBookSocketClient.SubscribeToOrderBookUpdatesAsync(SubscribeOrderBookRequest request, Action<ExchangeEvent<SharedOrderBook>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IOrderBookSocketClient)this).SubscribeOrderBookOptions.ValidateRequest(Exchange, request, exchangeParameters, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var result = await SubscribeToOrderBookUpdatesAsync(exchangeParameters!.GetValue<string>(Exchange, "SettleAsset")!, symbol, request.Limit ?? 20, 100, update => handler(update.AsExchangeEvent(Exchange, new SharedOrderBook(update.Data.Asks, update.Data.Bids))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Balance client
        SubscriptionOptions IBalanceSocketClient.SubscribeBalanceOptions { get; } = new SubscriptionOptions("SubscribeBalanceRequest", true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt"),
                new ParameterDescription("UserId", typeof(long), "The user id of the current API credentials", 123123123L)
            }
        };
        async Task<ExchangeResult<UpdateSubscription>> IBalanceSocketClient.SubscribeToBalanceUpdatesAsync(Action<ExchangeEvent<IEnumerable<SharedBalance>>> handler, ApiType? apiType, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IBalanceSocketClient)this).SubscribeBalanceOptions.ValidateRequest(Exchange, exchangeParameters, apiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var result = await SubscribeToBalanceUpdatesAsync(
                exchangeParameters!.GetValue<long>(Exchange, "UserId")!,
                exchangeParameters!.GetValue<string>(Exchange, "SettleAsset")!,
                update => handler(update.AsExchangeEvent(Exchange, update.Data.Select(x => new SharedBalance(x.Asset, x.Balance, x.Balance)))),
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Futures Order client

        SubscriptionOptions IFuturesOrderSocketClient.SubscribeFuturesOrderOptions { get; } = new SubscriptionOptions("SubscribeFuturesOrderRequest", true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt"),
                new ParameterDescription("UserId", typeof(long), "The user id of the current API credentials", 123123123L)
            }
        };
        async Task<ExchangeResult<UpdateSubscription>> IFuturesOrderSocketClient.SubscribeToFuturesOrderUpdatesAsync(Action<ExchangeEvent<IEnumerable<SharedFuturesOrder>>> handler, ApiType? apiType, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderSocketClient)this).SubscribeFuturesOrderOptions.ValidateRequest(Exchange, exchangeParameters, ApiType.Spot, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);
            var result = await SubscribeToOrderUpdatesAsync(
                exchangeParameters!.GetValue<long>(Exchange, "UserId")!,
                exchangeParameters!.GetValue<string>(Exchange, "SettleAsset")!,
                update => handler(update.AsExchangeEvent(Exchange, update.Data.Select(x =>
                    new SharedFuturesOrder(
                        x.Contract,
                        x.Id.ToString(),
                        ParseOrderType(x),
                        x.Quantity > 0 ? CryptoExchange.Net.SharedApis.Enums.SharedOrderSide.Buy : CryptoExchange.Net.SharedApis.Enums.SharedOrderSide.Sell,
                        x.Status == Enums.OrderStatus.Open ? SharedOrderStatus.Open : x.Status == Enums.OrderStatus.Canceled ? SharedOrderStatus.Canceled : SharedOrderStatus.Filled,
                        x.CreateTime)
                    {
                        ClientOrderId = x.Text,
                        Quantity = x.Quantity,
                        QuantityFilled = x.Quantity - x.QuantityRemaining,
                        UpdateTime = x.FinishTime ?? x.CreateTime,
                        Price = x.Price,
                        AveragePrice = x.FillPrice,
                        ReduceOnly = x.IsReduceOnly,
                        TimeInForce = x.TimeInForce == Enums.TimeInForce.ImmediateOrCancel ? CryptoExchange.Net.SharedApis.Enums.SharedTimeInForce.ImmediateOrCancel : x.TimeInForce == Enums.TimeInForce.FillOrKill ? CryptoExchange.Net.SharedApis.Enums.SharedTimeInForce.FillOrKill : x.TimeInForce == Enums.TimeInForce.GoodTillCancel ? CryptoExchange.Net.SharedApis.Enums.SharedTimeInForce.GoodTillCanceled : null
                    }
                ))),
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
        SubscriptionOptions IUserTradeSocketClient.SubscribeUserTradeOptions { get; } = new SubscriptionOptions("SubscribeUserTradeRequest", true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt"),
                new ParameterDescription("UserId", typeof(long), "The user id of the current API credentials", 123123123L)
            }
        };
        async Task<ExchangeResult<UpdateSubscription>> IUserTradeSocketClient.SubscribeToUserTradeUpdatesAsync(Action<ExchangeEvent<IEnumerable<SharedUserTrade>>> handler, ApiType? apiType, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IUserTradeSocketClient)this).SubscribeUserTradeOptions.ValidateRequest(Exchange, exchangeParameters, apiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var result = await SubscribeToUserTradeUpdatesAsync(
                exchangeParameters!.GetValue<long>(Exchange, "UserId")!,
                exchangeParameters!.GetValue<string>(Exchange, "SettleAsset")!,
                update => handler(update.AsExchangeEvent(Exchange, update.Data.Select(x =>
                    new SharedUserTrade(
                        x.Contract,
                        x.OrderId.ToString(),
                        x.Id.ToString(),
                        x.Quantity,
                        x.Price,
                        x.CreateTime)
                    {
                        Role = x.Role == Enums.Role.Maker ? SharedRole.Maker : SharedRole.Taker,
                        Fee = x.Fee
                    }
                ))),
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Position client
        SubscriptionOptions IPositionSocketClient.SubscribePositionOptions { get; } = new SubscriptionOptions("SubscribePositionRequest", true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt"),
                new ParameterDescription("UserId", typeof(long), "The user id of the current API credentials", 123123123L)
            }
        };
        async Task<ExchangeResult<UpdateSubscription>> IPositionSocketClient.SubscribeToPositionUpdatesAsync(Action<ExchangeEvent<IEnumerable<SharedPosition>>> handler, ApiType? apiType, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IUserTradeSocketClient)this).SubscribeUserTradeOptions.ValidateRequest(Exchange, exchangeParameters, ApiType.Spot, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var result = await SubscribeToPositionUpdatesAsync(
                exchangeParameters!.GetValue<long>(Exchange, "UserId")!,
                exchangeParameters!.GetValue<string>(Exchange, "SettleAsset")!,
                update => handler(update.AsExchangeEvent(Exchange, update.Data.Select(x => new SharedPosition(x.Contract, x.Size, update.Timestamp)
                {
                    AverageEntryPrice = x.EntryPrice,
#warning assumes that shorts are negative size. Correct?
                    PositionSide = x.PositionMode == Enums.PositionMode.Single ? (x.Size > 0 ? SharedPositionSide.Long : SharedPositionSide.Short) : x.PositionMode == Enums.PositionMode.DualShort ? SharedPositionSide.Short : SharedPositionSide.Long
                    LiquidationPrice = x.LiquidationPrice,
                    MaintenanceMargin = x.MaintenanceRate,
                    Leverage = x.Leverage
                }))),
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }

        #endregion
    }
}
