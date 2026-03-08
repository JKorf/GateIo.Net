using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using GateIo.Net.Objects.Models;
using GateIo.Net.Enums;

namespace GateIo.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// GateIo Spot exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IGateIoRestClientSpotApiExchangeData
    {
        /// <summary>
        /// Get the current server time
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#get-server-current-time" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get a list of supported assets
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#query-all-currency-information" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoAsset[]>> GetAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get info on a specific asset
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#query-single-currency-information" /></para>
        /// </summary>
        /// <param name="asset">Asset name, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoAsset>> GetAssetAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Get info on a specific symbol
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#query-single-currency-pair-details" /></para>
        /// </summary>
        /// <param name="symbol">Symbol name, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoSymbol>> GetSymbolAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get a list of supported symbols
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#query-all-supported-currency-pairs" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoSymbol[]>> GetSymbolsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get tickers for all or a single symbol
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#get-currency-pair-ticker-information" /></para>
        /// </summary>
        /// <param name="symbol">["<c>currency_pair</c>"] Filter for a single symbol, for example `ETH_USDT`</param>
        /// <param name="timezone">["<c>timezone</c>"] Timezone, utc0, utc8 or all</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoTicker[]>> GetTickersAsync(string? symbol = null, string? timezone = null, CancellationToken ct = default);

        /// <summary>
        /// Get the orderbook for a symbol
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#get-market-depth-information" /></para>
        /// </summary>
        /// <param name="symbol">["<c>currency_pair</c>"] Symbol name, for example `ETH_USDT`</param>
        /// <param name="mergeDepth">["<c>interval</c>"] Merge depth, defaults to 0</param>
        /// <param name="limit">["<c>limit</c>"] Book depth to return, defaults to 10</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoOrderBook>> GetOrderBookAsync(string symbol, int? mergeDepth = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get market trades for a symbol
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#query-market-transaction-records" /></para>
        /// </summary>
        /// <param name="symbol">["<c>currency_pair</c>"] Symbol name, for example `ETH_USDT`</param>
        /// <param name="limit">["<c>limit</c>"] Max amount of results</param>
        /// <param name="lastId">["<c>last_id</c>"] Specify list staring point using the id of last record in previous list-query results</param>
        /// <param name="reverse">["<c>reverse</c>"] Whether the id of records to be retrieved should be less than the last_id specified. Default to false.</param>
        /// <param name="startTime">["<c>from</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>to</c>"] Filter by end time</param>
        /// <param name="page">["<c>page</c>"] Page number</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoTrade[]>> GetTradesAsync(string symbol, int? limit = null, string? lastId = null, bool? reverse = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, CancellationToken ct = default);

        /// <summary>
        /// Get kline/candlesticks for a symbol
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#market-k-line-chart" /></para>
        /// </summary>
        /// <param name="symbol">["<c>currency_pair</c>"] Symbol name, for example `ETH_USDT`</param>
        /// <param name="interval">["<c>interval</c>"] The kline interval</param>
        /// <param name="startTime">["<c>from</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>to</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoKline[]>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get a list of networks for an asset
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#query-chains-supported-for-specified-currency" /></para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Asset, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoNetwork[]>> GetNetworksAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Get discount tiers
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#query-unified-account-tiered" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoDiscountTier[]>> GetDiscountTiersAsync(CancellationToken ct = default);

        /// <summary>
        /// Get loan margin tiers
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#query-unified-account-tiered-loan-margin" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoLoanMarginTier[]>> GetLoanMarginTiersAsync(CancellationToken ct = default);

        /// <summary>
        /// DEPRECATED
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#retrieve-detail-of-one-single-currency-supported-by-cross-margin" /></para>
        /// </summary>
        /// <param name="asset">Asset name, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoCrossMarginAsset>> GetCrossMarginAssetAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// DEPRECATED
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#currencies-supported-by-cross-margin" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoCrossMarginAsset[]>> GetCrossMarginAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get lending symbols
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#list-lending-markets" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoLendingSymbol[]>> GetLendingSymbolsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get lending symbol
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/#get-lending-market-details" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoLendingSymbol>> GetLendingSymbolAsync(string symbol, CancellationToken ct = default);
    }
}
