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
        /// <para><a href="https://www.gate.com/docs/developers/alpha/en/#query-currency-information" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="page">Page number</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<GateIoAlphaAsset[]>> GetAssetsAsync(string? asset = null, int? page = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get asset tickers
        /// <para><a href="https://www.gate.com/docs/developers/alpha/en/#query-currency-ticker" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="page">Page number</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<GateIoAlphaTicker[]>> GetTickersAsync(string? asset = null, int? page = null, int? limit = null, CancellationToken ct = default);

    }
}
