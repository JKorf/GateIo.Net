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
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#create-an-order" /><br />
        /// Endpoint:<br />
        /// /api/v4/spot/orders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>currency_pair</c>"] Symbol, for example `ETH_USDT`</param>
        /// <param name="type">["<c>type</c>"] Order type</param>
        /// <param name="side">["<c>side</c>"] Order side</param>
        /// <param name="quantity">["<c>amount</c>"] Order quantity in base asset. For Market Buy orders it's in quote asset</param>
        /// <param name="price">["<c>price</c>"] Price of the order for limit orders</param>
        /// <param name="timeInForce">["<c>time_in_force</c>"] Time in force</param>
        /// <param name="icebergQuantity">["<c>iceberg</c>"] Iceberg quantity</param>
        /// <param name="accountType">["<c>account</c>"] Account type</param>
        /// <param name="autoBorrow">["<c>auto_borrow</c>"] Auto borrow enabled</param>
        /// <param name="autoRepay">["<c>auto_repay</c>"] Auto repay enabled</param>
        /// <param name="selfTradePreventionMode">["<c>stp_act</c>"] Self trade prevention mode</param>
        /// <param name="text">["<c>text</c>"] User defined info</param>
        /// <param name="actionMode">["<c>action_mode</c>"] Order response mode</param>
        /// <param name="slippage">["<c>slippage</c>"] Max slippage for market orders, 0.03 means 3%</param>
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
            decimal? slippage = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get all open orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#list-all-open-orders" /><br />
        /// Endpoint:<br />
        /// /api/v4/spot/open_orders
        /// </para>
        /// </summary>
        /// <param name="page">["<c>page</c>"] Page</param>
        /// <param name="limit">["<c>limit</c>"] Max amount of results</param>
        /// <param name="accountType">["<c>account</c>"] Filter by account type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoSymbolOrders[]>> GetOpenOrdersAsync(
            int? page = null,
            int? limit = null,
            SpotAccountType? accountType = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#list-orders" /><br />
        /// Endpoint:<br />
        /// /api/v4/spot/orders
        /// </para>
        /// </summary>
        /// <param name="open">["<c>status</c>"] Open orders (true) or closed orders (false)</param>
        /// <param name="symbol">["<c>currency_pair</c>"] Filter by symbol, required for open orders, for example `ETH_USDT`</param>
        /// <param name="page">["<c>page</c>"] Page</param>
        /// <param name="limit">["<c>limit</c>"] Max amount of results</param>
        /// <param name="accountType">["<c>account</c>"] Filter by account type</param>
        /// <param name="startTime">["<c>from</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>to</c>"] Filter by end time</param>
        /// <param name="side">["<c>side</c>"] Filter by order side</param>
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
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#query-single-order-details" /><br />
        /// Endpoint:<br />
        /// /api/v4/spot/orders/{orderId}
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>currency_pair</c>"] Symbol, for example `ETH_USDT`</param>
        /// <param name="orderId">Order id, either this or `clientOrderId` should be provided</param>
        /// <param name="clientOrderId">Client order id, either this or `orderId` should be provided</param>
        /// <param name="accountType">["<c>account</c>"] Filter by account type</param>
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
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#cancel-all-open-orders-in-specified-currency-pair" /><br />
        /// Endpoint:<br />
        /// /api/v4/spot/orders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>currency_pair</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="side">["<c>side</c>"] Only cancel orders on this side</param>
        /// <param name="accountType">["<c>account</c>"] Account type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoOrderOperation[]>> CancelAllOrdersAsync(
            string symbol,
            OrderSide? side = null,
            SpotAccountType? accountType = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel multiple orders. Check the individual response models to see if cancellation succeeded
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#cancel-batch-orders-by-specified-id-list" /><br />
        /// Endpoint:<br />
        /// /api/v4/spot/cancel_batch_orders
        /// </para>
        /// </summary>
        /// <param name="orders">Orders to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoCancelResult[]>> CancelOrdersAsync(
            IEnumerable<GateIoBatchCancelRequest> orders,
            CancellationToken ct = default);

        /// <summary>
        /// Edit an active order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#amend-single-order" /><br />
        /// Endpoint:<br />
        /// /api/v4/spot/amend_batch_orders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>currency_pair</c>"] Symbol, for example `ETH_USDT`</param>
        /// <param name="orderId">Order id, either `orderId` or `clientOrderId` required</param>
        /// <param name="clientOrderId">user custom ID (i.e., t-123c456f), either `orderId` or `clientOrderId` required</param>
        /// <param name="price">["<c>price</c>"] New price</param>
        /// <param name="quantity">["<c>amount</c>"] New quantity</param>
        /// <param name="amendText">["<c>amend_text</c>"] Custom info during amending order</param>
        /// <param name="accountType">["<c>account</c>"] Specify operation account. Default to spot ,portfolio and margin account if not specified. Set to cross_margin to operate against margin account. Portfolio margin account must set to cross_margin only</param>
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
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#cancel-single-order" /><br />
        /// Endpoint:<br />
        /// /api/v4/spot/orders/{id}
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>currency_pair</c>"] Symbol of the order, for example `ETH_USDT`</param>
        /// <param name="orderId">Order id, either `orderId` or `clientOrderId` required</param>
        /// <param name="clientOrderId">user custom ID (i.e., t-123c456f), either `orderId` or `clientOrderId` required</param>
        /// <param name="accountType">["<c>account</c>"] Account type</param>
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
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#query-personal-trading-records" /><br />
        /// Endpoint:<br />
        /// /api/v4/spot/my_trades
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>currency_pair</c>"] Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="orderId">["<c>order_id</c>"] Filter by order id</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="page">["<c>page</c>"] Page number</param>
        /// <param name="startTime">["<c>from</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>to</c>"] Filter by end time</param>
        /// <param name="accountType">["<c>account</c>"] Filter by account type</param>
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
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#countdown-cancel-orders" /><br />
        /// Endpoint:<br />
        /// /api/v4/spot/countdown_cancel_all
        /// </para>
        /// </summary>
        /// <param name="cancelAfter">["<c>timeout</c>"] Cancel after period</param>
        /// <param name="symbol">["<c>currency_pair</c>"] Only cancel on this symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoCancelAfter>> CancelOrdersAfterAsync(
            TimeSpan cancelAfter,
            string? symbol = null,
            CancellationToken ct = default);

        /// <summary>
        /// Place a new price triggered order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#create-price-triggered-order" /><br />
        /// Endpoint:<br />
        /// /api/v4/spot/price_orders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>market</c>"] Symbol</param>
        /// <param name="orderSide">["<c>put.side</c>"] Order side</param>
        /// <param name="orderType">["<c>put.type</c>"] Order type</param>
        /// <param name="triggerType">["<c>trigger.rule</c>"] Type of trigger</param>
        /// <param name="triggerPrice">["<c>trigger.price</c>"] Trigger price</param>
        /// <param name="expiration">["<c>trigger.expiration</c>"] Time before trigger is cancelled</param>
        /// <param name="quantity">["<c>put.amount</c>"] Order quantity</param>
        /// <param name="orderPrice">["<c>put.price</c>"] Order price</param>
        /// <param name="timeInForce">["<c>put.time_in_force</c>"] Time in force</param>
        /// <param name="accountType">["<c>put.account</c>"] Account type</param>
        /// <param name="text">["<c>put.text</c>"] User text</param>
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
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#query-running-auto-order-list" /><br />
        /// Endpoint:<br />
        /// /api/v4/spot/price_orders
        /// </para>
        /// </summary>
        /// <param name="open">["<c>status</c>"] True for open orders, false for closed orders</param>
        /// <param name="symbol">["<c>market</c>"] Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="accountType">["<c>account</c>"] Filter by account type</param>
        /// <param name="limit">["<c>limit</c>"] Max amount of results</param>
        /// <param name="offset">["<c>offset</c>"] Offset</param>
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
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#cancel-all-auto-orders" /><br />
        /// Endpoint:<br />
        /// /api/v4/spot/price_orders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>market</c>"] Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="accountType">["<c>account</c>"] Filter by account type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoTriggerOrder[]>> CancelAllTriggerOrdersAsync(string? symbol = null, TriggerAccountType? accountType = null, CancellationToken ct = default);

        /// <summary>
        /// Get a trigger order by id
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#query-single-auto-order-details" /><br />
        /// Endpoint:<br />
        /// /api/v4/spot/price_orders/{id}
        /// </para>
        /// </summary>
        /// <param name="id">Id of the trigger order</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoTriggerOrder>> GetTriggerOrderAsync(long id, CancellationToken ct = default);

        /// <summary>
        /// Cancel price trigger order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#cancel-single-auto-order" /><br />
        /// Endpoint:<br />
        /// /api/v4/spot/price_orders/{id}
        /// </para>
        /// </summary>
        /// <param name="id">Id of the trigger order</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoTriggerOrder>> CancelTriggerOrderAsync(long id, CancellationToken ct = default);

        /// <summary>
        /// Place multiple orders in a single call
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#batch-place-orders" /><br />
        /// Endpoint:<br />
        /// /api/v4/spot/batch_orders
        /// </para>
        /// </summary>
        /// <param name="orders">Orders to place</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoOrderOperation[]>> PlaceMultipleOrderAsync(
            IEnumerable<GateIoBatchPlaceRequest> orders,
            CancellationToken ct = default);

        /// <summary>
        /// Edit multiple orders in a single call
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#batch-modification-of-orders" /><br />
        /// Endpoint:<br />
        /// /api/v4/spot/amend_batch_orders
        /// </para>
        /// </summary>
        /// <param name="orders">Orders to edit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoOrderOperation[]>> EditMultipleOrderAsync(
            IEnumerable<GateIoBatchEditRequest> orders,
            CancellationToken ct = default);
    }
}
