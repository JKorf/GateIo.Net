using CryptoExchange.Net.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects.Sockets;
using GateIo.Net.Objects.Models;
using GateIo.Net.Enums;
using System.Collections.Generic;

namespace GateIo.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Gate.io spot streams
    /// </summary>
    public interface IGateIoSocketClientSpotApi : ISocketApiClient, IDisposable
    {
        /// <summary>
        /// Get the shared socket subscription client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        public IGateIoSocketClientSpotApiShared SharedClient { get; }

        /// <summary>
        /// Subscribe to public trade updates
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#public-trades-channel" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH_USDT`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<GateIoTradeUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to public trade updates
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#public-trades-channel" /></para>
        /// </summary>
        /// <param name="symbols">Symbols, for example `ETH_USDT`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<GateIoTradeUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to ticker updates
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#tickers-channel" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH_USDT`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<GateIoTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to ticker updates
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#tickers-channel" /></para>
        /// </summary>
        /// <param name="symbols">Symbols, for example `ETH_USDT`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<GateIoTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline/candlestick updates
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#candlesticks-channel" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH_USDT`</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<GateIoKlineUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to book ticker updates
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#best-bid-or-ask-price" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH_USDT`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<GateIoBookTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to book ticker updates
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#best-bid-or-ask-price" /></para>
        /// </summary>
        /// <param name="symbols">Symbols, for example `ETH_USDT`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<GateIoBookTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order book change events. Only the changed entries will be pushed
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#changed-order-book-levels" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH_USDT`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<GateIoOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order book updates
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/ws/en/#order-book-v2-update-notification" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH_USDT`</param>
        /// <param name="depth">Book depth. 50 or 400. Depth 400 has an update frequency of 100ms while 50 has an update frequency of 20ms</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookV2UpdatesAsync(string symbol, int depth, Action<DataEvent<GateIoPerpOrderBookV2Update>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to partial full order book updates, Full order book will be pushed for a limited depth
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#limited-level-full-order-book-snapshot" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH_USDT`</param>
        /// <param name="depth">Depth of the book. 5, 10, 20, 50 or 100</param>
        /// <param name="updateMs">Update speed in milliseconds. 100 or 1000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int depth, int? updateMs, Action<DataEvent<GateIoPartialOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order updates
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#orders-channel" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<DataEvent<GateIoOrderUpdate[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user trade updates
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#user-trades-channel" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(Action<DataEvent<GateIoUserTradeUpdate[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to balance updates
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#spot-balance-channel" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(Action<DataEvent<GateIoBalanceUpdate[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to margin balance updates
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#margin-balance-channel" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarginBalanceUpdatesAsync(Action<DataEvent<GateIoMarginBalanceUpdate[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to funding balance updates
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#funding-balance-channel" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToFundingBalanceUpdatesAsync(Action<DataEvent<GateIoFundingBalanceUpdate[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to cross margin balance updates
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#cross-margin-balance-channel" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToCrossMarginBalanceUpdatesAsync(Action<DataEvent<GateIoCrossMarginBalanceUpdate[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to trigger order updates
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#priceorders-channel" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTriggerOrderUpdatesAsync(Action<DataEvent<GateIoTriggerOrderUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Place a new order
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#order-place" /></para>
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
        Task<CallResult<GateIoOrder>> PlaceOrderAsync(string symbol,
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
        /// Place multiple orders
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#order-place" /></para>
        /// </summary>
        /// <param name="orders">Orders</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<GateIoOrder[]>> PlaceMultipleOrdersAsync(IEnumerable<GateIoBatchPlaceRequest> orders, CancellationToken ct = default);

        /// <summary>
        /// Cancel an order by id
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#order-cancel" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH_USDT`</param>
        /// <param name="orderId">Order id, either `orderId` or `clientOrderId` required</param>
        /// <param name="clientOrderId">user custom ID (i.e., t-123c456f), either `orderId` or `clientOrderId` required</param>
        /// <param name="accountType">Account type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<GateIoOrder>> CancelOrderAsync(string symbol,
            long? orderId,
            string? clientOrderId = null,
            SpotAccountType? accountType = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel multiple orders
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#order-cancel-all-with-id-list" /></para>
        /// </summary>
        /// <param name="cancelRequests">Cancel requests</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<GateIoCancelResult[]>> CancelOrdersAsync(IEnumerable<GateIoBatchCancelRequest> cancelRequests, CancellationToken ct = default);

        /// <summary>
        /// Cancel all orders on a symbol
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#order-cancel-all-with-id-list" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH_USDT`</param>
        /// <param name="side">Filter by side</param>
        /// <param name="accountType">Account type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<GateIoOrder[]>> CancelAllOrdersAsync(string symbol, OrderSide? side = null, SpotAccountType? accountType = null, CancellationToken ct = default);

        /// <summary>
        /// Edit an order
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#order-amend" /></para>
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
        Task<CallResult<GateIoOrder>> EditOrderAsync(string symbol,
            long? orderId = null,
            string? clientOrderId = null,
            decimal? price = null,
            decimal? quantity = null,
            string? amendText = null,
            SpotAccountType? accountType = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get an order by id
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#order-status" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH_USDT`</param>
        /// <param name="orderId">Order id</param>
        /// <param name="accountType">Account type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<GateIoOrder>> GetOrderAsync(string symbol, long orderId, SpotAccountType? accountType = null, CancellationToken ct = default);

        /// <summary>
        /// Get orders list
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#list-orders" /></para>
        /// </summary>
        /// <param name="symbol">Symbol name, for example `ETH_USDT`</param>
        /// <param name="open">True for open orders, false for closed orders</param>
        /// <param name="accountType">Filter by account type</param>
        /// <param name="side">Filter by side</param>
        /// <param name="fromId">Returns orders with id larger than this</param>
        /// <param name="toId">Returns orders with id smaller than this</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<GateIoOrder[]>> GetOrdersAsync(string symbol, bool open, SpotAccountType? accountType = null, OrderSide? side = null, long? fromId = null, long? toId = null, int? page = null, int? pageSize = null, CancellationToken ct = default);
    }
}
