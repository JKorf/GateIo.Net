using GateIo.Net.Interfaces.Clients.SpotApi;
using GateIo.Net.Enums;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net;
using GateIo.Net.Objects.Models;
using System.Drawing;
using CryptoExchange.Net.Objects.Errors;

namespace GateIo.Net.Clients.FuturesApi
{
    internal partial class GateIoRestClientPerpetualFuturesApi : IGateIoRestClientPerpetualFuturesApiShared
    {
        private const string _topicId = "GateIoFutures";
        public string Exchange => "GateIo";
        public TradingMode[] SupportedTradingModes { get; } = new[] { TradingMode.PerpetualLinear, TradingMode.PerpetualInverse };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();

        #region Balance Client
        EndpointOptions<GetBalancesRequest> IBalanceRestClient.GetBalancesOptions { get; } = new EndpointOptions<GetBalancesRequest>(true);

        async Task<ExchangeWebResult<SharedBalance[]>> IBalanceRestClient.GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
        {
            var validationError = ((IBalanceRestClient)this).GetBalancesOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedBalance[]>(Exchange, validationError);

            var resultUsd = Account.GetAccountAsync("usd", ct: ct);
            var resultUsdt = Account.GetAccountAsync("usdt", ct: ct);
            var resultBtc = Account.GetAccountAsync("btc", ct: ct);
            await Task.WhenAll(resultBtc, resultUsdt, resultUsd).ConfigureAwait(false);
            if (!resultUsd.Result && !resultUsd.Result.Error!.ErrorCode!.Contains("NOT_FOUND"))
                return resultUsd.Result.AsExchangeResult<SharedBalance[]>(Exchange, null, default);
            if (!resultUsdt.Result && !resultUsdt.Result.Error!.ErrorCode!.Contains("USER_NOT_FOUND"))
                return resultUsdt.Result.AsExchangeResult<SharedBalance[]>(Exchange, null, default);
            if (!resultBtc.Result && !resultBtc.Result.Error!.ErrorCode!.Contains("USER_NOT_FOUND"))
                return resultBtc.Result.AsExchangeResult<SharedBalance[]>(Exchange, null, default);

            var result = new List<SharedBalance>();
            if (resultUsd.Result)
                result.Add(new SharedBalance(resultUsd.Result.Data.Asset, resultUsd.Result.Data.Available, resultUsd.Result.Data.Total));
            if (resultUsdt.Result)
                result.Add(new SharedBalance(resultUsdt.Result.Data.Asset, resultUsdt.Result.Data.Available, resultUsdt.Result.Data.Total));
            if (resultBtc.Result)
                result.Add(new SharedBalance(resultBtc.Result.Data.Asset, resultBtc.Result.Data.Available, resultBtc.Result.Data.Total));
            return (resultUsd.Result ? resultUsd.Result : resultUsdt.Result ? resultUsdt.Result : resultBtc.Result).AsExchangeResult<SharedBalance[]>(Exchange, SupportedTradingModes, result.ToArray());
        }

        #endregion

        #region Ticker client

        EndpointOptions<GetTickerRequest> IFuturesTickerRestClient.GetFuturesTickerOptions { get; } = new EndpointOptions<GetTickerRequest>(false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeWebResult<SharedFuturesTicker>> IFuturesTickerRestClient.GetFuturesTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesTickerRestClient)this).GetFuturesTickerOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesTicker>(Exchange, validationError);

            var resultContract = ExchangeData.GetContractAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, request.Symbol!.GetSymbol(FormatSymbol), ct);
            var resultTicker = ExchangeData.GetTickersAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, request.Symbol!.GetSymbol(FormatSymbol), ct);
            await Task.WhenAll(resultContract, resultTicker).ConfigureAwait(false);

            if (!resultContract.Result)
                return resultContract.Result.AsExchangeResult<SharedFuturesTicker>(Exchange, null, default);
            if (!resultTicker.Result)
                return resultTicker.Result.AsExchangeResult<SharedFuturesTicker>(Exchange, null, default);

            var ticker = resultTicker.Result.Data.SingleOrDefault();
            if (ticker == null)
                return resultTicker.Result.AsExchangeError<SharedFuturesTicker>(Exchange, new ServerError(new ErrorInfo(ErrorType.UnknownSymbol, "Symbol not found")));

            return resultContract.Result.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedFuturesTicker(ExchangeSymbolCache.ParseSymbol(_topicId, resultContract.Result.Data.Name), resultContract.Result.Data.Name, ticker.LastPrice, ticker.HighPrice, ticker.LowPrice, ticker.Volume, ticker.ChangePercentage)
            {
                MarkPrice = ticker.MarkPrice,
                IndexPrice = ticker.IndexPrice,
                FundingRate = ticker.FundingRate,
                NextFundingTime = resultContract.Result.Data.NextFundingTime
            });
        }

        EndpointOptions<GetTickersRequest> IFuturesTickerRestClient.GetFuturesTickersOptions { get; } = new EndpointOptions<GetTickersRequest>(false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeWebResult<SharedFuturesTicker[]>> IFuturesTickerRestClient.GetFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesTickerRestClient)this).GetFuturesTickersOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesTicker[]>(Exchange, validationError);

            var resultTickers = ExchangeData.GetTickersAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, ct: ct);
            var resultContracts = ExchangeData.GetContractsAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, ct: ct);
            await Task.WhenAll(resultTickers, resultContracts).ConfigureAwait(false);
            if (!resultTickers.Result)
                return resultTickers.Result.AsExchangeResult<SharedFuturesTicker[]>(Exchange, null, default);
            if (!resultContracts.Result)
                return resultContracts.Result.AsExchangeResult<SharedFuturesTicker[]>(Exchange, null, default);

            return resultTickers.Result.AsExchangeResult<SharedFuturesTicker[]>(Exchange, SupportedTradingModes, resultTickers.Result.Data.Select(x =>
            {
                var contract = resultContracts.Result.Data.Single(p => p.Name == x.Contract);
                if (request.TradingMode != null)
                {
                    if ((request.TradingMode == TradingMode.PerpetualLinear && contract.Type == ContractType.Inverse)
                    || (request.TradingMode == TradingMode.PerpetualInverse && contract.Type == ContractType.Direct))
                    {
                        return null;
                    }
                }    

                return new SharedFuturesTicker(ExchangeSymbolCache.ParseSymbol(_topicId, x.Contract), x.Contract, x.LastPrice, x.HighPrice, x.LowPrice, x.Volume, x.ChangePercentage)
                {
                    IndexPrice = contract.IndexPrice,
                    MarkPrice = contract.MarkPrice,
                    FundingRate = contract.FundingRate,
                    NextFundingTime = contract.NextFundingTime
                };
            }).Where(x => x != null).ToArray()!);
        }

        #endregion

        #region Book Ticker client

        EndpointOptions<GetBookTickerRequest> IBookTickerRestClient.GetBookTickerOptions { get; } = new EndpointOptions<GetBookTickerRequest>(false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeWebResult<SharedBookTicker>> IBookTickerRestClient.GetBookTickerAsync(GetBookTickerRequest request, CancellationToken ct)
        {
            var validationError = ((IBookTickerRestClient)this).GetBookTickerOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedBookTicker>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var resultTicker = await ExchangeData.GetOrderBookAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                symbol,
                depth: 1,
                ct: ct).ConfigureAwait(false);
            if (!resultTicker)
                return resultTicker.AsExchangeResult<SharedBookTicker>(Exchange, null, default);

            return resultTicker.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedBookTicker(
                ExchangeSymbolCache.ParseSymbol(_topicId, symbol),
                symbol,
                resultTicker.Data.Asks[0].Price,
                resultTicker.Data.Asks[0].Quantity,
                resultTicker.Data.Bids[0].Price,
                resultTicker.Data.Bids[0].Quantity));
        }

        #endregion

        #region Futures Symbol client

        EndpointOptions<GetSymbolsRequest> IFuturesSymbolRestClient.GetFuturesSymbolsOptions { get; } = new EndpointOptions<GetSymbolsRequest>(false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeWebResult<SharedFuturesSymbol[]>> IFuturesSymbolRestClient.GetFuturesSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesSymbolRestClient)this).GetFuturesSymbolsOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesSymbol[]>(Exchange, validationError);

            var result = await ExchangeData.GetContractsAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedFuturesSymbol[]>(Exchange, null, default);

            IEnumerable<GateIoPerpFuturesContract> data = result.Data;
            if (request.TradingMode.HasValue)
                data = data.Where(x => request.TradingMode == TradingMode.PerpetualLinear ? x.Type == ContractType.Direct : x.Type == ContractType.Inverse);
            
            var response = result.AsExchangeResult<SharedFuturesSymbol[]>(Exchange, 
                request.TradingMode == null ? SupportedTradingModes : new[] { request.TradingMode.Value },
                data.Select(s => new SharedFuturesSymbol(
                s.Type == ContractType.Inverse ? TradingMode.PerpetualInverse : TradingMode.PerpetualLinear,
                s.Name.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries)[0], s.Name.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries)[1],
                s.Name,
                !s.Delisting)
            {
                MinTradeQuantity = s.MinOrderQuantity,
                MaxTradeQuantity = s.MaxOrderQuantity,
                QuantityStep = 1,
                PriceStep = s.OrderPriceStep,
                ContractSize = s.Multiplier,
                MaxLongLeverage = s.MaxLeverage,
                MaxShortLeverage = s.MaxLeverage
                }).ToArray());

            ExchangeSymbolCache.UpdateSymbolInfo(_topicId, response.Data);
            return response;
        }

        #endregion

        #region Futures Order Client

        SharedFeeDeductionType IFuturesOrderRestClient.FuturesFeeDeductionType => SharedFeeDeductionType.AddToCost;
        SharedFeeAssetType IFuturesOrderRestClient.FuturesFeeAssetType => SharedFeeAssetType.InputAsset;
        SharedOrderType[] IFuturesOrderRestClient.FuturesSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market };
        SharedTimeInForce[] IFuturesOrderRestClient.FuturesSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };
        SharedQuantitySupport IFuturesOrderRestClient.FuturesSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts);

        string IFuturesOrderRestClient.GenerateClientOrderId() => "t-" + ExchangeHelpers.RandomString(26);

        PlaceFuturesOrderOptions IFuturesOrderRestClient.PlaceFuturesOrderOptions { get; } = new PlaceFuturesOrderOptions(false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeWebResult<SharedId>> IFuturesOrderRestClient.PlaceFuturesOrderAsync(PlaceFuturesOrderRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).PlaceFuturesOrderOptions.ValidateRequest(
                Exchange,
                request,
                request.Symbol!.TradingMode,
                SupportedTradingModes,
                ((IFuturesOrderRestClient)this).FuturesSupportedOrderTypes,
                ((IFuturesOrderRestClient)this).FuturesSupportedTimeInForce,
                ((IFuturesOrderRestClient)this).FuturesSupportedOrderQuantity);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var isReduce = (request.Side == SharedOrderSide.Buy && request.PositionSide == SharedPositionSide.Short)
                || (request.Side == SharedOrderSide.Sell && request.PositionSide == SharedPositionSide.Long);
            var result = await Trading.PlaceOrderAsync(
                    ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                    request.Symbol!.GetSymbol(FormatSymbol),
                    GetOrderSide(request.Side, request.PositionSide),
                    quantity: (int)(request.Quantity?.QuantityInContracts ?? 0),
                    price: request.Price,
                    reduceOnly: request.ReduceOnly ?? isReduce,
                    timeInForce: GetTimeInForce(request.OrderType, request.TimeInForce),
                    text: request.ClientOrderId,
                    ct: ct).ConfigureAwait(false);

                if (!result)
                    return result.AsExchangeResult<SharedId>(Exchange, null, default);

                return result.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedId(result.Data.Id.ToString()));
            
        }

        EndpointOptions<GetOrderRequest> IFuturesOrderRestClient.GetFuturesOrderOptions { get; } = new EndpointOptions<GetOrderRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeWebResult<SharedFuturesOrder>> IFuturesOrderRestClient.GetFuturesOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesOrderOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedFuturesOrder>(Exchange, ArgumentError.Invalid(nameof(GetOrderRequest.OrderId), "Invalid order id"));

            var order = await Trading.GetOrderAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, orderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedFuturesOrder>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, order.Data.Contract), 
                order.Data.Contract,
                order.Data.Id.ToString(),
                ParseOrderType(order.Data.TimeInForce, order.Data.Price),
                order.Data.Quantity > 0 ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.Status),
                order.Data.CreateTime)
            {
                ClientOrderId = order.Data.Text,
                AveragePrice = order.Data.FillPrice == 0 ? null : order.Data.FillPrice,
                OrderPrice = order.Data.Price == 0 ? null : order.Data.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: Math.Abs(order.Data.Quantity)),
                QuantityFilled = new SharedOrderQuantity(contractQuantity: Math.Abs(order.Data.Quantity) - order.Data.QuantityRemaining),
                TimeInForce = ParseTimeInForce(order.Data.TimeInForce),
                UpdateTime = order.Data.FinishTime ?? order.Data.CreateTime,
                ReduceOnly = order.Data.IsReduceOnly
            });
        }

        EndpointOptions<GetOpenOrdersRequest> IFuturesOrderRestClient.GetOpenFuturesOrdersOptions { get; } = new EndpointOptions<GetOpenOrdersRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeWebResult<SharedFuturesOrder[]>> IFuturesOrderRestClient.GetOpenFuturesOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetOpenFuturesOrdersOptions.ValidateRequest(Exchange, request, request.Symbol?.TradingMode ?? request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesOrder[]>(Exchange, validationError);

            var symbol = request.Symbol?.GetSymbol(FormatSymbol);
            var orders = await Trading.GetOrdersAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, OrderStatus.Open, symbol, ct: ct).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<SharedFuturesOrder[]>(Exchange, null, default);

            return orders.AsExchangeResult<SharedFuturesOrder[]>(Exchange, request.Symbol == null ? SupportedTradingModes : new[] { request.Symbol!.TradingMode }, orders.Data.Select(x => new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, x.Contract), 
                x.Contract,
                x.Id.ToString(),
                ParseOrderType(x.TimeInForce, x.Price),
                x.Quantity > 0 ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.Status),
                x.CreateTime)
            {
                ClientOrderId = x.Text,
                AveragePrice = x.FillPrice == 0 ? null : x.FillPrice,
                OrderPrice = x.Price == 0 ? null : x.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: Math.Abs(x.Quantity)),
                QuantityFilled = new SharedOrderQuantity(contractQuantity: Math.Abs(x.Quantity) - x.QuantityRemaining),
                TimeInForce = ParseTimeInForce(x.TimeInForce),
                UpdateTime = x.FinishTime ?? x.CreateTime,
                ReduceOnly = x.IsReduceOnly
            }).ToArray());
        }

        PaginatedEndpointOptions<GetClosedOrdersRequest> IFuturesOrderRestClient.GetClosedFuturesOrdersOptions { get; } = new PaginatedEndpointOptions<GetClosedOrdersRequest>(SharedPaginationSupport.Descending, true, 1000, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeWebResult<SharedFuturesOrder[]>> IFuturesOrderRestClient.GetClosedFuturesOrdersAsync(GetClosedOrdersRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetClosedFuturesOrdersOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesOrder[]>(Exchange, validationError);

            // Determine page token
            int? offset = null;
            if (pageToken is OffsetToken offsetToken)
                offset = offsetToken.Offset;

            // Get data
            var orders = await Trading.GetOrdersByTimestampAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                request.Symbol!.GetSymbol(FormatSymbol),
                startTime: request.StartTime,
                endTime: request.EndTime,
                limit: request.Limit ?? 1000,
                offset: offset,
                ct: ct).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<SharedFuturesOrder[]>(Exchange, null, default);

            // Get next token
            OffsetToken? nextToken = null;
            if (orders.Data.Count() == (request.Limit ?? 1000))
                nextToken = new OffsetToken((offset ?? 0) + orders.Data.Count());

            return orders.AsExchangeResult<SharedFuturesOrder[]>(Exchange, SupportedTradingModes ,orders.Data.Select(x => new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, x.Contract), 
                x.Contract,
                x.Id.ToString(),
                ParseOrderType(x.TimeInForce, x.Price),
                x.Quantity > 0 ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.Status),
                x.CreateTime)
            {
                ClientOrderId = x.Text,
                AveragePrice = x.FillPrice == 0 ? null : x.FillPrice,
                OrderPrice = x.Price == 0 ? null : x.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: Math.Abs(x.Quantity)),
                QuantityFilled = new SharedOrderQuantity(contractQuantity: Math.Abs(x.Quantity) - x.QuantityRemaining),
                TimeInForce = ParseTimeInForce(x.TimeInForce),
                UpdateTime = x.FinishTime ?? x.CreateTime,
                ReduceOnly = x.IsReduceOnly
            }).ToArray(), nextToken);
        }

        EndpointOptions<GetOrderTradesRequest> IFuturesOrderRestClient.GetFuturesOrderTradesOptions { get; } = new EndpointOptions<GetOrderTradesRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeWebResult<SharedUserTrade[]>> IFuturesOrderRestClient.GetFuturesOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesOrderTradesOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedUserTrade[]>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedUserTrade[]>(Exchange, ArgumentError.Invalid(nameof(GetOrderTradesRequest.OrderId), "Invalid order id"));

            var orders = await Trading.GetUserTradesAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, request.Symbol!.GetSymbol(FormatSymbol), orderId: orderId, ct: ct).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<SharedUserTrade[]>(Exchange, null, default);

            return orders.AsExchangeResult<SharedUserTrade[]>(Exchange, request.Symbol!.TradingMode,orders.Data.Select(x => new SharedUserTrade(
                ExchangeSymbolCache.ParseSymbol(_topicId, x.Contract), 
                x.Contract,
                x.OrderId.ToString(),
                x.Id.ToString(),
                x.Quantity > 0 ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                Math.Abs(x.Quantity),
                x.Price,
                x.CreateTime)
            {
                Price = x.Price,
                Quantity = x.Quantity,
                Fee = x.Fee,
                Role = x.Role == Role.Maker ? SharedRole.Maker : SharedRole.Taker
            }).ToArray());
        }

        PaginatedEndpointOptions<GetUserTradesRequest> IFuturesOrderRestClient.GetFuturesUserTradesOptions { get; } = new PaginatedEndpointOptions<GetUserTradesRequest>(SharedPaginationSupport.Descending, true, 1000, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeWebResult<SharedUserTrade[]>> IFuturesOrderRestClient.GetFuturesUserTradesAsync(GetUserTradesRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesUserTradesOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedUserTrade[]>(Exchange, validationError);

            // Determine page token
            int? offset = null;
            if (pageToken is OffsetToken offsetToken)
                offset = offsetToken.Offset;

            // Get data
            var orders = await Trading.GetUserTradesByTimestampAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, request.Symbol!.GetSymbol(FormatSymbol),
                startTime: request.StartTime,
                endTime: request.EndTime,
                limit: request.Limit ?? 1000,
                offset: offset,
                ct: ct
                ).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<SharedUserTrade[]>(Exchange, null, default);

            // Get next token
            OffsetToken? nextToken = null;
            if (orders.Data.Count() == (request.Limit ?? 1000))
                nextToken = new OffsetToken((offset ?? 0) + orders.Data.Count());

            return orders.AsExchangeResult<SharedUserTrade[]>(Exchange, request.Symbol!.TradingMode,orders.Data.Select(x => new SharedUserTrade(
                ExchangeSymbolCache.ParseSymbol(_topicId, x.Contract), 
                x.Contract,
                x.OrderId.ToString(),
                x.Id.ToString(),
                x.Quantity > 0 ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                Math.Abs(x.Quantity),
                x.Price,
                x.CreateTime)
            {
                Price = x.Price,
                Quantity = x.Quantity,
                Fee = x.Fee,
                Role = x.Role == Role.Maker ? SharedRole.Maker : SharedRole.Taker
            }).ToArray(), nextToken);
        }

        EndpointOptions<CancelOrderRequest> IFuturesOrderRestClient.CancelFuturesOrderOptions { get; } = new EndpointOptions<CancelOrderRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeWebResult<SharedId>> IFuturesOrderRestClient.CancelFuturesOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).CancelFuturesOrderOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedId>(Exchange, ArgumentError.Invalid(nameof(CancelOrderRequest.OrderId), "Invalid order id"));

            var order = await Trading.CancelOrderAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, orderId).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedId>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedId(order.Data.Id.ToString()));
        }

        EndpointOptions<GetPositionsRequest> IFuturesOrderRestClient.GetPositionsOptions { get; } = new EndpointOptions<GetPositionsRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeWebResult<SharedPosition[]>> IFuturesOrderRestClient.GetPositionsAsync(GetPositionsRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetPositionsOptions.ValidateRequest(Exchange, request, request.Symbol?.TradingMode ?? request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedPosition[]>(Exchange, validationError);

            if (request.Symbol == null)
            {
                var result = await Trading.GetPositionsAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, ct: ct).ConfigureAwait(false);
                if (!result)
                    return result.AsExchangeResult<SharedPosition[]>(Exchange, null, default);
                return result.AsExchangeResult<SharedPosition[]>(Exchange, SupportedTradingModes, result.Data.Select(x => new SharedPosition(ExchangeSymbolCache.ParseSymbol(_topicId, x.Contract), x.Contract, Math.Abs(x.Size), x.UpdateTime)
                {
                    UnrealizedPnl = x.UnrealisedPnl,
                    LiquidationPrice = x.LiquidationPrice,
                    AverageOpenPrice = x.EntryPrice,
                    Leverage = x.Leverage,
                    PositionSide = x.PositionMode == PositionMode.Single ? (x.Size > 0 ? SharedPositionSide.Long : SharedPositionSide.Short) : x.PositionMode == PositionMode.DualShort ? SharedPositionSide.Short : SharedPositionSide.Long
                }).ToArray());
            }
            else
            {
                var result = await Trading.GetPositionAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
                if (!result)
                    return result.AsExchangeResult<SharedPosition[]>(Exchange, null, default);

                return result.AsExchangeResult<SharedPosition[]>(Exchange, request.Symbol!.TradingMode, new[] { new SharedPosition(ExchangeSymbolCache.ParseSymbol(_topicId, result.Data.Contract), result.Data.Contract, Math.Abs(result.Data.Size), result.Data.UpdateTime)
                {
                    UnrealizedPnl = result.Data.UnrealisedPnl,
                    LiquidationPrice = result.Data.LiquidationPrice,
                    AverageOpenPrice = result.Data.EntryPrice,
                    Leverage = result.Data.Leverage,
                    PositionSide = result.Data.PositionMode == PositionMode.Single ? (result.Data.Size > 0 ? SharedPositionSide.Long : SharedPositionSide.Short) : result.Data.PositionMode == PositionMode.DualShort ? SharedPositionSide.Short : SharedPositionSide.Long
                } });
            }
        }

        EndpointOptions<ClosePositionRequest> IFuturesOrderRestClient.ClosePositionOptions { get; } = new EndpointOptions<ClosePositionRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeWebResult<SharedId>> IFuturesOrderRestClient.ClosePositionAsync(ClosePositionRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).ClosePositionOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var result = await Trading.PlaceOrderAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                request.Symbol!.GetSymbol(FormatSymbol),
                request.PositionSide == SharedPositionSide.Long ? OrderSide.Sell : OrderSide.Buy,
                0,
                0,
                timeInForce: TimeInForce.FillOrKill,
                closePosition: request.PositionSide == null ? true : null,
                closeSide: request.PositionSide == null ? null : request.PositionSide == SharedPositionSide.Long ? CloseSide.CloseLong : CloseSide.CloseShort,
                reduceOnly: request.PositionSide == null ? null : true,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedId>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedId(result.Data.Id.ToString()));
        }

        private OrderSide GetOrderSide(SharedOrderSide side, SharedPositionSide? posSide)
        {
            if (posSide == null)
                return side == SharedOrderSide.Sell ? OrderSide.Sell : OrderSide.Buy;

            if (posSide == SharedPositionSide.Long)
            {
                if (side == SharedOrderSide.Buy) return OrderSide.Buy;
                return OrderSide.Sell;
            }

            if (side == SharedOrderSide.Buy) return OrderSide.Buy;
            return OrderSide.Sell;
        }

        private TimeInForce? GetTimeInForce(SharedOrderType type, SharedTimeInForce? tif)
        {
            if (tif == null)
            {
                if (type == SharedOrderType.Market)
                    return TimeInForce.ImmediateOrCancel;

                return null;
            }

            if (tif == SharedTimeInForce.ImmediateOrCancel) return TimeInForce.ImmediateOrCancel;
            if (tif == SharedTimeInForce.FillOrKill) return TimeInForce.FillOrKill;
            if (tif == SharedTimeInForce.GoodTillCanceled) return TimeInForce.GoodTillCancel;

            return null;
        }

        private SharedOrderStatus ParseOrderStatus(OrderStatus status)
        {
            if (status == OrderStatus.Open) return SharedOrderStatus.Open;
            if (status == OrderStatus.Canceled) return SharedOrderStatus.Canceled;
            return SharedOrderStatus.Filled;
        }

        private SharedOrderType ParseOrderType(TimeInForce? tif, decimal? price)
        {
            if (tif == TimeInForce.ImmediateOrCancel && (price == null || price == 0)) return SharedOrderType.Market;
            if (tif == TimeInForce.PendingOrCancel) return SharedOrderType.LimitMaker;

            return SharedOrderType.Limit;
        }

        private SharedTimeInForce? ParseTimeInForce(TimeInForce? tif)
        {
            if (tif == TimeInForce.GoodTillCancel) return SharedTimeInForce.GoodTillCanceled;
            if (tif == TimeInForce.ImmediateOrCancel) return SharedTimeInForce.ImmediateOrCancel;
            if (tif == TimeInForce.FillOrKill) return SharedTimeInForce.FillOrKill;

            return null;
        }

        #endregion

        #region Futures Client Id Order Client

        EndpointOptions<GetOrderRequest> IFuturesOrderClientIdRestClient.GetFuturesOrderByClientOrderIdOptions { get; } = new EndpointOptions<GetOrderRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeWebResult<SharedFuturesOrder>> IFuturesOrderClientIdRestClient.GetFuturesOrderByClientOrderIdAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesOrderOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesOrder>(Exchange, validationError);

            var order = await Trading.GetOrderAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, clientOrderId: request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedFuturesOrder>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, order.Data.Contract),
                order.Data.Contract,
                order.Data.Id.ToString(),
                ParseOrderType(order.Data.TimeInForce, order.Data.Price),
                order.Data.Quantity > 0 ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.Status),
                order.Data.CreateTime)
            {
                ClientOrderId = order.Data.Text,
                AveragePrice = order.Data.FillPrice == 0 ? null : order.Data.FillPrice,
                OrderPrice = order.Data.Price == 0 ? null : order.Data.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: Math.Abs(order.Data.Quantity)),
                QuantityFilled = new SharedOrderQuantity(contractQuantity: Math.Abs(order.Data.Quantity) - order.Data.QuantityRemaining),
                TimeInForce = ParseTimeInForce(order.Data.TimeInForce),
                UpdateTime = order.Data.FinishTime ?? order.Data.CreateTime,
                ReduceOnly = order.Data.IsReduceOnly
            });
        }

        EndpointOptions<CancelOrderRequest> IFuturesOrderClientIdRestClient.CancelFuturesOrderByClientOrderIdOptions { get; } = new EndpointOptions<CancelOrderRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeWebResult<SharedId>> IFuturesOrderClientIdRestClient.CancelFuturesOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).CancelFuturesOrderOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var order = await Trading.CancelOrderAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, clientOrderId: request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedId>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedId(order.Data.Id.ToString()));
        }
        #endregion

        #region Klines client

        GetKlinesOptions IKlineRestClient.GetKlinesOptions { get; } = new GetKlinesOptions(SharedPaginationSupport.Descending, true, 2000, false,
            SharedKlineInterval.OneMinute,
            SharedKlineInterval.FiveMinutes,
            SharedKlineInterval.FifteenMinutes,
            SharedKlineInterval.ThirtyMinutes,
            SharedKlineInterval.OneHour,
            SharedKlineInterval.FourHours,
            SharedKlineInterval.EightHours,
            SharedKlineInterval.OneDay,
            SharedKlineInterval.OneWeek,
            SharedKlineInterval.OneMonth)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };

        async Task<ExchangeWebResult<SharedKline[]>> IKlineRestClient.GetKlinesAsync(GetKlinesRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeWebResult<SharedKline[]>(Exchange, ArgumentError.Invalid(nameof(GetKlinesRequest.Interval), "Interval not supported"));

            var validationError = ((IKlineRestClient)this).GetKlinesOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedKline[]>(Exchange, validationError);

            // Determine pagination
            // Data is normally returned oldest first, so to do newest first pagination we have to do some calc
            DateTime endTime = request.EndTime ?? DateTime.UtcNow;
            DateTime? startTime = request.StartTime;
            if (pageToken is DateTimeToken dateTimeToken)
                endTime = dateTimeToken.LastTime;

            var limit = request.Limit ?? 1000;
            var offset = (int)interval * (limit - 1);
            startTime = endTime.AddSeconds(-offset);

            if (startTime < request.StartTime)
                startTime = request.StartTime;

            var result = await ExchangeData.GetKlinesAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                request.Symbol!.GetSymbol(FormatSymbol),
                interval,
                startTime: startTime,
                endTime: endTime,
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
                    nextToken = new DateTimeToken(minOpenTime.AddSeconds(-(int)interval));
            }

            return result.AsExchangeResult<SharedKline[]>(Exchange, request.Symbol!.TradingMode, result.Data.Reverse().Select(x => new SharedKline(x.OpenTime, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice, x.Volume)).ToArray(), nextToken);
        }

        #endregion

        #region Index Klines client

        GetKlinesOptions IIndexPriceKlineRestClient.GetIndexPriceKlinesOptions { get; } = new GetKlinesOptions(SharedPaginationSupport.Descending, true, 100, false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };

        async Task<ExchangeWebResult<SharedFuturesKline[]>> IIndexPriceKlineRestClient.GetIndexPriceKlinesAsync(GetKlinesRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeWebResult<SharedFuturesKline[]>(Exchange, ArgumentError.Invalid(nameof(GetKlinesRequest.Interval), "Interval not supported"));

            var validationError = ((IIndexPriceKlineRestClient)this).GetIndexPriceKlinesOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesKline[]>(Exchange, validationError);

            // Determine pagination
            // Data is normally returned oldest first, so to do newest first pagination we have to do some calc
            DateTime endTime = request.EndTime ?? DateTime.UtcNow;
            DateTime? startTime = request.StartTime;
            if (pageToken is DateTimeToken dateTimeToken)
                endTime = dateTimeToken.LastTime;

            var limit = request.Limit ?? 100;
            var offset = (int)interval * (limit - 1);
            startTime = endTime.AddSeconds(-offset);

            if (startTime < request.StartTime)
                startTime = request.StartTime;

            var result = await ExchangeData.GetIndexKlinesAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                request.Symbol!.GetSymbol(FormatSymbol),
                interval,
                startTime: startTime,
                endTime: endTime,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedFuturesKline[]>(Exchange, null, default);

            // Get next token
            DateTimeToken? nextToken = null;
            if (result.Data.Count() == limit)
            {
                var minOpenTime = result.Data.Min(x => x.OpenTime);
                if (request.StartTime == null || minOpenTime > request.StartTime.Value)
                    nextToken = new DateTimeToken(minOpenTime.AddSeconds(-(int)interval));
            }

            return result.AsExchangeResult<SharedFuturesKline[]>(Exchange, request.Symbol!.TradingMode, result.Data.Reverse().Select(x => new SharedFuturesKline(x.OpenTime, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice)).ToArray(), nextToken);
        }

        #endregion

        #region Recent Trade client

        GetRecentTradesOptions IRecentTradeRestClient.GetRecentTradesOptions { get; } = new GetRecentTradesOptions(1000, false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeWebResult<SharedTrade[]>> IRecentTradeRestClient.GetRecentTradesAsync(GetRecentTradesRequest request, CancellationToken ct)
        {
            var validationError = ((IRecentTradeRestClient)this).GetRecentTradesOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedTrade[]>(Exchange, validationError);

            var result = await ExchangeData.GetTradesAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                request.Symbol!.GetSymbol(FormatSymbol),
                limit: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedTrade[]>(Exchange, null, default);

            return result.AsExchangeResult<SharedTrade[]>(Exchange, request.Symbol!.TradingMode, result.Data.Select(x => new SharedTrade(Math.Abs(x.Quantity), x.Price, x.CreateTime)).ToArray());
        }

        #endregion

        #region Trade History client
        GetTradeHistoryOptions ITradeHistoryRestClient.GetTradeHistoryOptions { get; } = new GetTradeHistoryOptions(SharedPaginationSupport.Descending, true, 1000, false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };

        async Task<ExchangeWebResult<SharedTrade[]>> ITradeHistoryRestClient.GetTradeHistoryAsync(GetTradeHistoryRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((ITradeHistoryRestClient)this).GetTradeHistoryOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedTrade[]>(Exchange, validationError);

            int offset = 0;
            if (pageToken is OffsetToken token)
                offset = token.Offset;

            // Get data
            var limit = request.Limit ?? 1000;
            var result = await ExchangeData.GetTradesAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                request.Symbol!.GetSymbol(FormatSymbol),
                startTime: request.StartTime,
                endTime: request.EndTime,
                limit: limit,
                offset: offset,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedTrade[]>(Exchange, null, default);

            OffsetToken? nextToken = null;
            if (result.Data.Count() == limit)
                nextToken = new OffsetToken(offset + limit);

            // Return
            return result.AsExchangeResult<SharedTrade[]>(Exchange, request.Symbol!.TradingMode, result.Data.Select(x => new SharedTrade(Math.Abs(x.Quantity), x.Price, x.CreateTime)).ToArray(), nextToken);
        }
        #endregion

        #region Leverage client
        SharedLeverageSettingMode ILeverageRestClient.LeverageSettingType => SharedLeverageSettingMode.PerSymbol;

        EndpointOptions<GetLeverageRequest> ILeverageRestClient.GetLeverageOptions { get; } = new EndpointOptions<GetLeverageRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeWebResult<SharedLeverage>> ILeverageRestClient.GetLeverageAsync(GetLeverageRequest request, CancellationToken ct)
        {
            var validationError = ((ILeverageRestClient)this).GetLeverageOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedLeverage>(Exchange, validationError);

            var result = await Trading.GetPositionAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, 
                request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedLeverage>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedLeverage(result.Data.Leverage)
            {
                Side = request.PositionSide
            });
        }

        SetLeverageOptions ILeverageRestClient.SetLeverageOptions { get; } = new SetLeverageOptions()
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeWebResult<SharedLeverage>> ILeverageRestClient.SetLeverageAsync(SetLeverageRequest request, CancellationToken ct)
        {
            var validationError = ((ILeverageRestClient)this).SetLeverageOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedLeverage>(Exchange, validationError);

            var result = await Trading.UpdatePositionLeverageAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                request.Symbol!.GetSymbol(FormatSymbol),
                request.Leverage,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedLeverage>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedLeverage(result.Data.Leverage));
        }
        #endregion

        #region Order Book client
        GetOrderBookOptions IOrderBookRestClient.GetOrderBookOptions { get; } = new GetOrderBookOptions(new[] { 1, 5000 }, false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeWebResult<SharedOrderBook>> IOrderBookRestClient.GetOrderBookAsync(GetOrderBookRequest request, CancellationToken ct)
        {
            var validationError = ((IOrderBookRestClient)this).GetOrderBookOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedOrderBook>(Exchange, validationError);

            var result = await ExchangeData.GetOrderBookAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                request.Symbol!.GetSymbol(FormatSymbol),
                depth: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedOrderBook>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedOrderBook(result.Data.Asks, result.Data.Bids));
        }

        #endregion

        #region Open Interest client

        EndpointOptions<GetOpenInterestRequest> IOpenInterestRestClient.GetOpenInterestOptions { get; } = new EndpointOptions<GetOpenInterestRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeWebResult<SharedOpenInterest>> IOpenInterestRestClient.GetOpenInterestAsync(GetOpenInterestRequest request, CancellationToken ct)
        {
            var validationError = ((IOpenInterestRestClient)this).GetOpenInterestOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedOpenInterest>(Exchange, validationError);

            var result = await ExchangeData.GetContractAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedOpenInterest>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedOpenInterest(result.Data.PositionSize));
        }

        #endregion

        #region Funding Rate client
        GetFundingRateHistoryOptions IFundingRateRestClient.GetFundingRateHistoryOptions { get; } = new GetFundingRateHistoryOptions(SharedPaginationSupport.Descending, true, 1000, false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };

        async Task<ExchangeWebResult<SharedFundingRate[]>> IFundingRateRestClient.GetFundingRateHistoryAsync(GetFundingRateHistoryRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((IFundingRateRestClient)this).GetFundingRateHistoryOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFundingRate[]>(Exchange, validationError);

            DateTime? fromTime = null;
            if (pageToken is DateTimeToken token)
                fromTime = token.LastTime;

            // Get data
            var result = await ExchangeData.GetFundingRateHistoryAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                request.Symbol!.GetSymbol(FormatSymbol),
                startTime: request.StartTime,
                endTime: fromTime ?? request.EndTime,
                limit: request.Limit ?? 1000,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedFundingRate[]>(Exchange, null, default);

            DateTimeToken? nextToken = null;
            if (result.Data.Count() == (request.Limit ?? 1000))
                nextToken = new DateTimeToken(result.Data.Min(x => x.Timestamp).AddSeconds(-1));

            // Return
            return result.AsExchangeResult<SharedFundingRate[]>(Exchange, request.Symbol!.TradingMode,result.Data.Select(x => new SharedFundingRate(x.FundingRate, x.Timestamp)).ToArray(), nextToken);
        }
        #endregion

        #region Position Mode client
        SharedPositionModeSelection IPositionModeRestClient.PositionModeSettingType => SharedPositionModeSelection.PerAccount;

        GetPositionModeOptions IPositionModeRestClient.GetPositionModeOptions { get; } = new GetPositionModeOptions()
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeWebResult<SharedPositionModeResult>> IPositionModeRestClient.GetPositionModeAsync(GetPositionModeRequest request, CancellationToken ct)
        {
            var validationError = ((IPositionModeRestClient)this).GetPositionModeOptions.ValidateRequest(Exchange, request, request.Symbol?.TradingMode ?? request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedPositionModeResult>(Exchange, validationError);

            var result = await Account.GetAccountAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedPositionModeResult>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, SupportedTradingModes, new SharedPositionModeResult(result.Data.DualMode ? SharedPositionMode.HedgeMode : SharedPositionMode.OneWay));
        }

        SetPositionModeOptions IPositionModeRestClient.SetPositionModeOptions { get; } = new SetPositionModeOptions()
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeWebResult<SharedPositionModeResult>> IPositionModeRestClient.SetPositionModeAsync(SetPositionModeRequest request, CancellationToken ct)
        {
            var validationError = ((IPositionModeRestClient)this).SetPositionModeOptions.ValidateRequest(Exchange, request, request.Symbol?.TradingMode ?? request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedPositionModeResult>(Exchange, validationError);

            var result = await Account.UpdatePositionModeAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, request.PositionMode == SharedPositionMode.HedgeMode, ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedPositionModeResult>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, SupportedTradingModes, new SharedPositionModeResult(request.PositionMode));
        }
        #endregion

        #region Position History client

        GetPositionHistoryOptions IPositionHistoryRestClient.GetPositionHistoryOptions { get; } = new GetPositionHistoryOptions(SharedPaginationSupport.Descending, true, 100)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeWebResult<SharedPositionHistory[]>> IPositionHistoryRestClient.GetPositionHistoryAsync(GetPositionHistoryRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((IPositionHistoryRestClient)this).GetPositionHistoryOptions.ValidateRequest(Exchange, request, request.Symbol?.TradingMode ?? request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedPositionHistory[]>(Exchange, validationError);

            // Determine page token
            int offset = 0;
            int limit = request.Limit ?? 100;
            if (pageToken is OffsetToken token)
                offset = token.Offset;

            // Get data
            var orders = await Trading.GetPositionCloseHistoryAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                contract: request.Symbol?.GetSymbol(FormatSymbol),
                startTime: request.StartTime,
                endTime: request.EndTime,
                offset: offset,
                limit: limit,
                ct: ct
                ).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<SharedPositionHistory[]>(Exchange, null, default);

            // Get next token
            OffsetToken? nextToken = null;
            if (orders.Data.Count() == limit)
                nextToken = new OffsetToken(offset + limit);

            return orders.AsExchangeResult<SharedPositionHistory[]>(Exchange, request.Symbol == null ? SupportedTradingModes : new[] { request.Symbol!.TradingMode }, orders.Data.Select(x => new SharedPositionHistory(
                ExchangeSymbolCache.ParseSymbol(_topicId, x.Contract), 
                x.Contract,
                x.Side == PositionSide.Long ? SharedPositionSide.Long : SharedPositionSide.Short,
                x.Side == PositionSide.Long ? x.LongPrice : x.ShortPrice,
                x.Side == PositionSide.Short ? x.LongPrice : x.ShortPrice,
                x.AccumelatedSize ?? 0,
                x.RealisedPnlPosition ?? 0,
                x.Timestamp)
            {
            }).ToArray(), nextToken);
        }
        #endregion

        #region Fee Client
        EndpointOptions<GetFeeRequest> IFeeRestClient.GetFeeOptions { get; } = new EndpointOptions<GetFeeRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };

        async Task<ExchangeWebResult<SharedFee>> IFeeRestClient.GetFeesAsync(GetFeeRequest request, CancellationToken ct)
        {
            var validationError = ((IFeeRestClient)this).GetFeeOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFee>(Exchange, validationError);

            // Get data
            var result = await Account.GetTradingFeeAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                request.Symbol!.GetSymbol(FormatSymbol),
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedFee>(Exchange, null, default);

            var symbol = result.Data.Single();

            // Return
            return result.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedFee(symbol.Value.MakerFee * 100, symbol.Value.TakerFee * 100));
        }
        #endregion

        #region Futures Trigger Order Client
        PlaceFuturesTriggerOrderOptions IFuturesTriggerOrderRestClient.PlaceFuturesTriggerOrderOptions { get; } = new PlaceFuturesTriggerOrderOptions(false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeWebResult<SharedId>> IFuturesTriggerOrderRestClient.PlaceFuturesTriggerOrderAsync(PlaceFuturesTriggerOrderRequest request, CancellationToken ct)
        {
            var side = GetTriggerOrderSide(request);
            var validationError = ((IFuturesTriggerOrderRestClient)this).PlaceFuturesTriggerOrderOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes, side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell, ((IFuturesOrderRestClient)this).FuturesSupportedOrderQuantity);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var orderType = request.OrderPrice == null ? NewOrderType.Market : NewOrderType.Limit;
            var result = await Trading.PlaceTriggerOrderAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                request.Symbol!.GetSymbol(FormatSymbol),
                side,
                (int)(request.Quantity.QuantityInContracts ?? 0),
                request.PriceDirection == SharedTriggerPriceDirection.PriceAbove ? TriggerType.EqualOrHigher : TriggerType.EqualOrLower,
                orderPrice: request.OrderPrice,
                triggerPrice: request.TriggerPrice,
                text: request.ClientOrderId,
                priceType: GetPriceType(request),
                timeInForce: GetTimeInForce(orderType == NewOrderType.Market ? SharedOrderType.Market : SharedOrderType.Limit, request.TimeInForce) ?? (orderType == NewOrderType.Market ? TimeInForce.ImmediateOrCancel : TimeInForce.GoodTillCancel),
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedId>(Exchange, null, default);

            // Return
            return result.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedId(result.Data.Id.ToString()));
        }

        private PriceType? GetPriceType(PlaceFuturesTriggerOrderRequest request)
        {
            if (request.TriggerPriceType == null)
                return null;

            if (request.TriggerPriceType == SharedTriggerPriceType.LastPrice)
                return PriceType.LastTradePrice;

            if (request.TriggerPriceType == SharedTriggerPriceType.IndexPrice)
                return PriceType.IndexPrice;

            return PriceType.MarkPrice;
        }

        private OrderSide GetTriggerOrderSide(PlaceFuturesTriggerOrderRequest request)
        {
            if (request.PositionSide == SharedPositionSide.Long)            
                return request.OrderDirection == SharedTriggerOrderDirection.Enter ? OrderSide.Buy : OrderSide.Sell;
            
            return request.OrderDirection == SharedTriggerOrderDirection.Enter ? OrderSide.Sell : OrderSide.Buy;
        }

        EndpointOptions<GetOrderRequest> IFuturesTriggerOrderRestClient.GetFuturesTriggerOrderOptions { get; } = new EndpointOptions<GetOrderRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeWebResult<SharedFuturesTriggerOrder>> IFuturesTriggerOrderRestClient.GetFuturesTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesTriggerOrderRestClient)this).GetFuturesTriggerOrderOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesTriggerOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedFuturesTriggerOrder>(Exchange, ArgumentError.Invalid(nameof(GetOrderRequest.OrderId), "Invalid order id"));

            var settleAsset = ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!;
            var order = await Trading.GetTriggerOrderAsync(
                settleAsset, 
                orderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedFuturesTriggerOrder>(Exchange, null, default);

            GateIoPerpOrder? orderInfo = null;
            if (order.Data.TradeId > 0)
            {
                var orderInfoResult = await Trading.GetOrderAsync(settleAsset, order.Data.TradeId).ConfigureAwait(false);
                if (!orderInfoResult)
                    return orderInfoResult.AsExchangeResult<SharedFuturesTriggerOrder>(Exchange, null, default);

                orderInfo = orderInfoResult.Data;
            }

            var side = order.Data.Order.Quantity > 0 ? SharedOrderSide.Buy : SharedOrderSide.Sell;
            return order.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedFuturesTriggerOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, order.Data.Order.Contract),
                order.Data.Order.Contract,
                order.Data.Id.ToString(),
                order.Data.Order.Price > 0 ? SharedOrderType.Limit : SharedOrderType.Market,
                null,
                ParseTriggerOrderStatus(order.Data.Status, orderInfo),
                order.Data.Trigger.Price,
                null,
                order.Data.CreateTime)
            {
                PlacedOrderId = order.Data.TradeId == 0 ? null : order.Data.TradeId?.ToString(),
                AveragePrice = orderInfo?.FillPrice == 0 ? null : orderInfo?.FillPrice,
                OrderPrice = order.Data.Order.Price == 0 ? null : order.Data.Order.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: Math.Abs(order.Data.Order.Quantity)),
                QuantityFilled = new SharedOrderQuantity(contractQuantity: (Math.Abs(orderInfo?.Quantity ?? 0) - orderInfo?.QuantityRemaining) ?? 0),
                TimeInForce = ParseTimeInForce(order.Data.Order.TimeInForce),
                UpdateTime = orderInfo?.FinishTime ?? orderInfo?.CreateTime ?? order.Data.FinishTime ?? order.Data.CreateTime,
                ClientOrderId = orderInfo?.Text
            });
        }

        private SharedTriggerOrderStatus ParseTriggerOrderStatus(FuturesTriggerOrderStatus? status, GateIoPerpOrder? orderInfo)
        {
            if (status == FuturesTriggerOrderStatus.Invalid)
                return SharedTriggerOrderStatus.CanceledOrRejected;

            if (orderInfo == null)
                // Order not placed yet
                return SharedTriggerOrderStatus.Active;

            if (orderInfo.Status == OrderStatus.Canceled)
                return SharedTriggerOrderStatus.CanceledOrRejected;

            if (orderInfo.Status == OrderStatus.Open)
                return SharedTriggerOrderStatus.Active;

            return SharedTriggerOrderStatus.Filled;
        }

        EndpointOptions<CancelOrderRequest> IFuturesTriggerOrderRestClient.CancelFuturesTriggerOrderOptions { get; } = new EndpointOptions<CancelOrderRequest>(true);
        async Task<ExchangeWebResult<SharedId>> IFuturesTriggerOrderRestClient.CancelFuturesTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesTriggerOrderRestClient)this).CancelFuturesTriggerOrderOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedId>(Exchange, ArgumentError.Invalid(nameof(GetOrderRequest.OrderId), "Invalid order id"));

            var settleAsset = ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!;
            var order = await Trading.CancelTriggerOrderAsync(
                settleAsset,
                orderId,
                ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedId>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedId(request.OrderId));
        }

        #endregion

        #region Tp/SL Client
        EndpointOptions<SetTpSlRequest> IFuturesTpSlRestClient.SetFuturesTpSlOptions { get; } = new EndpointOptions<SetTpSlRequest>(true)
        {
            RequiredOptionalParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(SetTpSlRequest.PositionMode), typeof(SharedPositionMode), "Position mode the account is in", SharedPositionMode.OneWay)
            },
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };

        async Task<ExchangeWebResult<SharedId>> IFuturesTpSlRestClient.SetFuturesTpSlAsync(SetTpSlRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesTpSlRestClient)this).SetFuturesTpSlOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var result = await Trading.PlaceTriggerOrderAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                request.Symbol!.GetSymbol(FormatSymbol),
                request.PositionSide == SharedPositionSide.Long ? OrderSide.Sell : OrderSide.Buy,
                0,
                GetTriggerType(request),
                request.TriggerPrice,
                closePosition: request.PositionMode == SharedPositionMode.HedgeMode ? null: true,
                timeInForce: TimeInForce.ImmediateOrCancel,
                closeSide: request.PositionMode != SharedPositionMode.HedgeMode ? null : request.PositionSide == SharedPositionSide.Long ? CloseSide.CloseLong : CloseSide.CloseShort,
                triggerOrderType: request.PositionSide == SharedPositionSide.Long ? TriggerOrderType.CloseLongPosition : TriggerOrderType.CloseShortPosition,
                ct: ct).ConfigureAwait(false);

            if (!result)
                return result.AsExchangeResult<SharedId>(Exchange, null, default);

            // Return
            return result.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedId(result.Data.Id.ToString()));
        }

        private TriggerType GetTriggerType(SetTpSlRequest request)
        {
            if (request.PositionSide == SharedPositionSide.Long)
                return request.TpSlSide == SharedTpSlSide.TakeProfit ? TriggerType.EqualOrHigher : TriggerType.EqualOrLower;

            return request.TpSlSide == SharedTpSlSide.TakeProfit ? TriggerType.EqualOrLower : TriggerType.EqualOrHigher;
        }

        EndpointOptions<CancelTpSlRequest> IFuturesTpSlRestClient.CancelFuturesTpSlOptions { get; } = new EndpointOptions<CancelTpSlRequest>(true)
        {
            RequiredOptionalParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(CancelTpSlRequest.OrderId), typeof(string), "Id of the tp/sl order", "123123")
            }
        };

        async Task<ExchangeWebResult<bool>> IFuturesTpSlRestClient.CancelFuturesTpSlAsync(CancelTpSlRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesTpSlRestClient)this).CancelFuturesTpSlOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<bool>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<bool>(Exchange, ArgumentError.Invalid(nameof(CancelTpSlRequest.OrderId), "Invalid order id"));

            var result = await Trading.CancelTriggerOrderAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                orderId,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<bool>(Exchange, null, default);

            // Return
            return result.AsExchangeResult(Exchange, request.Symbol!.TradingMode, true);
        }

        #endregion
    }
}
