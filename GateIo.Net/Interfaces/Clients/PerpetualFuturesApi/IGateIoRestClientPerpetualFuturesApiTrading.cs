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
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/futures/#get-user-position-list" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/positions
        /// </para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="holding">["<c>holding</c>"] True to return only active positions, false to return all</param>
        /// <param name="offset">["<c>offset</c>"] Result offset</param>
        /// <param name="limit">["<c>limit</c>"] Max amount of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<HttpResult<GateIoPosition[]>> GetPositionsAsync(string settlementAsset, bool? holding = null, int? offset = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get single position
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/futures/#get-single-position-information" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/positions/{contract}
        /// </para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<HttpResult<GateIoPosition>> GetPositionAsync(string settlementAsset, string contract, CancellationToken ct = default);

        /// <summary>
        /// Update position margin
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/futures/#update-position-margin" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/positions/{contract}/margin
        /// </para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="change">["<c>change</c>"] Change margin</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<HttpResult<GateIoPosition>> UpdatePositionMarginAsync(string settlementAsset, string contract, decimal change, CancellationToken ct = default);

        /// <summary>
        /// Update position leverage
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/futures/#update-position-leverage" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/positions/{contract}/leverage
        /// </para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="leverage">["<c>leverage</c>"] New leverage</param>
        /// <param name="crossLeverageLimit">["<c>cross_leverage_limit</c>"] Cross margin leverage</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<HttpResult<GateIoPosition>> UpdatePositionLeverageAsync(string settlementAsset, string contract, decimal leverage, decimal? crossLeverageLimit = null, CancellationToken ct = default);

        /// <summary>
        /// Update position risk limit
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/futures/#update-position-risk-limit" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/positions/{contract}/risk_limit
        /// </para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="riskLimit">["<c>risk_limit</c>"] Risk limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<HttpResult<GateIoPosition>> UpdatePositionRiskLimitAsync(string settlementAsset, string contract, decimal riskLimit, CancellationToken ct = default);

        /// <summary>
        /// Get positions in dual mode
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/futures/#get-position-information-in-dual-mode" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/dual_comp/positions/{contract}
        /// </para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<HttpResult<GateIoPosition[]>> GetDualModePositionsAsync(string settlementAsset, string contract, CancellationToken ct = default);

        /// <summary>
        /// Update position margin for dual position mode
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/futures/#update-position-margin-in-dual-mode" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/dual_comp/positions/{contract}/margin
        /// </para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="change">["<c>change</c>"] Change</param>
        /// <param name="mode">["<c>dual_side</c>"] Side</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<HttpResult<GateIoPosition[]>> UpdateDualModePositionMarginAsync(string settlementAsset, string contract, decimal change, PositionMode mode, CancellationToken ct = default);

        /// <summary>
        /// Update position leverage in dual position mode
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/futures/#update-position-leverage-in-dual-mode" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/dual_comp/positions/{contract}/leverage
        /// </para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="leverage">["<c>leverage</c>"] Leverage</param>
        /// <param name="crossLeverageLimit">["<c>cross_leverage_limit</c>"] Cross margin leverage</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<HttpResult<GateIoPosition[]>> UpdateDualModePositionLeverageAsync(string settlementAsset, string contract, decimal leverage, decimal? crossLeverageLimit = null, CancellationToken ct = default);

        /// <summary>
        /// Update position risk limit in dual position mode
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/futures/#update-position-risk-limit-in-dual-mode" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/dual_comp/positions/{contract}/risk_limit
        /// </para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="riskLimit">["<c>risk_limit</c>"] Risk limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<HttpResult<GateIoPosition[]>> UpdateDualModePositionRiskLimitAsync(string settlementAsset, string contract, int riskLimit, CancellationToken ct = default);

        /// <summary>
        /// Place a new order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/futures/#place-futures-order" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/orders
        /// </para>
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
        /// <param name="takeProfitTriggerPrice">["<c>tpsl_tp_trigger_price</c>"] Predefined take profit trigger price</param>
        /// <param name="stopLossTriggerPrice">["<c>tpsl_sl_trigger_price</c>"] Predefined stop loss trigger price</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<HttpResult<GateIoPerpOrder>> PlaceOrderAsync(
            string settlementAsset,
            string contract,
            OrderSide orderSide,
            decimal quantity,
            decimal? price = null,
            bool? closePosition = null,
            bool? reduceOnly = null,
            TimeInForce? timeInForce = null,
            int? icebergQuantity = null,
            CloseSide? closeSide = null,
            SelfTradePreventionMode? stpMode = null,
            string? text = null,
            decimal? takeProfitTriggerPrice = null,
            decimal? stopLossTriggerPrice = null,
            CancellationToken ct = default);

        /// <summary>
        /// Place multiple new orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/futures/#place-batch-futures-orders" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/batch_orders
        /// </para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="orders">Order info</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<HttpResult<GateIoPerpOrder[]>> PlaceMultipleOrderAsync(
            string settlementAsset,
            IEnumerable<GateIoPerpBatchPlaceRequest> orders,
            CancellationToken ct = default);

        /// <summary>
        /// Get orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/futures/#query-futures-order-list-2" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/orders
        /// </para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">["<c>contract</c>"] Filter by contract, for example `ETH_USDT`</param>
        /// <param name="status">["<c>status</c>"] Filter by status</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="offset">["<c>offset</c>"] Offset</param>
        /// <param name="lastId">["<c>last_id</c>"] Filter by last order id of previous result</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<HttpResult<GateIoPerpOrder[]>> GetOrdersAsync(
            string settlementAsset,
            OrderStatus status,
            string? contract = null,
            int? limit = null,
            int? offset = null,
            string? lastId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel orders after a certain period. Can be called at interval to act as a deadmans switch. Using TimeSpan.Zero cancels the countdown
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/futures/#countdown-cancel-orders-3" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/countdown_cancel_all
        /// </para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="cancelAfter">["<c>timeout</c>"] Timespan after which to cancel, TimeSpan.Zero to cancel the countdown</param>
        /// <param name="contract">["<c>contract</c>"] Filter by contract, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<HttpResult<GateIoCancelAfter>> CancelOrdersAfterAsync(string settlementAsset, TimeSpan cancelAfter, string? contract = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel orders by id
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/futures/#cancel-batch-orders-by-specified-id-list-2" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/batch_cancel_orders
        /// </para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="orderIds">Ids of orders to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<HttpResult<GateIoFuturesCancelResult[]>> CancelOrdersAsync(string settlementAsset, IEnumerable<long> orderIds, CancellationToken ct = default);

        /// <summary>
        /// Get orders with timestamp filtering
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/futures/#query-futures-order-list-by-time-range" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/orders_timerange
        /// </para>
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
        Task<HttpResult<GateIoPerpOrder[]>> GetOrdersByTimestampAsync(
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
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/futures/#cancel-all-orders-with-open-status-2" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/orders
        /// </para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">["<c>contract</c>"] Filter by contract, for example `ETH_USDT`</param>
        /// <param name="side">["<c>side</c>"] Filter by order side</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<HttpResult<GateIoPerpOrder[]>> CancelAllOrdersAsync(string settlementAsset, string contract, OrderSide? side = null, CancellationToken ct = default);

        /// <summary>
        /// Get order by id
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/futures/#query-single-order-details-3" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/orders/{id}
        /// </para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="orderId">Order id, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">Client order id, either this or orderId should be provided</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<HttpResult<GateIoPerpOrder>> GetOrderAsync(
            string settlementAsset,
            long? orderId = null,
            string? clientOrderId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel an order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/futures/#cancel-single-order-3" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/orders/{id}
        /// </para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="orderId">Order id, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">Client order id, either this or orderId should be provided</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<HttpResult<GateIoPerpOrder>> CancelOrderAsync(
            string settlementAsset,
            long? orderId = null,
            string? clientOrderId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Edit an existing order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/futures/#amend-single-order" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/orders/{id}
        /// </para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="orderId">Order id, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">Client order id, either this or orderId should be provided</param>
        /// <param name="quantity">["<c>size</c>"] New quantity</param>
        /// <param name="price">["<c>price</c>"] New price</param>
        /// <param name="amendText">["<c>amend_text</c>"] Amend text</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<HttpResult<GateIoPerpOrder>> EditOrderAsync(
            string settlementAsset,
            long? orderId = null,
            string? clientOrderId = null,
            int? quantity = null,
            decimal? price = null,
            string? amendText = null,
            CancellationToken ct = default);

        /// <summary>
        /// Edit multiple existing orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/futures/#batch-modify-orders-by-specified-ids" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/batch_amend_orders
        /// </para>
        /// </summary>
        /// <param name="settlementAsset">Settlement asset</param>
        /// <param name="requests">Edit requests</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<GateIoPerpOrder[]>> EditMultipleOrdersAsync(string settlementAsset, IEnumerable<GateIoPerpBatchEditRequest> requests, CancellationToken ct = default);

        /// <summary>
        /// Get user trades
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/futures/#query-personal-trading-records-2" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/my_trades
        /// </para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">["<c>contract</c>"] Filter by contract, for example `ETH_USDT`</param>
        /// <param name="orderId">["<c>order</c>"] Filter by order id</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="offset">["<c>offset</c>"] Offset</param>
        /// <param name="lastId">["<c>last_id</c>"] Last id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<HttpResult<GateIoPerpUserTrade[]>> GetUserTradesAsync(
            string settlementAsset,
            string? contract = null,
            long? orderId = null,
            int? limit = null,
            int? offset = null,
            long? lastId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get user trades by timestamp filter
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/futures/#query-personal-trading-records-by-time-range" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/my_trades_timerange
        /// </para>
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
        Task<HttpResult<GateIoPerpUserTrade[]>> GetUserTradesByTimestampAsync(
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
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/futures/#query-position-close-history" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/position_close
        /// </para>
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
        Task<HttpResult<GateIoPerpPositionClose[]>> GetPositionCloseHistoryAsync(
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
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/futures/#query-liquidation-history" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/liquidates
        /// </para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">["<c>contract</c>"] Filter by contract, for example `ETH_USDT`</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<HttpResult<GateIoPerpLiquidation[]>> GetLiquidationHistoryAsync(
            string settlementAsset,
            string? contract = null,
            int? limit = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get user auto deleveraging history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/futures/#query-adl-auto-deleveraging-order-information" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/auto_deleverages
        /// </para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">["<c>contract</c>"] Filter by contract, for example `ETH_USDT`</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<HttpResult<GateIoPerpAutoDeleverage[]>> GetAutoDeleveragingHistoryAsync(
            string settlementAsset,
            string? contract = null,
            int? limit = null,
            CancellationToken ct = default);

        /// <summary>
        /// Place a new trigger order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/futures/#create-price-triggered-order-2" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/price_orders
        /// </para>
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
        Task<HttpResult<GateIoId>> PlaceTriggerOrderAsync(
            string settlementAsset,
            string contract,
            OrderSide orderSide,
            decimal quantity,
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
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/futures/#query-auto-order-list" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/price_orders
        /// </para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="open">["<c>status</c>"] True for open orders, false for closed</param>
        /// <param name="contract">["<c>contract</c>"] Filter by contract, for example `ETH_USDT`</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="offset">["<c>offset</c>"] Offset</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<HttpResult<GateIoPerpTriggerOrder[]>> GetTriggerOrdersAsync(
            string settlementAsset,
            bool open,
            string? contract = null,
            int? limit = null,
            int? offset = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel all trigger orders on a contract
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/futures/#cancel-all-auto-orders-2" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/price_orders
        /// </para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">["<c>contract</c>"] Contract, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<HttpResult<GateIoPerpTriggerOrder[]>> CancelTriggerOrdersAsync(
            string settlementAsset,
            string contract,
            CancellationToken ct = default);

        /// <summary>
        /// Get a trigger order by id
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/futures/#query-single-auto-order-details-2" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/price_orders/{orderId}
        /// </para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="orderId">Order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<HttpResult<GateIoPerpTriggerOrder>> GetTriggerOrderAsync(
            string settlementAsset,
            long orderId,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel a trigger order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/futures/#cancel-single-auto-order-2" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/price_orders/{orderId}
        /// </para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="orderId">Order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<HttpResult<GateIoPerpTriggerOrder>> CancelTriggerOrderAsync(
            string settlementAsset,
            long orderId,
            CancellationToken ct = default);
    }
}
