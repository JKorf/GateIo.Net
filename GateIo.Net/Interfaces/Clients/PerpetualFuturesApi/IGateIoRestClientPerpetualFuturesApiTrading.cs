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
    /// Gate futures trading endpoints, placing and managing orders.
    /// </summary>
    public interface IGateIoRestClientPerpetualFuturesApiTrading
    {
        /// <summary>
        /// Get positions
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#get-user-position-list" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="holding">["<c>holding</c>"] True to return only active positions, false to return all</param>
        /// <param name="offset">["<c>offset</c>"] Result offset</param>
        /// <param name="limit">["<c>limit</c>"] Max amount of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPosition[]>> GetPositionsAsync(string settlementAsset, bool? holding = null, int? offset = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get single position
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#get-single-position-information" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPosition>> GetPositionAsync(string settlementAsset, string contract, CancellationToken ct = default);

        /// <summary>
        /// Update position margin
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#update-position-margin" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="change">["<c>change</c>"] Change margin</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPosition>> UpdatePositionMarginAsync(string settlementAsset, string contract, decimal change, CancellationToken ct = default);

        /// <summary>
        /// Update position leverage
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#update-position-leverage" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="leverage">["<c>leverage</c>"] New leverage</param>
        /// <param name="crossLeverageLimit">["<c>cross_leverage_limit</c>"] Cross margin leverage</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPosition>> UpdatePositionLeverageAsync(string settlementAsset, string contract, decimal leverage, decimal? crossLeverageLimit = null, CancellationToken ct = default);

        /// <summary>
        /// Update position risk limit
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#update-position-risk-limit" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="riskLimit">["<c>risk_limit</c>"] Risk limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPosition>> UpdatePositionRiskLimitAsync(string settlementAsset, string contract, decimal riskLimit, CancellationToken ct = default);

        /// <summary>
        /// Get positions in dual mode
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#get-position-information-in-dual-mode" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPosition[]>> GetDualModePositionsAsync(string settlementAsset, string contract, CancellationToken ct = default);

        /// <summary>
        /// Update position margin for dual position mode
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#update-position-margin-in-dual-mode" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="change">["<c>change</c>"] Change</param>
        /// <param name="mode">["<c>dual_side</c>"] Side</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPosition[]>> UpdateDualModePositionMarginAsync(string settlementAsset, string contract, decimal change, PositionMode mode, CancellationToken ct = default);

        /// <summary>
        /// Update position leverage in dual position mode
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#update-position-leverage-in-dual-mode" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="leverage">["<c>leverage</c>"] Leverage</param>
        /// <param name="crossLeverageLimit">["<c>cross_leverage_limit</c>"] Cross margin leverage</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPosition[]>> UpdateDualModePositionLeverageAsync(string settlementAsset, string contract, decimal leverage, decimal? crossLeverageLimit = null, CancellationToken ct = default);

        /// <summary>
        /// Update position risk limit in dual position mode
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#update-position-risk-limit-in-dual-mode" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="riskLimit">["<c>risk_limit</c>"] Risk limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPosition[]>> UpdateDualModePositionRiskLimitAsync(string settlementAsset, string contract, int riskLimit, CancellationToken ct = default);

        /// <summary>
        /// Place a new order
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#place-futures-order" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">["<c>contract</c>"] Contract, for example `ETH_USDT`</param>
        /// <param name="orderSide">["<c>size</c>"] Order side</param>
        /// <param name="quantity">["<c>size</c>"] Order quantity in number of contracts. Use the `Multiplier` property of the ExchangeData.GetContractsAsync endpoint to see how much currency 1 size contract represents</param>
        /// <param name="price">["<c>price</c>"] Limit price</param>
        /// <param name="closePosition">["<c>close</c>"] Close position flag, set as true to close the position, with quantity set to 0</param>
        /// <param name="reduceOnly">["<c>reduce_only</c>"] Set as true to be reduce-only order</param>
        /// <param name="timeInForce">["<c>tif</c>"] Time in force</param>
        /// <param name="icebergQuantity">["<c>iceberg</c>"] Iceberg quantity</param>
        /// <param name="closeSide">["<c>auto_size</c>"] Set side to close dual-mode position</param>
        /// <param name="stpMode">["<c>stp_act</c>"] Self-Trading Prevention action</param>
        /// <param name="text">["<c>text</c>"] User defined text</param>
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
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#place-batch-futures-orders" /></para>
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
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#query-futures-order-list-2" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">["<c>contract</c>"] Filter by contract, for example `ETH_USDT`</param>
        /// <param name="status">["<c>status</c>"] Filter by status</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="offset">["<c>offset</c>"] Offset</param>
        /// <param name="lastId">["<c>last_id</c>"] Filter by last order id of previous result</param>
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
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#countdown-cancel-orders-3" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="cancelAfter">["<c>timeout</c>"] Timespan after which to cancel, TimeSpan.Zero to cancel the countdown</param>
        /// <param name="contract">["<c>contract</c>"] Filter by contract, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoCancelAfter>> CancelOrdersAfterAsync(string settlementAsset, TimeSpan cancelAfter, string? contract = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel orders by id
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#cancel-batch-orders-by-specified-id-list-2" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="orderIds">Ids of orders to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoFuturesCancelResult[]>> CancelOrdersAsync(string settlementAsset, IEnumerable<long> orderIds, CancellationToken ct = default);

        /// <summary>
        /// Get orders with timestamp filtering
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#query-futures-order-list-by-time-range" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">["<c>contract</c>"] Filter by contract, for example `ETH_USDT`</param>
        /// <param name="status">["<c>status</c>"] Filter by status</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="offset">["<c>offset</c>"] Offset</param>
        /// <param name="startTime">["<c>from</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>to</c>"] Filter by end time</param>
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
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#cancel-all-orders-with-open-status-2" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">["<c>contract</c>"] Filter by contract, for example `ETH_USDT`</param>
        /// <param name="side">["<c>side</c>"] Filter by order side</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpOrder[]>> CancelAllOrdersAsync(string settlementAsset, string contract, OrderSide? side = null, CancellationToken ct = default);

        /// <summary>
        /// Get order by id
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#query-single-order-details-3" /></para>
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
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#cancel-single-order-3" /></para>
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
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#amend-single-order" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="orderId">Order id, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">Client order id, either this or orderId should be provided</param>
        /// <param name="quantity">["<c>size</c>"] New quantity</param>
        /// <param name="price">["<c>price</c>"] New price</param>
        /// <param name="amendText">["<c>amend_text</c>"] Amend text</param>
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
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#batch-modify-orders-by-specified-ids" /></para>
        /// </summary>
        /// <param name="settlementAsset">Settlement asset</param>
        /// <param name="requests">Edit requests</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<GateIoPerpOrder[]>> EditMultipleOrdersAsync(string settlementAsset, IEnumerable<GateIoPerpBatchEditRequest> requests, CancellationToken ct = default);

        /// <summary>
        /// Get user trades
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#query-personal-trading-records-2" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">["<c>contract</c>"] Filter by contract, for example `ETH_USDT`</param>
        /// <param name="orderId">["<c>order</c>"] Filter by order id</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="offset">["<c>offset</c>"] Offset</param>
        /// <param name="lastId">["<c>last_id</c>"] Last id</param>
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
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#query-personal-trading-records-by-time-range" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">["<c>contract</c>"] Filter by contract, for example `ETH_USDT`</param>
        /// <param name="orderId">["<c>order</c>"] Filter by order id</param>
        /// <param name="startTime">["<c>from</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>to</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="offset">["<c>offset</c>"] Offset</param>
        /// <param name="role">["<c>role</c>"] Filter by role</param>
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
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#query-position-close-history" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">["<c>contract</c>"] Filter by contract, for example `ETH_USDT`</param>
        /// <param name="startTime">["<c>from</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>to</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="offset">["<c>offset</c>"] Offset</param>
        /// <param name="role">["<c>role</c>"] Filter by role</param>
        /// <param name="side">["<c>side</c>"] Filter by side</param>
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
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#query-liquidation-history" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">["<c>contract</c>"] Filter by contract, for example `ETH_USDT`</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpLiquidation[]>> GetLiquidationHistoryAsync(
            string settlementAsset,
            string? contract = null,
            int? limit = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get user auto deleveraging history
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#query-adl-auto-deleveraging-order-information" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">["<c>contract</c>"] Filter by contract, for example `ETH_USDT`</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpAutoDeleverage[]>> GetAutoDeleveragingHistoryAsync(
            string settlementAsset,
            string? contract = null,
            int? limit = null,
            CancellationToken ct = default);

        /// <summary>
        /// Place a new trigger order
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#create-price-triggered-order-2" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">["<c>initial.contract</c>"] Contract, for example `ETH_USDT`</param>
        /// <param name="orderSide">["<c>initial.size</c>"] Order side</param>
        /// <param name="quantity">["<c>initial.size</c>"] Quantity</param>
        /// <param name="triggerType">["<c>trigger.rule</c>"] Trigger type</param>
        /// <param name="triggerPrice">["<c>trigger.price</c>"] Trigger price</param>
        /// <param name="orderPrice">["<c>initial.price</c>"] Order price</param>
        /// <param name="closePosition">["<c>initial.close</c>"] Set to true if trying to close the position</param>
        /// <param name="reduceOnly">["<c>initial.reduce_only</c>"] Set to true to create a reduce-only order</param>
        /// <param name="closeSide">["<c>initial.auto_size</c>"] Set side to close dual-mode position</param>
        /// <param name="priceType">["<c>trigger.price_type</c>"] Price type</param>
        /// <param name="triggerOrderType">["<c>order_type</c>"] Trigger order type</param>
        /// <param name="timeInForce">["<c>initial.tif</c>"] Time in force</param>
        /// <param name="text">["<c>initial.text</c>"] User text</param>
        /// <param name="expiration">["<c>trigger.expiration</c>"] Trigger expiration time</param>
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
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#query-auto-order-list" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="open">["<c>status</c>"] True for open orders, false for closed</param>
        /// <param name="contract">["<c>contract</c>"] Filter by contract, for example `ETH_USDT`</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="offset">["<c>offset</c>"] Offset</param>
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
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#cancel-all-auto-orders-2" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">["<c>contract</c>"] Contract, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpTriggerOrder[]>> CancelTriggerOrdersAsync(
            string settlementAsset,
            string contract,
            CancellationToken ct = default);

        /// <summary>
        /// Get a trigger order by id
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#query-single-auto-order-details-2" /></para>
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
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#cancel-single-auto-order-2" /></para>
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
