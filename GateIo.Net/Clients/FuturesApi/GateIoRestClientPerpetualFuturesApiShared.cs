using GateIo.Net.Interfaces.Clients.SpotApi;
using GateIo.Net.Enums;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis.Interfaces;
using CryptoExchange.Net.SharedApis.Models.Rest;
using CryptoExchange.Net.SharedApis.RequestModels;
using CryptoExchange.Net.SharedApis.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.SharedApis.Enums;
using GateIo.Net.Objects.Models;
using CryptoExchange.Net.SharedApis.Models;
using CryptoExchange.Net.SharedApis.Models.FilterOptions;
using CryptoExchange.Net.SharedApis.Interfaces.Rest.Spot;
using CryptoExchange.Net.SharedApis.Interfaces.Rest.Futures;
using CryptoExchange.Net.SharedApis.Models.EndpointOptions;

namespace GateIo.Net.Clients.FuturesApi
{
    internal partial class GateIoRestClientPerpetualFuturesApi : IGateIoRestClientPerpetualFuturesApiShared
    {
        public string Exchange => "GateIo";
        public ApiType[] SupportedApiTypes { get; } = new[] { ApiType.PerpetualLinear, ApiType.PerpetualInverse };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();

        #region Balance Client
        EndpointOptions<GetBalancesRequest> IBalanceRestClient.GetBalancesOptions { get; } = new EndpointOptions<GetBalancesRequest>(true);

        async Task<ExchangeWebResult<IEnumerable<SharedBalance>>> IBalanceRestClient.GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
        {
            var validationError = ((IBalanceRestClient)this).GetBalancesOptions.ValidateRequest(Exchange, request, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedBalance>>(Exchange, validationError);

            var resultUsd = Account.GetAccountAsync("usd", ct: ct);
            var resultUsdt = Account.GetAccountAsync("usdt", ct: ct);
            var resultBtc = Account.GetAccountAsync("btc", ct: ct);
            await Task.WhenAll(resultBtc, resultUsdt, resultUsd).ConfigureAwait(false);
            if (!resultUsd.Result && !resultUsd.Result.Error!.Message.Contains("USER_NOT_FOUND"))
                return resultUsd.Result.AsExchangeResult<IEnumerable<SharedBalance>>(Exchange, default);
            if (!resultUsdt.Result && !resultUsdt.Result.Error!.Message.Contains("USER_NOT_FOUND"))
                return resultUsdt.Result.AsExchangeResult<IEnumerable<SharedBalance>>(Exchange, default);
            if (!resultBtc.Result && !resultBtc.Result.Error!.Message.Contains("USER_NOT_FOUND"))
                return resultBtc.Result.AsExchangeResult<IEnumerable<SharedBalance>>(Exchange, default);

            var result = new List<SharedBalance>();
            if (resultUsd.Result)
                result.Add(new SharedBalance(resultUsd.Result.Data.Asset, resultUsd.Result.Data.Available, resultUsd.Result.Data.Total));
            if (resultUsdt.Result)
                result.Add(new SharedBalance(resultUsdt.Result.Data.Asset, resultUsdt.Result.Data.Available, resultUsdt.Result.Data.Total));
            if (resultBtc.Result)
                result.Add(new SharedBalance(resultBtc.Result.Data.Asset, resultBtc.Result.Data.Available, resultBtc.Result.Data.Total));
            return (resultUsd.Result ? resultUsd.Result : resultUsdt.Result ? resultUsdt.Result : resultBtc.Result).AsExchangeResult<IEnumerable<SharedBalance>>(Exchange, result);
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
            var validationError = ((IFuturesTickerRestClient)this).GetFuturesTickerOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesTicker>(Exchange, validationError);

            var resultContract = ExchangeData.GetContractAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, request.Symbol.GetSymbol(FormatSymbol), ct);
            var resultTicker = ExchangeData.GetTickersAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, request.Symbol.GetSymbol(FormatSymbol), ct);
            await Task.WhenAll(resultContract, resultTicker).ConfigureAwait(false);

            if (!resultContract.Result)
                return resultContract.Result.AsExchangeResult<SharedFuturesTicker>(Exchange, default);
            if (!resultTicker.Result)
                return resultTicker.Result.AsExchangeResult<SharedFuturesTicker>(Exchange, default);

            var ticker = resultTicker.Result.Data.SingleOrDefault();

            return resultContract.Result.AsExchangeResult(Exchange, new SharedFuturesTicker(resultContract.Result.Data.Name, ticker.LastPrice, ticker.HighPrice, ticker.LowPrice, ticker.Volume, ticker.ChangePercentage)
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
        async Task<ExchangeWebResult<IEnumerable<SharedFuturesTicker>>> IFuturesTickerRestClient.GetFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesTickerRestClient)this).GetFuturesTickersOptions.ValidateRequest(Exchange, request, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedFuturesTicker>>(Exchange, validationError);

            var resultTickers = ExchangeData.GetTickersAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, ct: ct);
            var resultContracts = ExchangeData.GetContractsAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, ct: ct);
            await Task.WhenAll(resultTickers, resultContracts).ConfigureAwait(false);
            if (!resultTickers.Result)
                return resultTickers.Result.AsExchangeResult<IEnumerable<SharedFuturesTicker>>(Exchange, default);
            if (!resultContracts.Result)
                return resultContracts.Result.AsExchangeResult<IEnumerable<SharedFuturesTicker>>(Exchange, default);

            return resultTickers.Result.AsExchangeResult<IEnumerable<SharedFuturesTicker>>(Exchange, resultTickers.Result.Data.Select(x =>
            {
                var contract = resultContracts.Result.Data.Single(p => p.Name == x.Contract);
                return new SharedFuturesTicker(x.Contract, x.LastPrice, x.HighPrice, x.LowPrice, x.Volume, x.ChangePercentage)
                {
                    IndexPrice = contract.IndexPrice,
                    MarkPrice = contract.MarkPrice,
                    FundingRate = contract.FundingRate,
                    NextFundingTime = contract.NextFundingTime
                };
            }).ToArray());
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
        async Task<ExchangeWebResult<IEnumerable<SharedFuturesSymbol>>> IFuturesSymbolRestClient.GetFuturesSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesSymbolRestClient)this).GetFuturesSymbolsOptions.ValidateRequest(Exchange, request, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedFuturesSymbol>>(Exchange, validationError);

            var result = await ExchangeData.GetContractsAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedFuturesSymbol>>(Exchange, default);

            var data = result.Data;
            if (request.ApiType.HasValue)
                data = data.Where(x => request.ApiType == ApiType.PerpetualLinear ? x.Type == ContractType.Direct : x.Type == ContractType.Inverse);
            return result.AsExchangeResult<IEnumerable<SharedFuturesSymbol>>(Exchange, data.Select(s => new SharedFuturesSymbol(
                s.Type == ContractType.Inverse ? SharedSymbolType.PerpetualInverse : SharedSymbolType.PerpetualLinear,
                s.Name.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries)[0], s.Name.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries)[1],
                s.Name,
                !s.Delisting)
            {
                MinTradeQuantity = s.MinOrderQuantity,
                MaxTradeQuantity = s.MaxOrderQuantity,
                QuantityStep = 1,
                PriceStep = s.OrderPriceStep,
                ContractSize = s.Multiplier
            }).ToArray());
        }

        #endregion

        #region Futures Order Client

        PlaceFuturesOrderOptions IFuturesOrderRestClient.PlaceFuturesOrderOptions { get; } = new PlaceFuturesOrderOptions(
            new[]
            {
                SharedOrderType.Limit,
                SharedOrderType.Market
            },
            new[]
            {
                SharedTimeInForce.GoodTillCanceled,
                SharedTimeInForce.ImmediateOrCancel,
                SharedTimeInForce.FillOrKill
            },
            new SharedQuantitySupport(
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts))
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };

        SharedFeeDeductionType IFuturesOrderRestClient.FuturesFeeDeductionType => SharedFeeDeductionType.AddToCost;
        SharedFeeAssetType IFuturesOrderRestClient.FuturesFeeAssetType => SharedFeeAssetType.QuoteAsset;


        async Task<ExchangeWebResult<SharedId>> IFuturesOrderRestClient.PlaceFuturesOrderAsync(PlaceFuturesOrderRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).PlaceFuturesOrderOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

#warning unclear how to reduce a position in when in hedge mode
            //var isIncrease = (request.Side == SharedOrderSide.Buy && request.PositionSide == SharedPositionSide.Long)
            //    || (request.Side == SharedOrderSide.Sell && request.PositionSide == SharedPositionSide.Short);
            //if (request.PositionSide == null || isIncrease)
            //{
            var result = await Trading.PlaceOrderAsync(
                    ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                    request.Symbol.GetSymbol(FormatSymbol),
                    GetOrderSide(request.Side, request.PositionSide),
                    quantity: (int)(request.Quantity ?? 0),
                    price: request.Price,
                    reduceOnly: request.ReduceOnly,
                    timeInForce: GetTimeInForce(request.OrderType, request.TimeInForce),
                    text: request.ClientOrderId,
                    ct: ct).ConfigureAwait(false);

                if (!result)
                    return result.AsExchangeResult<SharedId>(Exchange, default);

                return result.AsExchangeResult(Exchange, new SharedId(result.Data.Id.ToString()));
            //}
            //else
            //{
            //    var result = await Trading.PlaceOrderAsync(
            //        ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
            //        request.Symbol.GetSymbol(FormatSymbol),
            //        OrderSide.Buy,
            //        quantity: (int)(Math.Abs(request.Quantity ?? 0)),
            //        price: request.Price,
            //        timeInForce: GetTimeInForce(request.OrderType, request.TimeInForce),
            //        text: request.ClientOrderId,
            //        closePosition : true,
            //        //closeSide: request.PositionSide == SharedPositionSide.Long ? CloseSide.CloseLong : CloseSide.CloseShort,
            //        reduceOnly: true,
            //        ct: ct).ConfigureAwait(false);

            //    if (!result)
            //        return result.AsExchangeResult<SharedId>(Exchange, default);

            //    return result.AsExchangeResult(Exchange, new SharedId(result.Data.Id.ToString()));
            //}
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
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesOrderOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedFuturesOrder>(Exchange, new ArgumentError("Invalid order id"));

            var order = await Trading.GetOrderAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, orderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedFuturesOrder>(Exchange, default);

            return order.AsExchangeResult(Exchange, new SharedFuturesOrder(
                order.Data.Contract,
                order.Data.Id.ToString(),
                ParseOrderType(order.Data.TimeInForce, order.Data.Price),
                order.Data.Quantity > 0 ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.Status),
                order.Data.CreateTime)
            {
                ClientOrderId = order.Data.Text,
                AveragePrice = order.Data.FillPrice == 0 ? null : order.Data.FillPrice,
                Price = order.Data.Price,
                Quantity = Math.Abs(order.Data.Quantity),
                QuantityFilled = Math.Abs(order.Data.Quantity) - order.Data.QuantityRemaining,
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
        async Task<ExchangeWebResult<IEnumerable<SharedFuturesOrder>>> IFuturesOrderRestClient.GetOpenFuturesOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetOpenFuturesOrdersOptions.ValidateRequest(Exchange, request, request.Symbol?.ApiType ?? request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedFuturesOrder>>(Exchange, validationError);

            var symbol = request.Symbol?.GetSymbol(FormatSymbol);
            var orders = await Trading.GetOrdersAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, OrderStatus.Open, symbol, ct: ct).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<IEnumerable<SharedFuturesOrder>>(Exchange, default);

            return orders.AsExchangeResult<IEnumerable<SharedFuturesOrder>>(Exchange, orders.Data.Select(x => new SharedFuturesOrder(
                x.Contract,
                x.Id.ToString(),
                ParseOrderType(x.TimeInForce, x.Price),
                x.Quantity > 0 ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.Status),
                x.CreateTime)
            {
                ClientOrderId = x.Text,
                AveragePrice = x.FillPrice == 0 ? null : x.FillPrice,
                Price = x.Price,
                Quantity = Math.Abs(x.Quantity),
                QuantityFilled = Math.Abs(x.Quantity) - x.QuantityRemaining,
                TimeInForce = ParseTimeInForce(x.TimeInForce),
                UpdateTime = x.FinishTime ?? x.CreateTime,
                ReduceOnly = x.IsReduceOnly
            }).ToArray());
        }

        PaginatedEndpointOptions<GetClosedOrdersRequest> IFuturesOrderRestClient.GetClosedFuturesOrdersOptions { get; } = new PaginatedEndpointOptions<GetClosedOrdersRequest>(SharedPaginationType.Ascending, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeWebResult<IEnumerable<SharedFuturesOrder>>> IFuturesOrderRestClient.GetClosedFuturesOrdersAsync(GetClosedOrdersRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetClosedFuturesOrdersOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedFuturesOrder>>(Exchange, validationError);

            // Determine page token
            int? offset = null;
            if (pageToken is OffsetToken offsetToken)
                offset = offsetToken.Offset;

            // Get data
            var orders = await Trading.GetOrdersByTimestampAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                request.Symbol.GetSymbol(FormatSymbol),
                startTime: request.StartTime,
                endTime: request.EndTime,
                limit: request.Limit ?? 1000,
                offset: offset,
                ct: ct).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<IEnumerable<SharedFuturesOrder>>(Exchange, default);

            // Get next token
            OffsetToken? nextToken = null;
            if (orders.Data.Count() == (request.Limit ?? 1000))
                nextToken = new OffsetToken((offset ?? 0) + orders.Data.Count());

            return orders.AsExchangeResult<IEnumerable<SharedFuturesOrder>>(Exchange, orders.Data.Select(x => new SharedFuturesOrder(
                x.Contract,
                x.Id.ToString(),
                ParseOrderType(x.TimeInForce, x.Price),
                x.Quantity > 0 ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.Status),
                x.CreateTime)
            {
                ClientOrderId = x.Text,
                AveragePrice = x.FillPrice == 0 ? null : x.FillPrice,
                Price = x.Price,
                Quantity = Math.Abs(x.Quantity),
                QuantityFilled = Math.Abs(x.Quantity) - x.QuantityRemaining,
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
        async Task<ExchangeWebResult<IEnumerable<SharedUserTrade>>> IFuturesOrderRestClient.GetFuturesOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesOrderTradesOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedUserTrade>>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<IEnumerable<SharedUserTrade>>(Exchange, new ArgumentError("Invalid order id"));

            var orders = await Trading.GetUserTradesAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, request.Symbol.GetSymbol(FormatSymbol), orderId: orderId, ct: ct).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, default);

            return orders.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, orders.Data.Select(x => new SharedUserTrade(
                x.Contract,
                x.OrderId.ToString(),
                x.Id.ToString(),
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

        PaginatedEndpointOptions<GetUserTradesRequest> IFuturesOrderRestClient.GetFuturesUserTradesOptions { get; } = new PaginatedEndpointOptions<GetUserTradesRequest>(SharedPaginationType.Ascending, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeWebResult<IEnumerable<SharedUserTrade>>> IFuturesOrderRestClient.GetFuturesUserTradesAsync(GetUserTradesRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesUserTradesOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedUserTrade>>(Exchange, validationError);

            // Determine page token
            int? offset = null;
            if (pageToken is OffsetToken offsetToken)
                offset = offsetToken.Offset;

            // Get data
            var orders = await Trading.GetUserTradesByTimestampAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, request.Symbol.GetSymbol(FormatSymbol),
                startTime: request.StartTime,
                endTime: request.EndTime,
                limit: request.Limit ?? 1000,
                offset: offset,
                ct: ct
                ).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, default);

            // Get next token
            OffsetToken? nextToken = null;
            if (orders.Data.Count() == (request.Limit ?? 1000))
                nextToken = new OffsetToken((offset ?? 0) + orders.Data.Count());

            return orders.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, orders.Data.Select(x => new SharedUserTrade(
                x.Contract,
                x.OrderId.ToString(),
                x.Id.ToString(),
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
            var validationError = ((IFuturesOrderRestClient)this).CancelFuturesOrderOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedId>(Exchange, new ArgumentError("Invalid order id"));

            var order = await Trading.CancelOrderAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, orderId).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedId>(Exchange, default);

            return order.AsExchangeResult(Exchange, new SharedId(order.Data.Id.ToString()));
        }

        EndpointOptions<GetPositionsRequest> IFuturesOrderRestClient.GetPositionsOptions { get; } = new EndpointOptions<GetPositionsRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeWebResult<IEnumerable<SharedPosition>>> IFuturesOrderRestClient.GetPositionsAsync(GetPositionsRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetPositionsOptions.ValidateRequest(Exchange, request, request.Symbol?.ApiType ?? request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedPosition>>(Exchange, validationError);

            if (request.Symbol == null)
            {
                var result = await Trading.GetPositionsAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, ct: ct).ConfigureAwait(false);
                if (!result)
                    return result.AsExchangeResult<IEnumerable<SharedPosition>>(Exchange, default);
                return result.AsExchangeResult<IEnumerable<SharedPosition>>(Exchange, result.Data.Select(x => new SharedPosition(x.Contract, Math.Abs(x.Size), x.UpdateTime)
                {
                    UnrealizedPnl = x.UnrealisedPnl,
                    LiquidationPrice = x.LiquidationPrice,
                    AverageEntryPrice = x.EntryPrice,
                    InitialMargin = x.InitialMargin,
                    Leverage = x.Leverage,
                    MaintenanceMargin = x.MaintenanceRate,
                    PositionSide = x.PositionMode == PositionMode.Single ? (x.Size > 0 ? SharedPositionSide.Long : SharedPositionSide.Short) : x.PositionMode == PositionMode.DualShort ? SharedPositionSide.Short : SharedPositionSide.Long
                }).ToArray());
            }
            else
            {
                var result = await Trading.GetPositionAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, request.Symbol.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
                if (!result)
                    return result.AsExchangeResult<IEnumerable<SharedPosition>>(Exchange, default);

                return result.AsExchangeResult<IEnumerable<SharedPosition>>(Exchange, new[] { new SharedPosition(result.Data.Contract, Math.Abs(result.Data.Size), result.Data.UpdateTime)
                {
                    UnrealizedPnl = result.Data.UnrealisedPnl,
                    LiquidationPrice = result.Data.LiquidationPrice,
                    AverageEntryPrice = result.Data.EntryPrice,
                    InitialMargin = result.Data.InitialMargin,
                    Leverage = result.Data.Leverage,
                    MaintenanceMargin = result.Data.MaintenanceRate,
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
            var validationError = ((IFuturesOrderRestClient)this).ClosePositionOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var result = await Trading.PlaceOrderAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                request.Symbol.GetSymbol(FormatSymbol),
                request.PositionSide == SharedPositionSide.Long ? OrderSide.Sell : OrderSide.Buy,
                0,
                0,
                timeInForce: TimeInForce.FillOrKill,
                closePosition: request.PositionSide == null ? true : null,
                closeSide: request.PositionSide == null ? null : request.PositionSide == SharedPositionSide.Long ? CloseSide.CloseLong : CloseSide.CloseShort,
                reduceOnly: request.PositionSide == null ? null : true,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedId>(Exchange, default);

            return result.AsExchangeResult(Exchange, new SharedId(result.Data.Id.ToString()));
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

        #region Klines client

        GetKlinesOptions IKlineRestClient.GetKlinesOptions { get; } = new GetKlinesOptions(SharedPaginationType.Descending, false)
        {
            MaxRequestDataPoints = 2000,
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };

        async Task<ExchangeWebResult<IEnumerable<SharedKline>>> IKlineRestClient.GetKlinesAsync(GetKlinesRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeWebResult<IEnumerable<SharedKline>>(Exchange, new ArgumentError("Interval not supported"));

            var validationError = ((IKlineRestClient)this).GetKlinesOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedKline>>(Exchange, validationError);

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
                request.Symbol.GetSymbol(FormatSymbol),
                interval,
                startTime: startTime,
                endTime: endTime,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedKline>>(Exchange, default);

            // Get next token
            DateTimeToken? nextToken = null;
            if (result.Data.Count() == limit)
            {
                var minOpenTime = result.Data.Min(x => x.OpenTime);
                if (request.StartTime == null || minOpenTime > request.StartTime.Value)
                    nextToken = new DateTimeToken(minOpenTime.AddSeconds(-(int)interval));
            }

            return result.AsExchangeResult<IEnumerable<SharedKline>>(Exchange, result.Data.Reverse().Select(x => new SharedKline(x.OpenTime, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice, x.Volume)).ToArray(), nextToken);
        }

        #endregion

        #region Index Klines client

        GetKlinesOptions IIndexPriceKlineRestClient.GetIndexPriceKlinesOptions { get; } = new GetKlinesOptions(SharedPaginationType.Descending, false)
        {
            MaxRequestDataPoints = 1000,
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };

        async Task<ExchangeWebResult<IEnumerable<SharedMarkKline>>> IIndexPriceKlineRestClient.GetIndexPriceKlinesAsync(GetKlinesRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeWebResult<IEnumerable<SharedMarkKline>>(Exchange, new ArgumentError("Interval not supported"));

            var validationError = ((IIndexPriceKlineRestClient)this).GetIndexPriceKlinesOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedMarkKline>>(Exchange, validationError);

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

            var result = await ExchangeData.GetIndexKlinesAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                request.Symbol.GetSymbol(FormatSymbol),
                interval,
                startTime: startTime,
                endTime: endTime,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedMarkKline>>(Exchange, default);

            // Get next token
            DateTimeToken? nextToken = null;
            if (result.Data.Count() == limit)
            {
                var minOpenTime = result.Data.Min(x => x.OpenTime);
                if (request.StartTime == null || minOpenTime > request.StartTime.Value)
                    nextToken = new DateTimeToken(minOpenTime.AddSeconds(-(int)interval));
            }

            return result.AsExchangeResult<IEnumerable<SharedMarkKline>>(Exchange, result.Data.Reverse().Select(x => new SharedMarkKline(x.OpenTime, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice)).ToArray(), nextToken);
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
        async Task<ExchangeWebResult<IEnumerable<SharedTrade>>> IRecentTradeRestClient.GetRecentTradesAsync(GetRecentTradesRequest request, CancellationToken ct)
        {
            var validationError = ((IRecentTradeRestClient)this).GetRecentTradesOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedTrade>>(Exchange, validationError);

            var result = await ExchangeData.GetTradesAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                request.Symbol.GetSymbol(FormatSymbol),
                limit: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedTrade>>(Exchange, default);

            return result.AsExchangeResult<IEnumerable<SharedTrade>>(Exchange, result.Data.Select(x => new SharedTrade(Math.Abs(x.Quantity), x.Price, x.CreateTime)).ToArray());
        }

        #endregion

        #region Trade History client
        GetTradeHistoryOptions ITradeHistoryRestClient.GetTradeHistoryOptions { get; } = new GetTradeHistoryOptions(SharedPaginationType.Descending, false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };

        async Task<ExchangeWebResult<IEnumerable<SharedTrade>>> ITradeHistoryRestClient.GetTradeHistoryAsync(GetTradeHistoryRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((ITradeHistoryRestClient)this).GetTradeHistoryOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedTrade>>(Exchange, validationError);

            int offset = 0;
            if (pageToken is OffsetToken token)
                offset = token.Offset;

            // Get data
            var limit = request.Limit ?? 1000;
            var result = await ExchangeData.GetTradesAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                request.Symbol.GetSymbol(FormatSymbol),
                startTime: request.StartTime,
                endTime: request.EndTime,
                limit: limit,
                offset: offset,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedTrade>>(Exchange, default);

            OffsetToken? nextToken = null;
            if (result.Data.Count() == limit)
                nextToken = new OffsetToken(offset + limit);

            // Return
            return result.AsExchangeResult<IEnumerable<SharedTrade>>(Exchange, result.Data.Select(x => new SharedTrade(Math.Abs(x.Quantity), x.Price, x.CreateTime)).ToArray(), nextToken);
        }
        #endregion

        #region Leverage client

        EndpointOptions<GetLeverageRequest> ILeverageRestClient.GetLeverageOptions { get; } = new EndpointOptions<GetLeverageRequest>(true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeWebResult<SharedLeverage>> ILeverageRestClient.GetLeverageAsync(GetLeverageRequest request, CancellationToken ct)
        {
            var validationError = ((ILeverageRestClient)this).GetLeverageOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedLeverage>(Exchange, validationError);

            var result = await Trading.GetPositionAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, 
                request.Symbol.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedLeverage>(Exchange, default);

            return result.AsExchangeResult(Exchange, new SharedLeverage(result.Data.Leverage)
            {
                Side = request.Side
            });
        }

        SetLeverageOptions ILeverageRestClient.SetLeverageOptions { get; } = new SetLeverageOptions(false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeWebResult<SharedLeverage>> ILeverageRestClient.SetLeverageAsync(SetLeverageRequest request, CancellationToken ct)
        {
            var validationError = ((ILeverageRestClient)this).SetLeverageOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedLeverage>(Exchange, validationError);

            var result = await Trading.UpdatePositionLeverageAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                request.Symbol.GetSymbol(FormatSymbol),
                request.Leverage,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedLeverage>(Exchange, default);

            return result.AsExchangeResult(Exchange, new SharedLeverage(result.Data.Leverage));
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
            var validationError = ((IOrderBookRestClient)this).GetOrderBookOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedOrderBook>(Exchange, validationError);

            var result = await ExchangeData.GetOrderBookAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                request.Symbol.GetSymbol(FormatSymbol),
                depth: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedOrderBook>(Exchange, default);

            return result.AsExchangeResult(Exchange, new SharedOrderBook(result.Data.Asks, result.Data.Bids));
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
            var validationError = ((IOpenInterestRestClient)this).GetOpenInterestOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedOpenInterest>(Exchange, validationError);

            var result = await ExchangeData.GetContractAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, request.Symbol.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedOpenInterest>(Exchange, default);

            return result.AsExchangeResult(Exchange, new SharedOpenInterest(result.Data.PositionSize));
        }

        #endregion

        #region Funding Rate client
        GetFundingRateHistoryOptions IFundingRateRestClient.GetFundingRateHistoryOptions { get; } = new GetFundingRateHistoryOptions(SharedPaginationType.NotSupported, false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };

        async Task<ExchangeWebResult<IEnumerable<SharedFundingRate>>> IFundingRateRestClient.GetFundingRateHistoryAsync(GetFundingRateHistoryRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((IFundingRateRestClient)this).GetFundingRateHistoryOptions.ValidateRequest(Exchange, request, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedFundingRate>>(Exchange, validationError);

            // Get data
            var result = await ExchangeData.GetFundingRateHistoryAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                request.Symbol.GetSymbol(FormatSymbol),
                limit: 1000,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedFundingRate>>(Exchange, default);

            // Return
            return result.AsExchangeResult<IEnumerable<SharedFundingRate>>(Exchange, result.Data.Select(x => new SharedFundingRate(x.FundingRate, x.Timestamp)).ToArray());
        }
        #endregion

        #region Position Mode client

        GetPositionModeOptions IPositionModeRestClient.GetPositionModeOptions { get; } = new GetPositionModeOptions(false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeWebResult<SharedPositionModeResult>> IPositionModeRestClient.GetPositionModeAsync(GetPositionModeRequest request, CancellationToken ct)
        {
            var validationError = ((IPositionModeRestClient)this).GetPositionModeOptions.ValidateRequest(Exchange, request, request.Symbol?.ApiType ?? request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedPositionModeResult>(Exchange, validationError);

            var result = await Account.GetAccountAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedPositionModeResult>(Exchange, default);

            return result.AsExchangeResult(Exchange, new SharedPositionModeResult(result.Data.DualMode ? SharedPositionMode.LongShort : SharedPositionMode.OneWay));
        }

        SetPositionModeOptions IPositionModeRestClient.SetPositionModeOptions { get; } = new SetPositionModeOptions(true, true, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeWebResult<SharedPositionModeResult>> IPositionModeRestClient.SetPositionModeAsync(SetPositionModeRequest request, CancellationToken ct)
        {
            var validationError = ((IPositionModeRestClient)this).SetPositionModeOptions.ValidateRequest(Exchange, request, request.Symbol?.ApiType ?? request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedPositionModeResult>(Exchange, validationError);

            var result = await Account.UpdatePositionModeAsync(ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!, request.Mode == SharedPositionMode.LongShort, ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedPositionModeResult>(Exchange, default);

            return result.AsExchangeResult(Exchange, new SharedPositionModeResult(request.Mode));
        }
        #endregion

        #region Position History client

        GetPositionHistoryOptions IPositionHistoryRestClient.GetPositionHistoryOptions { get; } = new GetPositionHistoryOptions(false, SharedPaginationType.Descending)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        async Task<ExchangeWebResult<IEnumerable<SharedPositionHistory>>> IPositionHistoryRestClient.GetPositionHistoryAsync(GetPositionHistoryRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((IPositionHistoryRestClient)this).GetPositionHistoryOptions.ValidateRequest(Exchange, request, request.Symbol?.ApiType ?? request.ApiType!.Value, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedPositionHistory>>(Exchange, validationError);

            // Determine page token
            int offset = 0;
            int limit = request.Limit ?? 100;
            if (pageToken is OffsetToken token)
                offset = token.Offset;

            // Get data
            var orders = await Trading.GetPositionCloseHistoryAsync(
                ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!,
                contract: request.Symbol!.GetSymbol(FormatSymbol),
                startTime: request.StartTime,
                endTime: request.EndTime,
                offset: offset,
                limit: limit,
                ct: ct
                ).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<IEnumerable<SharedPositionHistory>>(Exchange, default);

            // Get next token
            OffsetToken? nextToken = null;
            if (orders.Data.Count() == limit)
                nextToken = new OffsetToken(offset + limit);

            return orders.AsExchangeResult<IEnumerable<SharedPositionHistory>>(Exchange, orders.Data.Select(x => new SharedPositionHistory(
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
    }
}
