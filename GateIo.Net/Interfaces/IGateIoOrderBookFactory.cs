using CryptoExchange.Net.Interfaces;
using System;
using GateIo.Net.Objects.Options;
using CryptoExchange.Net.SharedApis;

namespace GateIo.Net.Interfaces
{
    /// <summary>
    /// Gate local order book factory
    /// </summary>
    public interface IGateIoOrderBookFactory : IExchangeService
    {
        /// <summary>
        /// Spot order book factory methods
        /// </summary>
        public IOrderBookFactory<GateIoOrderBookOptions> Spot { get; }
        /// <summary>
        /// BTC perpetual futures order book factory methods
        /// </summary>
        public IOrderBookFactory<GateIoOrderBookOptions> PerpetualFuturesBtc { get; }
        /// <summary>
        /// USD perpetual futures order book factory methods
        /// </summary>
        public IOrderBookFactory<GateIoOrderBookOptions> PerpetualFuturesUsd { get; }
        /// <summary>
        /// USDT perpetual futures order book factory methods
        /// </summary>
        public IOrderBookFactory<GateIoOrderBookOptions> PerpetualFuturesUsdt { get; }

        /// <summary>
        /// Create a SymbolOrderBook for the symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="settlementAsset">Settlement asset for futures</param>
        /// <param name="options">Book options</param>
        /// <returns></returns>
        ISymbolOrderBook Create(SharedSymbol symbol, string? settlementAsset = null, Action<GateIoOrderBookOptions>? options = null);

        /// <summary>
        /// Create a new perpetual futures local order book instance
        /// </summary>
        /// <param name="settlementAsset"></param>
        /// <param name="symbol"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        ISymbolOrderBook CreatePerpetualFutures(string settlementAsset, string symbol, Action<GateIoOrderBookOptions>? options = null);

        /// <summary>
        /// Create a new spot local order book instance
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        ISymbolOrderBook CreateSpot(string symbol, Action<GateIoOrderBookOptions>? options = null);
    }
}
