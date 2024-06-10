using CryptoExchange.Net.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects.Sockets;
using GateIo.Net.Objects.Models;
using System.Collections.Generic;
using GateIo.Net.Enums;

namespace GateIo.Net.Interfaces.Clients.PerpetualFuturesApi
{
    /// <summary>
    /// GateIo futures streams
    /// </summary>
    public interface IGateIoSocketClientPerpetualFuturesApi : ISocketApiClient, IDisposable
    {
        /// <summary>
        /// Subscribe to public trade updates
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#trades-api" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string settlementAsset, string contract, Action<DataEvent<IEnumerable<GateIoPerpTradeUpdate>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to ticker updates
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#tickers-api" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string settlementAsset, string contract, Action<DataEvent<IEnumerable<GateIoPerpTickerUpdate>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to best book price updates
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#best-ask-bid-subscription" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string settlementAsset, string contract, Action<DataEvent<GateIoPerpBookTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order book updates
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#order-book-update-subscription" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract</param>
        /// <param name="updateMs">Update interval in ms. 20, 100 or 1000</param>
        /// <param name="depth">Book depth. 5, 10, 20, 50 or 100. For the 20ms update interval only 20 depth is supported</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string settlementAsset, string contract, int updateMs, int depth, Action<DataEvent<GateIoPerpOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline updates
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#order-book-update-subscription" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string settlementAsset, string contract, KlineInterval interval, Action<DataEvent<IEnumerable<GateIoPerpKlineUpdate>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user order updates
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#orders-api" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="userId">User id</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<IEnumerable<GateIoPerpOrder>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user trade updates
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#user-trades-notification" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="userId">User id</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<IEnumerable<GateIoPerpUserTrade>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user liquidation updates
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#user-trades-notification" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="userId">User id</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserLiquidationUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<IEnumerable<GateIoPerpLiquidation>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user auto deleverage updates
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#user-trades-notification" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="userId">User id</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserAutoDeleverageUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<IEnumerable<GateIoPerpAutoDeleverage>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user position close updates
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#position-closes-api" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="userId">User id</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPositionCloseUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<IEnumerable<GateIoPerpPositionCloseUpdate>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to balance updates
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#balances-api" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="userId">User id</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<IEnumerable<GateIoPerpBalanceUpdate>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user reduce risk limit updates
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#reduce-risk-limits-api" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="userId">User id</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToReduceRiskLimitUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<IEnumerable<GateIoPerpRiskLimitUpdate>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to position updates
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#positions-notification" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="userId">User id</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<IEnumerable<GateIoPositionUpdate>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to trigger order updates
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#auto-orders-api" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="userId">User id</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTriggerOrderUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<IEnumerable<GateIoPerpTriggerOrderUpdate>>> onMessage, CancellationToken ct = default);
    }
}
