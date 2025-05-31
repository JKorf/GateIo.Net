using CryptoExchange.Net.Objects;
using GateIo.Net.Enums;
using GateIo.Net.Objects.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GateIo.Net.Interfaces.Clients.PerpetualFuturesApi
{
    /// <summary>
    /// GateIo futures account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface IGateIoRestClientPerpetualFuturesApiAccount
    {
        /// <summary>
        /// Get futures account info
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#query-futures-account" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoFuturesAccount>> GetAccountAsync(
            string settlementAsset,
            CancellationToken ct = default);

        /// <summary>
        /// Get futures account ledger
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#query-account-book-2" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Filter by contract, for example `ETH_USDT`</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page number</param>
        /// <param name="limit">Max amount of results</param>
        /// <param name="type">Filter by type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpLedgerEntry[]>> GetLedgerAsync(string settlementAsset, string? contract = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, string? type = null, CancellationToken ct = default);

        /// <summary>
        /// Set dual position mode
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#enable-or-disable-dual-mode" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="dualMode">Dual mode enabled</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoFuturesAccount>> UpdatePositionModeAsync(string settlementAsset, bool dualMode, CancellationToken ct = default);

        /// <summary>
        /// Set margin mode for a position
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="marginMode">Margin mode</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPosition[]>> SetMarginModeAsync(string settlementAsset, string contract, MarginMode marginMode, CancellationToken ct = default);

        /// <summary>
        /// Get user trading fees
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#enable-or-disable-dual-mode" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Filter by contract, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<Dictionary<string, GateIoPerpFee>>> GetTradingFeeAsync(string settlementAsset, string? contract = null, CancellationToken ct = default);
    }
}
