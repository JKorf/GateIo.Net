using CryptoExchange.Net.Objects;
using GateIo.Net.Objects.Models;
using GateIo.Net.Enums;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GateIo.Net.Interfaces.Clients.PerpetualFuturesApi
{
    /// <summary>
    /// GateIo futures exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IGateIoRestClientPerpetualFuturesApiExchangeData
    {
        /// <summary>
        /// 
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/#get-server-current-time" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get list of contract
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#list-all-futures-contracts" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpFuturesContract[]>> GetContractsAsync(string settlementAsset, CancellationToken ct = default);

        /// <summary>
        /// Get a specific contract
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#get-a-single-contract" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract name, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpFuturesContract>> GetContractAsync(string settlementAsset, string contract, CancellationToken ct = default);

        /// <summary>
        /// Get order book
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#futures-order-book" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract name, for example `ETH_USDT`</param>
        /// <param name="mergeDepth">Merge depth</param>
        /// <param name="depth">Number of rows</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpOrderBook>> GetOrderBookAsync(string settlementAsset, string contract, int? mergeDepth = null, int? depth = null, CancellationToken ct = default);

        /// <summary>
        /// Get recent trades
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#futures-trading-history" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract name, for example `ETH_USDT`</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="offset">Offset</param>
        /// <param name="lastId">Specify the starting point for this list based on a previously retrieved id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpTrade[]>> GetTradesAsync(
            string settlementAsset,
            string contract,
            int? limit = null,
            int? offset = null,
            string? lastId = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get kline/candlesticks
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#get-futures-candlesticks" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract name, for example `ETH_USDT`</param>
        /// <param name="interval">Interval</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpKline[]>> GetKlinesAsync(
            string settlementAsset,
            string contract,
            KlineInterval interval,
            int? limit = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get premium index kline/candlesticks
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#premium-index-k-line" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract name, for example `ETH_USDT`</param>
        /// <param name="interval">Interval</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpIndexKline[]>> GetIndexKlinesAsync(
            string settlementAsset,
            string contract,
            KlineInterval interval,
            int? limit = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get ticker info
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#list-futures-tickers" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpTicker[]>> GetTickersAsync(
            string settlementAsset,
            string? contract = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get funding rate history
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#funding-rate-history" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpFundingRate[]>> GetFundingRateHistoryAsync(
            string settlementAsset,
            string contract,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get insurance balance history
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#futures-insurance-balance-history" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpInsurance[]>> GetInsuranceBalanceHistoryAsync(
            string settlementAsset,
            int? limit = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get contract statistics
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#futures-stats" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="limit">Limit</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpContractStats[]>> GetContractStatsAsync(
            string settlementAsset,
            string contract,
            int? limit = null,
            DateTime? startTime = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get constituents for a contract
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#get-index-constituents" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpConstituent>> GetIndexConstituentsAsync(
            string settlementAsset,
            string contract,
            CancellationToken ct = default);

        /// <summary>
        /// Get liquidation history
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#retrieve-liquidation-history" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Fitler by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoLiquidation[]>> GetLiquidationsAsync(
            string settlementAsset,
            string? contract = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get risk limit tiers
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#list-risk-limit-tiers" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract, for example `ETH_USDT`</param>
        /// <param name="offset">Result offset</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoRiskLimitTier[]>> GetRiskLimitTiersAsync(
            string settlementAsset,
            string contract,
            int? offset = null,
            int? limit = null,
            CancellationToken ct = default);
    }
}
