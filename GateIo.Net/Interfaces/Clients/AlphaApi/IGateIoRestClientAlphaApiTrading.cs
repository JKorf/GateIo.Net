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
        /// <param name="asset">["<c>currency</c>"] Asset</param>
        /// <param name="side">["<c>side</c>"] Order side</param>
        /// <param name="quantity">["<c>amount</c>"] Quantity, for buys this is in USDT, for sells in the asset</param>
        /// <param name="gasMode">["<c>gas_mode</c>"] Gas mode</param>
        /// <param name="slippage">["<c>slippage</c>"] Max slippage, 10 equals 10%. Only required when using GasMode.Custom</param>
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
        /// <param name="asset">["<c>currency</c>"] Asset</param>
        /// <param name="side">["<c>side</c>"] Order side</param>
        /// <param name="quantity">["<c>amount</c>"] Quantity, for buys this is in USDT, for sells in the asset</param>
        /// <param name="gasMode">["<c>gas_mode</c>"] Gas mode</param>
        /// <param name="quoteId">["<c>quote_id</c>"] Quote id received from <see cref="GetQuoteAsync(string, OrderSide, decimal, GasMode, decimal?, CancellationToken)"/></param>
        /// <param name="slippage">["<c>slippage</c>"] Max slippage, 10 equals 10%. Only required when using GasMode.Custom</param>
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
        /// <param name="asset">["<c>currency</c>"] Asset name</param>
        /// <param name="side">["<c>side</c>"] Order side</param>
        /// <param name="status">["<c>status</c>"] Order status</param>
        /// <param name="startTime">["<c>from</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>to</c>"] Filter by end time</param>
        /// <param name="page">["<c>page</c>"] Page number</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
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
        /// <param name="orderId">["<c>order_id</c>"] Order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<GateIoAlphaOrder>> GetOrderAsync(
            string orderId,
            CancellationToken ct = default);
    }
}
