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
using CryptoExchange.Net.Objects.Errors;

namespace GateIo.Net.Clients.FuturesApi
{
    internal partial class GateIoRestClientPerpetualFuturesApi : IGateIoRestClientPerpetualFuturesApiShared
    {
        private const string _topicId = "GateIoFutures";
        private const string _exchangeName = "GateIo";
        public TradingMode[] SupportedTradingModes { get; } = new[] { TradingMode.PerpetualLinear, TradingMode.PerpetualInverse };

        public SharedClientInfo Discover() => SharedUtils.GetClientInfo(GateIoExchange.Metadata, this);

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();

        #region Balance Client
        GetBalancesOptions IBalanceRestClient.GetBalancesOptions { get; } = new GetBalancesOptions(_exchangeName, AccountTypeFilter.Futures);

        async Task<HttpResult<SharedBalance[]>> IBalanceRestClient.GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetBalancesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedBalance[]>(Exchange, validationError);

            var resultUsd = Account.GetAccountAsync("usd", ct: ct);
            var resultUsdt = Account.GetAccountAsync("usdt", ct: ct);
            var resultBtc = Account.GetAccountAsync("btc", ct: ct);
            await Task.WhenAll(resultBtc, resultUsdt, resultUsd).ConfigureAwait(false);
            if (!resultUsd.Result.Success && !resultUsd.Result.Error!.ErrorCode!.Contains("NOT_FOUND"))
                return HttpResult.Fail<SharedBalance[]>(resultUsd.Result);
            if (!resultUsdt.Result.Success && !resultUsdt.Result.Error!.ErrorCode!.Contains("USER_NOT_FOUND"))
                return HttpResult.Fail<SharedBalance[]>(resultUsdt.Result);
            if (!resultBtc.Result.Success && !resultBtc.Result.Error!.ErrorCode!.Contains("USER_NOT_FOUND"))
                return HttpResult.Fail<SharedBalance[]>(resultBtc.Result);

            var result = new List<SharedBalance>();
            if (resultUsd.Result.Success)
                result.Add(new SharedBalance(SupportedTradingModes, resultUsd.Result.Data.Asset, resultUsd.Result.Data.Available, resultUsd.Result.Data.Total));
            if (resultUsdt.Result.Success)
                result.Add(new SharedBalance(SupportedTradingModes, resultUsdt.Result.Data.Asset, resultUsdt.Result.Data.Available, resultUsdt.Result.Data.Total));
            if (resultBtc.Result.Success)
                result.Add(new SharedBalance(SupportedTradingModes, resultBtc.Result.Data.Asset, resultBtc.Result.Data.Available, resultBtc.Result.Data.Total));
            return HttpResult.Ok((resultUsd.Result.Success ? resultUsd.Result : resultUsdt.Result.Success ? resultUsdt.Result : resultBtc.Result), result.ToArray());
        }

        #endregion

        #region Ticker client

        GetFuturesTickerOptions IFuturesTickerRestClient.GetFuturesTickerOptions { get; } = new GetFuturesTickerOptions(_exchangeName)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<HttpResult<SharedFuturesTicker>> IFuturesTickerRestClient.GetFuturesTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetFuturesTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTicker>(Exchange, validationError);

            var resultContract = ExchangeData.GetContractAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, request.Symbol!.GetSymbol(FormatSymbol), ct);
            var resultTicker = ExchangeData.GetTickersAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, request.Symbol!.GetSymbol(FormatSymbol), ct);
            await Task.WhenAll(resultContract, resultTicker).ConfigureAwait(false);

            if (!resultContract.Result.Success)
                return HttpResult.Fail<SharedFuturesTicker>(resultContract.Result);
            if (!resultTicker.Result.Success)
                return HttpResult.Fail<SharedFuturesTicker>(resultTicker.Result);

            var ticker = resultTicker.Result.Data.SingleOrDefault();
            if (ticker == null)
                return HttpResult.Fail<SharedFuturesTicker>(resultTicker.Result, new ServerError(new ErrorInfo(ErrorType.UnknownSymbol, "Symbol not found")));

            return HttpResult.Ok(resultContract.Result, new SharedFuturesTicker(ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, resultContract.Result.Data.Name), resultContract.Result.Data.Name, ticker.LastPrice, ticker.HighPrice, ticker.LowPrice, ticker.Volume, ticker.ChangePercentage)
            {
                MarkPrice = ticker.MarkPrice,
                IndexPrice = ticker.IndexPrice,
                FundingRate = ticker.FundingRate,
                NextFundingTime = resultContract.Result.Data.NextFundingTime
            });
        }

        GetFuturesTickersOptions IFuturesTickerRestClient.GetFuturesTickersOptions { get; } = new GetFuturesTickersOptions(_exchangeName)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<HttpResult<SharedFuturesTicker[]>> IFuturesTickerRestClient.GetFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetFuturesTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTicker[]>(Exchange, validationError);

            var resultTickers = ExchangeData.GetTickersAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, ct: ct);
            var resultContracts = ExchangeData.GetContractsAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, ct: ct);
            await Task.WhenAll(resultTickers, resultContracts).ConfigureAwait(false);
            if (!resultTickers.Result.Success)
                return HttpResult.Fail<SharedFuturesTicker[]>(resultTickers.Result);
            if (!resultContracts.Result.Success)
                return HttpResult.Fail<SharedFuturesTicker[]>(resultContracts.Result);

            return HttpResult.Ok<SharedFuturesTicker[]>(resultTickers.Result, resultTickers.Result.Data.Select(x =>
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

                return new SharedFuturesTicker(ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, x.Contract), x.Contract, x.LastPrice, x.HighPrice, x.LowPrice, x.Volume, x.ChangePercentage)
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

        GetBookTickerOptions IBookTickerRestClient.GetBookTickerOptions { get; } = new GetBookTickerOptions(_exchangeName, false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<HttpResult<SharedBookTicker>> IBookTickerRestClient.GetBookTickerAsync(GetBookTickerRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetBookTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedBookTicker>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var resultTicker = await ExchangeData.GetOrderBookAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                symbol,
                depth: 1,
                ct: ct).ConfigureAwait(false);
            if (!resultTicker.Success)
                return HttpResult.Fail<SharedBookTicker>(resultTicker);

            return HttpResult.Ok(resultTicker, new SharedBookTicker(
                ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, symbol),
                symbol,
                resultTicker.Data.Asks[0].Price,
                resultTicker.Data.Asks[0].Quantity,
                resultTicker.Data.Bids[0].Price,
                resultTicker.Data.Bids[0].Quantity));
        }

        #endregion

        #region Futures Symbol client

        SharedSymbolCatalog? IFuturesSymbolRestClient.FuturesSymbolCatalog => ExchangeSymbolCache.GetSymbolCatalog(_topicId, EnvironmentName, null);
        GetFuturesSymbolsOptions IFuturesSymbolRestClient.GetFuturesSymbolsOptions { get; } = new GetFuturesSymbolsOptions(_exchangeName, false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<HttpResult<SharedFuturesSymbol[]>> IFuturesSymbolRestClient.GetFuturesSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetFuturesSymbolsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesSymbol[]>(Exchange, validationError);

            var settleAsset = ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!;
            var result = await ExchangeData.GetContractsAsync(settleAsset, ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFuturesSymbol[]>(result);

            var data = result.Data
               .Select(x => ParseSymbol(x))
               .ToArray();

            ExchangeSymbolCache.UpdateSymbolInfo(_topicId, EnvironmentName, settleAsset, data);
            return HttpResult.Ok(result, SharedUtils.ApplySymbolFilter(data, request));
        }

        private SharedFuturesSymbol ParseSymbol(GateIoPerpFuturesContract s)
        {
            var result = new SharedFuturesSymbol(
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
                MaxShortLeverage = s.MaxLeverage,
                DisplayName = s.Name,
            };

            if (result.TradingMode.IsInverse())
            {
                result.QuoteAssetType = SharedAssetType.Fiat;
            }
            else
            {
                result.QuoteAssetType = SharedAssetType.Crypto;
                if (LibraryHelpers.IsStableCoin(result.QuoteAsset))
                    result.QuoteAssetSubType = SharedAssetSubType.StableCoin;
            }

            if (s.ContractType == ContractTargetType.Metals || s.ContractType == ContractTargetType.Commodities)
            {
                result.BaseAssetType = SharedAssetType.TradFi;
                result.BaseAssetSubType = SharedAssetSubType.Commodity;
            }
            else if(s.ContractType == ContractTargetType.Forex)
            {
                result.BaseAssetType = SharedAssetType.Fiat;
            }
            else if(s.ContractType == ContractTargetType.Stocks)
            {
                result.BaseAssetType = SharedAssetType.TradFi;
                result.BaseAssetSubType = SharedAssetSubType.Stock;
            }
            else if (s.ContractType == ContractTargetType.Indices)
            {
                result.BaseAssetType = SharedAssetType.TradFi;
                result.BaseAssetSubType = SharedAssetSubType.Index;
            }
            else
            {
                result.BaseAssetType = SharedAssetType.Crypto;
            }

            return result;
        }

        async Task<ExchangeCallResult<SharedSymbol[]>> IFuturesSymbolRestClient.GetFuturesSymbolsForBaseAssetAsync(string baseAsset)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId, EnvironmentName, null))
            {
                var symbols = await ((IFuturesSymbolRestClient)this).GetFuturesSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<SharedSymbol[]>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<SharedSymbol[]>.Ok(Exchange, ExchangeSymbolCache.GetSymbolsForBaseAsset(_topicId, EnvironmentName, null, baseAsset));
        }

        async Task<ExchangeCallResult<bool>> IFuturesSymbolRestClient.SupportsFuturesSymbolAsync(SharedSymbol symbol)
        {
            if (symbol.TradingMode == TradingMode.Spot)
                throw new ArgumentException(nameof(symbol), "Spot symbols not allowed");

            if (!ExchangeSymbolCache.HasCached(_topicId, EnvironmentName, null))
            {
                var symbols = await ((IFuturesSymbolRestClient)this).GetFuturesSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, EnvironmentName, null, symbol));
        }

        async Task<ExchangeCallResult<bool>> IFuturesSymbolRestClient.SupportsFuturesSymbolAsync(string symbolName)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId, EnvironmentName, null))
            {
                var symbols = await ((IFuturesSymbolRestClient)this).GetFuturesSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, EnvironmentName, null, symbolName));
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

        PlaceFuturesOrderOptions IFuturesOrderRestClient.PlaceFuturesOrderOptions { get; } = new PlaceFuturesOrderOptions(_exchangeName, false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<HttpResult<SharedId>> IFuturesOrderRestClient.PlaceFuturesOrderAsync(PlaceFuturesOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.PlaceFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

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

                if (!result.Success)
                    return HttpResult.Fail<SharedId>(result);

                return HttpResult.Ok(result, new SharedId(result.Data.Id.ToString()));
            
        }

        GetFuturesOrderOptions IFuturesOrderRestClient.GetFuturesOrderOptions { get; } = new GetFuturesOrderOptions(_exchangeName, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<HttpResult<SharedFuturesOrder>> IFuturesOrderRestClient.GetFuturesOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedFuturesOrder>(Exchange, ArgumentError.Invalid(nameof(GetOrderRequest.OrderId), "Invalid order id"));

            var order = await Trading.GetOrderAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, orderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedFuturesOrder>(order);

            return HttpResult.Ok(order, new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, order.Data.Contract), 
                order.Data.Contract,
                order.Data.Id.ToString(),
                ParseOrderType(order.Data.TimeInForce, order.Data.Price),
                order.Data.Quantity > 0 ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.Status, order.Data.FinishedAs),
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

        GetOpenFuturesOrdersOptions IFuturesOrderRestClient.GetOpenFuturesOrdersOptions { get; } = new GetOpenFuturesOrdersOptions(_exchangeName, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<HttpResult<SharedFuturesOrder[]>> IFuturesOrderRestClient.GetOpenFuturesOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetOpenFuturesOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder[]>(Exchange, validationError);

            var symbol = request.Symbol?.GetSymbol(FormatSymbol);
            var orders = await Trading.GetOrdersAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, OrderStatus.Open, symbol, ct: ct).ConfigureAwait(false);
            if (!orders.Success)
                return HttpResult.Fail<SharedFuturesOrder[]>(orders);

            return HttpResult.Ok(orders, orders.Data.Select(x => new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, x.Contract), 
                x.Contract,
                x.Id.ToString(),
                ParseOrderType(x.TimeInForce, x.Price),
                x.Quantity > 0 ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.Status, x.FinishedAs),
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

        GetFuturesClosedOrdersOptions IFuturesOrderRestClient.GetClosedFuturesOrdersOptions { get; } = new GetFuturesClosedOrdersOptions(_exchangeName, false, true, true, 1000)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<HttpResult<SharedFuturesOrder[]>> IFuturesOrderRestClient.GetClosedFuturesOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = SharedClient.GetClosedFuturesOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder[]>(Exchange, validationError);

            int limit = request.Limit ?? 1000;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await Trading.GetOrdersByTimestampAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                request.Symbol!.GetSymbol(FormatSymbol),
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                offset: pageParams.Offset,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFuturesOrder[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromOffset(pageParams, result.Data.Length),
                     result.Data.Length,
                     result.Data.Select(x => x.CreateTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                    .Select(x => 
                        new SharedFuturesOrder(
                            ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, x.Contract), 
                            x.Contract,
                            x.Id.ToString(),
                            ParseOrderType(x.TimeInForce, x.Price),
                            x.Quantity > 0 ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            ParseOrderStatus(x.Status, x.FinishedAs),
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
                        })
                    .ToArray(), nextPageRequest);
        }

        GetFuturesOrderTradesOptions IFuturesOrderRestClient.GetFuturesOrderTradesOptions { get; } = new GetFuturesOrderTradesOptions(_exchangeName, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<HttpResult<SharedUserTrade[]>> IFuturesOrderRestClient.GetFuturesOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetFuturesOrderTradesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, ArgumentError.Invalid(nameof(GetOrderTradesRequest.OrderId), "Invalid order id"));

            var orders = await Trading.GetUserTradesAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, request.Symbol!.GetSymbol(FormatSymbol), orderId: orderId, ct: ct).ConfigureAwait(false);
            if (!orders.Success)
                return HttpResult.Fail<SharedUserTrade[]>(orders);

            return HttpResult.Ok(orders, orders.Data.Select(x => new SharedUserTrade(
                ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, x.Contract), 
                x.Contract,
                x.OrderId.ToString(),
                x.Id.ToString(),
                x.Quantity > 0 ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                Math.Abs(x.Quantity),
                x.Price,
                x.CreateTime)
            {
                ClientOrderId = x.Text,
                Fee = x.Fee,
                Role = x.Role == Role.Maker ? SharedRole.Maker : SharedRole.Taker
            }).ToArray());
        }

        GetFuturesUserTradesOptions IFuturesOrderRestClient.GetFuturesUserTradesOptions { get; } = new GetFuturesUserTradesOptions(_exchangeName, false, true, true, 1000)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<HttpResult<SharedUserTrade[]>> IFuturesOrderRestClient.GetFuturesUserTradesAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = SharedClient.GetFuturesUserTradesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

            int limit = request.Limit ?? 1000;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await Trading.GetUserTradesByTimestampAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, request.Symbol!.GetSymbol(FormatSymbol),
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                offset: pageParams.Offset,
                ct: ct
                ).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedUserTrade[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromOffset(pageParams, result.Data.Length),
                     result.Data.Length,
                     result.Data.Select(x => x.CreateTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                    .Select(x => 
                        new SharedUserTrade(
                            ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, x.Contract), 
                            x.Contract,
                            x.OrderId.ToString(),
                            x.Id.ToString(),
                            x.Quantity > 0 ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            Math.Abs(x.Quantity),
                            x.Price,
                            x.CreateTime)
                        {
                            ClientOrderId = x.Text,
                            Fee = x.Fee,
                            Role = x.Role == Role.Maker ? SharedRole.Maker : SharedRole.Taker
                        })
                    .ToArray(), nextPageRequest);
        }

        CancelFuturesOrderOptions IFuturesOrderRestClient.CancelFuturesOrderOptions { get; } = new CancelFuturesOrderOptions(_exchangeName, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<HttpResult<SharedId>> IFuturesOrderRestClient.CancelFuturesOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.CancelFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid(nameof(CancelOrderRequest.OrderId), "Invalid order id"));

            var order = await Trading.CancelOrderAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, orderId).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(order.Data.Id.ToString()));
        }

        GetPositionsOptions IFuturesOrderRestClient.GetPositionsOptions { get; } = new GetPositionsOptions(_exchangeName, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<HttpResult<SharedPosition[]>> IFuturesOrderRestClient.GetPositionsAsync(GetPositionsRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetPositionsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedPosition[]>(Exchange, validationError);

            if (request.Symbol == null)
            {
                var result = await Trading.GetPositionsAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedPosition[]>(result);
                return HttpResult.Ok(result, result.Data.Select(x => new SharedPosition(ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, x.Contract), x.Contract, Math.Abs(x.Size), x.UpdateTime)
                {
                    UnrealizedPnl = x.UnrealisedPnl,
                    LiquidationPrice = x.LiquidationPrice,
                    AverageOpenPrice = x.EntryPrice,
                    Leverage = x.Leverage,
                    PositionMode = x.PositionMode == PositionMode.Single ? SharedPositionMode.OneWay : SharedPositionMode.HedgeMode,
                    PositionSide = x.PositionMode == PositionMode.Single ? (x.Size > 0 ? SharedPositionSide.Long : SharedPositionSide.Short) : x.PositionMode == PositionMode.DualShort ? SharedPositionSide.Short : SharedPositionSide.Long
                }).ToArray());
            }
            else
            {
                var result = await Trading.GetPositionAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedPosition[]>(result);

                return HttpResult.Ok(result, new[] { new SharedPosition(ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, result.Data.Contract), result.Data.Contract, Math.Abs(result.Data.Size), result.Data.UpdateTime)
                {
                    UnrealizedPnl = result.Data.UnrealisedPnl,
                    LiquidationPrice = result.Data.LiquidationPrice,
                    AverageOpenPrice = result.Data.EntryPrice,
                    Leverage = result.Data.Leverage,
                    PositionMode = result.Data.PositionMode == PositionMode.Single ? SharedPositionMode.OneWay : SharedPositionMode.HedgeMode,
                    PositionSide = result.Data.PositionMode == PositionMode.Single ? (result.Data.Size > 0 ? SharedPositionSide.Long : SharedPositionSide.Short) : result.Data.PositionMode == PositionMode.DualShort ? SharedPositionSide.Short : SharedPositionSide.Long
                } });
            }
        }

        ClosePositionOptions IFuturesOrderRestClient.ClosePositionOptions { get; } = new ClosePositionOptions(_exchangeName, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<HttpResult<SharedId>> IFuturesOrderRestClient.ClosePositionAsync(ClosePositionRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.ClosePositionOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

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
            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            return HttpResult.Ok(result, new SharedId(result.Data.Id.ToString()));
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

        private SharedOrderStatus ParseOrderStatus(OrderStatus status, OrderFinishType? finishedAs)
        {
            if (status == Enums.OrderStatus.Open)
                return SharedOrderStatus.Open;
            if (status == OrderStatus.Canceled)
                return SharedOrderStatus.Canceled;

            if (finishedAs == Enums.OrderFinishType.Filled)
                return SharedOrderStatus.Filled;

            if (finishedAs == OrderFinishType.Unknown)
                return SharedOrderStatus.Unknown;

            return SharedOrderStatus.Canceled;
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

        GetFuturesOrderByClientOrderIdOptions IFuturesOrderClientIdRestClient.GetFuturesOrderByClientOrderIdOptions { get; } = new GetFuturesOrderByClientOrderIdOptions(_exchangeName, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<HttpResult<SharedFuturesOrder>> IFuturesOrderClientIdRestClient.GetFuturesOrderByClientOrderIdAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder>(Exchange, validationError);

            var order = await Trading.GetOrderAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, clientOrderId: request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedFuturesOrder>(order);

            return HttpResult.Ok(order, new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, order.Data.Contract),
                order.Data.Contract,
                order.Data.Id.ToString(),
                ParseOrderType(order.Data.TimeInForce, order.Data.Price),
                order.Data.Quantity > 0 ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.Status, order.Data.FinishedAs),
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

        CancelFuturesOrderByClientOrderIdOptions IFuturesOrderClientIdRestClient.CancelFuturesOrderByClientOrderIdOptions { get; } = new CancelFuturesOrderByClientOrderIdOptions(_exchangeName, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<HttpResult<SharedId>> IFuturesOrderClientIdRestClient.CancelFuturesOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.CancelFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var order = await Trading.CancelOrderAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, clientOrderId: request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(order.Data.Id.ToString()));
        }
        #endregion

        #region Klines client

        GetKlinesOptions IKlineRestClient.GetKlinesOptions { get; } = new GetKlinesOptions(_exchangeName, false, true, true, 2000, false,
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

        async Task<HttpResult<SharedKline[]>> IKlineRestClient.GetKlinesAsync(GetKlinesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;

            var validationError = SharedClient.GetKlinesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedKline[]>(Exchange, validationError);

            int limit = request.Limit ?? 1000;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await ExchangeData.GetKlinesAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                symbol,
                interval,
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                ct: ct
                ).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedKline[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.OpenTime)),
                     result.Data.Length,
                     result.Data.Select(x => x.OpenTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.OpenTime, request.StartTime, request.EndTime, direction)
                    .Select(x => 
                        new SharedKline(request.Symbol, symbol, x.OpenTime, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice, x.Volume))
                    .ToArray(), nextPageRequest);
        }

        #endregion

        #region Index Klines client

        GetIndexPriceKlinesOptions IIndexPriceKlineRestClient.GetIndexPriceKlinesOptions { get; } = new GetIndexPriceKlinesOptions(_exchangeName, false, true, true, 1000, false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };

        async Task<HttpResult<SharedFuturesKline[]>> IIndexPriceKlineRestClient.GetIndexPriceKlinesAsync(GetKlinesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;

            var validationError = SharedClient.GetIndexPriceKlinesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesKline[]>(Exchange, validationError);

            int limit = request.Limit ?? 1000;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await ExchangeData.GetIndexKlinesAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                symbol,
                interval,
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                ct: ct
                ).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFuturesKline[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.OpenTime)),
                     result.Data.Length,
                     result.Data.Select(x => x.OpenTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.OpenTime, request.StartTime, request.EndTime, direction)
                    .Select(x => 
                        new SharedFuturesKline(request.Symbol, symbol, x.OpenTime, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice))
                    .ToArray(), nextPageRequest);
        }

        #endregion

        #region Recent Trade client

        GetRecentTradesOptions IRecentTradeRestClient.GetRecentTradesOptions { get; } = new GetRecentTradesOptions(_exchangeName, 1000, false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<HttpResult<SharedTrade[]>> IRecentTradeRestClient.GetRecentTradesAsync(GetRecentTradesRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetRecentTradesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedTrade[]>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await ExchangeData.GetTradesAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                symbol,
                limit: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedTrade[]>(result);

            return HttpResult.Ok(result, result.Data.Select(x => 
                new SharedTrade(request.Symbol, symbol, Math.Abs(x.Quantity), x.Price, x.CreateTime)).ToArray());
        }

        #endregion

        #region Trade History client
        GetTradeHistoryOptions ITradeHistoryRestClient.GetTradeHistoryOptions { get; } = new GetTradeHistoryOptions(_exchangeName, false, true, true, 1000, false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };

        async Task<HttpResult<SharedTrade[]>> ITradeHistoryRestClient.GetTradeHistoryAsync(GetTradeHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = SharedClient.GetTradeHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedTrade[]>(Exchange, validationError);

            int limit = request.Limit ?? 1000;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await ExchangeData.GetTradesAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                symbol,
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                offset: pageParams.Offset,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedTrade[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromOffset(pageParams, result.Data.Length),
                     result.Data.Length,
                     result.Data.Select(x => x.CreateTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedTrade(request.Symbol, symbol, Math.Abs(x.Quantity), x.Price, x.CreateTime))
                    .ToArray(), nextPageRequest);
        }
        #endregion

        #region Leverage client
        SharedLeverageSettingMode ILeverageRestClient.LeverageSettingType => SharedLeverageSettingMode.PerSymbol;

        GetLeverageOptions ILeverageRestClient.GetLeverageOptions { get; } = new GetLeverageOptions(_exchangeName, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<HttpResult<SharedLeverage>> ILeverageRestClient.GetLeverageAsync(GetLeverageRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetLeverageOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedLeverage>(Exchange, validationError);

            var result = await Trading.GetPositionAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, 
                request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedLeverage>(result);

            return HttpResult.Ok(result, new SharedLeverage(result.Data.Leverage)
            {
                Side = request.PositionSide
            });
        }

        SetLeverageOptions ILeverageRestClient.SetLeverageOptions { get; } = new SetLeverageOptions(_exchangeName)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<HttpResult<SharedLeverage>> ILeverageRestClient.SetLeverageAsync(SetLeverageRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.SetLeverageOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedLeverage>(Exchange, validationError);

            var result = await Trading.UpdatePositionLeverageAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                request.Symbol!.GetSymbol(FormatSymbol),
                request.Leverage,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedLeverage>(result);

            return HttpResult.Ok(result, new SharedLeverage(result.Data.Leverage));
        }
        #endregion

        #region Order Book client
        GetOrderBookOptions IOrderBookRestClient.GetOrderBookOptions { get; } = new GetOrderBookOptions(_exchangeName, 1, 300, false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<HttpResult<SharedOrderBook>> IOrderBookRestClient.GetOrderBookAsync(GetOrderBookRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetOrderBookOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedOrderBook>(Exchange, validationError);

            var result = await ExchangeData.GetOrderBookAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                request.Symbol!.GetSymbol(FormatSymbol),
                depth: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedOrderBook>(result);

            return HttpResult.Ok(result, new SharedOrderBook(result.Data.Asks, result.Data.Bids));
        }

        #endregion

        #region Open Interest client

        GetOpenInterestOptions IOpenInterestRestClient.GetOpenInterestOptions { get; } = new GetOpenInterestOptions(_exchangeName, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<HttpResult<SharedOpenInterest>> IOpenInterestRestClient.GetOpenInterestAsync(GetOpenInterestRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetOpenInterestOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedOpenInterest>(Exchange, validationError);

            var result = await ExchangeData.GetContractAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedOpenInterest>(result);

            return HttpResult.Ok(result, new SharedOpenInterest(result.Data.PositionSize));
        }

        #endregion

        #region Funding Rate client
        GetFundingRateHistoryOptions IFundingRateRestClient.GetFundingRateHistoryOptions { get; } = new GetFundingRateHistoryOptions(_exchangeName, false, true, true, 1000, false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };

        async Task<HttpResult<SharedFundingRate[]>> IFundingRateRestClient.GetFundingRateHistoryAsync(GetFundingRateHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = SharedClient.GetFundingRateHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFundingRate[]>(Exchange, validationError);

            int limit = request.Limit ?? 1000;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await ExchangeData.GetFundingRateHistoryAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                request.Symbol!.GetSymbol(FormatSymbol),
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFundingRate[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.Timestamp)),
                     result.Data.Length,
                     result.Data.Select(x => x.Timestamp),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.Timestamp, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedFundingRate(x.FundingRate, x.Timestamp))
                    .ToArray(), nextPageRequest);
        }
        #endregion

        #region Position Mode client
        SharedPositionModeSelection IPositionModeRestClient.PositionModeSettingType => SharedPositionModeSelection.PerAccount;

        GetPositionModeOptions IPositionModeRestClient.GetPositionModeOptions { get; } = new GetPositionModeOptions(_exchangeName)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<HttpResult<SharedPositionModeResult>> IPositionModeRestClient.GetPositionModeAsync(GetPositionModeRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetPositionModeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedPositionModeResult>(Exchange, validationError);

            var result = await Account.GetAccountAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedPositionModeResult>(result);

            return HttpResult.Ok(result, new SharedPositionModeResult(result.Data.DualMode ? SharedPositionMode.HedgeMode : SharedPositionMode.OneWay));
        }

        SetPositionModeOptions IPositionModeRestClient.SetPositionModeOptions { get; } = new SetPositionModeOptions(_exchangeName)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<HttpResult<SharedPositionModeResult>> IPositionModeRestClient.SetPositionModeAsync(SetPositionModeRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.SetPositionModeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedPositionModeResult>(Exchange, validationError);

            var result = await Account.UpdatePositionModeAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, request.PositionMode == SharedPositionMode.HedgeMode, ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedPositionModeResult>(result);

            return HttpResult.Ok(result, new SharedPositionModeResult(request.PositionMode));
        }
        #endregion

        #region Position History client

        GetPositionHistoryOptions IPositionHistoryRestClient.GetPositionHistoryOptions { get; } = new GetPositionHistoryOptions(_exchangeName, false, true, true, 1000)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<HttpResult<SharedPositionHistory[]>> IPositionHistoryRestClient.GetPositionHistoryAsync(GetPositionHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = SharedClient.GetPositionHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedPositionHistory[]>(Exchange, validationError);

            int limit = request.Limit ?? 1000;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await Trading.GetPositionCloseHistoryAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                contract: request.Symbol?.GetSymbol(FormatSymbol),
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                offset: pageParams.Offset,
                limit: pageParams.Limit,
                ct: ct
                ).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedPositionHistory[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                      () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.Timestamp)),
                      result.Data.Length,
                      result.Data.Select(x => x.Timestamp),
                      request.StartTime,
                      request.EndTime ?? DateTime.UtcNow,
                      pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.Timestamp, request.StartTime, request.EndTime, direction)
                    .Select(x => 
                        new SharedPositionHistory(
                            ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, x.Contract), 
                            x.Contract,
                            x.Side == PositionSide.Long ? SharedPositionSide.Long : SharedPositionSide.Short,
                            x.Side == PositionSide.Long ? x.LongPrice : x.ShortPrice,
                            x.Side == PositionSide.Short ? x.LongPrice : x.ShortPrice,
                            x.AccumelatedSize ?? 0,
                            x.RealisedPnlPosition ?? 0,
                            x.Timestamp)
                        {
                        })
                    .ToArray(), nextPageRequest);
        }
        #endregion

        #region Fee Client
        GetFeeOptions IFeeRestClient.GetFeeOptions { get; } = new GetFeeOptions(_exchangeName, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };

        async Task<HttpResult<SharedFee>> IFeeRestClient.GetFeesAsync(GetFeeRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetFeeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFee>(Exchange, validationError);

            // Get data
            var result = await Account.GetTradingFeeAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                request.Symbol!.GetSymbol(FormatSymbol),
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFee>(result);

            var symbol = result.Data.Single();

            // Return
            return HttpResult.Ok(result, new SharedFee(symbol.Value.MakerFee * 100, symbol.Value.TakerFee * 100));
        }
        #endregion

        #region Futures Trigger Order Client
        PlaceFuturesTriggerOrderOptions IFuturesTriggerOrderRestClient.PlaceFuturesTriggerOrderOptions { get; } = new PlaceFuturesTriggerOrderOptions(_exchangeName, false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<HttpResult<SharedId>> IFuturesTriggerOrderRestClient.PlaceFuturesTriggerOrderAsync(PlaceFuturesTriggerOrderRequest request, CancellationToken ct)
        {
            var side = GetTriggerOrderSide(request);
            var validationError = SharedClient.PlaceFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

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
            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            // Return
            return HttpResult.Ok(result, new SharedId(result.Data.Id.ToString()));
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

        GetFuturesTriggerOrderOptions IFuturesTriggerOrderRestClient.GetFuturesTriggerOrderOptions { get; } = new GetFuturesTriggerOrderOptions(_exchangeName, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<HttpResult<SharedFuturesTriggerOrder>> IFuturesTriggerOrderRestClient.GetFuturesTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTriggerOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedFuturesTriggerOrder>(Exchange, ArgumentError.Invalid(nameof(GetOrderRequest.OrderId), "Invalid order id"));

            var settleAsset = request.GetParamValue<string>(Exchange, "SettleAsset");
            var order = await Trading.GetTriggerOrderAsync(
                settleAsset!, 
                orderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedFuturesTriggerOrder>(order);

            GateIoPerpOrder? orderInfo = null;
            if (order.Data.TradeId > 0)
            {
                var orderInfoResult = await Trading.GetOrderAsync(settleAsset!, order.Data.TradeId).ConfigureAwait(false);
                if (!orderInfoResult.Success)
                    return HttpResult.Fail<SharedFuturesTriggerOrder>(orderInfoResult);

                orderInfo = orderInfoResult.Data;
            }

            var side = order.Data.Order.Quantity > 0 ? SharedOrderSide.Buy : SharedOrderSide.Sell;
            return HttpResult.Ok(order, new SharedFuturesTriggerOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, order.Data.Order.Contract),
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

            if (orderInfo.Status == OrderStatus.Closed)
                return SharedTriggerOrderStatus.Filled;

            return SharedTriggerOrderStatus.Unknown;
        }

        CancelFuturesTriggerOrderOptions IFuturesTriggerOrderRestClient.CancelFuturesTriggerOrderOptions { get; } = new CancelFuturesTriggerOrderOptions(_exchangeName, true);
        async Task<HttpResult<SharedId>> IFuturesTriggerOrderRestClient.CancelFuturesTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.CancelFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid(nameof(GetOrderRequest.OrderId), "Invalid order id"));

            var settleAsset = ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!;
            var order = await Trading.CancelTriggerOrderAsync(
                settleAsset,
                orderId,
                ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(request.OrderId));
        }

        #endregion

        #region Tp/SL Client
        SetFuturesTpSlOptions IFuturesTpSlRestClient.SetFuturesTpSlOptions { get; } = new SetFuturesTpSlOptions(_exchangeName, true)
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

        async Task<HttpResult<SharedId>> IFuturesTpSlRestClient.SetFuturesTpSlAsync(SetTpSlRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.SetFuturesTpSlOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

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

            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            // Return
            return HttpResult.Ok(result, new SharedId(result.Data.Id.ToString()));
        }

        private TriggerType GetTriggerType(SetTpSlRequest request)
        {
            if (request.PositionSide == SharedPositionSide.Long)
                return request.TpSlSide == SharedTpSlSide.TakeProfit ? TriggerType.EqualOrHigher : TriggerType.EqualOrLower;

            return request.TpSlSide == SharedTpSlSide.TakeProfit ? TriggerType.EqualOrLower : TriggerType.EqualOrHigher;
        }

        CancelFuturesTpSlOptions IFuturesTpSlRestClient.CancelFuturesTpSlOptions { get; } = new CancelFuturesTpSlOptions(_exchangeName, true)
        {
            RequiredOptionalParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(CancelTpSlRequest.OrderId), typeof(string), "Id of the tp/sl order", "123123")
            }
        };

        async Task<HttpResult<bool>> IFuturesTpSlRestClient.CancelFuturesTpSlAsync(CancelTpSlRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.CancelFuturesTpSlOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<bool>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<bool>(Exchange, ArgumentError.Invalid(nameof(CancelTpSlRequest.OrderId), "Invalid order id"));

            var result = await Trading.CancelTriggerOrderAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                orderId,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<bool>(result);

            // Return
            return HttpResult.Ok(result, true);
        }

        #endregion
    }
}
