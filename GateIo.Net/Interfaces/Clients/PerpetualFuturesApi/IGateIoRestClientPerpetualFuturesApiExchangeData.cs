using CryptoExchange.Net.Objects;
using GateIo.Net.Objects.Models;
using GateIo.Net.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GateIo.Net.Interfaces.Clients.PerpetualFuturesApi
{
    /// <summary>
    /// Gate futures exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IGateIoRestClientPerpetualFuturesApiExchangeData
    {
        /// <summary>
        ///
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#get-server-current-time" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get list of contract
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#query-all-futures-contracts" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpFuturesContract[]>> GetContractsAsync(string settlementAsset, CancellationToken ct = default);

        /// <summary>
        /// Get a specific contract
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#query-single-contract-information" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract name, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpFuturesContract>> GetContractAsync(string settlementAsset, string contract, CancellationToken ct = default);

        /// <summary>
        /// Get order book
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#query-futures-market-depth-information" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">["<c>contract</c>"] Contract name, for example `ETH_USDT`</param>
        /// <param name="mergeDepth">["<c>interval</c>"] Merge depth</param>
        /// <param name="depth">["<c>limit</c>"] Number of rows</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpOrderBook>> GetOrderBookAsync(string settlementAsset, string contract, int? mergeDepth = null, int? depth = null, CancellationToken ct = default);

        /// <summary>
        /// Get recent trades
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#futures-market-transaction-records" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">["<c>contract</c>"] Contract name, for example `ETH_USDT`</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="offset">["<c>offset</c>"] Offset</param>
        /// <param name="lastId">["<c>last_id</c>"] Specify the starting point for this list based on a previously retrieved id</param>
        /// <param name="startTime">["<c>from</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>to</c>"] Filter by end time</param>
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
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#futures-market-k-line-chart" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">["<c>contract</c>"] Contract name, for example `ETH_USDT`</param>
        /// <param name="interval">["<c>interval</c>"] Interval</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="startTime">["<c>from</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>to</c>"] Filter by end time</param>
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
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#premium-index-k-line-chart" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">["<c>contract</c>"] Contract name, for example `ETH_USDT`</param>
        /// <param name="interval">["<c>interval</c>"] Interval</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="startTime">["<c>from</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>to</c>"] Filter by end time</param>
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
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#get-all-futures-trading-statistics" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">["<c>contract</c>"] Contract, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpTicker[]>> GetTickersAsync(
            string settlementAsset,
            string? contract = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get funding rate history
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#futures-market-historical-funding-rate" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">["<c>contract</c>"] Contract, for example `ETH_USDT`</param>
        /// <param name="startTime">["<c>from</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>to</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
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
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#futures-market-insurance-fund-history" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpInsurance[]>> GetInsuranceBalanceHistoryAsync(
            string settlementAsset,
            int? limit = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get contract statistics
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#futures-statistics" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">["<c>contract</c>"] Contract, for example `ETH_USDT`</param>
        /// <param name="limit">["<c>limit</c>"] Limit</param>
        /// <param name="startTime">["<c>from</c>"] Filter by start time</param>
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
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#query-index-constituents" /></para>
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
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#query-liquidation-order-history" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">["<c>contract</c>"] Contract, for example `ETH_USDT`</param>
        /// <param name="startTime">["<c>from</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>to</c>"] Fitler by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
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
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#query-risk-limit-tiers" /></para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">["<c>contract</c>"] Contract, for example `ETH_USDT`</param>
        /// <param name="offset">["<c>offset</c>"] Result offset</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
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
