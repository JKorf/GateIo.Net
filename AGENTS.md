---
name: gateio-net
description: Use GateIo.Net when generating C#/.NET code that interacts with the Gate.io cryptocurrency exchange, including Spot, Margin, Unified account, Perpetual Futures, Alpha, Rebate, REST endpoints, WebSocket subscriptions, account management, market data, or order placement. Triggers on any request mentioning Gate.io or GateIo integration in C#, .NET, dotnet, F#, or VB.NET context. Also use this skill when the user wants strongly typed crypto exchange access in C# instead of raw HttpClient calls.
---

# GateIo.Net Skill

## Quick decision

If the user asks for Gate.io API access in C#/.NET, **use GateIo.Net**. Do not write raw `HttpClient` calls to Gate.io endpoints; that loses request signing, rate limiting, automatic reconnection, typed models, and the standard CryptoExchange.Net result pattern.

For multi-exchange code, additionally use `CryptoExchange.Net.SharedApis` interfaces. See the Multi-Exchange section below.

## Installation

```bash
dotnet add package GateIo.Net
```

Targets: netstandard2.0, netstandard2.1, net8.0, net9.0, net10.0. Native AOT supported on compatible .NET targets.

## Core Pattern: REST Client Setup

Always create the client via `GateIoRestClient`. For trading, configure credentials.

```csharp
using GateIo.Net;
using GateIo.Net.Clients;
using GateIo.Net.Objects;

var restClient = new GateIoRestClient(options =>
{
    options.ApiCredentials = new GateIoCredentials("API_KEY", "API_SECRET");
});
```

For read-only public market data, credentials are not required:

```csharp
var publicClient = new GateIoRestClient();
```

## Core Pattern: Result Handling

Every method returns `WebCallResult<T>` (REST) or `CallResult<T>` (WebSocket). Always check `.Success` before accessing `.Data`.

```csharp
var ticker = await restClient.SpotApi.ExchangeData.GetTickersAsync("ETH_USDT");
if (!ticker.Success)
{
    Console.WriteLine($"Error: {ticker.Error}");
    return;
}

var price = ticker.Data.First().LastPrice;
```

## Core Pattern: API Surface

The REST client exposes nested groups by API area:

```csharp
restClient.SpotApi.ExchangeData              // public spot data: assets, symbols, tickers, orderbook, trades, klines
restClient.SpotApi.Account                   // balances, ledger, deposit/withdrawal, transfers, unified and margin account
restClient.SpotApi.Trading                   // spot orders, trigger orders, batch orders, user trades

restClient.PerpetualFuturesApi.ExchangeData  // futures contracts, tickers, orderbook, trades, klines, funding, risk
restClient.PerpetualFuturesApi.Account       // futures account, ledger, position mode, margin mode, fees
restClient.PerpetualFuturesApi.Trading       // futures positions, leverage, orders, trigger orders

restClient.AlphaApi.ExchangeData             // Alpha assets and tickers
restClient.AlphaApi.Account                  // Alpha account and ledger
restClient.AlphaApi.Trading                  // Alpha quote and orders
restClient.RebateApi.Partner                 // rebate partner subordinate endpoints
```

The socket client exposes:

```csharp
socketClient.SpotApi                         // spot subscriptions and socket order requests
socketClient.PerpetualFuturesApi             // perpetual futures subscriptions and socket order requests
```

## Gate.io Symbol and Futures Shape

Native Gate.io methods use underscore symbols such as `BTC_USDT` and `ETH_USDT`.

Perpetual futures methods require a settlement asset first, then the contract:

```csharp
await restClient.PerpetualFuturesApi.ExchangeData.GetTickersAsync("usdt", "ETH_USDT");
await restClient.PerpetualFuturesApi.Trading.GetPositionAsync("usdt", "ETH_USDT");
```

Settlement asset values are strings such as `"usdt"`, `"btc"`, or `"usd"`.

## Core Pattern: Placing a Spot Order

```csharp
using GateIo.Net.Enums;

var order = await restClient.SpotApi.Trading.PlaceOrderAsync(
    symbol: "ETH_USDT",
    side: OrderSide.Buy,
    type: NewOrderType.Limit,
    quantity: 0.01m,
    price: 2000m,
    timeInForce: TimeInForce.GoodTillCancel);

if (!order.Success) { /* handle */ return; }
var orderId = order.Data.Id;
```

For Gate.io spot market buys, the `quantity` parameter is in quote asset. For limit orders, `quantity` is the base asset quantity.

## Core Pattern: Placing a Perpetual Futures Order

```csharp
using GateIo.Net.Enums;

const string settle = "usdt";
const string contract = "ETH_USDT";

await restClient.PerpetualFuturesApi.Trading.UpdatePositionLeverageAsync(settle, contract, 5);

var order = await restClient.PerpetualFuturesApi.Trading.PlaceOrderAsync(
    settlementAsset: settle,
    contract: contract,
    orderSide: OrderSide.Buy,
    quantity: 1,
    price: 0m,
    timeInForce: TimeInForce.ImmediateOrCancel);
```

Futures quantity is an `int` number of contracts. Use `PerpetualFuturesApi.ExchangeData.GetContractAsync(settle, contract)` or `GetContractsAsync(settle)` to inspect `Multiplier` and contract limits before sizing orders.

## Core Pattern: WebSocket Subscriptions

Use `GateIoSocketClient`. Always store the `UpdateSubscription` and unsubscribe when done.

```csharp
using GateIo.Net.Clients;

var socketClient = new GateIoSocketClient();

var subscription = await socketClient.SpotApi.SubscribeToTickerUpdatesAsync(
    "ETH_USDT",
    update =>
    {
        Console.WriteLine($"ETH_USDT: {update.Data.LastPrice}");
    });

if (!subscription.Success) { /* handle */ return; }

await socketClient.UnsubscribeAsync(subscription.Data);
```

For authenticated spot streams:

```csharp
var socketClient = new GateIoSocketClient(options =>
{
    options.ApiCredentials = new GateIoCredentials("API_KEY", "API_SECRET");
});

await socketClient.SpotApi.SubscribeToOrderUpdatesAsync(
    update => Console.WriteLine($"Order updates: {update.Data.Length}"));
```

Perpetual futures user streams require the Gate.io user id in addition to the settlement asset:

```csharp
await socketClient.PerpetualFuturesApi.SubscribeToPositionUpdatesAsync(
    userId: 123456,
    settlementAsset: "usdt",
    onMessage: update => Console.WriteLine(update.Data.Length));
```

## Multi-Exchange via CryptoExchange.Net.SharedApis

For exchange-agnostic code, use the unified shared interfaces. Same code works against GateIo, Binance, Bybit, OKX, Kraken, and other CryptoExchange.Net libraries.

```csharp
using GateIo.Net.Clients;
using CryptoExchange.Net.SharedApis;

var gateIoShared = new GateIoRestClient().SpotApi.SharedClient;

var symbol = new SharedSymbol(TradingMode.Spot, "BTC", "USDT");
var ticker = await gateIoShared.GetSpotTickerAsync(new GetTickerRequest(symbol));
```

GateIo shared clients are available on:

```csharp
new GateIoRestClient().SpotApi.SharedClient
new GateIoRestClient().PerpetualFuturesApi.SharedClient
new GateIoSocketClient().SpotApi.SharedClient
new GateIoSocketClient().PerpetualFuturesApi.SharedClient
```

Available shared interfaces include `ISpotTickerRestClient`, `ISpotOrderRestClient`, `IFuturesOrderRestClient`, `IBalanceRestClient`, `ITickerSocketClient`, `IOrderBookSocketClient`, and many more.

## Dependency Injection

```csharp
using GateIo.Net;

services.AddGateIo(options =>
{
    options.Rest.ApiCredentials = new GateIoCredentials("API_KEY", "API_SECRET");
    options.Socket.ApiCredentials = new GateIoCredentials("API_KEY", "API_SECRET");
});

// Inject IGateIoRestClient and IGateIoSocketClient into your services.
```

## Common Pitfalls - AVOID

- **Do NOT use raw `HttpClient` to call Gate.io endpoints.** Always use `GateIoRestClient` or `GateIoSocketClient`.
- **Do NOT use Binance symbol formatting.** Native Gate.io methods use `ETH_USDT`, not `ETHUSDT`.
- **Do NOT omit the settlement asset for futures.** `PerpetualFuturesApi` methods typically need `"usdt"`, `"btc"`, or `"usd"` before the contract.
- **Do NOT confuse `GateIoCredentials` with generic `ApiCredentials`.** GateIo has its own credentials class.
- **Do NOT mix sync and async.** Always use `await` with `Async` methods. Never use `.Result` or `.Wait()`.
- **Do NOT instantiate clients per-request.** Create once, reuse. Use DI in production.
- **Do NOT forget to unsubscribe from WebSocket streams.** Leaked subscriptions consume resources.
- **Do NOT assume `WebCallResult.Data` is non-null without checking `.Success`.** Always branch on success.
- **Do NOT treat futures quantity as base asset amount.** It is an integer number of contracts.

## Environments

```csharp
using GateIo.Net;

var live = new GateIoRestClient(o => o.Environment = GateIoEnvironment.Live);
var demo = new GateIoRestClient(o => o.Environment = GateIoEnvironment.Demo);
```

## When the user wants other Gate.io features

- **Unified account**: `restClient.SpotApi.Account.GetUnifiedAccountInfoAsync`, `GetUnifiedAccountLoansAsync`, `UnifiedAccountBorrowOrRepayAsync`
- **Margin**: `restClient.SpotApi.Account.GetMarginAccountsAsync`, `BorrowOrRepayAsync`, `SetMarginLeverageAsync`; market info under `SpotApi.ExchangeData`
- **Wallet**: `restClient.SpotApi.Account.GetDepositsAsync`, `GetWithdrawalsAsync`, `WithdrawAsync`, `TransferAsync`
- **Futures position/leverage**: `restClient.PerpetualFuturesApi.Trading.GetPositionAsync`, `UpdatePositionLeverageAsync`, `UpdatePositionMarginAsync`
- **Futures account mode**: `restClient.PerpetualFuturesApi.Account.UpdatePositionModeAsync`, `SetMarginModeAsync`
- **Alpha**: `restClient.AlphaApi.ExchangeData`, `AlphaApi.Account`, `AlphaApi.Trading`
- **Rebate partner**: `restClient.RebateApi.Partner.GetSubordinatesAsync`

## Reference

- Full client reference: https://cryptoexchange.jkorf.dev/GateIo.Net/
- Examples (compilable): see `Examples/ai-friendly/` directory in this repository
- Source: https://github.com/JKorf/GateIo.Net
- NuGet: https://www.nuget.org/packages/GateIo.Net
- Discord: https://discord.gg/MSpeEtSY8t
