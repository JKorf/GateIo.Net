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
    internal partial class GateIoRestClientSpotSharedApi
    {
        #region Place Spot Trigger Order

        async Task<IExchangeCallResult<SharedId>> IPlaceSpotTriggerOrder.PlaceSpotTriggerOrderAsync(PlaceSpotTriggerOrderRequest request, CancellationToken ct)
            => await PlaceSpotTriggerOrderAsync(request, ct).ConfigureAwait(false);

        public PlaceSpotTriggerOrderOptions PlaceSpotTriggerOrderOptions { get; } = new PlaceSpotTriggerOrderOptions(_exchangeName, false);
        public async Task<HttpResult<SharedId>> PlaceSpotTriggerOrderAsync(PlaceSpotTriggerOrderRequest request, CancellationToken ct)
        {
            var validationError = PlaceSpotTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var orderType = request.OrderPrice == null ? NewOrderType.Market : NewOrderType.Limit;
            var result = await _api.Trading.PlaceTriggerOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                request.OrderSide == SharedOrderSide.Buy ? OrderSide.Buy : OrderSide.Sell,
                orderType,
                request.PriceDirection == SharedTriggerPriceDirection.PriceAbove ? TriggerType.EqualOrHigher : TriggerType.EqualOrLower,
                quantity: (orderType == NewOrderType.Market && request.OrderSide == SharedOrderSide.Buy ? request.Quantity?.QuantityInQuoteAsset : request.Quantity?.QuantityInBaseAsset) ?? 0,
                orderPrice: request.OrderPrice,
                triggerPrice: request.TriggerPrice,
                expiration: TimeSpan.FromDays(30),
                accountType: TriggerAccountType.Normal,
                text: request.ClientOrderId,
                timeInForce: GetTimeInForce(orderType == NewOrderType.Market ? SharedOrderType.Market: SharedOrderType.Limit, request.TimeInForce) ?? (orderType == NewOrderType.Market ? TimeInForce.ImmediateOrCancel : TimeInForce.GoodTillCancel),
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            // Return
            return HttpResult.Ok(result, new SharedId(result.Data.Id.ToString()));
        }

        #endregion

        #region Get Spot Trigger Order

        async Task<IExchangeCallResult<SharedSpotTriggerOrder>> IGetSpotTriggerOrder.GetSpotTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
            => await GetSpotTriggerOrderAsync(request, ct).ConfigureAwait(false);

        public GetSpotTriggerOrderOptions GetSpotTriggerOrderOptions { get; } = new GetSpotTriggerOrderOptions(_exchangeName, true)
        {
        };
        public async Task<HttpResult<SharedSpotTriggerOrder>> GetSpotTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetSpotTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotTriggerOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedSpotTriggerOrder>(Exchange, ArgumentError.Invalid(nameof(GetOrderRequest.OrderId), "Invalid order id"));

            var order = await _api.Trading.GetTriggerOrderAsync(orderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedSpotTriggerOrder>(order);

            GateIoOrder? orderInfo = null;
            if (order.Data.TriggeredOrderId > 0)
            {
                var orderInfoResult = await _api.Trading.GetOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), order.Data.TriggeredOrderId).ConfigureAwait(false);
                if (!orderInfoResult.Success)
                    return HttpResult.Fail<SharedSpotTriggerOrder>(orderInfoResult);

                orderInfo = orderInfoResult.Data;
            }

            return HttpResult.Ok(order, new SharedSpotTriggerOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, order.Data.Symbol),
                order.Data.Symbol,
                order.Data.Id.ToString(),
                order.Data.Order.Type == NewOrderType.Market ? SharedOrderType.Market: SharedOrderType.Limit,
                order.Data.Order.Side == OrderSide.Buy ? SharedTriggerOrderDirection.Enter : SharedTriggerOrderDirection.Exit,
                ParseTriggerOrderStatus(order.Data.Status, orderInfo),
                order.Data.Trigger.Price,
                order.Data.CreateTime)
            {
                PlacedOrderId = order.Data.TriggeredOrderId?.ToString(),
                AveragePrice = orderInfo?.AveragePrice == 0 ? null : orderInfo?.AveragePrice,
                OrderPrice = order.Data.Order.Price,
                OrderQuantity = new SharedOrderQuantity(order.Data.Order.Quantity),
                QuantityFilled = new SharedOrderQuantity(orderInfo?.QuantityFilled, orderInfo?.QuoteQuantityFilled),
                TimeInForce = ParseTimeInForce(order.Data.Order.TimeInForce),
                UpdateTime = orderInfo?.UpdateTime ?? order.Data.TriggerTime ?? order.Data.CreateTime,
                Fee = orderInfo?.Fee,
                FeeAsset = orderInfo?.FeeAsset
            });
        }

        #endregion

        private SharedTriggerOrderStatus ParseTriggerOrderStatus(TriggerOrderStatus? status, GateIoOrder? orderInfo)
        {
            if (status == TriggerOrderStatus.Expired || status == TriggerOrderStatus.Canceled || status == TriggerOrderStatus.Failed)
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

        #region Cancel Spot Trigger Order

        async Task<IExchangeCallResult<SharedId>> ICancelSpotTriggerOrder.CancelSpotTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
            => await CancelSpotTriggerOrderAsync(request, ct).ConfigureAwait(false);

        public CancelSpotTriggerOrderOptions CancelSpotTriggerOrderOptions { get; } = new CancelSpotTriggerOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelSpotTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelSpotTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid(nameof(CancelOrderRequest.OrderId), "Invalid order id"));

            var order = await _api.Trading.CancelTriggerOrderAsync(
                orderId,
                ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(request.OrderId));
        }

        #endregion

    }
}
