// 01-spot-quickstart.cs
//
// Demonstrates: client setup, public market data, authenticated balances,
// limit order placement, order status check.
//
// Setup:
//   dotnet new console -n SpotQuickstart && cd SpotQuickstart
//   dotnet add package GateIo.Net
//   Copy this file content into Program.cs
//   Substitute API_KEY / API_SECRET below
//   dotnet run

using GateIo.Net;
using GateIo.Net.Clients;
using GateIo.Net.Enums;
using GateIo.Net.Objects;

// ---- 1. PUBLIC CLIENT (no credentials needed for market data) ----
// Reuse this client across the application; do not create per-request.
var publicClient = new GateIoRestClient();

// Gate.io native symbol format uses an underscore: BTC_USDT, ETH_USDT, etc.
var tickerResult = await publicClient.SpotApi.ExchangeData.GetTickersAsync("ETH_USDT");
if (!tickerResult.Success)
{
    // .Error contains Code, Message, and may include exchange-specific data.
    Console.WriteLine($"Failed to get ticker: {tickerResult.Error}");
    return;
}

var ticker = tickerResult.Data.First();
Console.WriteLine($"ETH/USDT last price: {ticker.LastPrice}");
Console.WriteLine($"24h base volume: {ticker.BaseVolume} ETH");
Console.WriteLine($"24h quote volume: {ticker.QuoteVolume} USDT");

// ---- 2. AUTHENTICATED CLIENT (for account / trading) ----
var tradingClient = new GateIoRestClient(options =>
{
    options.ApiCredentials = new GateIoCredentials("API_KEY", "API_SECRET");
});

var balances = await tradingClient.SpotApi.Account.GetBalancesAsync();
if (!balances.Success)
{
    Console.WriteLine($"Failed to get balances: {balances.Error}");
    return;
}

foreach (var balance in balances.Data.Where(b => b.Available + b.Locked > 0))
{
    Console.WriteLine($"{balance.Asset}: {balance.Available} available, {balance.Locked} locked");
}

// ---- 3. PLACE A LIMIT BUY ORDER ----
// Limit, Buy, 0.01 ETH at a price 5% below current; likely will not fill immediately.
// Gate.io spot limit order quantity is in the base asset. For market buy orders,
// Gate.io uses quote-asset quantity instead.
var safePrice = Math.Round(ticker.LastPrice * 0.95m, 2);

var order = await tradingClient.SpotApi.Trading.PlaceOrderAsync(
    symbol: "ETH_USDT",
    side: OrderSide.Buy,
    type: NewOrderType.Limit,
    quantity: 0.01m,
    price: safePrice,
    timeInForce: TimeInForce.GoodTillCancel);

if (!order.Success)
{
    Console.WriteLine($"Failed to place order: {order.Error}");
    return;
}

Console.WriteLine($"Placed order {order.Data.Id} at {safePrice}, status: {order.Data.Status}");

// ---- 4. CHECK ORDER STATUS ----
var status = await tradingClient.SpotApi.Trading.GetOrderAsync("ETH_USDT", order.Data.Id);
if (status.Success)
{
    Console.WriteLine($"Order status: {status.Data.Status}, filled: {status.Data.QuantityFilled}");
}

// ---- 5. CANCEL THE ORDER (cleanup for this example) ----
var cancel = await tradingClient.SpotApi.Trading.CancelOrderAsync("ETH_USDT", order.Data.Id);
if (cancel.Success)
{
    Console.WriteLine($"Cancelled order {order.Data.Id}");
}

// Common variations:
//   Market order:       type: NewOrderType.Market, omit price and timeInForce
//   Spot trigger order: tradingClient.SpotApi.Trading.PlaceTriggerOrderAsync(...)
//   Open orders:        tradingClient.SpotApi.Trading.GetOpenOrdersAsync()
//   Margin account:     pass accountType: SpotAccountType.Margin where supported
//   Unified account:    tradingClient.SpotApi.Account.GetUnifiedAccountInfoAsync()
