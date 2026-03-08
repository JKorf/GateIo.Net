using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using GateIo.Net.Objects.Models;

namespace GateIo.Net.Interfaces.Clients.AlphaApi
{
    /// <summary>
    /// GateIo alpha market data endpoints
    /// </summary>
    public interface IGateIoRestClientAlphaApiExchangeData
    {
        /// <summary>
        /// Get asset information
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/alpha/en/#query-currency-information" /><br />
        /// Endpoint:<br />
        /// /api/v4/alpha/currencies
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Filter by asset</param>
        /// <param name="page">["<c>page</c>"] Page number</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<GateIoAlphaAsset[]>> GetAssetsAsync(string? asset = null, int? page = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get asset tickers
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.gate.com/docs/developers/alpha/en/#query-currency-ticker" /><br />
        /// Endpoint:<br />
        /// /api/v4/alpha/tickers
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Filter by asset</param>
        /// <param name="page">["<c>page</c>"] Page number</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<GateIoAlphaTicker[]>> GetTickersAsync(string? asset = null, int? page = null, int? limit = null, CancellationToken ct = default);

    }
}
