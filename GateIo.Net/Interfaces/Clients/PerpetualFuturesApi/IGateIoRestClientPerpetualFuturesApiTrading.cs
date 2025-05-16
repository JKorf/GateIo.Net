using CryptoExchange.Net.Objects;
using GateIo.Net.Objects.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GateIo.Net.Enums;
using System;

namespace GateIo.Net.Interfaces.Clients.PerpetualFuturesApi
{
    /// <summary>
    /// GateIo futures trading endpoints, placing and mananging orders.
    /// </summary>
    public interface IGateIoRestClientPerpetualFuturesApiTrading
    {
        /// <summary>
        /// Get positions
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#list-all-positions-of-a-user" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="holding">True to return only active positions, false to return all</param>
        /// <param name="page">Page number</param>
        /// <param name="limit">Max amount of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPosition[]>> GetPositionsAsync(string settlementAsset, bool? holding = null, int? page = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get single position
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#get-single-position" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPosition>> GetPositionAsync(string settlementAsset, string contract, CancellationToken ct = default);

        /// <summary>
        /// Update position margin
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#update-position-margin" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="change">Change margin</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPosition>> UpdatePositionMarginAsync(string settlementAsset, string contract, decimal change, CancellationToken ct = default);

        /// <summary>
        /// Update position leverage
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#update-position-leverage" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="leverage">New leverage</param>
        /// <param name="crossLeverageLimit">Cross margin leverage</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPosition>> UpdatePositionLeverageAsync(string settlementAsset, string contract, decimal leverage, decimal? crossLeverageLimit = null, CancellationToken ct = default);

        /// <summary>
        /// Update position risk limit
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#update-position-risk-limit" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="riskLimit">Risk limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPosition>> UpdatePositionRiskLimitAsync(string settlementAsset, string contract, decimal riskLimit, CancellationToken ct = default);

        /// <summary>
        /// Get positions in dual mode
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#retrieve-position-detail-in-dual-mode" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPosition[]>> GetDualModePositionsAsync(string settlementAsset, string contract, CancellationToken ct = default);

        /// <summary>
        /// Update position margin for dual position mode
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#retrieve-position-detail-in-dual-mode" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="change">Change</param>
        /// <param name="mode">Side</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPosition[]>> UpdateDualModePositionMarginAsync(string settlementAsset, string contract, decimal change, PositionMode mode, CancellationToken ct = default);

        /// <summary>
        /// Update position leverage in dual position mode
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#update-position-leverage-in-dual-mode" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="leverage">Leverage</param>
        /// <param name="crossLeverageLimit">Cross margin leverage</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPosition[]>> UpdateDualModePositionLeverageAsync(string settlementAsset, string contract, decimal leverage, decimal? crossLeverageLimit = null, CancellationToken ct = default);

        /// <summary>
        /// Update position risk limit in dual position mode
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#update-position-risk-limit-in-dual-mode" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="riskLimit">Risk limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPosition[]>> UpdateDualModePositionRiskLimitAsync(string settlementAsset, string contract, int riskLimit, CancellationToken ct = default);

        /// <summary>
        /// Place a new order
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#create-a-futures-order" /></para>
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
        Task<WebCallResult<GateIoPerpOrder>> PlaceOrderAsync(
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
        /// Place multiple new orders
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#create-a-futures-order" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="orders">Order info</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpOrder[]>> PlaceMultipleOrderAsync(
            string settlementAsset,
            IEnumerable<GateIoPerpBatchPlaceRequest> orders,
            CancellationToken ct = default);

        /// <summary>
        /// Get orders
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#list-futures-orders" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Filter by contract, for example `ETH_USDT`</param>
        /// <param name="status">Filter by status</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="offset">Offset</param>
        /// <param name="lastId">Filter by last order id of previous result</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpOrder[]>> GetOrdersAsync(
            string settlementAsset,
            OrderStatus status,
            string? contract = null,
            int? limit = null,
            int? offset = null,
            string? lastId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel orders after a certain period. Can be called at interval to act as a deadmans switch. Using TimeSpan.Zero cancels the countdown
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#countdown-cancel-orders-2" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="cancelAfter">Timespan after which to cancel, TimeSpan.Zero to cancel the countdown</param>
        /// <param name="contract">Filter by contract, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoCancelAfter>> CancelOrdersAfterAsync(string settlementAsset, TimeSpan cancelAfter, string? contract = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel orders by id
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#cancel-a-batch-of-orders-with-an-id-list-2" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="orderIds">Ids of orders to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoFuturesCancelResult[]>> CancelOrdersAsync(string settlementAsset, IEnumerable<long> orderIds, CancellationToken ct = default);

        /// <summary>
        /// Get orders with timestamp filtering
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#list-futures-orders-by-time-range" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Filter by contract, for example `ETH_USDT`</param>
        /// <param name="status">Filter by status</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="offset">Offset</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpOrder[]>> GetOrdersByTimestampAsync(
            string settlementAsset,
            string? contract = null,
            OrderStatus? status = null,
            int? limit = null,
            int? offset = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel all open orders
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#list-futures-orders" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Filter by contract, for example `ETH_USDT`</param>
        /// <param name="side">Filter by order side</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpOrder[]>> CancelAllOrdersAsync(string settlementAsset, string contract, OrderSide? side = null, CancellationToken ct = default);

        /// <summary>
        /// Get order by id
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#get-a-single-order-2" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="orderId">Order id, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">Client order id, either this or orderId should be provided</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpOrder>> GetOrderAsync(
            string settlementAsset,
            long? orderId = null,
            string? clientOrderId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel an order
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#cancel-a-single-order-2" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="orderId">Order id, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">Client order id, either this or orderId should be provided</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpOrder>> CancelOrderAsync(
            string settlementAsset,
            long? orderId = null,
            string? clientOrderId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Edit an existing order
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#amend-an-order-2" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="orderId">Order id, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">Client order id, either this or orderId should be provided</param>
        /// <param name="quantity">New quantity</param>
        /// <param name="price">New price</param>
        /// <param name="amendText">Amend text</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpOrder>> EditOrderAsync(
            string settlementAsset,
            long? orderId = null,
            string? clientOrderId = null,
            int? quantity = null,
            decimal? price = null,
            string? amendText = null,
            CancellationToken ct = default);

        /// <summary>
        /// Edit multiple existing orders
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#batch-modify-orders-with-specified-ids" /></para>
        /// </summary>
        /// <param name="settlementAsset">Settlement asset</param>
        /// <param name="requests">Edit requests</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<GateIoPerpOrder[]>> EditMultipleOrdersAsync(string settlementAsset, IEnumerable<GateIoPerpBatchEditRequest> requests, CancellationToken ct = default);

        /// <summary>
        /// Get user trades
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#list-personal-trading-history-2" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Filter by contract, for example `ETH_USDT`</param>
        /// <param name="orderId">Filter by order id</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="offset">Offset</param>
        /// <param name="lastId">Last id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpUserTrade[]>> GetUserTradesAsync(
            string settlementAsset,
            string? contract = null,
            long? orderId = null,
            int? limit = null,
            int? offset = null,
            long? lastId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get user trades by timestamp filter
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#list-personal-trading-history-by-time-range" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Filter by contract, for example `ETH_USDT`</param>
        /// <param name="orderId">Filter by order id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="offset">Offset</param>
        /// <param name="role">Filter by role</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpUserTrade[]>> GetUserTradesByTimestampAsync(
            string settlementAsset,
            string? contract = null,
            long? orderId = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            int? offset = null,
            Role? role = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get position closing history
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#list-position-close-history" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Filter by contract, for example `ETH_USDT`</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="offset">Offset</param>
        /// <param name="role">Filter by role</param>
        /// <param name="side">Filter by side</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpPositionClose[]>> GetPositionCloseHistoryAsync(
            string settlementAsset,
            string? contract = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            int? offset = null,
            Role? role = null,
            PositionSide? side = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get user liquidation history
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#list-liquidation-history" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Filter by contract, for example `ETH_USDT`</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpLiquidation[]>> GetLiquidationHistoryAsync(
            string settlementAsset,
            string? contract = null,
            int? limit = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get user auto deleveraging history
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#list-auto-deleveraging-history" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Filter by contract, for example `ETH_USDT`</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpAutoDeleverage[]>> GetAutoDeleveragingHistoryAsync(
            string settlementAsset,
            string? contract = null,
            int? limit = null,
            CancellationToken ct = default);

        /// <summary>
        /// Place a new trigger order
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#create-a-price-triggered-order-2" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="orderSide">Order side</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="triggerType">Trigger type</param>
        /// <param name="triggerPrice">Trigger price</param>
        /// <param name="orderPrice">Order price</param>
        /// <param name="closePosition">Set to true if trying to close the position</param>
        /// <param name="reduceOnly">Set to true to create a reduce-only order</param>
        /// <param name="closeSide">Set side to close dual-mode position</param>
        /// <param name="priceType">Price type</param>
        /// <param name="triggerOrderType">Trigger order type</param>
        /// <param name="timeInForce">Time in force</param>
        /// <param name="text">User text</param>
        /// <param name="expiration">Trigger expiration time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoId>> PlaceTriggerOrderAsync(
            string settlementAsset,
            string contract,
            OrderSide orderSide,
            int quantity,
            TriggerType triggerType,
            decimal triggerPrice,
            decimal? orderPrice = null,
            bool? closePosition = null,
            bool? reduceOnly = null,
            CloseSide? closeSide = null,
            PriceType? priceType = null,
            TriggerOrderType? triggerOrderType = null,
            TimeInForce? timeInForce = null,
            string? text = null,
            TimeSpan? expiration = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get trigger orders
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#list-all-auto-orders" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="open">True for open orders, false for closed</param>
        /// <param name="contract">Filter by contract, for example `ETH_USDT`</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="offset">Offset</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpTriggerOrder[]>> GetTriggerOrdersAsync(
            string settlementAsset,
            bool open,
            string? contract = null,
            int? limit = null,
            int? offset = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel all trigger orders on a contract
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#cancel-all-open-orders-2" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpTriggerOrder[]>> CancelTriggerOrdersAsync(
            string settlementAsset,
            string contract,
            CancellationToken ct = default);

        /// <summary>
        /// Get a trigger order by id
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#get-a-price-triggered-order-2" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="orderId">Order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpTriggerOrder>> GetTriggerOrderAsync(
            string settlementAsset,
            long orderId,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel a trigger order
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#cancel-a-price-triggered-order-2" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="orderId">Order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpTriggerOrder>> CancelTriggerOrderAsync(
            string settlementAsset,
            long orderId,
            CancellationToken ct = default);
    }
}
