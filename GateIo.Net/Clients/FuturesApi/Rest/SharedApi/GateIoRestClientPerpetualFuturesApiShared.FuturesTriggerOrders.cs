using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using GateIo.Net.Enums;
using GateIo.Net.Interfaces.Clients.SpotApi;
using GateIo.Net.Objects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace GateIo.Net.Clients.FuturesApi
{
    internal partial class GateIoRestClientPerpetualFuturesSharedApi
    {
        #region Place Futures Trigger Order

        async Task<ICallResult<SharedId>> IPlaceFuturesTriggerOrder.PlaceFuturesTriggerOrderAsync(PlaceFuturesTriggerOrderRequest request, CancellationToken ct)
            => await PlaceFuturesTriggerOrderAsync(request, ct).ConfigureAwait(false);

        public PlaceFuturesTriggerOrderOptions PlaceFuturesTriggerOrderOptions { get; } = new PlaceFuturesTriggerOrderOptions(_exchangeName, false)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        public async Task<HttpResult<SharedId>> PlaceFuturesTriggerOrderAsync(PlaceFuturesTriggerOrderRequest request, CancellationToken ct)
        {
            var side = GetTriggerOrderSide(request);
            var validationError = PlaceFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var orderType = request.OrderPrice == null ? NewOrderType.Market : NewOrderType.Limit;
            var result = await _api.Trading.PlaceTriggerOrderAsync(
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

        #endregion

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

        #region Get Futures Trigger Order

        async Task<ICallResult<SharedFuturesTriggerOrder>> IGetFuturesTriggerOrder.GetFuturesTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
            => await GetFuturesTriggerOrderAsync(request, ct).ConfigureAwait(false);

        public GetFuturesTriggerOrderOptions GetFuturesTriggerOrderOptions { get; } = new GetFuturesTriggerOrderOptions(_exchangeName, true)
        {
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("SettleAsset", typeof(string), "Settlement asset, btc, usd or usdt", "usdt")
            }
        };
        public async Task<HttpResult<SharedFuturesTriggerOrder>> GetFuturesTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTriggerOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedFuturesTriggerOrder>(Exchange, ArgumentError.Invalid(nameof(GetOrderRequest.OrderId), "Invalid order id"));

            var settleAsset = request.GetParamValue<string>(Exchange, "SettleAsset");
            var order = await _api.Trading.GetTriggerOrderAsync(
                settleAsset!, 
                orderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedFuturesTriggerOrder>(order);

            GateIoPerpOrder? orderInfo = null;
            if (order.Data.TradeId > 0)
            {
                var orderInfoResult = await _api.Trading.GetOrderAsync(settleAsset!, order.Data.TradeId).ConfigureAwait(false);
                if (!orderInfoResult.Success)
                    return HttpResult.Fail<SharedFuturesTriggerOrder>(orderInfoResult);

                orderInfo = orderInfoResult.Data;
            }

            var side = order.Data.Order.Quantity > 0 ? SharedOrderSide.Buy : SharedOrderSide.Sell;
            return HttpResult.Ok(order, new SharedFuturesTriggerOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, order.Data.Order.Contract),
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

        #endregion

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

        #region Cancel Futures Trigger Order

        async Task<ICallResult<SharedId>> ICancelFuturesTriggerOrder.CancelFuturesTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
            => await CancelFuturesTriggerOrderAsync(request, ct).ConfigureAwait(false);

        public CancelFuturesTriggerOrderOptions CancelFuturesTriggerOrderOptions { get; } = new CancelFuturesTriggerOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelFuturesTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid(nameof(GetOrderRequest.OrderId), "Invalid order id"));

            var settleAsset = ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "SettleAsset")!;
            var order = await _api.Trading.CancelTriggerOrderAsync(
                settleAsset,
                orderId,
                ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(request.OrderId));
        }

        #endregion

    }
}
