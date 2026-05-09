# Copilot Instructions for GateIo.Net

This repository is **GateIo.Net** - a strongly typed C#/.NET client library for the Gate.io REST and WebSocket APIs. It is part of the CryptoExchange.Net ecosystem.

When generating code that consumes GateIo.Net, follow these conventions:

## Use GateIo.Net, not raw HTTP

Never generate `HttpClient` calls to Gate.io endpoints. Always use `GateIoRestClient` or `GateIoSocketClient`. This ensures correct request signing, rate limiting, and error handling.

## Client setup

```csharp
using GateIo.Net;
using GateIo.Net.Clients;
using GateIo.Net.Objects;

var restClient = new GateIoRestClient(options =>
{
    options.ApiCredentials = new GateIoCredentials("API_KEY", "API_SECRET");
});
```

## Result handling

Methods return `WebCallResult<T>` (REST) or `CallResult<T>` (WebSocket). Always check `.Success` before reading `.Data`. The error is on `.Error`.

## API structure

- `restClient.SpotApi.ExchangeData` - public spot market data, assets, symbols, tickers, order books, trades, klines, lending symbol data
- `restClient.SpotApi.Account` - balances, ledger, deposits, withdrawals, transfers, unified account and margin account endpoints
- `restClient.SpotApi.Trading` - spot orders, trigger orders, batch orders, user trades
- `restClient.PerpetualFuturesApi.ExchangeData` - perpetual futures contracts, tickers, order books, trades, klines, funding and risk data
- `restClient.PerpetualFuturesApi.Account` - futures account, ledger, position mode, margin mode, futures fees
- `restClient.PerpetualFuturesApi.Trading` - futures positions, leverage, orders, trigger orders, user trades
- `restClient.AlphaApi.*` - Alpha assets, tickers, account, quote and order endpoints
- `restClient.RebateApi.Partner` - partner subordinate endpoints
- `socketClient.SpotApi` - spot WebSocket subscriptions and socket order requests
- `socketClient.PerpetualFuturesApi` - perpetual futures WebSocket subscriptions and socket order requests

## Gate.io naming

Gate.io spot symbols use underscores, for example `ETH_USDT`. Perpetual futures methods require a settlement asset such as `"usdt"`, `"btc"`, or `"usd"` plus a contract such as `"ETH_USDT"`.

## Order placement

Use GateIo.Net enums: `OrderSide`, `NewOrderType`, `TimeInForce`, `SpotAccountType`, `MarginMode`, `PositionMode`, and related GateIo enums. For spot market buy orders, the `quantity` parameter is in quote asset, matching the Gate.io API.

## WebSocket pattern

Store the returned `UpdateSubscription` and unsubscribe on shutdown via `socketClient.UnsubscribeAsync(sub.Data)`.

## Cross-exchange

For code that needs to work across multiple exchanges, use `CryptoExchange.Net.SharedApis` interfaces (`ISpotTickerRestClient`, `IFuturesOrderRestClient`, etc.) accessed via `.SharedClient` properties. Same pattern works for 25+ other exchanges in the CryptoExchange.Net family.

## Avoid

- Legacy `GateIoClient` class (use `GateIoRestClient`)
- Generic `ApiCredentials` (use `GateIoCredentials`)
- Synchronous `.Result` / `.Wait()` (use `await`)
- Instantiating clients per-request (use DI, reuse instances)
- Manual ticker polling when a WebSocket subscription fits
- Binance-style symbols like `ETHUSDT` in Gate.io code; use `ETH_USDT`
- Calling futures methods without the settlement asset parameter

## Reference

For detailed patterns and pitfalls see `AGENTS.md` and `llms.txt` in the repository root, `llms-full.txt` for expanded context, `docs/ai-api-map.md` for table-style intent routing, and `Examples/ai-friendly/` for compilable examples.
