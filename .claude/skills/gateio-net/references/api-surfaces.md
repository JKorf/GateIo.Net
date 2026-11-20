# API surfaces

## Client roots, symbols, and settlement

`client` below is a `GateIoRestClient`; `socketClient` is a `GateIoSocketClient`. REST provides `SpotApi`, `PerpetualFuturesApi`, and `AlphaApi` with `ExchangeData`, `Account`, and `Trading` groups, plus `RebateApi.Partner`. Sockets expose `SpotApi` and `PerpetualFuturesApi` directly.

Spot symbols and futures contracts use underscores, for example `ETH_USDT`. Most futures operations take settlement asset first (`usdt`, `btc`, or `usd` as supported), then contract. Keep the same settlement/contract for metadata, orders, positions, and cancellation. `GateIoExchange.FormatSymbol` formats a native symbol from shared assets/mode, but does not replace settlement selection.

Spot order types use `NewOrderType`; account selection uses `SpotAccountType`; futures position and margin settings have their own enums. Native futures placement accepts a `decimal` quantity in contracts. Convert to exposure with contract `Multiplier` and inspect size/price limits; decimal support does not mean any fractional size is valid for every contract.

## Spot REST

| User intent | GateIo.Net member |
|---|---|
| Get server time | `client.SpotApi.ExchangeData.GetServerTimeAsync()` |
| Get supported assets | `client.SpotApi.ExchangeData.GetAssetsAsync()` |
| Get one asset | `client.SpotApi.ExchangeData.GetAssetAsync("ETH")` |
| Get supported spot symbols | `client.SpotApi.ExchangeData.GetSymbolsAsync()` |
| Get one spot symbol | `client.SpotApi.ExchangeData.GetSymbolAsync("ETH_USDT")` |
| Get latest spot ticker | `client.SpotApi.ExchangeData.GetTickersAsync("ETH_USDT")` |
| Get all spot tickers | `client.SpotApi.ExchangeData.GetTickersAsync()` |
| Get spot order book | `client.SpotApi.ExchangeData.GetOrderBookAsync("ETH_USDT")` |
| Get recent spot trades | `client.SpotApi.ExchangeData.GetTradesAsync("ETH_USDT")` |
| Get spot klines/candles | `client.SpotApi.ExchangeData.GetKlinesAsync("ETH_USDT", KlineInterval.OneMinute)` |
| Get asset networks | `client.SpotApi.ExchangeData.GetNetworksAsync("ETH")` |
| Get discount tiers | `client.SpotApi.ExchangeData.GetDiscountTiersAsync()` |
| Get loan margin tiers | `client.SpotApi.ExchangeData.GetLoanMarginTiersAsync()` |
| Get lending symbols | `client.SpotApi.ExchangeData.GetLendingSymbolsAsync()` |
| Get one lending symbol | `client.SpotApi.ExchangeData.GetLendingSymbolAsync("ETH_USDT")` |
| Get spot balances | `client.SpotApi.Account.GetBalancesAsync()` |
| Get one spot asset balance | `client.SpotApi.Account.GetBalancesAsync("ETH")` |
| Get spot ledger | `client.SpotApi.Account.GetLedgerAsync(...)` |
| Generate deposit address | `client.SpotApi.Account.GenerateDepositAddressAsync("ETH")` |
| Get deposit history | `client.SpotApi.Account.GetDepositsAsync(...)` |
| Get withdrawal history | `client.SpotApi.Account.GetWithdrawalsAsync(...)` |
| Withdraw asset | `client.SpotApi.Account.WithdrawAsync(...)` |
| Cancel withdrawal | `client.SpotApi.Account.CancelWithdrawalAsync(withdrawalId)` |
| Transfer between accounts | `client.SpotApi.Account.TransferAsync(asset, from, to, quantity, ...)` |
| Get transfer status | `client.SpotApi.Account.GetTransferStatusAsync(...)` |
| Get trading fees | `client.SpotApi.Account.GetTradingFeeAsync(...)` |
| Get account total valuation | `client.SpotApi.Account.GetAccountBalancesAsync(...)` |
| Get small balances | `client.SpotApi.Account.GetSmallBalancesAsync()` |
| Convert small balances | `client.SpotApi.Account.ConvertSmallBalancesAsync(...)` |
| Get unified account info | `client.SpotApi.Account.GetUnifiedAccountInfoAsync(...)` |
| Get unified borrowable amount | `client.SpotApi.Account.GetUnifiedAccountBorrowableAsync("USDT")` |
| Get unified transferable amount | `client.SpotApi.Account.GetUnifiedAccountTransferableAsync("USDT")` |
| Borrow or repay unified account | `client.SpotApi.Account.UnifiedAccountBorrowOrRepayAsync(...)` |
| Get unified loans | `client.SpotApi.Account.GetUnifiedAccountLoansAsync(...)` |
| Get margin accounts | `client.SpotApi.Account.GetMarginAccountsAsync(...)` |
| Borrow or repay margin loan | `client.SpotApi.Account.BorrowOrRepayAsync(...)` |
| Set margin leverage | `client.SpotApi.Account.SetMarginLeverageAsync(...)` |
| Place spot order | `client.SpotApi.Trading.PlaceOrderAsync(...)` |
| Query spot order | `client.SpotApi.Trading.GetOrderAsync(symbol, orderId)` |
| Get open spot orders | `client.SpotApi.Trading.GetOpenOrdersAsync(...)` |
| Get spot orders | `client.SpotApi.Trading.GetOrdersAsync(open, symbol, ...)` |
| Cancel spot order | `client.SpotApi.Trading.CancelOrderAsync(symbol, orderId)` |
| Cancel all spot orders | `client.SpotApi.Trading.CancelAllOrdersAsync(symbol)` |
| Edit spot order | `client.SpotApi.Trading.EditOrderAsync(...)` |
| Get user trades | `client.SpotApi.Trading.GetUserTradesAsync(...)` |
| Place spot trigger order | `client.SpotApi.Trading.PlaceTriggerOrderAsync(...)` |
| Get spot trigger orders | `client.SpotApi.Trading.GetTriggerOrdersAsync(...)` |
| Cancel spot trigger order | `client.SpotApi.Trading.CancelTriggerOrderAsync(id)` |
| Place multiple spot orders | `client.SpotApi.Trading.PlaceMultipleOrderAsync(...)` |
| Edit multiple spot orders | `client.SpotApi.Trading.EditMultipleOrderAsync(...)` |

## Perpetual Futures REST

Perpetual futures methods use a settlement asset such as `"usdt"`, `"btc"`, or `"usd"` plus a contract such as `"ETH_USDT"`.

| User intent | GateIo.Net member |
|---|---|
| Get futures server time | `client.PerpetualFuturesApi.ExchangeData.GetServerTimeAsync()` |
| Get futures contracts | `client.PerpetualFuturesApi.ExchangeData.GetContractsAsync("usdt")` |
| Get one futures contract | `client.PerpetualFuturesApi.ExchangeData.GetContractAsync("usdt", "ETH_USDT")` |
| Get futures order book | `client.PerpetualFuturesApi.ExchangeData.GetOrderBookAsync("usdt", "ETH_USDT")` |
| Get futures trades | `client.PerpetualFuturesApi.ExchangeData.GetTradesAsync("usdt", "ETH_USDT")` |
| Get futures klines | `client.PerpetualFuturesApi.ExchangeData.GetKlinesAsync("usdt", "ETH_USDT", KlineInterval.OneMinute)` |
| Get futures index klines | `client.PerpetualFuturesApi.ExchangeData.GetIndexKlinesAsync(...)` |
| Get futures ticker | `client.PerpetualFuturesApi.ExchangeData.GetTickersAsync("usdt", "ETH_USDT")` |
| Get all futures tickers | `client.PerpetualFuturesApi.ExchangeData.GetTickersAsync("usdt")` |
| Get funding rate history | `client.PerpetualFuturesApi.ExchangeData.GetFundingRateHistoryAsync(...)` |
| Get insurance balance history | `client.PerpetualFuturesApi.ExchangeData.GetInsuranceBalanceHistoryAsync(...)` |
| Get contract stats | `client.PerpetualFuturesApi.ExchangeData.GetContractStatsAsync(...)` |
| Get index constituents | `client.PerpetualFuturesApi.ExchangeData.GetIndexConstituentsAsync(...)` |
| Get liquidations | `client.PerpetualFuturesApi.ExchangeData.GetLiquidationsAsync(...)` |
| Get risk limit tiers | `client.PerpetualFuturesApi.ExchangeData.GetRiskLimitTiersAsync(...)` |
| Get futures account | `client.PerpetualFuturesApi.Account.GetAccountAsync("usdt")` |
| Get futures ledger | `client.PerpetualFuturesApi.Account.GetLedgerAsync("usdt", ...)` |
| Set position mode | `client.PerpetualFuturesApi.Account.UpdatePositionModeAsync("usdt", dualMode)` |
| Set margin mode | `client.PerpetualFuturesApi.Account.SetMarginModeAsync("usdt", "ETH_USDT", MarginMode.Isolated)` |
| Get futures trading fee | `client.PerpetualFuturesApi.Account.GetTradingFeeAsync("usdt", "ETH_USDT")` |
| Get futures positions | `client.PerpetualFuturesApi.Trading.GetPositionsAsync("usdt", holding: true)` |
| Get one futures position | `client.PerpetualFuturesApi.Trading.GetPositionAsync("usdt", "ETH_USDT")` |
| Update position margin | `client.PerpetualFuturesApi.Trading.UpdatePositionMarginAsync(...)` |
| Set futures leverage | `client.PerpetualFuturesApi.Trading.UpdatePositionLeverageAsync("usdt", "ETH_USDT", leverage)` |
| Update risk limit | `client.PerpetualFuturesApi.Trading.UpdatePositionRiskLimitAsync(...)` |
| Get dual-mode positions | `client.PerpetualFuturesApi.Trading.GetDualModePositionsAsync(...)` |
| Place futures order | `client.PerpetualFuturesApi.Trading.PlaceOrderAsync(...)` |
| Place multiple futures orders | `client.PerpetualFuturesApi.Trading.PlaceMultipleOrderAsync(...)` |
| Get futures orders | `client.PerpetualFuturesApi.Trading.GetOrdersAsync(...)` |
| Get futures order | `client.PerpetualFuturesApi.Trading.GetOrderAsync(...)` |
| Cancel futures order | `client.PerpetualFuturesApi.Trading.CancelOrderAsync(...)` |
| Cancel all futures orders | `client.PerpetualFuturesApi.Trading.CancelAllOrdersAsync(...)` |
| Edit futures order | `client.PerpetualFuturesApi.Trading.EditOrderAsync(...)` |
| Get futures user trades | `client.PerpetualFuturesApi.Trading.GetUserTradesAsync(...)` |
| Get position close history | `client.PerpetualFuturesApi.Trading.GetPositionCloseHistoryAsync(...)` |
| Get liquidation history | `client.PerpetualFuturesApi.Trading.GetLiquidationHistoryAsync(...)` |
| Place futures trigger order | `client.PerpetualFuturesApi.Trading.PlaceTriggerOrderAsync(...)` |
| Get futures trigger orders | `client.PerpetualFuturesApi.Trading.GetTriggerOrdersAsync(...)` |
| Cancel futures trigger order | `client.PerpetualFuturesApi.Trading.CancelTriggerOrderAsync(...)` |

## Alpha REST

| User intent | GateIo.Net member |
|---|---|
| Get Alpha assets | `client.AlphaApi.ExchangeData.GetAssetsAsync(...)` |
| Get Alpha tickers | `client.AlphaApi.ExchangeData.GetTickersAsync(...)` |
| Get Alpha account info | `client.AlphaApi.Account.GetAccountInfoAsync()` |
| Get Alpha ledger | `client.AlphaApi.Account.GetLedgerAsync(...)` |
| Get Alpha quote | `client.AlphaApi.Trading.GetQuoteAsync(...)` |
| Place Alpha order | `client.AlphaApi.Trading.PlaceOrderAsync(...)` |
| Get Alpha orders | `client.AlphaApi.Trading.GetOrdersAsync(...)` |
| Get Alpha order | `client.AlphaApi.Trading.GetOrderAsync(...)` |

## Rebate REST

| User intent | GateIo.Net member |
|---|---|
| Get partner subordinates | `client.RebateApi.Partner.GetSubordinatesAsync()` |

## Spot WebSocket

| User intent | GateIo.Net member |
|---|---|
| Subscribe spot trades | `socketClient.SpotApi.SubscribeToTradeUpdatesAsync(symbol, handler)` |
| Subscribe many spot trades | `socketClient.SpotApi.SubscribeToTradeUpdatesAsync(symbols, handler)` |
| Subscribe spot ticker updates | `socketClient.SpotApi.SubscribeToTickerUpdatesAsync(symbol, handler)` |
| Subscribe many spot ticker updates | `socketClient.SpotApi.SubscribeToTickerUpdatesAsync(symbols, handler)` |
| Subscribe spot klines | `socketClient.SpotApi.SubscribeToKlineUpdatesAsync(symbol, interval, handler)` |
| Subscribe spot book ticker | `socketClient.SpotApi.SubscribeToBookTickerUpdatesAsync(symbol, handler)` |
| Subscribe spot order book | `socketClient.SpotApi.SubscribeToOrderBookUpdatesAsync(symbol, handler)` |
| Subscribe spot order book v2 | `socketClient.SpotApi.SubscribeToOrderBookV2UpdatesAsync(symbol, depth, handler)` |
| Subscribe spot partial order book | `socketClient.SpotApi.SubscribeToPartialOrderBookUpdatesAsync(symbol, depth, updateMs, handler)` |
| Subscribe spot order updates | `socketClient.SpotApi.SubscribeToOrderUpdatesAsync(handler)` |
| Subscribe spot user trade updates | `socketClient.SpotApi.SubscribeToUserTradeUpdatesAsync(handler)` |
| Subscribe spot balance updates | `socketClient.SpotApi.SubscribeToBalanceUpdatesAsync(handler)` |
| Subscribe margin balance updates | `socketClient.SpotApi.SubscribeToMarginBalanceUpdatesAsync(handler)` |
| Subscribe funding balance updates | `socketClient.SpotApi.SubscribeToFundingBalanceUpdatesAsync(handler)` |
| Subscribe cross-margin balance updates | `socketClient.SpotApi.SubscribeToCrossMarginBalanceUpdatesAsync(handler)` |
| Subscribe trigger order updates | `socketClient.SpotApi.SubscribeToTriggerOrderUpdatesAsync(handler)` |
| Socket API place spot order | `socketClient.SpotApi.PlaceOrderAsync(...)` |
| Socket API cancel spot order | `socketClient.SpotApi.CancelOrderAsync(...)` |
| Socket API get spot order | `socketClient.SpotApi.GetOrderAsync(...)` |
| Socket API get spot orders | `socketClient.SpotApi.GetOrdersAsync(...)` |

## Perpetual Futures WebSocket

| User intent | GateIo.Net member |
|---|---|
| Subscribe futures trades | `socketClient.PerpetualFuturesApi.SubscribeToTradeUpdatesAsync(settle, contract, handler)` |
| Subscribe futures ticker updates | `socketClient.PerpetualFuturesApi.SubscribeToTickerUpdatesAsync(settle, contract, handler)` |
| Subscribe futures book ticker | `socketClient.PerpetualFuturesApi.SubscribeToBookTickerUpdatesAsync(settle, contract, handler)` |
| Subscribe futures order book v2 | `socketClient.PerpetualFuturesApi.SubscribeToOrderBookV2UpdatesAsync(settle, contract, depth, handler)` |
| Subscribe futures order book | `socketClient.PerpetualFuturesApi.SubscribeToOrderBookUpdatesAsync(settle, contract, updateMs, depth, handler)` |
| Subscribe futures klines | `socketClient.PerpetualFuturesApi.SubscribeToKlineUpdatesAsync(settle, contract, interval, handler)` |
| Subscribe futures contract stats | `socketClient.PerpetualFuturesApi.SubscribeToContractStatsUpdatesAsync(settle, contract, interval, handler)` |
| Subscribe futures order updates | `socketClient.PerpetualFuturesApi.SubscribeToOrderUpdatesAsync(userId, settle, handler)` |
| Subscribe futures user trades | `socketClient.PerpetualFuturesApi.SubscribeToUserTradeUpdatesAsync(userId, settle, handler)` |
| Subscribe futures liquidations | `socketClient.PerpetualFuturesApi.SubscribeToUserLiquidationUpdatesAsync(userId, settle, handler)` |
| Subscribe futures auto-deleverage | `socketClient.PerpetualFuturesApi.SubscribeToUserAutoDeleverageUpdatesAsync(userId, settle, handler)` |
| Subscribe futures position close | `socketClient.PerpetualFuturesApi.SubscribeToPositionCloseUpdatesAsync(userId, settle, handler)` |
| Subscribe futures balance updates | `socketClient.PerpetualFuturesApi.SubscribeToBalanceUpdatesAsync(userId, settle, handler)` |
| Subscribe futures risk limit updates | `socketClient.PerpetualFuturesApi.SubscribeToReduceRiskLimitUpdatesAsync(userId, settle, handler)` |
| Subscribe futures position updates | `socketClient.PerpetualFuturesApi.SubscribeToPositionUpdatesAsync(userId, settle, handler)` |
| Subscribe futures trigger orders | `socketClient.PerpetualFuturesApi.SubscribeToTriggerOrderUpdatesAsync(userId, settle, handler)` |
| Subscribe futures ADL updates | `socketClient.PerpetualFuturesApi.SubscribeToAdlUpdatesAsync(settle, handler)` |
| Socket API place futures order | `socketClient.PerpetualFuturesApi.PlaceOrderAsync(...)` |
| Socket API cancel futures order | `socketClient.PerpetualFuturesApi.CancelOrderAsync(settle, orderId)` |
| Socket API get futures order | `socketClient.PerpetualFuturesApi.GetOrderAsync(settle, orderId)` |
| Socket API get futures orders | `socketClient.PerpetualFuturesApi.GetOrdersAsync(...)` |

## Shared API V2

Shared surfaces are `client.SpotApi.SharedApi`, `client.PerpetualFuturesApi.SharedApi`, `socketClient.SpotApi.SharedApi`, and `socketClient.PerpetualFuturesApi.SharedApi`. The DI aggregate `IGateIoSharedApiClient` exposes `SpotRest`, `PerpetualFuturesRest`, `SpotSocket`, and `PerpetualFuturesSocket`. Alpha, rebate, and detailed margin/unified workflows use native APIs.

| Surface | Representative capabilities |
| --- | --- |
| Spot REST | `IGetSpotSymbolsRest`, `IGetTickerRest`, `IGetAllTickersRest`, assets, balances, deposits/withdrawals, fees, `ITransferRest`, `IPlaceSpotOrderRest`, order/client-ID lifecycle and trigger orders |
| Perpetual futures REST | `IGetFuturesSymbolsRest`, `IGetTickerRest`, balances, `IPlaceFuturesOrderRest`, order/client-ID lifecycle, `IGetPositionsRest`, `ICloseFullPositionRest`, leverage, position mode/history, funding, TP/SL, trigger orders, and index klines |
| Spot socket subscriptions | `ISubscribeTickerSocket`, `ISubscribeTradesSocket`, `ISubscribeBookTickerSocket`, `ISubscribeKlinesSocket`, `ISubscribeOrderBookSocket`, `ISubscribeBalancesSocket`, `ISubscribeUserTradesSocket`, `ISubscribeSpotOrdersSocket` |
| Futures socket subscriptions | Public ticker/trade/book-ticker/kline/book capabilities, `ISubscribeBalancesSocket`, `ISubscribeFuturesOrdersSocket`, `ISubscribeUserTradesSocket`, `ISubscribePositionsSocket` |
| Socket order queries | Spot `IPlaceSpotOrderSocket` / `ICancelSpotOrderSocket`; futures `IPlaceFuturesOrderSocket` / `ICancelFuturesOrderSocket` |

Both REST surfaces provide candles, books, recent trades, and book tickers; perpetual futures also exposes historical trades. Do not assume every native API area has shared capability coverage.

For dynamic selection, call the aggregate's `GetCapability` with a descriptor such as `SharedCapabilities.Tickers.GetTicker.Rest` and the required trading mode. Check for a missing match before accessing `Capability` and `Options`. `GetCapabilities` enumerates implementations; `Discover()` describes supported operations. With multiple exchanges registered, resolve `IEnumerable<TCapability>` and select by exchange and supported mode.

### Catalogs, quantities, and exchange parameters

Use `IGetSpotSymbolsRest.GetSpotSymbolsAsync(new GetSymbolsRequest())` and `IGetFuturesSymbolsRest.GetFuturesSymbolsAsync(new GetSymbolsRequest())`. Futures symbol requests require `SettleAsset` in `ExchangeParameters`. After a successful request, read `SpotSymbolCatalog` or `FuturesSymbolCatalog`; populating one settlement group does not imply all settlements were loaded. Requests support base/quote asset type and subtype filters; metadata includes `DisplayName`, constraints, and classifications (`Crypto`, `Fiat`, `TradFi`; applicable `StableCoin`, `Equity`, `Commodity` subtypes).

Shared orders use `SharedOrderQuantity`. Spot limit orders and market sells use base-asset quantity; market buys use quote-asset quantity. Shared futures orders use `QuantityInContracts`. Spot shared books use `SharedQuantityType.BaseAsset`, while futures REST/socket books use `SharedQuantityType.Contracts`.

Many shared futures REST and socket requests require `SettleAsset`. Private futures subscription requests also require `UserId`, matching the credential owner. Socket order-query parameter requirements differ from subscription requirements, so inspect the selected capability's rules. Shared futures placement does not set leverage or margin mode inline; configure them separately through supported native/shared operations.

Shared REST uses `HttpResult<T>` / `HttpResult`, subscriptions use `WebSocketResult<UpdateSubscription>`, socket order queries use `QueryResult<T>` / `QueryResult`, and non-I/O symbol/cache helpers can return `ExchangeCallResult<T>`.

## Environments and helpers

Clients default to `GateIoEnvironment.Live`; select `GateIoEnvironment.Demo` explicitly with matching credentials for demo workflows. `CreateCustom` takes a name, REST address, spot socket address, and futures socket address.

Checkout-only: [public interfaces](../../../../GateIo.Net/Interfaces/Clients/), [local order books](../../../../GateIo.Net/SymbolOrderBooks/), and [tracker factory](../../../../GateIo.Net/GateIoTrackerFactory.cs). Use maintained local books when synchronized state is required rather than treating a delta as a full snapshot.
