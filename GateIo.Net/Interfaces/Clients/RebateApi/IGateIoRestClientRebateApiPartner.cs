using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using GateIo.Net.Objects.Models;

namespace GateIo.Net.Interfaces.Clients.RebateApi
{
    /// <summary>
    /// GateIo partner subordinate
    /// </summary>
    public interface IGateIoRestClientRebateApiPartner
    {
        /// <summary>
        /// Get partner list
        /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#partner-subordinate-list" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoRebatePartnerSubordinateList>> GetSubordinatesAsync(CancellationToken ct = default);
    }
}
