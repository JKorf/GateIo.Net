using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using GateIo.Net.Enums;
using GateIo.Net.Objects.Models;

namespace GateIo.Net.Interfaces.Clients.AlphaApi
{
    /// <summary>
    /// GateIo alpha trading endpoints
    /// </summary>
    public interface IGateIoRestClientAlphaApiTrading
    {
        /// <summary>
        /// Get an order quote
        /// <para><a href="https://www.gate.com/docs/developers/alpha/en/#alpha-quote-api" /></para>
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="side">Order side</param>
        /// <param name="quantity">Quantity, for buys this is in USDT, for sells in the asset</param>
        /// <param name="gasMode">Gas mode</param>
        /// <param name="slippage">Max slippage, 10 equals 10%. Only required when using GasMode.Custom</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<GateIoAlphaQuote>> GetQuoteAsync(
            string asset,
            OrderSide side,
            decimal quantity,
            GasMode gasMode,
            decimal? slippage = null,
            CancellationToken ct = default);

        /// <summary>
        /// Place order using a quote
        /// <para><a href="https://www.gate.com/docs/developers/alpha/en/#alpha-order-api" /></para>
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="side">Order side</param>
        /// <param name="quantity">Quantity, for buys this is in USDT, for sells in the asset</param>
        /// <param name="gasMode">Gas mode</param>
        /// <param name="quoteId">Quote id received from <see cref="GetQuoteAsync(string, OrderSide, decimal, GasMode, decimal?, CancellationToken)"/></param>
        /// <param name="slippage">Max slippage, 10 equals 10%. Only required when using GasMode.Custom</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<GateIoAlphaOrderResult>> PlaceOrderAsync(
            string asset,
            OrderSide side,
            decimal quantity,
            GasMode gasMode,
            string quoteId,
            decimal? slippage = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get order history
        /// <para><a href="https://www.gate.com/docs/developers/alpha/en/#alpha-order-list-api" /></para>
        /// </summary>
        /// <param name="asset">Asset name</param>
        /// <param name="side">Order side</param>
        /// <param name="status">Order status</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page number</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<GateIoAlphaOrder[]>> GetOrdersAsync(
            string asset,
            OrderSide side,
            AlphaOrderStatus status,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? page = null,
            int? limit = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get order info by id
        /// <para><a href="https://www.gate.com/docs/developers/alpha/en/#alpha-single-order-query-api" /></para>
        /// </summary>
        /// <param name="orderId">Order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<GateIoAlphaOrder>> GetOrderAsync(
            string orderId,
            CancellationToken ct = default);
    }
}
