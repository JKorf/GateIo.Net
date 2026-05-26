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
        /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/rebate/#partner-subordinate-list" /></para>
        /// </summary>
        /// <param name="userId">User ID. If not specified, all user records will be returned</param>
        /// <param name="limit">Maximum number of records returned in a single list</param>
        /// <param name="offset">List offset, starting from 0</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<GateIoRebatePartnerSubordinateList>> GetSubordinatesAsync(long? userId = null, int limit = 100, int offset = 0, CancellationToken ct = default);
    }
}
