# GateIo.Net AI API Quick Map

Use this file to route common user intents to the correct GateIo.Net client member. If a method name or parameter is not listed here, inspect `GateIo.Net/Interfaces/Clients/**` before generating code.

## Client Roots

| Intent | Use |
|---|---|
| REST calls | `new GateIoRestClient()` |
| WebSocket streams and socket API requests | `new GateIoSocketClient()` |
| API key authentication | `options.ApiCredentials = new GateIoCredentials("key", "secret")` |
| Live environment | `GateIoEnvironment.Live` |
| Demo/test environment | `GateIoEnvironment.Demo` |
| Dependency injection | `services.AddGateIo(options => { ... })` |
| Native spot symbol format | `"BTC_USDT"` / `"ETH_USDT"` |
| Perpetual futures settlement asset | `"usdt"`, `"btc"`, or `"usd"` |

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

## SharedApis

Use SharedApis for exchange-agnostic code across GateIo, Binance, Bybit, OKX, Kraken, and other CryptoExchange.Net libraries.

| User intent | GateIo.Net member or interface |
|---|---|
| Shared spot REST client | `new GateIoRestClient().SpotApi.SharedClient` |
| Shared perpetual futures REST client | `new GateIoRestClient().PerpetualFuturesApi.SharedClient` |
| Shared spot socket client | `new GateIoSocketClient().SpotApi.SharedClient` |
| Shared perpetual futures socket client | `new GateIoSocketClient().PerpetualFuturesApi.SharedClient` |
| Shared spot ticker REST | `ISpotTickerRestClient.GetSpotTickerAsync(new GetTickerRequest(symbol))` |
| Shared spot order REST | `ISpotOrderRestClient.PlaceSpotOrderAsync(...)` |
| Shared futures order REST | `IFuturesOrderRestClient.PlaceFuturesOrderAsync(...)` |
| Shared ticker socket | `ITickerSocketClient.SubscribeToTickerUpdatesAsync(...)` |
| Shared order book socket | `IOrderBookSocketClient.SubscribeToOrderBookUpdatesAsync(...)` |

For shared socket subscriptions, keep the concrete socket client and unsubscribe with `await socketClient.UnsubscribeAsync(subscription.Data)`.

## Result Handling

| Situation | Pattern |
|---|---|
| REST success check | `if (!result.Success) { Console.WriteLine(result.Error); return; }` |
| Socket subscription success check | `if (!sub.Success) { Console.WriteLine(sub.Error); return; }` |
| Read REST data | Read `result.Data` only after `result.Success` |
| Retry decision | Retry only when `result.Error?.IsTransient == true` |
| Cancellation | Pass `ct: cancellationToken` |

## Common Routing Pitfalls

| Do not use | Use instead |
|---|---|
| `GateIoClient` | `GateIoRestClient` |
| `ApiCredentials` | `GateIoCredentials` |
| `ETHUSDT` in native Gate.io calls | `ETH_USDT` |
| `UsdFuturesApi` / `CoinFuturesApi` | `PerpetualFuturesApi` |
| Futures call without settlement asset | Include `"usdt"`, `"btc"`, or `"usd"` first |
| Decimal futures order quantity | Integer contract quantity |
| `SpotApi.Margin` | `SpotApi.Account` / `SpotApi.ExchangeData` margin methods |
| `.Data` without `.Success` check | Check `.Success` first |
| `ITickerSocketClient.UnsubscribeAsync(...)` | Keep the concrete socket client and call `socketClient.UnsubscribeAsync(subscription.Data)` |
