// 05-error-handling.cs
//
// Demonstrates: WebCallResult patterns, retry logic, common Gate.io routing
// and validation scenarios.
//
// Setup: dotnet add package GateIo.Net

using GateIo.Net;
using GateIo.Net.Clients;
using GateIo.Net.Enums;
using GateIo.Net.Objects;
using CryptoExchange.Net.Objects;

var client = new GateIoRestClient(options =>
{
    options.ApiCredentials = new GateIoCredentials("API_KEY", "API_SECRET");
});

// ---- 1. THE BASIC PATTERN ----
// Every method returns WebCallResult<T> (REST) or CallResult<T> (WebSocket).
// .Success is true/false. .Data is the payload (only valid when .Success).
// .Error contains structured error info when .Success is false.
// .Error.IsTransient hints if a retry might succeed (rate limit, network, 5xx).

var result = await client.SpotApi.ExchangeData.GetTickersAsync("ETH_USDT");

if (result.Success)
{
    Console.WriteLine($"Price: {result.Data.First().LastPrice}");
}
else
{
    Console.WriteLine($"Code:      {result.Error?.Code}");
    Console.WriteLine($"Message:   {result.Error?.Message}");
    Console.WriteLine($"Type:      {result.Error?.ErrorType}");
    Console.WriteLine($"Transient: {result.Error?.IsTransient}");
}

// ---- 2. SIMPLE RETRY WITH BACKOFF ----
// Retry only on transient errors (rate limit, network blip, server overload).
// Do not retry on validation errors or insufficient balance; they will repeat.

async Task<WebCallResult<T>> WithRetry<T>(
    Func<Task<WebCallResult<T>>> call,
    int maxAttempts = 3)
{
    WebCallResult<T> last = default!;
    for (int attempt = 1; attempt <= maxAttempts; attempt++)
    {
        last = await call();
        if (last.Success) return last;
        if (last.Error?.IsTransient != true) return last;

        // Exponential backoff: 0.5s, 1s, 2s.
        await Task.Delay(TimeSpan.FromMilliseconds(250 * Math.Pow(2, attempt)));
    }

    return last;
}

var ticker = await WithRetry(
    () => client.SpotApi.ExchangeData.GetTickersAsync("ETH_USDT"));

if (ticker.Success)
    Console.WriteLine($"Retried ticker price: {ticker.Data.First().LastPrice}");

// ---- 3. COMMON GATE.IO ERROR SCENARIOS ----
//
// Wrong symbol format:
//   Native Gate.io methods use "ETH_USDT", not "ETHUSDT".
//   SharedApis can normalize symbols, but concrete GateIo.Net methods expect Gate.io format.
//
// Missing futures settlement asset:
//   PerpetualFuturesApi methods generally require settlementAsset first:
//   "usdt", "btc", or "usd".
//
// Wrong futures quantity type:
//   Futures PlaceOrderAsync quantity is an int number of contracts.
//   Use ExchangeData.GetContractAsync to inspect the contract multiplier.
//
// Missing credentials:
//   Account, trading, withdrawal, and authenticated socket endpoints require GateIoCredentials.
//
// Permanent validation errors:
//   Invalid price/quantity/time-in-force/account type/leverage should be fixed before retrying.
//
// Transient failures:
//   Network, server, or rate-limit failures may be retried only when Error.IsTransient is true.

// ---- 4. SYMBOL AND CONTRACT METADATA CHECKS ----
var symbolInfo = await client.SpotApi.ExchangeData.GetSymbolAsync("ETH_USDT");
if (!symbolInfo.Success)
{
    Console.WriteLine($"Cannot fetch spot symbol info: {symbolInfo.Error}");
    return;
}

var contractInfo = await client.PerpetualFuturesApi.ExchangeData.GetContractAsync("usdt", "ETH_USDT");
if (!contractInfo.Success)
{
    Console.WriteLine($"Cannot fetch futures contract info: {contractInfo.Error}");
    return;
}

Console.WriteLine($"Futures contract multiplier: {contractInfo.Data.Multiplier}");

// ---- 5. ORDER PLACEMENT WITH EXPLICIT RESULT HANDLING ----
var order = await client.SpotApi.Trading.PlaceOrderAsync(
    symbol: "ETH_USDT",
    side: OrderSide.Buy,
    type: NewOrderType.Limit,
    quantity: 0.01m,
    price: 2000m,
    timeInForce: TimeInForce.GoodTillCancel);

if (!order.Success)
{
    string category = order.Error?.IsTransient == true
        ? "Transient - retry with backoff"
        : "Permanent - surface to caller or fix request";

    Console.WriteLine($"{category}: {order.Error?.Code} {order.Error?.Message}");
    return;
}

Console.WriteLine($"Placed order {order.Data.Id}");

// ---- 6. EXCEPTIONS VS ERROR RESULTS ----
// GateIo.Net returns API, rate-limit, and network errors via WebCallResult.Error,
// not via thrown exceptions. Exceptions are generally for:
//   - Misconfiguration, for example disposed clients or missing required arguments
//   - OperationCanceledException when CancellationToken is triggered
//   - Programmer errors
// API errors, validation failures, and rate limits -> all on .Error.

// Common variations:
//   With CancellationToken:    pass `ct: cancellationToken` to any method
//   With timeout per request:  options.RequestTimeout = TimeSpan.FromSeconds(10)
//   Polly integration:         use IsTransient as the IsTransientPredicate
