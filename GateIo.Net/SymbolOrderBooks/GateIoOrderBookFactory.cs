using CryptoExchange.Net.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using GateIo.Net.Interfaces;
using GateIo.Net.Interfaces.Clients;
using GateIo.Net.Objects.Options;
using CryptoExchange.Net.OrderBook;

namespace GateIo.Net.SymbolOrderBooks
{
    /// <summary>
    /// GateIo order book factory
    /// </summary>
    public class GateIoOrderBookFactory : IGateIoOrderBookFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public GateIoOrderBookFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            Spot = new OrderBookFactory<GateIoOrderBookOptions>((symbol, options) => CreateSpot(symbol, options), (baseAsset, quoteAsset, options) => CreateSpot(baseAsset + "_" + quoteAsset, options));
            PerpetualFuturesBtc = new OrderBookFactory<GateIoOrderBookOptions>((symbol, options) => CreatePerpetualFutures("btc", symbol, options), (baseAsset, quoteAsset, options) => CreatePerpetualFutures("btc", baseAsset + "_" + quoteAsset, options));
            PerpetualFuturesUsd = new OrderBookFactory<GateIoOrderBookOptions>((symbol, options) => CreatePerpetualFutures("usd", symbol, options), (baseAsset, quoteAsset, options) => CreatePerpetualFutures("usd", baseAsset + "_" + quoteAsset, options));
            PerpetualFuturesUsdt = new OrderBookFactory<GateIoOrderBookOptions>((symbol, options) => CreatePerpetualFutures("usdt", symbol, options), (baseAsset, quoteAsset, options) => CreatePerpetualFutures("usdt", baseAsset + "_" + quoteAsset, options));
        }

        /// <inheritdoc />
        public IOrderBookFactory<GateIoOrderBookOptions> Spot { get; }
        /// <inheritdoc />
        public IOrderBookFactory<GateIoOrderBookOptions> PerpetualFuturesBtc { get; }
        /// <inheritdoc />
        public IOrderBookFactory<GateIoOrderBookOptions> PerpetualFuturesUsd { get; }
        /// <inheritdoc />
        public IOrderBookFactory<GateIoOrderBookOptions> PerpetualFuturesUsdt { get; }

        /// <inheritdoc />
        public ISymbolOrderBook CreateSpot(string symbol, Action<GateIoOrderBookOptions>? options = null)
            => new GateIoSpotSymbolOrderBook(symbol,
                                             options,
                                             _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                             _serviceProvider.GetRequiredService<IGateIoRestClient>(),
                                             _serviceProvider.GetRequiredService<IGateIoSocketClient>());


        /// <inheritdoc />
        public ISymbolOrderBook CreatePerpetualFutures(string settlementAsset, string symbol, Action<GateIoOrderBookOptions>? options = null)
            => new GateIoPerpetualFuturesSymbolOrderBook(settlementAsset,
                                             symbol,
                                             options,
                                             _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                             _serviceProvider.GetRequiredService<IGateIoRestClient>(),
                                             _serviceProvider.GetRequiredService<IGateIoSocketClient>());
    }
}
