using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using GateIo.Net.Objects.Models;

namespace GateIo.Net.Interfaces.Clients.AlphaApi
{
    /// <summary>
    /// GateIo alpha account endpoints
    /// </summary>
    public interface IGateIoRestClientAlphaApiAccount
    {
        /// <summary>
        /// Get account info
        /// <para><a href="https://www.gate.com/docs/developers/alpha/en/#alpha-account-api" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<GateIoAlphaAccount[]>> GetAccountInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Get account ledger
        /// <para><a href="https://www.gate.com/docs/developers/alpha/en/#alpha-account-transaction-history-api" /></para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page number</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<GateIoAlphaLedgerEntry[]>> GetLedgerAsync(
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? page = null,
            int? limit = null,
            CancellationToken ct = default);
    }
}
