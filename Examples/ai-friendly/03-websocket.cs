// 03-websocket.cs
//
// Demonstrates: WebSocket subscriptions - public ticker, klines,
// authenticated spot updates, and a futures ticker stream. Includes proper teardown.
//
// Setup: dotnet add package GateIo.Net

using GateIo.Net;
using GateIo.Net.Clients;
using GateIo.Net.Enums;
using GateIo.Net.Objects;

// ---- 1. PUBLIC SOCKET CLIENT - for market data streams ----
// Reuse a single client instance across subscriptions.
var publicSocket = new GateIoSocketClient();

var tickerSub = await publicSocket.SpotApi.SubscribeToTickerUpdatesAsync(
    "ETH_USDT",
    update =>
    {
        Console.WriteLine($"ETH: {update.Data.LastPrice} (24h quote vol {update.Data.QuoteVolume:F2})");
    });

if (!tickerSub.Success)
{
    Console.WriteLine($"Failed to subscribe ticker: {tickerSub.Error}");
    return;
}

var klineSub = await publicSocket.SpotApi.SubscribeToKlineUpdatesAsync(
    "ETH_USDT",
    KlineInterval.OneMinute,
    update =>
    {
        if (update.Data.Final)
        {
            Console.WriteLine($"ETH 1m closed: O={update.Data.OpenPrice} H={update.Data.HighPrice} L={update.Data.LowPrice} C={update.Data.ClosePrice}");
        }
    });

if (!klineSub.Success)
{
    Console.WriteLine($"Failed to subscribe klines: {klineSub.Error}");
    await publicSocket.UnsubscribeAsync(tickerSub.Data);
    return;
}

// Futures public subscriptions use settlement asset + contract.
var futuresTickerSub = await publicSocket.PerpetualFuturesApi.SubscribeToTickerUpdatesAsync(
    "usdt",
    "ETH_USDT",
    update =>
    {
        var first = update.Data.FirstOrDefault();
        if (first != null)
            Console.WriteLine($"Futures {first.Contract}: {first.LastPrice}");
    });

if (!futuresTickerSub.Success)
{
    Console.WriteLine($"Failed to subscribe futures ticker: {futuresTickerSub.Error}");
    await publicSocket.UnsubscribeAsync(tickerSub.Data);
    await publicSocket.UnsubscribeAsync(klineSub.Data);
    return;
}

// ---- 2. AUTHENTICATED SOCKET CLIENT - for user data ----
// Spot authenticated streams push order, trade, and balance updates.
var authSocket = new GateIoSocketClient(options =>
{
    options.ApiCredentials = new GateIoCredentials("API_KEY", "API_SECRET");
});

var orderSub = await authSocket.SpotApi.SubscribeToOrderUpdatesAsync(
    update =>
    {
        foreach (var order in update.Data)
            Console.WriteLine($"Order {order.Id} {order.Symbol}: event={order.Event}, filled={order.QuantityFilled}/{order.Quantity}");
    });

if (!orderSub.Success)
{
    Console.WriteLine($"Failed to subscribe order updates: {orderSub.Error}");
    await publicSocket.UnsubscribeAsync(tickerSub.Data);
    await publicSocket.UnsubscribeAsync(klineSub.Data);
    await publicSocket.UnsubscribeAsync(futuresTickerSub.Data);
    return;
}

var balanceSub = await authSocket.SpotApi.SubscribeToBalanceUpdatesAsync(
    update =>
    {
        foreach (var balance in update.Data)
            Console.WriteLine($"Balance update {balance.Asset}: available={balance.Available} total={balance.Total}");
    });

if (!balanceSub.Success)
{
    Console.WriteLine($"Failed to subscribe balance updates: {balanceSub.Error}");
    await publicSocket.UnsubscribeAsync(tickerSub.Data);
    await publicSocket.UnsubscribeAsync(klineSub.Data);
    await publicSocket.UnsubscribeAsync(futuresTickerSub.Data);
    await authSocket.UnsubscribeAsync(orderSub.Data);
    return;
}

Console.WriteLine("All subscriptions active. Press Enter to teardown...");
Console.ReadLine();

// ---- 3. TEARDOWN - IMPORTANT! ----
// Always unsubscribe on shutdown to release resources cleanly.
await publicSocket.UnsubscribeAsync(tickerSub.Data);
await publicSocket.UnsubscribeAsync(klineSub.Data);
await publicSocket.UnsubscribeAsync(futuresTickerSub.Data);
await authSocket.UnsubscribeAsync(orderSub.Data);
await authSocket.UnsubscribeAsync(balanceSub.Data);

Console.WriteLine("Clean shutdown complete.");

// Common variations:
//   Multiple symbols:       SubscribeToTickerUpdatesAsync(new[] { "BTC_USDT", "ETH_USDT" }, handler)
//   Spot order book:        SubscribeToPartialOrderBookUpdatesAsync(symbol, depth, updateMs, handler)
//   Spot user trades:       authSocket.SpotApi.SubscribeToUserTradeUpdatesAsync(handler)
//   Futures user streams:   require userId, for example SubscribeToPositionUpdatesAsync(userId, "usdt", handler)
