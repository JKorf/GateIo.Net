using CryptoExchange.Net.Interfaces.Clients;
using GateIo.Net.Interfaces.Clients.AlphaApi;
using System;

namespace GateIo.Net.Interfaces.Clients.RebateApi
{
    /// <summary>
    /// GateIo Alpha API endpoints
    /// </summary>
    public interface IGateIoRestClientAlphaApi : IRestApiClient, IDisposable
    {
        /// <summary>
        /// Alpha account endpoints
        /// </summary>
        /// <see cref="IGateIoRestClientAlphaApiAccount"/>
        IGateIoRestClientAlphaApiAccount Account { get; }
        /// <summary>
        /// Alpha exchange data endpoints
        /// </summary>
        /// <see cref="IGateIoRestClientAlphaApiExchangeData"/>
        IGateIoRestClientAlphaApiExchangeData ExchangeData { get; }
        /// <summary>
        /// Alpha trading endpoints
        /// </summary>
        /// <see cref="IGateIoRestClientAlphaApiTrading"/>
        IGateIoRestClientAlphaApiTrading Trading { get; }
    }
}
