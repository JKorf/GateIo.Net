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
        /// Subscribe to public trade updates
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#public-trades-channel" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<GateIoTradeUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to public trade updates
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#public-trades-channel" /></para>
        /// </summary>
        /// <param name="symbols">Symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<GateIoTradeUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to ticker updates
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#tickers-channel" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<GateIoTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to ticker updates
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#tickers-channel" /></para>
        /// </summary>
        /// <param name="symbols">Symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<GateIoTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline/candlestick updates
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#candlesticks-channel" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<GateIoKlineUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to book ticker updates
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#best-bid-or-ask-price" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<GateIoBookTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to book ticker updates
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#best-bid-or-ask-price" /></para>
        /// </summary>
        /// <param name="symbols">Symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<GateIoBookTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order book change events. Only the changed entries will be pushed
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#changed-order-book-levels" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="updateMs">Update speed in milliseconds. 100 or 1000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int? updateMs, Action<DataEvent<GateIoOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to partial full order book updates, Full order book will be pushed for a limited depth
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#limited-level-full-order-book-snapshot" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
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
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<DataEvent<IEnumerable<GateIoOrderUpdate>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user trade updates
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#user-trades-channel" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(Action<DataEvent<IEnumerable<GateIoUserTradeUpdate>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to balance updates
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#spot-balance-channel" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(Action<DataEvent<IEnumerable<GateIoBalanceUpdate>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to margin balance updates
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#margin-balance-channel" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarginBalanceUpdatesAsync(Action<DataEvent<IEnumerable<GateIoMarginBalanceUpdate>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to funding balance updates
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#funding-balance-channel" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToFundingBalanceUpdatesAsync(Action<DataEvent<IEnumerable<GateIoFundingBalanceUpdate>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to cross margin balance updates
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#cross-margin-balance-channel" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToCrossMarginBalanceUpdatesAsync(Action<DataEvent<IEnumerable<GateIoCrossMarginBalanceUpdate>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to trigger order updates
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/ws/en/#priceorders-channel" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTriggerOrderUpdatesAsync(Action<DataEvent<GateIoTriggerOrderUpdate>> onMessage, CancellationToken ct = default);
    }
}
