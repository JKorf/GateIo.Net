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
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#get-server-current-time" /><br />
        /// Endpoint:<br />
        /// /api/v4/spot/time
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get list of contract
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#query-all-futures-contracts" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/contracts
        /// </para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpFuturesContract[]>> GetContractsAsync(string settlementAsset, CancellationToken ct = default);

        /// <summary>
        /// Get a specific contract
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#query-single-contract-information" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/contracts/{id}
        /// </para>
        /// </summary>
        /// <param name="settlementAsset">The settlement asset. btc, usdt or usd</param>
        /// <param name="contract">Contract name, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoPerpFuturesContract>> GetContractAsync(string settlementAsset, string contract, CancellationToken ct = default);

        /// <summary>
        /// Get order book
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#query-futures-market-depth-information" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/order_book
        /// </para>
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
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#futures-market-transaction-records" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/trades
        /// </para>
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
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#futures-market-k-line-chart" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/candlesticks
        /// </para>
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
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#premium-index-k-line-chart" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/premium_index
        /// </para>
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
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#get-all-futures-trading-statistics" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/tickers
        /// </para>
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
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#futures-market-historical-funding-rate" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/funding_rate
        /// </para>
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
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#futures-market-insurance-fund-history" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/insurance
        /// </para>
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
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#futures-statistics" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/contract_stats
        /// </para>
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
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#query-index-constituents" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/index_constituents/{contract}
        /// </para>
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
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#query-liquidation-order-history" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/liq_orders
        /// </para>
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
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/apiv4/en/#query-risk-limit-tiers" /><br />
        /// Endpoint:<br />
        /// /api/v4/futures/{settlementAsset.ToLowerInvariant()}/risk_limit_tiers
        /// </para>
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
