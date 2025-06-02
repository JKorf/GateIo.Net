using System;
using CryptoExchange.Net.Interfaces;

namespace GateIo.Net.Interfaces.Clients.RebateApi
{
    /// <summary>
    /// GateIo eebate API endpoints
    /// </summary>
    public interface IGateIoRestClientRebateApi : IRestApiClient, IDisposable
    {
        /// <summary>
        /// Partner subordinate
        /// </summary>
        public IGateIoRestClientRebateApiPartner Partner { get; }
    }
}
