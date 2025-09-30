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
        /// Get the shared socket subscription client. This interface is shared with other exhanges to allow for a common implementation for different exchanges.
        /// </summary>
        IGateIoSocketClientPerpetualFuturesApiShared SharedClient { get; }

        /// <summary>
        /// Subscribe to public trade updates
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#trades-api" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string settlementAsset, string contract, Action<DataEvent<GateIoPerpTradeUpdate[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to public trade updates
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#trades-api" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contracts">Contracts to subscribe, for example `ETH_USDT`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string settlementAsset, IEnumerable<string> contracts, Action<DataEvent<GateIoPerpTradeUpdate[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to ticker updates
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#tickers-api" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string settlementAsset, string contract, Action<DataEvent<GateIoPerpTickerUpdate[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to ticker updates
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#tickers-api" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contracts">Contracts to subscribe, for example `ETH_USDT`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string settlementAsset, IEnumerable<string> contracts, Action<DataEvent<GateIoPerpTickerUpdate[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to best book price updates
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#best-ask-bid-subscription" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string settlementAsset, string contract, Action<DataEvent<GateIoPerpBookTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to best book price updates for multiple contracts
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#best-ask-bid-subscription" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contracts">Contracts to subscribe, for example `ETH_USDT`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string settlementAsset, IEnumerable<string> contracts, Action<DataEvent<GateIoPerpBookTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order book updates
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#order-book-v2-api" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="depth">Book depth. 50 or 400. Depth 400 has an update frequency of 100ms while 50 has an update frequency of 20ms</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookV2UpdatesAsync(string settlementAsset, string contract, int depth, Action<DataEvent<GateIoPerpOrderBookV2Update>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order book updates
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#order-book-update-subscription" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
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
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string settlementAsset, string contract, KlineInterval interval, Action<DataEvent<GateIoPerpKlineUpdate[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to contract stats updates
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#contract-stats-subscription" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="interval">Update interval</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToContractStatsUpdatesAsync(string settlementAsset, string contract, KlineInterval interval, Action<DataEvent<GateIoPerpContractStats>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user order updates
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#orders-api" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="userId">User id. Can be obtained via <see cref="IGateIoRestClientPerpetualFuturesApiAccount.GetAccountAsync(string, CancellationToken)">restClient.PerpetualFuturesApi.Account.GetAccountAsync</see>.</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<GateIoPerpOrder[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user trade updates
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#user-trades-notification" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="userId">User id. Can be obtained via <see cref="IGateIoRestClientPerpetualFuturesApiAccount.GetAccountAsync(string, CancellationToken)">restClient.PerpetualFuturesApi.Account.GetAccountAsync</see>.</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<GateIoPerpUserTrade[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user liquidation updates
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#user-trades-notification" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="userId">User id. Can be obtained via <see cref="IGateIoRestClientPerpetualFuturesApiAccount.GetAccountAsync(string, CancellationToken)">restClient.PerpetualFuturesApi.Account.GetAccountAsync</see>.</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserLiquidationUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<GateIoPerpLiquidation[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user auto deleverage updates
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#user-trades-notification" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="userId">User id. Can be obtained via <see cref="IGateIoRestClientPerpetualFuturesApiAccount.GetAccountAsync(string, CancellationToken)">restClient.PerpetualFuturesApi.Account.GetAccountAsync</see>.</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserAutoDeleverageUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<GateIoPerpAutoDeleverage[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user position close updates
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#position-closes-api" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="userId">User id. Can be obtained via <see cref="IGateIoRestClientPerpetualFuturesApiAccount.GetAccountAsync(string, CancellationToken)">restClient.PerpetualFuturesApi.Account.GetAccountAsync</see>.</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPositionCloseUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<GateIoPerpPositionCloseUpdate[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to balance updates
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#balances-api" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="userId">User id. Can be obtained via <see cref="IGateIoRestClientPerpetualFuturesApiAccount.GetAccountAsync(string, CancellationToken)">restClient.PerpetualFuturesApi.Account.GetAccountAsync</see>.</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<GateIoPerpBalanceUpdate[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user reduce risk limit updates
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#reduce-risk-limits-api" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="userId">User id. Can be obtained via <see cref="IGateIoRestClientPerpetualFuturesApiAccount.GetAccountAsync(string, CancellationToken)">restClient.PerpetualFuturesApi.Account.GetAccountAsync</see>.</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToReduceRiskLimitUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<GateIoPerpRiskLimitUpdate[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to position updates
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#positions-subscription" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="userId">User id. Can be obtained via <see cref="IGateIoRestClientPerpetualFuturesApiAccount.GetAccountAsync(string, CancellationToken)">restClient.PerpetualFuturesApi.Account.GetAccountAsync</see>.</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<GateIoPositionUpdate[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to trigger order updates
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#auto-orders-api" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="userId">User id. Can be obtained via <see cref="IGateIoRestClientPerpetualFuturesApiAccount.GetAccountAsync(string, CancellationToken)">restClient.PerpetualFuturesApi.Account.GetAccountAsync</see>.</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTriggerOrderUpdatesAsync(long userId, string settlementAsset, Action<DataEvent<GateIoPerpTriggerOrderUpdate[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user ADL updates
        /// <para><a href="https://www.gate.com/docs/developers/futures/ws/en/#positions-adl-subscription" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAdlUpdatesAsync(string settlementAsset, Action<DataEvent<GateIoAdlUpdate[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Place a new order
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#order-place" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="orderSide">Order side</param>
        /// <param name="quantity">Order quantity in number of contracts. Use the `Multiplier` property of the ExchangeData.GetContractsAsync endpoint to see how much currency 1 size contract represents</param>
        /// <param name="price">Limit price</param>
        /// <param name="closePosition">Close position flag, set as true to close the position, with quantity set to 0</param>
        /// <param name="reduceOnly">Set as true to be reduce-only order</param>
        /// <param name="timeInForce">Time in force</param>
        /// <param name="icebergQuantity">Iceberg quantity</param>
        /// <param name="closeSide">Set side to close dual-mode position</param>
        /// <param name="stpMode">Self-Trading Prevention action</param>
        /// <param name="text">User defined text</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<GateIoPerpOrder>> PlaceOrderAsync(
            string settlementAsset,
            string contract,
            OrderSide orderSide,
            int quantity,
            decimal? price = null,
            bool? closePosition = null,
            bool? reduceOnly = null,
            TimeInForce? timeInForce = null,
            int? icebergQuantity = null,
            CloseSide? closeSide = null,
            SelfTradePreventionMode? stpMode = null,
            string? text = null,
            CancellationToken ct = default);

        /// <summary>
        /// Place multiple orders
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="orders">Orders</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<GateIoPerpOrder[]>> PlaceMultipleOrderAsync(
            string settlementAsset,
            IEnumerable<GateIoPerpBatchPlaceRequest> orders,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel an order by id
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#order-cancel" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="orderId">Order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<GateIoPerpOrder>> CancelOrderAsync(string settlementAsset, long orderId, CancellationToken ct = default);

        /// <summary>
        /// Get order info by id
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#order-status" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="orderId">Order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<GateIoPerpOrder>> GetOrderAsync(string settlementAsset, long orderId, CancellationToken ct = default);

        /// <summary>
        /// Get orders
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#order-list" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="open">True for open orders, false for closed orders</param>
        /// <param name="contract">Filter by contract, for example `ETH_USDT`</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="offset">Offset</param>
        /// <param name="lastId">Last id to retrieve from</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<GateIoPerpOrder[]>> GetOrdersAsync(
            string settlementAsset,
            bool open,
            string? contract = null,
            int? limit = null,
            int? offset = null,
            string? lastId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel orders
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#cancel-all-open-orders-matched" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="side">Order side</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<GateIoPerpOrder[]>> CancelOrdersAsync(
            string settlementAsset,
            string contract,
            OrderSide? side = null,
            CancellationToken ct = default);

        /// <summary>
        /// Edit an order
        /// <para><a href="https://www.gate.io/docs/developers/futures/ws/en/#order-amend" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="orderId">Order id</param>
        /// <param name="price">New price</param>
        /// <param name="quantity">New quantity</param>
        /// <param name="amendText">Amend text</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<GateIoOrder>> EditOrderAsync(string settlementAsset,
            long orderId,
            decimal? price = null,
            int? quantity = null,
            string? amendText = null,
            CancellationToken ct = default);
    }
}
