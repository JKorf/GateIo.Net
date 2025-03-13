using GateIo.Net.Interfaces.Clients.SpotApi;
using GateIo.Net.Enums;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GateIo.Net.Objects.Models;
using CryptoExchange.Net;

namespace GateIo.Net.Clients.SpotApi
{
    internal partial class GateIoRestClientSpotApi : IGateIoRestClientSpotApiShared
    {
        private const string _topicId = "GateIoSpot";

        public string Exchange => "GateIo";
        public TradingMode[] SupportedTradingModes { get; } = new[] { TradingMode.Spot };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();

        #region Kline client

        GetKlinesOptions IKlineRestClient.GetKlinesOptions { get; } = new GetKlinesOptions(SharedPaginationSupport.Descending, true, 1000, false,
            SharedKlineInterval.OneMinute,
            SharedKlineInterval.FiveMinutes,
            SharedKlineInterval.FifteenMinutes,
            SharedKlineInterval.ThirtyMinutes,
            SharedKlineInterval.OneHour,
            SharedKlineInterval.FourHours,
            SharedKlineInterval.EightHours,
            SharedKlineInterval.OneDay,
            SharedKlineInterval.OneWeek,
            SharedKlineInterval.OneMonth);

        async Task<ExchangeWebResult<SharedKline[]>> IKlineRestClient.GetKlinesAsync(GetKlinesRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeWebResult<SharedKline[]>(Exchange, new ArgumentError("Interval not supported"));

            var validationError = ((IKlineRestClient)this).GetKlinesOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedKline[]>(Exchange, validationError);

            // Determine pagination
            // Data is normally returned oldest first, so to do newest first pagination we have to do some calc
            DateTime endTime = request.EndTime ?? DateTime.UtcNow;
            DateTime? startTime = request.StartTime;
            if (pageToken is DateTimeToken dateTimeToken)
                endTime = dateTimeToken.LastTime;

            var limit = request.Limit ?? 1000;
            if (startTime == null || startTime < endTime)
            {
                var offset = (int)interval * (limit - 1);
                startTime = endTime.AddSeconds(-offset);
            }

            if (startTime < request.StartTime)
                startTime = request.StartTime;

            // Get data
            var result = await ExchangeData.GetKlinesAsync(
                request.Symbol.GetSymbol(FormatSymbol),
                interval,
                startTime,
                endTime,
                limit,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedKline[]>(Exchange, null, default);

            // Get next token
            DateTimeToken? nextToken = null;
            if (result.Data.Count() == limit)
            {
                var minOpenTime = result.Data.Min(x => x.OpenTime);
                if (request.StartTime == null || minOpenTime > request.StartTime.Value)
                    nextToken = new DateTimeToken(minOpenTime.AddSeconds(-(int)(interval - 1)));
            }

            return result.AsExchangeResult<SharedKline[]>(Exchange, request.Symbol.TradingMode, result.Data.Reverse().Select(x => new SharedKline(x.OpenTime, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice, x.BaseVolume)).ToArray(), nextToken);
        }

        #endregion

        #region Spot Symbol client
        EndpointOptions<GetSymbolsRequest> ISpotSymbolRestClient.GetSpotSymbolsOptions { get; } = new EndpointOptions<GetSymbolsRequest>(false);

        async Task<ExchangeWebResult<SharedSpotSymbol[]>> ISpotSymbolRestClient.GetSpotSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotSymbolRestClient)this).GetSpotSymbolsOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotSymbol[]>(Exchange, validationError);

            var result = await ExchangeData.GetSymbolsAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedSpotSymbol[]>(Exchange, null, default);

            var response = result.AsExchangeResult<SharedSpotSymbol[]>(Exchange, TradingMode.Spot, result.Data.Select(s => new SharedSpotSymbol(s.BaseAsset, s.QuoteAsset, s.Name, s.TradeStatus == SymbolStatus.Tradable)
            {
                MinTradeQuantity = s.MinBaseQuantity,
                MaxTradeQuantity = s.MaxBaseQuantity,
                MinNotionalValue = s.MinQuoteQuantity,
                PriceDecimals = s.PricePrecision,
                QuantityDecimals = s.QuantityPrecision
            }).ToArray());

            ExchangeSymbolCache.UpdateSymbolInfo(_topicId, response.Data);
            return response;
        }

        #endregion

        #region Ticker client

        EndpointOptions<GetTickerRequest> ISpotTickerRestClient.GetSpotTickerOptions { get; } = new EndpointOptions<GetTickerRequest>(false);
        async Task<ExchangeWebResult<SharedSpotTicker>> ISpotTickerRestClient.GetSpotTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotTickerRestClient)this).GetSpotTickerOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotTicker>(Exchange, validationError);

            var result = await ExchangeData.GetTickersAsync(request.Symbol.GetSymbol(FormatSymbol), null, ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedSpotTicker>(Exchange, null, default);

            var ticker = result.Data.Single();
            return result.AsExchangeResult(Exchange, TradingMode.Spot, new SharedSpotTicker(ExchangeSymbolCache.ParseSymbol(_topicId, ticker.Symbol), ticker.Symbol, ticker.LastPrice, ticker.HighPrice, ticker.LowPrice, ticker.BaseVolume, ticker.ChangePercentage24h));
        }

        EndpointOptions<GetTickersRequest> ISpotTickerRestClient.GetSpotTickersOptions { get; } = new EndpointOptions<GetTickersRequest>(false);
        async Task<ExchangeWebResult<SharedSpotTicker[]>> ISpotTickerRestClient.GetSpotTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotTickerRestClient)this).GetSpotTickersOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotTicker[]>(Exchange, validationError);

            var result = await ExchangeData.GetTickersAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedSpotTicker[]>(Exchange, null, default);

            return result.AsExchangeResult<SharedSpotTicker[]>(Exchange, TradingMode.Spot, result.Data.Select(x => new SharedSpotTicker(ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), x.Symbol, x.LastPrice, x.HighPrice, x.LowPrice, x.BaseVolume, x.ChangePercentage24h)).ToArray());
        }

        #endregion

        #region Recent Trade client
        GetRecentTradesOptions IRecentTradeRestClient.GetRecentTradesOptions { get; } = new GetRecentTradesOptions(1000, false);

        async Task<ExchangeWebResult<SharedTrade[]>> IRecentTradeRestClient.GetRecentTradesAsync(GetRecentTradesRequest request, CancellationToken ct)
        {
            var validationError = ((IRecentTradeRestClient)this).GetRecentTradesOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedTrade[]>(Exchange, validationError);

            var result = await ExchangeData.GetTradesAsync(
                request.Symbol.GetSymbol(FormatSymbol),
                limit: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedTrade[]>(Exchange, null, default);

            return result.AsExchangeResult<SharedTrade[]>(Exchange, request.Symbol.TradingMode, result.Data.Select(x => new SharedTrade(x.Quantity, x.Price, x.CreateTime)
            {
                Side = x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
            }).ToArray());
        }

        #endregion

        #region Balance client
        EndpointOptions<GetBalancesRequest> IBalanceRestClient.GetBalancesOptions { get; } = new EndpointOptions<GetBalancesRequest>(true);

        async Task<ExchangeWebResult<SharedBalance[]>> IBalanceRestClient.GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
        {
            var validationError = ((IBalanceRestClient)this).GetBalancesOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedBalance[]>(Exchange, validationError);

            var result = await Account.GetBalancesAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedBalance[]>(Exchange, null, default);

            return result.AsExchangeResult<SharedBalance[]>(Exchange, TradingMode.Spot, result.Data.Select(x => new SharedBalance(x.Asset, x.Available, x.Available + x.Locked)).ToArray());
        }

        #endregion

        #region Spot Order client


        SharedFeeDeductionType ISpotOrderRestClient.SpotFeeDeductionType => SharedFeeDeductionType.DeductFromOutput;
        SharedFeeAssetType ISpotOrderRestClient.SpotFeeAssetType => SharedFeeAssetType.OutputAsset;
        SharedOrderType[] ISpotOrderRestClient.SpotSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market, SharedOrderType.LimitMaker };
        SharedTimeInForce[] ISpotOrderRestClient.SpotSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };

        SharedQuantitySupport ISpotOrderRestClient.SpotSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset,
                SharedQuantityType.QuoteAsset,
                SharedQuantityType.BaseAsset);

        string ISpotOrderRestClient.GenerateClientOrderId() => "t-" + ExchangeHelpers.RandomString(26);

        PlaceSpotOrderOptions ISpotOrderRestClient.PlaceSpotOrderOptions { get; } = new PlaceSpotOrderOptions();
        async Task<ExchangeWebResult<SharedId>> ISpotOrderRestClient.PlaceSpotOrderAsync(PlaceSpotOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).PlaceSpotOrderOptions.ValidateRequest(
                Exchange,
                request,
                request.Symbol.TradingMode,
                SupportedTradingModes,
                ((ISpotOrderRestClient)this).SpotSupportedOrderTypes,
                ((ISpotOrderRestClient)this).SpotSupportedTimeInForce,
                ((ISpotOrderRestClient)this).SpotSupportedOrderQuantity);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var result = await Trading.PlaceOrderAsync(
                request.Symbol.GetSymbol(FormatSymbol),
                request.Side == SharedOrderSide.Buy ? OrderSide.Buy : OrderSide.Sell,
                GetOrderType(request.OrderType),
                quantity: (request.OrderType == SharedOrderType.Market && request.Side == SharedOrderSide.Buy ? request.QuoteQuantity : request.Quantity) ?? 0,
                price: request.Price,
                timeInForce: GetTimeInForce(request.OrderType, request.TimeInForce),
                text: string.IsNullOrEmpty(request.ClientOrderId) ? null : request.ClientOrderId,
                accountType: SpotAccountType.Spot,
                ct: ct
                ).ConfigureAwait(false);

            if (!result)
                return result.AsExchangeResult<SharedId>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(result.Data.Id.ToString()));
        }

        EndpointOptions<GetOrderRequest> ISpotOrderRestClient.GetSpotOrderOptions { get; } = new EndpointOptions<GetOrderRequest>(true);
        async Task<ExchangeWebResult<SharedSpotOrder>> ISpotOrderRestClient.GetSpotOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotOrderOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedSpotOrder>(Exchange, new ArgumentError("Invalid order id"));

            var orders = await Trading.GetOrderAsync(request.Symbol.GetSymbol(FormatSymbol), orderId).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<SharedSpotOrder>(Exchange, null, default);

            return orders.AsExchangeResult(Exchange, TradingMode.Spot, new SharedSpotOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, orders.Data.Symbol),
                orders.Data.Symbol,
                orders.Data.Id.ToString(),
                ParseOrderType(orders.Data.Type, orders.Data.TimeInForce),
                orders.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(orders.Data.Status),
                orders.Data.CreateTime)
            {
                ClientOrderId = orders.Data.Text?.StartsWith("t-") == true ? orders.Data.Text.Replace("t-", "") : orders.Data.Text,
                OrderPrice = orders.Data.Price == 0 ? null : orders.Data.Price,
                Quantity = orders.Data.Type == OrderType.Market && orders.Data.Side == OrderSide.Buy ? null : orders.Data.Quantity,
                QuantityFilled = orders.Data.QuantityFilled,
                UpdateTime = orders.Data.UpdateTime,
                QuoteQuantity = orders.Data.Type == OrderType.Market && orders.Data.Side == OrderSide.Buy ? orders.Data.Quantity : null,
                QuoteQuantityFilled = orders.Data.QuoteQuantityFilled,
                Fee = orders.Data.Fee,
                FeeAsset = orders.Data.FeeAsset,
                TimeInForce = ParseTimeInForce(orders.Data.TimeInForce)
            });
        }

        EndpointOptions<GetOpenOrdersRequest> ISpotOrderRestClient.GetOpenSpotOrdersOptions { get; } = new EndpointOptions<GetOpenOrdersRequest>(true);
        async Task<ExchangeWebResult<SharedSpotOrder[]>> ISpotOrderRestClient.GetOpenSpotOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetOpenSpotOrdersOptions.ValidateRequest(Exchange, request, request.Symbol?.TradingMode ?? request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotOrder[]>(Exchange, validationError);

            var symbol = request.Symbol?.GetSymbol(FormatSymbol);
            var orders = await Trading.GetOpenOrdersAsync().ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<SharedSpotOrder[]>(Exchange, null, default);

            IEnumerable<GateIoOrder> orderList;
            if (symbol != null)
                orderList = orders.Data.SingleOrDefault(x => x.Symbol == symbol)?.Orders ?? Array.Empty<GateIoOrder>();
            else
                orderList = orders.Data.SelectMany(x => x.Orders);

            return orders.AsExchangeResult<SharedSpotOrder[]>(Exchange, TradingMode.Spot, orderList.Select(x => new SharedSpotOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), 
                x.Symbol,
                x.Id.ToString(),
                ParseOrderType(x.Type, x.TimeInForce),
                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.Status),
                x.CreateTime)
            {
                ClientOrderId = x.Text?.StartsWith("t-") == true ? x.Text.Replace("t-", "") : x.Text,
                OrderPrice = x.Price == 0 ? null : x.Price,
                Quantity = x.Type == OrderType.Market && x.Side == OrderSide.Buy ? null : x.Quantity,
                QuantityFilled = x.QuantityFilled,
                UpdateTime = x.UpdateTime,
                QuoteQuantity = x.Type == OrderType.Market && x.Side == OrderSide.Buy ? x.Quantity : null,
                QuoteQuantityFilled = x.QuoteQuantityFilled,
                Fee = x.Fee,
                FeeAsset = x.FeeAsset,
                TimeInForce = ParseTimeInForce(x.TimeInForce)
            }).ToArray());
        }

        PaginatedEndpointOptions<GetClosedOrdersRequest> ISpotOrderRestClient.GetClosedSpotOrdersOptions { get; } = new PaginatedEndpointOptions<GetClosedOrdersRequest>(SharedPaginationSupport.Descending, true, 100, true);
        async Task<ExchangeWebResult<SharedSpotOrder[]>> ISpotOrderRestClient.GetClosedSpotOrdersAsync(GetClosedOrdersRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetClosedSpotOrdersOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotOrder[]>(Exchange, validationError);

            // Determine page token
            int page = 1;
            int pageSize = request.Limit ?? 100;
            if (pageToken is PageToken token)
            {
                page = token.Page;
                pageSize = token.PageSize;
            }

            // Get data
            var orders = await Trading.GetOrdersAsync(
                false, 
                request.Symbol.GetSymbol(FormatSymbol), 
                startTime: request.StartTime, 
                endTime: request.EndTime, 
                page: page,
                limit: pageSize,
                ct: ct).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<SharedSpotOrder[]>(Exchange, null, default);

            // Get next token
            PageToken? nextToken = null;
            if (orders.Data.Count() == pageSize)
                nextToken = new PageToken(page + 1, pageSize);

            return orders.AsExchangeResult<SharedSpotOrder[]>(Exchange, TradingMode.Spot, orders.Data.Select(x => new SharedSpotOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), 
                x.Symbol,
                x.Id.ToString(),
                ParseOrderType(x.Type, x.TimeInForce),
                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.Status),
                x.CreateTime)
            {
                ClientOrderId = x.Text?.StartsWith("t-") == true ? x.Text.Replace("t-", "") : x.Text,
                OrderPrice = x.Price == 0 ? null : x.Price,
                Quantity = x.Type == OrderType.Market && x.Side == OrderSide.Buy ? null : x.Quantity,
                QuantityFilled = x.QuantityFilled,
                UpdateTime = x.UpdateTime,
                QuoteQuantity = x.Type == OrderType.Market && x.Side == OrderSide.Buy ? x.Quantity : null,
                QuoteQuantityFilled = x.QuoteQuantityFilled,
                Fee = x.Fee,
                FeeAsset = x.FeeAsset,
                TimeInForce = ParseTimeInForce(x.TimeInForce)
            }).ToArray(), nextToken);
        }

        EndpointOptions<GetOrderTradesRequest> ISpotOrderRestClient.GetSpotOrderTradesOptions { get; } = new EndpointOptions<GetOrderTradesRequest>(true);
        async Task<ExchangeWebResult<SharedUserTrade[]>> ISpotOrderRestClient.GetSpotOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotOrderTradesOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedUserTrade[]>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedUserTrade[]>(Exchange, new ArgumentError("Invalid order id"));

            var orders = await Trading.GetUserTradesAsync(request.Symbol.GetSymbol(FormatSymbol), orderId: orderId).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<SharedUserTrade[]>(Exchange, null, default);

            return orders.AsExchangeResult<SharedUserTrade[]>(Exchange, request.Symbol.TradingMode,orders.Data.Select(x => new SharedUserTrade(
                ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), 
                x.Symbol,
                x.OrderId.ToString(),
                x.Id.ToString(),
                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                x.Quantity,
                x.Price,
                x.CreateTime)
            {
                Role = x.Role == Role.Maker ? SharedRole.Maker : SharedRole.Taker,
                Fee = x.Fee,
                FeeAsset = x.FeeAsset,
            }).ToArray());
        }

        PaginatedEndpointOptions<GetUserTradesRequest> ISpotOrderRestClient.GetSpotUserTradesOptions { get; } = new PaginatedEndpointOptions<GetUserTradesRequest>(SharedPaginationSupport.Descending, true, 1000, true);
        async Task<ExchangeWebResult<SharedUserTrade[]>> ISpotOrderRestClient.GetSpotUserTradesAsync(GetUserTradesRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotUserTradesOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedUserTrade[]>(Exchange, validationError);

            // Determine page token
            int page = 1;
            int pageSize = request.Limit ?? 500;
            if (pageToken is PageToken token)
            {
                page = token.Page;
                pageSize = token.PageSize;
            }

            // Get data
            var orders = await Trading.GetUserTradesAsync(
                request.Symbol.GetSymbol(FormatSymbol),
                startTime: request.StartTime, 
                endTime: request.EndTime, 
                page: page,
                limit: pageSize,
                ct: ct).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<SharedUserTrade[]>(Exchange, null, default);

            // Get next token
            PageToken? nextToken = null;
            if (orders.Data.Count() == pageSize)
                nextToken = new PageToken(page + 1, pageSize);

            return orders.AsExchangeResult<SharedUserTrade[]>(Exchange, request.Symbol.TradingMode,orders.Data.Select(x => new SharedUserTrade(
                ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), 
                x.Symbol,
                x.OrderId.ToString(),
                x.Id.ToString(),
                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                x.Quantity,
                x.Price,
                x.CreateTime)
            {
                Role = x.Role == Role.Maker ? SharedRole.Maker : SharedRole.Taker,
                Fee = x.Fee,
                FeeAsset = x.FeeAsset,
            }).ToArray(), nextToken);
        }

        EndpointOptions<CancelOrderRequest> ISpotOrderRestClient.CancelSpotOrderOptions { get; } = new EndpointOptions<CancelOrderRequest>(true);
        async Task<ExchangeWebResult<SharedId>> ISpotOrderRestClient.CancelSpotOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).CancelSpotOrderOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            string? clientOrderId = null;

            if (!long.TryParse(request.OrderId, out var orderId))
                clientOrderId = $"t-{request.OrderId}";

            var order = await Trading.CancelOrderAsync(request.Symbol.GetSymbol(FormatSymbol), orderId == 0 ? null : orderId, clientOrderId).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedId>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(order.Data.Id.ToString()));
        }

        private SharedOrderStatus ParseOrderStatus(OrderStatus status)
        {
            if (status == OrderStatus.Open) return SharedOrderStatus.Open;
            if (status == OrderStatus.Canceled) return SharedOrderStatus.Canceled;
            return SharedOrderStatus.Filled;
        }

        private SharedOrderType ParseOrderType(OrderType type, TimeInForce tif)
        {
            if (type == OrderType.Market) return SharedOrderType.Market;
            if (tif == TimeInForce.PendingOrCancel) return SharedOrderType.LimitMaker;

            return SharedOrderType.Limit;
        }

        private SharedTimeInForce? ParseTimeInForce(TimeInForce tif)
        {
            if (tif == TimeInForce.ImmediateOrCancel) return SharedTimeInForce.ImmediateOrCancel;
            if (tif == TimeInForce.FillOrKill) return SharedTimeInForce.FillOrKill;
            if (tif == TimeInForce.GoodTillCancel) return SharedTimeInForce.GoodTillCanceled;

            return null;
        }

        private NewOrderType GetOrderType(SharedOrderType type)
        {
            if (type == SharedOrderType.Market) return NewOrderType.Market;

            return NewOrderType.Limit;
        }

        private TimeInForce? GetTimeInForce(SharedOrderType orderType, SharedTimeInForce? tif)
        {
            if (orderType == SharedOrderType.LimitMaker) return TimeInForce.PendingOrCancel;
            if (tif == SharedTimeInForce.ImmediateOrCancel) return TimeInForce.ImmediateOrCancel;
            if (tif == SharedTimeInForce.FillOrKill) return TimeInForce.FillOrKill;
            if (tif == SharedTimeInForce.GoodTillCanceled) return TimeInForce.GoodTillCancel;
            if (orderType == SharedOrderType.Market) return TimeInForce.FillOrKill;

            return null;
        }

        #endregion

        #region Spot Client Id Order Client

        EndpointOptions<GetOrderRequest> ISpotOrderClientIdClient.GetSpotOrderByClientOrderIdOptions { get; } = new EndpointOptions<GetOrderRequest>(true);
        async Task<ExchangeWebResult<SharedSpotOrder>> ISpotOrderClientIdClient.GetSpotOrderByClientOrderIdAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotOrderOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotOrder>(Exchange, validationError);

            var orders = await Trading.GetOrderAsync(request.Symbol.GetSymbol(FormatSymbol), clientOrderId: request.OrderId, ct: ct).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<SharedSpotOrder>(Exchange, null, default);

            return orders.AsExchangeResult(Exchange, TradingMode.Spot, new SharedSpotOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, orders.Data.Symbol),
                orders.Data.Symbol,
                orders.Data.Id.ToString(),
                ParseOrderType(orders.Data.Type, orders.Data.TimeInForce),
                orders.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(orders.Data.Status),
                orders.Data.CreateTime)
            {
                ClientOrderId = orders.Data.Text?.StartsWith("t-") == true ? orders.Data.Text.Replace("t-", "") : orders.Data.Text,
                OrderPrice = orders.Data.Price == 0 ? null : orders.Data.Price,
                Quantity = orders.Data.Type == OrderType.Market && orders.Data.Side == OrderSide.Buy ? null : orders.Data.Quantity,
                QuantityFilled = orders.Data.QuantityFilled,
                UpdateTime = orders.Data.UpdateTime,
                QuoteQuantity = orders.Data.Type == OrderType.Market && orders.Data.Side == OrderSide.Buy ? orders.Data.Quantity : null,
                QuoteQuantityFilled = orders.Data.QuoteQuantityFilled,
                Fee = orders.Data.Fee,
                FeeAsset = orders.Data.FeeAsset,
                TimeInForce = ParseTimeInForce(orders.Data.TimeInForce)
            });
        }

        EndpointOptions<CancelOrderRequest> ISpotOrderClientIdClient.CancelSpotOrderByClientOrderIdOptions { get; } = new EndpointOptions<CancelOrderRequest>(true);
        async Task<ExchangeWebResult<SharedId>> ISpotOrderClientIdClient.CancelSpotOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).CancelSpotOrderOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var order = await Trading.CancelOrderAsync(request.Symbol.GetSymbol(FormatSymbol), clientOrderId: request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedId>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(order.Data.Id.ToString()));
        }
        #endregion

        #region Asset client
        EndpointOptions<GetAssetRequest> IAssetsRestClient.GetAssetOptions { get; } = new EndpointOptions<GetAssetRequest>(false);
        async Task<ExchangeWebResult<SharedAsset>> IAssetsRestClient.GetAssetAsync(GetAssetRequest request, CancellationToken ct)
        {
            var validationError = ((IAssetsRestClient)this).GetAssetOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedAsset>(Exchange, validationError);

            var assets = await ExchangeData.GetNetworksAsync(request.Asset, ct: ct).ConfigureAwait(false);
            if (!assets)
                return assets.AsExchangeResult<SharedAsset>(Exchange, null, default);

            return assets.AsExchangeResult(Exchange, TradingMode.Spot, new SharedAsset(request.Asset)
            {
                Networks = assets.Data.Select(x => new SharedAssetNetwork(x.Network)
                {
                    FullName = x.NetworkEn,
                    DepositEnabled = !x.IsDepositDisabled,
                    WithdrawEnabled = !x.IsWithdrawalDisabled,
                }).ToArray()
            });
        }

        EndpointOptions<GetAssetsRequest> IAssetsRestClient.GetAssetsOptions { get; } = new EndpointOptions<GetAssetsRequest>(false);

        async Task<ExchangeWebResult<SharedAsset[]>> IAssetsRestClient.GetAssetsAsync(GetAssetsRequest request, CancellationToken ct)
        {
            var validationError = ((IAssetsRestClient)this).GetAssetsOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedAsset[]>(Exchange, validationError);

            var assets = await ExchangeData.GetAssetsAsync(ct: ct).ConfigureAwait(false);
            if (!assets)
                return assets.AsExchangeResult<SharedAsset[]>(Exchange, null, default);

            return assets.AsExchangeResult<SharedAsset[]>(Exchange, TradingMode.Spot, assets.Data.Select(x => new SharedAsset(x.Asset)
            {
                FullName = x.Name,
                Networks = x.Networks.Select(x => new SharedAssetNetwork(x.Name)
                {
                    DepositEnabled = !x.DepositDisabled,
                    WithdrawEnabled = !x.WithdrawDisabled
                }).ToArray()
            }).ToArray());
        }

        #endregion

        #region Deposit client

        EndpointOptions<GetDepositAddressesRequest> IDepositRestClient.GetDepositAddressesOptions { get; } = new EndpointOptions<GetDepositAddressesRequest>(true);
        async Task<ExchangeWebResult<SharedDepositAddress[]>> IDepositRestClient.GetDepositAddressesAsync(GetDepositAddressesRequest request, CancellationToken ct)
        {
            var validationError = ((IDepositRestClient)this).GetDepositAddressesOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedDepositAddress[]>(Exchange, validationError);

            var depositAddresses = await Account.GenerateDepositAddressAsync(request.Asset).ConfigureAwait(false);
            if (!depositAddresses)
                return depositAddresses.AsExchangeResult<SharedDepositAddress[]>(Exchange, null, default);

            return depositAddresses.AsExchangeResult<SharedDepositAddress[]>(Exchange, 
                TradingMode.Spot,
                depositAddresses.Data.MultichainAddress.Where(x => string.IsNullOrEmpty(request.Network) ? true : x.Network == request.Network).Select(x => new SharedDepositAddress(depositAddresses.Data.Asset, x.Address)
                {
                    Network = x.Network,
                    TagOrMemo = x.PaymentId
                }
            ).ToArray());
        }

        GetDepositsOptions IDepositRestClient.GetDepositsOptions { get; } = new GetDepositsOptions(SharedPaginationSupport.Descending, true, 500);
        async Task<ExchangeWebResult<SharedDeposit[]>> IDepositRestClient.GetDepositsAsync(GetDepositsRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((IDepositRestClient)this).GetDepositsOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedDeposit[]>(Exchange, validationError);

            // Determine page token
            int? offset = null;
            if (pageToken is OffsetToken offsetToken)
                offset = offsetToken.Offset;

            // Get data
            var deposits = await Account.GetDepositsAsync(
                request.Asset,
                startTime: request.StartTime,
                endTime: request.EndTime,
                limit: request.Limit ?? 100,
                offset: offset,
                ct: ct).ConfigureAwait(false);
            if (!deposits)
                return deposits.AsExchangeResult<SharedDeposit[]>(Exchange, null, default);

            // Determine next token
            OffsetToken? nextToken = null;
            if (deposits.Data.Count() == (request.Limit ?? 100))
                nextToken = new OffsetToken((offset ?? 0) + deposits.Data.Count());

            return deposits.AsExchangeResult<SharedDeposit[]>(Exchange, TradingMode.Spot, deposits.Data.Select(x => new SharedDeposit(x.Asset, x.Quantity, x.Status == WithdrawalStatus.Done, x.Timestamp)
            {
                Network = x.Network,
                TransactionId = x.TransactionId,
                Tag = x.Memo
            }).ToArray(), nextToken);
        }

        #endregion

        #region Order Book client
        GetOrderBookOptions IOrderBookRestClient.GetOrderBookOptions { get; } = new GetOrderBookOptions(1, 5000, false);
        async Task<ExchangeWebResult<SharedOrderBook>> IOrderBookRestClient.GetOrderBookAsync(GetOrderBookRequest request, CancellationToken ct)
        {
            var validationError = ((IOrderBookRestClient)this).GetOrderBookOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedOrderBook>(Exchange, validationError);

            var result = await ExchangeData.GetOrderBookAsync(
                request.Symbol.GetSymbol(FormatSymbol),
                limit: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedOrderBook>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedOrderBook(result.Data.Asks, result.Data.Bids));
        }
        #endregion

        #region Withdrawal client

        GetWithdrawalsOptions IWithdrawalRestClient.GetWithdrawalsOptions { get; } = new GetWithdrawalsOptions(SharedPaginationSupport.Descending, true, 100);
        async Task<ExchangeWebResult<SharedWithdrawal[]>> IWithdrawalRestClient.GetWithdrawalsAsync(GetWithdrawalsRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((IWithdrawalRestClient)this).GetWithdrawalsOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedWithdrawal[]>(Exchange, validationError);

            // Determine page token
            int? offset = null;
            if (pageToken is OffsetToken offsetToken)
                offset = offsetToken.Offset;

            // Get data
            var withdrawals = await Account.GetWithdrawalsAsync(
                request.Asset,
                startTime: request.StartTime,
                endTime: request.EndTime,
                limit: request.Limit ?? 100,
                offset: offset,
                ct: ct).ConfigureAwait(false);
            if (!withdrawals)
                return withdrawals.AsExchangeResult<SharedWithdrawal[]>(Exchange, null, default);

            // Determine next token
            OffsetToken? nextToken = null;
            if (withdrawals.Data.Count() == (request.Limit ?? 100))
                nextToken = new OffsetToken((offset ?? 0) + withdrawals.Data.Count());

            return withdrawals.AsExchangeResult<SharedWithdrawal[]>(Exchange, TradingMode.Spot, withdrawals.Data.Select(x => new SharedWithdrawal(x.Asset, x.Address, x.Quantity, x.Status == WithdrawalStatus.Done, x.Timestamp)
            {
                Network = x.Network,
                Tag = x.Memo,
                TransactionId = x.TransactionId,
                Fee = x.Fee
            }).ToArray(), nextToken);
        }

        #endregion

        #region Withdraw client

        WithdrawOptions IWithdrawRestClient.WithdrawOptions { get; } = new WithdrawOptions()
        {
            RequiredOptionalParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(WithdrawRequest.Network), typeof(string), "Network is required for withdrawing", "TRX")
            }
        };
        async Task<ExchangeWebResult<SharedId>> IWithdrawRestClient.WithdrawAsync(WithdrawRequest request, CancellationToken ct)
        {
            var validationError = ((IWithdrawRestClient)this).WithdrawOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            // Get data
            var withdrawal = await Account.WithdrawAsync(
                request.Asset,
                address: request.Address,
                quantity: request.Quantity,
                network: request.Network!,
                memo: request.AddressTag,
                ct: ct).ConfigureAwait(false);
            if (!withdrawal)
                return withdrawal.AsExchangeResult<SharedId>(Exchange, null, default);

            return withdrawal.AsExchangeResult(Exchange, TradingMode.Spot, new SharedId(withdrawal.Data.Id));
        }

        #endregion

        #region Fee Client
        EndpointOptions<GetFeeRequest> IFeeRestClient.GetFeeOptions { get; } = new EndpointOptions<GetFeeRequest>(true);

        async Task<ExchangeWebResult<SharedFee>> IFeeRestClient.GetFeesAsync(GetFeeRequest request, CancellationToken ct)
        {
            var validationError = ((IFeeRestClient)this).GetFeeOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFee>(Exchange, validationError);

            // Get data
            var result = await Account.GetTradingFeeAsync(
                request.Symbol.GetSymbol(FormatSymbol),
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedFee>(Exchange, null, default);

            // Return
            return result.AsExchangeResult(Exchange, TradingMode.Spot, new SharedFee(result.Data.MakerFee * 100, result.Data.TakerFee * 100));
        }
        #endregion
    }
}
