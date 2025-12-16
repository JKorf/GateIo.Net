using System;
using CryptoExchange.Net.Interfaces.Clients;

namespace GateIo.Net.Interfaces.Clients.RebateApi
{
    /// <summary>
    /// GateIo rebate API endpoints
    /// </summary>
    public interface IGateIoRestClientRebateApi : IRestApiClient, IDisposable
    {
        /// <summary>
        /// Partner subordinate
        /// </summary>
        /// <see cref="IGateIoRestClientRebateApiPartner"/>
        public IGateIoRestClientRebateApiPartner Partner { get; }
    }
}
