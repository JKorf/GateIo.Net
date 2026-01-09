using CryptoExchange.Net.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using GateIo.Net.Interfaces;
using GateIo.Net.Interfaces.Clients;
using GateIo.Net.Objects.Options;
using CryptoExchange.Net.OrderBook;
using CryptoExchange.Net.SharedApis;

namespace GateIo.Net.SymbolOrderBooks
{
    /// <summary>
    /// GateIo order book factory
    /// </summary>
    public class GateIoOrderBookFactory : IGateIoOrderBookFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <inheritdoc />
        public string ExchangeName => GateIoExchange.ExchangeName;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public GateIoOrderBookFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            Spot = new OrderBookFactory<GateIoOrderBookOptions>(
                CreateSpot,
                (sharedSymbol, options) => CreateSpot(GateIoExchange.FormatSymbol(sharedSymbol.BaseAsset, sharedSymbol.QuoteAsset, sharedSymbol.TradingMode, sharedSymbol.DeliverTime), options));
            PerpetualFuturesBtc = new OrderBookFactory<GateIoOrderBookOptions>(
                (symbol, options) => CreatePerpetualFutures("btc", symbol, options),
                (sharedSymbol, options) => CreatePerpetualFutures("btc", GateIoExchange.FormatSymbol(sharedSymbol.BaseAsset, sharedSymbol.QuoteAsset, sharedSymbol.TradingMode, sharedSymbol.DeliverTime), options));
            PerpetualFuturesUsd = new OrderBookFactory<GateIoOrderBookOptions>(
                (symbol, options) => CreatePerpetualFutures("usd", symbol, options),
                (sharedSymbol, options) => CreatePerpetualFutures("usd", GateIoExchange.FormatSymbol(sharedSymbol.BaseAsset, sharedSymbol.QuoteAsset, sharedSymbol.TradingMode, sharedSymbol.DeliverTime), options));
            PerpetualFuturesUsdt = new OrderBookFactory<GateIoOrderBookOptions>(
                (symbol, options) => CreatePerpetualFutures("usdt", symbol, options),
                (sharedSymbol, options) => CreatePerpetualFutures("usdt", GateIoExchange.FormatSymbol(sharedSymbol.BaseAsset, sharedSymbol.QuoteAsset, sharedSymbol.TradingMode, sharedSymbol.DeliverTime), options));
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
        public ISymbolOrderBook Create(SharedSymbol symbol, string? settlementAsset = null, Action<GateIoOrderBookOptions>? options = null)
        {
            var symbolName = symbol.GetSymbol(GateIoExchange.FormatSymbol);
            if (symbol.TradingMode == TradingMode.Spot)
                return CreateSpot(symbolName, options);

            return CreatePerpetualFutures(settlementAsset ?? "usd", symbolName, options);
        }

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
