---
name: gateio-net
description: Implement or review GateIo.Net spot, perpetual futures, margin and unified accounts, Alpha and rebate workflows, REST calls, WebSocket subscriptions and order queries, authentication, Shared API integration, and dependency injection in .NET applications.
---
# GateIo.Net

Install with `dotnet add package GateIo.Net`.

- Reuse `GateIoRestClient` and `GateIoSocketClient`, or use dependency injection. REST has `SpotApi`, `PerpetualFuturesApi`, `AlphaApi`, and `RebateApi`; sockets have spot and perpetual futures roots.
- Private operations use `GateIoCredentials(key, secret)` without a passphrase. Public market data needs no credentials.
- Native symbols/contracts use underscores, such as `ETH_USDT`. Futures methods also take settlement asset strings such as `usdt`, `btc`, or `usd`; preserve settlement routing across related operations.
- REST returns `HttpResult<T>` / `HttpResult`; subscriptions return `WebSocketResult<UpdateSubscription>`; socket order/query methods return `QueryResult<T>` / `QueryResult`. Check `Success` before reading `Data` and handle `Error`. Inspect individual batch outcomes after outer success.
- Spot limit quantity is in base asset, while market-buy quantity is in quote asset. Futures quantities are contract counts, accepted as `decimal` by current native placement methods; use contract metadata for sizing and constraints.
- Keep the concrete socket client and unsubscribe successful subscriptions on shutdown. Socket order/query results are not subscriptions. Private futures streams require user ID and settlement asset in addition to credentials.
- Use supported roots' `SharedApi` for Shared API V2 and depend on the required capability interface. Shared futures requests require operation-specific exchange parameters; read their rules before constructing portable requests.

## References

- Read [API surfaces](references/api-surfaces.md) for routing, settlement/account selection, Shared API capabilities, symbol catalogs, and quantities.
- Read [usage](references/usage.md) for authentication, market/account reads, trading, public/private subscription cleanup, socket queries, Shared API V2, and DI snippets.
- Read [operational considerations](references/safety.md) for quantity units, margin and position modes, nested outcomes, submission recovery, and fund movements.

Checkout-only: [repository instructions](../../../AGENTS.md) and [complete examples](../../../Examples/ai-friendly/).
