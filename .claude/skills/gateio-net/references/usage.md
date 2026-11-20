# Usage examples

Snippets are independent. Those using `client` assume a reused `GateIoRestClient`, configured with credentials for private calls. Trading snippets change live state when executed; see [operational considerations](safety.md).

## Imports

```csharp
using System;
using System.Linq;
using System.Threading.Tasks;
using GateIo.Net;
using GateIo.Net.Clients;
using GateIo.Net.Enums;
using GateIo.Net.Interfaces.Clients;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.DependencyInjection;
```

## Public spot tickers and results

```csharp
using var client = new GateIoRestClient();
var result = await client.SpotApi.ExchangeData.GetTickersAsync("ETH_USDT");
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}
Console.WriteLine(result.Data.FirstOrDefault()?.LastPrice);
```

## Credentials and balances

```csharp
using var client = new GateIoRestClient(options =>
{
    options.ApiCredentials = new GateIoCredentials("API_KEY", "API_SECRET");
});
var balances = await client.SpotApi.Account.GetBalancesAsync();
if (!balances.Success)
{
    Console.WriteLine(balances.Error);
    return;
}
foreach (var balance in balances.Data)
    Console.WriteLine($"{balance.Asset}: available={balance.Available}");
```

## Spot limit order and cancellation

```csharp
var order = await client.SpotApi.Trading.PlaceOrderAsync(
    "ETH_USDT", OrderSide.Buy, NewOrderType.Limit, quantity: 0.01m,
    price: 1000m, timeInForce: TimeInForce.GoodTillCancel);
if (!order.Success)
{
    Console.WriteLine(order.Error);
    return;
}
var cancel = await client.SpotApi.Trading.CancelOrderAsync("ETH_USDT", order.Data.Id);
if (!cancel.Success)
    Console.WriteLine(cancel.Error);
```

## Futures contract metadata

```csharp
var contract = await client.PerpetualFuturesApi.ExchangeData.GetContractAsync("usdt", "ETH_USDT");
if (!contract.Success)
{
    Console.WriteLine(contract.Error);
    return;
}
Console.WriteLine($"{contract.Data.Name}: multiplier={contract.Data.Multiplier}");
```

## Futures limit order

The quantity below is a contract count. Select settlement, contract, size, and account position/margin configuration for the intended trade.

```csharp
var order = await client.PerpetualFuturesApi.Trading.PlaceOrderAsync(
    settlementAsset: "usdt", contract: "ETH_USDT", orderSide: OrderSide.Buy,
    quantity: 1m, price: 1000m, timeInForce: TimeInForce.GoodTillCancel);
if (!order.Success)
{
    Console.WriteLine(order.Error);
    return;
}
Console.WriteLine(order.Data.Id);
```

## Public subscription lifecycle

```csharp
using var socketClient = new GateIoSocketClient();
var subscription = await socketClient.SpotApi.SubscribeToTickerUpdatesAsync(
    "ETH_USDT", update => Console.WriteLine(update.Data.LastPrice));
if (!subscription.Success)
{
    Console.WriteLine(subscription.Error);
    return;
}
try
{
    await Task.Delay(TimeSpan.FromSeconds(30));
}
finally
{
    await socketClient.UnsubscribeAsync(subscription.Data);
}
```

## Private futures positions

Replace the user ID with the owner of the configured API credentials.

```csharp
using var socketClient = new GateIoSocketClient(options =>
{
    options.ApiCredentials = new GateIoCredentials("API_KEY", "API_SECRET");
});
var subscription = await socketClient.PerpetualFuturesApi.SubscribeToPositionUpdatesAsync(
    userId: 123456, settlementAsset: "usdt",
    onMessage: update => Console.WriteLine($"Position updates: {update.Data.Length}"));
if (!subscription.Success)
{
    Console.WriteLine(subscription.Error);
    return;
}
try
{
    await Task.Delay(TimeSpan.FromSeconds(30));
}
finally
{
    await socketClient.UnsubscribeAsync(subscription.Data);
}
```

## Socket order query

This submits an authenticated order; its query result is not a subscription.

```csharp
using var socketClient = new GateIoSocketClient(options =>
{
    options.ApiCredentials = new GateIoCredentials("API_KEY", "API_SECRET");
});
var order = await socketClient.SpotApi.PlaceOrderAsync(
    "ETH_USDT", OrderSide.Buy, NewOrderType.Limit, quantity: 0.01m, price: 1000m);
if (!order.Success)
{
    Console.WriteLine(order.Error);
    return;
}
Console.WriteLine(order.Data.Id);
```

## Shared API V2

```csharp
IGetTickerRest ticker = client.SpotApi.SharedApi;
var symbol = new SharedSymbol(TradingMode.Spot, "ETH", "USDT");
var result = await ticker.GetTickerAsync(new GetTickerRequest(symbol));
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}
Console.WriteLine(result.Data.LastPrice);
```

See [API surfaces](api-surfaces.md#shared-api-v2) for catalogs, quantities, settlement parameters, and runtime selection.

## Dependency injection

```csharp
var services = new ServiceCollection();
services.AddGateIo(options =>
{
    options.Rest.ApiCredentials = new GateIoCredentials("API_KEY", "API_SECRET");
    options.Socket.ApiCredentials = new GateIoCredentials("API_KEY", "API_SECRET");
});
using var provider = services.BuildServiceProvider();
var client = provider.GetRequiredService<IGateIoRestClient>();
var shared = provider.GetRequiredService<IGateIoSharedApiClient>();
```

## Complete examples (checkout-only)

The documentation test compiles these programs without executing exchange calls. Running them can contact Gate.io and execute illustrated orders and account changes.

- [Spot quickstart](../../../../Examples/ai-friendly/01-spot-quickstart.cs)
- [Futures](../../../../Examples/ai-friendly/02-futures.cs)
- [WebSockets](../../../../Examples/ai-friendly/03-websocket.cs)
- [Shared API](../../../../Examples/ai-friendly/04-multi-exchange.cs)
- [Error handling](../../../../Examples/ai-friendly/05-error-handling.cs)
