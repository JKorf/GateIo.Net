using CryptoExchange.Net.Objects;
using GateIo.Net.Objects.Models;
using GateIo.Net.Enums;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace GateIo.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// GateIo Spot trading endpoints, placing and managing orders.
    /// </summary>
    public interface IGateIoRestClientSpotApiTrading
    {
        /// <summary>
        /// Place a new order
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#create-an-order" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH_USDT`</param>
        /// <param name="type">Order type</param>
        /// <param name="side">Order side</param>
        /// <param name="quantity">Order quantity in base asset. For Market Buy orders it's in quote asset</param>
        /// <param name="price">Price of the order for limit orders</param>
        /// <param name="timeInForce">Time in force</param>
        /// <param name="icebergQuantity">Iceberg quantity</param>
        /// <param name="accountType">Account type</param>
        /// <param name="autoBorrow">Auto borrow enabled</param>
        /// <param name="autoRepay">Auto repay enabled</param>
        /// <param name="selfTradePreventionMode">Self trade prevention mode</param>
        /// <param name="text">User defined info</param>
        /// <param name="actionMode">Order response mode</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoOrder>> PlaceOrderAsync(
            string symbol,
            OrderSide side,
            NewOrderType type,
            decimal quantity,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            decimal? icebergQuantity = null,
            SpotAccountType? accountType = null,
            bool? autoBorrow = null,
            bool? autoRepay = null,
            SelfTradePreventionMode? selfTradePreventionMode = null,
            string? text = null,
            OrderActionMode? actionMode = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get all open orders
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#list-all-open-orders" /></para>
        /// </summary>
        /// <param name="page">Page</param>
        /// <param name="limit">Max amount of results</param>
        /// <param name="accountType">Filter by account type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoSymbolOrders[]>> GetOpenOrdersAsync(
            int? page = null,
            int? limit = null,
            SpotAccountType? accountType = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get orders
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#list-orders" /></para>
        /// </summary>
        /// <param name="open">Open orders (true) or closed orders (false)</param>
        /// <param name="symbol">Filter by symbol, required for open orders, for example `ETH_USDT`</param>
        /// <param name="page">Page</param>
        /// <param name="limit">Max amount of results</param>
        /// <param name="accountType">Filter by account type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="side">Filter by order side</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoOrder[]>> GetOrdersAsync(
            bool open,
            string? symbol = null,
            int? page = null,
            int? limit = null,
            SpotAccountType? accountType = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            OrderSide? side = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get a specific order by id
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#get-a-single-order" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH_USDT`</param>
        /// <param name="orderId">Order id, either this or `clientOrderId` should be provided</param>
        /// <param name="clientOrderId">Client order id, either this or `orderId` should be provided</param>
        /// <param name="accountType">Filter by account type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoOrder>> GetOrderAsync(
            string symbol,
            long? orderId = null,
            string? clientOrderId = null,
            SpotAccountType? accountType = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel all orders on a specific symbol
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/#cancel-all-open-orders-in-specified-currency-pair" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="side">Only cancel orders on this side</param>
        /// <param name="accountType">Account type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoOrderOperation[]>> CancelAllOrdersAsync(
            string symbol,
            OrderSide? side = null,
            SpotAccountType? accountType = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel multiple orders. Check the individual response models to see if cancelation succeeded
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/#cancel-a-batch-of-orders-with-an-id-list" /></para>
        /// </summary>
        /// <param name="orders">Orders to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoCancelResult[]>> CancelOrdersAsync(
            IEnumerable<GateIoBatchCancelRequest> orders,
            CancellationToken ct = default);

        /// <summary>
        /// Edit an active order
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/#amend-an-order" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH_USDT`</param>
        /// <param name="orderId">Order id, either `orderId` or `clientOrderId` required</param>
        /// <param name="clientOrderId">user custom ID (i.e., t-123c456f), either `orderId` or `clientOrderId` required</param>
        /// <param name="price">New price</param>
        /// <param name="quantity">New quantity</param>
        /// <param name="amendText">Custom info during amending order</param>
        /// <param name="accountType">Specify operation account. Default to spot ,portfolio and margin account if not specified. Set to cross_margin to operate against margin account. Portfolio margin account must set to cross_margin only</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoOrder>> EditOrderAsync(
            string symbol,
            long? orderId,
            string? clientOrderId = null,
            decimal? price = null,
            decimal? quantity = null,
            string? amendText = null,
            SpotAccountType? accountType = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel an order
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/#cancel-a-single-order" /></para>
        /// </summary>
        /// <param name="symbol">Symbol of the order, for example `ETH_USDT`</param>
        /// <param name="orderId">Order id, either `orderId` or `clientOrderId` required</param>
        /// <param name="clientOrderId">user custom ID (i.e., t-123c456f), either `orderId` or `clientOrderId` required</param>
        /// <param name="accountType">Account type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoOrder>> CancelOrderAsync(
            string symbol,
            long? orderId = null,
            string? clientOrderId = null,
            SpotAccountType? accountType = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get a list of trades for the current user
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/#list-personal-trading-history" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="orderId">Filter by order id</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="page">Page number</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="accountType">Filter by account type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoUserTrade[]>> GetUserTradesAsync(
            string? symbol = null,
            long? orderId = null,
            int? limit = null,
            int? page = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            SpotAccountType? accountType = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel orders after a certain period. Can be called at interval to act as a deadmans switch. Using TimeSpan.Zero cancels the countdown
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/#countdown-cancel-orders" /></para>
        /// </summary>
        /// <param name="cancelAfter">Cancel after period</param>
        /// <param name="symbol">Only cancel on this symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoCancelAfter>> CancelOrdersAfterAsync(
            TimeSpan cancelAfter,
            string? symbol = null,
            CancellationToken ct = default);

        /// <summary>
        /// Place a new price triggered order 
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/#create-a-price-triggered-order" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="orderSide">Order side</param>
        /// <param name="orderType">Order type</param>
        /// <param name="triggerType">Type of trigger</param>
        /// <param name="triggerPrice">Trigger price</param>
        /// <param name="expiration">Time before trigger is cancelled</param>
        /// <param name="quantity">Order quantity</param>
        /// <param name="orderPrice">Order price</param>
        /// <param name="timeInForce">Time in force</param>
        /// <param name="accountType">Account type</param>
        /// <param name="text">User text</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoId>> PlaceTriggerOrderAsync(
            string symbol,
            OrderSide orderSide,
            NewOrderType orderType,
            TriggerType triggerType,
            decimal triggerPrice,
            TimeSpan expiration,
            decimal quantity,
            TriggerAccountType accountType,
            TimeInForce timeInForce,
            decimal? orderPrice = null,
            string? text = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get list of trigger orders
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/#retrieve-running-auto-order-list" /></para>
        /// </summary>
        /// <param name="open">True for open orders, false for closed orders</param>
        /// <param name="symbol">Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="accountType">Filter by account type</param>
        /// <param name="limit">Max amount of results</param>
        /// <param name="offset">Offset</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoTriggerOrder[]>> GetTriggerOrdersAsync(
            bool open,
            string? symbol = null,
            TriggerAccountType? accountType = null,
            int? limit = null,
            int? offset = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel all trigger orders
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/#cancel-all-open-orders" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="accountType">Filter by account type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoTriggerOrder[]>> CancelAllTriggerOrdersAsync(string? symbol = null, TriggerAccountType? accountType = null, CancellationToken ct = default);

        /// <summary>
        /// Get a trigger order by id
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/#get-a-price-triggered-order" /></para>
        /// </summary>
        /// <param name="id">Id of the trigger order</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoTriggerOrder>> GetTriggerOrderAsync(long id, CancellationToken ct = default);

        /// <summary>
        /// Cancel price trigger order
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/#cancel-a-price-triggered-order" /></para>
        /// </summary>
        /// <param name="id">Id of the trigger order</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoTriggerOrder>> CancelTriggerOrderAsync(long id, CancellationToken ct = default);

        /// <summary>
        /// Place multiple orders in a single call
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/#create-a-batch-of-orders" /></para>
        /// </summary>
        /// <param name="orders">Orders to place</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoOrderOperation[]>> PlaceMultipleOrderAsync(
            IEnumerable<GateIoBatchPlaceRequest> orders,
            CancellationToken ct = default);

        /// <summary>
        /// Edit multiple orders in a single call
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/#batch-modification-of-orders" /></para>
        /// </summary>
        /// <param name="orders">Orders to edit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoOrderOperation[]>> EditMultipleOrderAsync(
            IEnumerable<GateIoBatchEditRequest> orders,
            CancellationToken ct = default);
    }
}
