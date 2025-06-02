using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;

namespace GateIo.Net.Interfaces.Clients.RebateApi
{
    /// <summary>
    /// GateIo Rebate exchange data endpoints.
    /// </summary>
    public interface IGateIoRestClientRebateApiExchangeData
    {
        /// <summary>
        /// Get the current server time
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/#get-server-current-time" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);
    }
}
