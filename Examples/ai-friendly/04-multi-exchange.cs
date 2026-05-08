// 04-multi-exchange.cs
//
// Demonstrates: writing exchange-agnostic code using CryptoExchange.Net.SharedApis.
// Same code works against GateIo, Binance, OKX, Bybit, Kraken, and other exchanges
// from the CryptoExchange.Net family.
//
// Setup:
//   dotnet add package GateIo.Net
//   dotnet add package Binance.Net    // optional, for a Binance comparison
//   dotnet add package JK.OKX.Net     // optional, for an OKX comparison

using GateIo.Net.Clients;
using CryptoExchange.Net.SharedApis;

// ---- THE PATTERN ----
// Each exchange client exposes a `.SharedClient` property on its API surfaces.
// SharedClient implements interfaces like ISpotTickerRestClient, ISpotOrderRestClient,
// IBalanceRestClient, etc. - a common abstraction across all exchanges.

ISpotTickerRestClient gateIoShared = new GateIoRestClient().SpotApi.SharedClient;

// To add Binance or OKX, install the package and:
//   ISpotTickerRestClient binanceShared = new BinanceRestClient().SpotApi.SharedClient;
//   ISpotTickerRestClient okxShared     = new OKXRestClient().UnifiedApi.SharedClient;

// Common symbol type handles formatting differences automatically.
// Gate.io uses "ETH_USDT", Binance uses "ETHUSDT", OKX uses "ETH-USDT".
var ethusdt = new SharedSymbol(TradingMode.Spot, "ETH", "USDT");

await PrintTicker(gateIoShared, ethusdt);
// await PrintTicker(binanceShared, ethusdt);
// await PrintTicker(okxShared, ethusdt);

// ---- AGNOSTIC METHOD - works against any exchange ----
async Task PrintTicker(ISpotTickerRestClient client, SharedSymbol symbol)
{
    var result = await client.GetSpotTickerAsync(new GetTickerRequest(symbol));
    if (!result.Success)
    {
        Console.WriteLine($"[{client.Exchange}] Failed: {result.Error}");
        return;
    }

    Console.WriteLine($"[{client.Exchange}] {result.Data.Symbol}: {result.Data.LastPrice}");
}

// ---- WHY THIS MATTERS ----
// You can build:
//   - Multi-exchange arbitrage scanners
//   - Best-execution routers
//   - Unified portfolio dashboards
//   - Exchange comparison tools
// without writing per-exchange branches everywhere.

// ---- AVAILABLE SHARED INTERFACES ON GATEIO.NET ----
// REST:
//   ISpotTickerRestClient, ISpotSymbolRestClient, ISpotOrderRestClient
//   ISpotOrderClientIdRestClient, ISpotTriggerOrderRestClient
//   IFuturesOrderRestClient, IFuturesSymbolRestClient, IFuturesTriggerOrderRestClient
//   IFuturesTpSlRestClient, IBalanceRestClient, IFeeRestClient
//   IOrderBookRestClient, IRecentTradeRestClient, IKlineRestClient
//   IDepositRestClient, IWithdrawalRestClient, IWithdrawRestClient
//   ITransferRestClient, IBookTickerRestClient
// WebSocket:
//   ITickerSocketClient, IBookTickerSocketClient
//   IOrderBookSocketClient, ITradeSocketClient, IKlineSocketClient
//   IUserTradeSocketClient, IBalanceSocketClient, ISpotOrderSocketClient,
//   IFuturesOrderSocketClient, IPositionSocketClient

// ---- WEBSOCKET EXAMPLE - SHARED SUBSCRIPTION ----
var gateIoSocket = new GateIoSocketClient();
ITickerSocketClient gateIoTickerSocket = gateIoSocket.SpotApi.SharedClient;

var sub = await gateIoTickerSocket.SubscribeToTickerUpdatesAsync(
    new SubscribeTickerRequest(ethusdt),
    update => Console.WriteLine($"[{gateIoTickerSocket.Exchange}] {update.Data.Symbol}: {update.Data.LastPrice}"));

if (!sub.Success)
{
    Console.WriteLine($"Subscribe failed: {sub.Error}");
    return;
}

Console.WriteLine("Press Enter to exit");
Console.ReadLine();

await gateIoSocket.UnsubscribeAsync(sub.Data);

// Common variations:
//   Multi-exchange arbitrage:  loop over List<ISpotTickerRestClient>, find max bid / min ask
//   Cross-exchange orderbook:  IOrderBookSocketClient on each exchange, merge into composite book
//   Best execution:            ISpotOrderRestClient on N exchanges, route by liquidity
//   Futures comparison:        use new GateIoRestClient().PerpetualFuturesApi.SharedClient
