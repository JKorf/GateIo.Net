# ![GateIo.Net](https://github.com/JKorf/GateIo.Net/blob/07cc75279de9dc4b9c7dbcf4df6bf6a8cc3cf9ef/GateIo.Net/Icon/icon.png) GateIo.Net  

[![.NET](https://img.shields.io/github/actions/workflow/status/JKorf/GateIo.Net/dotnet.yml?style=for-the-badge)](https://github.com/JKorf/GateIo.Net/actions/workflows/dotnet.yml) ![License](https://img.shields.io/github/license/JKorf/GateIo.Net?style=for-the-badge)

GateIo.Net is a client library for accessing the [Gate.io REST and Websocket API](https://www.gate.io/docs/developers/apiv4).
## Features
* Response data is mapped to descriptive models
* Input parameters and response values are mapped to discriptive enum values where possible
* Automatic websocket (re)connection management 
* Client side rate limiting 
* Client side order book implementation
* Support for managing different accounts
* Extensive logging
* Support for different environments
* Easy integration with other exchange client based on the CryptoExchange.Net base library
* Native AOT support

## Supported Frameworks
The library is targeting both `.NET Standard 2.0` and `.NET Standard 2.1` for optimal compatibility, as well as dotnet 8.0 and 9.0 to use the latest framework features.

|.NET implementation|Version Support|
|--|--|
|.NET Core|`2.0` and higher|
|.NET Framework|`4.6.1` and higher|
|Mono|`5.4` and higher|
|Xamarin.iOS|`10.14` and higher|
|Xamarin.Android|`8.0` and higher|
|UWP|`10.0.16299` and higher|
|Unity|`2018.1` and higher|

## Install the library

### NuGet 
[![NuGet version](https://img.shields.io/nuget/v/GateIo.net.svg?style=for-the-badge)](https://www.nuget.org/packages/GateIo.Net)  [![Nuget downloads](https://img.shields.io/nuget/dt/GateIo.Net.svg?style=for-the-badge)](https://www.nuget.org/packages/GateIo.Net)

	dotnet add package GateIo.Net
	
### GitHub packages
GateIo.Net is available on [GitHub packages](https://github.com/JKorf/GateIo.Net/pkgs/nuget/GateIo.Net). You'll need to add `https://nuget.pkg.github.com/JKorf/index.json` as a NuGet package source.

### Download release
[![GitHub Release](https://img.shields.io/github/v/release/JKorf/GateIo.Net?style=for-the-badge&label=GitHub)](https://github.com/JKorf/GateIo.Net/releases)

The NuGet package files are added along side the source with the latest GitHub release which can found [here](https://github.com/JKorf/GateIo.Net/releases).

	
## How to use
* REST Endpoints
	```csharp
	// Get the ETH/USDT ticker via rest request
	var restClient = new GateIoRestClient();
	var tickerResult = await restClient.SpotApi.ExchangeData.GetTickersAsync("ETH_USDT");
	var lastPrice = tickerResult.Data.First().LastPrice;
	```
* Websocket streams
	```csharp
	// Subscribe to ETH/USDT ticker updates via the websocket API
	var socketClient = new GateIoSocketClient();
	var tickerSubscriptionResult = socketClient.SpotApi.SubscribeToTickerUpdatesAsync("ETH_USDT", data =>
	{
		var lastPrice = data.Data.LastPrice;
	});
	```

For information on the clients, dependency injection, response processing and more see the [documentation](https://cryptoexchange.jkorf.dev?library=GateIo.Net), or have a look at the examples [here](https://github.com/JKorf/GateIo.Net/tree/main/Examples) or [here](https://github.com/JKorf/CryptoExchange.Net/tree/master/Examples).

## CryptoExchange.Net
GateIo.Net is based on the [CryptoExchange.Net](https://github.com/JKorf/CryptoExchange.Net) base library. Other exchange API implementations based on the CryptoExchange.Net base library are available and follow the same logic.

CryptoExchange.Net also allows for [easy access to different exchange API's](https://cryptoexchange.jkorf.dev/client-libs/shared).

|Exchange|Repository|Nuget|
|--|--|--|
|Binance|[JKorf/Binance.Net](https://github.com/JKorf/Binance.Net)|[![Nuget version](https://img.shields.io/nuget/v/Binance.net.svg?style=flat-square)](https://www.nuget.org/packages/Binance.Net)|
|BingX|[JKorf/BingX.Net](https://github.com/JKorf/BingX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.BingX.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.BingX.Net)|
|Bitfinex|[JKorf/Bitfinex.Net](https://github.com/JKorf/Bitfinex.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bitfinex.net.svg?style=flat-square)](https://www.nuget.org/packages/Bitfinex.Net)|
|Bitget|[JKorf/Bitget.Net](https://github.com/JKorf/Bitget.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.Bitget.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.Bitget.Net)|
|BitMart|[JKorf/BitMart.Net](https://github.com/JKorf/BitMart.Net)|[![Nuget version](https://img.shields.io/nuget/v/BitMart.net.svg?style=flat-square)](https://www.nuget.org/packages/BitMart.Net)|
|BitMEX|[JKorf/BitMEX.Net](https://github.com/JKorf/BitMEX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.BitMEX.net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.BitMEX.Net)|
|BloFin|[JKorf/BloFin.Net](https://github.com/JKorf/BloFin.Net)|[![Nuget version](https://img.shields.io/nuget/v/BloFin.net.svg?style=flat-square)](https://www.nuget.org/packages/BloFin.Net)|
|Bybit|[JKorf/Bybit.Net](https://github.com/JKorf/Bybit.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bybit.net.svg?style=flat-square)](https://www.nuget.org/packages/Bybit.Net)|
|Coinbase|[JKorf/Coinbase.Net](https://github.com/JKorf/Coinbase.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.Coinbase.Net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.Coinbase.Net)|
|CoinEx|[JKorf/CoinEx.Net](https://github.com/JKorf/CoinEx.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinEx.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinEx.Net)|
|CoinGecko|[JKorf/CoinGecko.Net](https://github.com/JKorf/CoinGecko.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinGecko.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinGecko.Net)|
|CoinW|[JKorf/CoinW.Net](https://github.com/JKorf/CoinW.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinW.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinW.Net)|
|Crypto.com|[JKorf/CryptoCom.Net](https://github.com/JKorf/CryptoCom.Net)|[![Nuget version](https://img.shields.io/nuget/v/CryptoCom.net.svg?style=flat-square)](https://www.nuget.org/packages/CryptoCom.Net)|
|DeepCoin|[JKorf/DeepCoin.Net](https://github.com/JKorf/DeepCoin.Net)|[![Nuget version](https://img.shields.io/nuget/v/DeepCoin.net.svg?style=flat-square)](https://www.nuget.org/packages/DeepCoin.Net)|
|HTX|[JKorf/HTX.Net](https://github.com/JKorf/HTX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.HTX.Net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.HTX.Net)|
|HyperLiquid|[JKorf/HyperLiquid.Net](https://github.com/JKorf/HyperLiquid.Net)|[![Nuget version](https://img.shields.io/nuget/v/HyperLiquid.Net.svg?style=flat-square)](https://www.nuget.org/packages/HyperLiquid.Net)|
|Kraken|[JKorf/Kraken.Net](https://github.com/JKorf/Kraken.Net)|[![Nuget version](https://img.shields.io/nuget/v/KrakenExchange.net.svg?style=flat-square)](https://www.nuget.org/packages/KrakenExchange.Net)|
|Kucoin|[JKorf/Kucoin.Net](https://github.com/JKorf/Kucoin.Net)|[![Nuget version](https://img.shields.io/nuget/v/Kucoin.net.svg?style=flat-square)](https://www.nuget.org/packages/Kucoin.Net)|
|Mexc|[JKorf/Mexc.Net](https://github.com/JKorf/Mexc.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.Mexc.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.Mexc.Net)|
|OKX|[JKorf/OKX.Net](https://github.com/JKorf/OKX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.OKX.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.OKX.Net)|
|Toobit|[JKorf/Toobit.Net](https://github.com/JKorf/Toobit.Net)|[![Nuget version](https://img.shields.io/nuget/v/Toobit.net.svg?style=flat-square)](https://www.nuget.org/packages/Toobit.Net)|
|WhiteBit|[JKorf/WhiteBit.Net](https://github.com/JKorf/WhiteBit.Net)|[![Nuget version](https://img.shields.io/nuget/v/WhiteBit.net.svg?style=flat-square)](https://www.nuget.org/packages/WhiteBit.Net)|
|XT|[JKorf/XT.Net](https://github.com/JKorf/XT.Net)|[![Nuget version](https://img.shields.io/nuget/v/XT.net.svg?style=flat-square)](https://www.nuget.org/packages/XT.Net)|

When using multiple of these API's the [CryptoClients.Net](https://github.com/JKorf/CryptoClients.Net) package can be used which combines this and the other packages and allows easy access to all exchange API's.

## Discord
[![Nuget version](https://img.shields.io/discord/847020490588422145?style=for-the-badge)](https://discord.gg/MSpeEtSY8t)  
A Discord server is available [here](https://discord.gg/MSpeEtSY8t). For discussion and/or questions around the CryptoExchange.Net and implementation libraries, feel free to join.

## Supported functionality
The following modules are supported of the latest V4 API.
### Account & Margin
|API|Supported|Location|
|--|--:|--|
|Withdrawal|✓|`restClient.SpotApi.Account`|
|Wallet|✓|`restClient.SpotApi.Account`|
|Subaccount|X||
|Unified|✓|`restClient.SpotApi.Account` / `restClient.SpotApi.ExchangeData`|
|Margin|✓|`restClient.SpotApi.Account` / `restClient.SpotApi.ExchangeData`|
|Marginuni|✓|`restClient.SpotApi.Account` / `restClient.SpotApi.ExchangeData`|
|Flash_swap|X||
|Earnuni|X||
|Collateral-Loan|X||
|Multi-Collateral-Loan|X||
|Earn|X||
|Account|✓|`restClient.SpotApi.Account`|
|Rebates|X||

### Spot Rest
|API|Supported|Location|
|--|--:|--|
|Account|✓|`restClient.SpotApi.Account`|
|Public data|✓|`restClient.SpotApi.ExchangeData`|
|Trading|✓|`restClient.SpotApi.Trading`|

### Spot Websocket
|API|Supported|Location|
|--|--:|--|
|Public data|✓|`socketClient.SpotApi`|
|Trading|✓|`socketClient.SpotApi`|

### Perpetual Futures Rest
|API|Supported|Location|
|--|--:|--|
|Account|✓|`restClient.PerpetualFuturesApi.Account`|
|Public data|✓|`restClient.PerpetualFuturesApi.ExchangeData`|
|Trading|✓|`restClient.PerpetualFuturesApi.Trading`|

### Perpetual Futures Websocket
|API|Supported|Location|
|--|--:|--|
|Public data|✓|`socketClient.PerpetualFuturesApi`|
|Trading|✓|`socketClient.PerpetualFuturesApi`|

### Delivery Futures
|API|Supported|Location|
|--|--:|--|
|*|X||

### Options
|API|Supported|Location|
|--|--:|--|
|*|X||

## Support the project
Any support is greatly appreciated.

### Donate
Make a one time donation in a crypto currency of your choice. If you prefer to donate a currency not listed here please contact me.

**Btc**:  bc1q277a5n54s2l2mzlu778ef7lpkwhjhyvghuv8qf  
**Eth**:  0xcb1b63aCF9fef2755eBf4a0506250074496Ad5b7   
**USDT (TRX)**  TKigKeJPXZYyMVDgMyXxMf17MWYia92Rjd

### Sponsor
Alternatively, sponsor me on Github using [Github Sponsors](https://github.com/sponsors/JKorf). 

## Release notes
* Version 2.9.1 - 02 Oct 2025
    * Fixed incorrect response model type for restClient.SpotApi.Account.GetIsolatedMarginAccountsAsync endpoint

* Version 2.9.0 - 30 Sep 2025
    * Updated CryptoExchange.Net version to 9.8.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added ITrackerFactory to TrackerFactory implementation
    * Added ContractAddress mapping in Shared IAssetClient implementation
    * Add support for Demo environment
    * Added socketClient.PerpetualFuturesApi.SubscribeToAdlUpdatesAsync subscription
    * Added Leverage property to GateIoLoanMarginTier response model
    * Added restClient.SpotApi.Account.GetIsolatedMarginAccountsAsync endpoint
    * Updated WithdrawalStatus enum values

* Version 2.8.1 - 10 Sep 2025
    * Added withdrawalId, assetClass and withdrawClientOrderId parameters to restClient.SpotApi.Account.GetWithdrawalsAsync endpoint

* Version 2.8.0 - 01 Sep 2025
    * Updated CryptoExchange.Net version to 9.7.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * HTTP REST requests will now use HTTP version 2.0 by default
    * Updated GateIoSpotSymbolOrderBook sync logic, fixing memory leak if syncing failed

* Version 2.7.0 - 25 Aug 2025
    * Updated CryptoExchange.Net version to 9.6.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added ClearUserClients method to user client provider

* Version 2.6.1 - 21 Aug 2025
    * Added websocket error mapping

* Version 2.6.0 - 20 Aug 2025
    * Updated CryptoExchange.Net to version 9.5.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added improved error parsing
    * Added Rebate endpoints
    * Updated rest request sending too prevent duplicate parameter serializationRemoved incorrect RequiredExchangeParameter notation on Shared SpotApi GetBookTickerAsync request
    * Fixed incorrect RequiredExchangeParameter in Shared spot GetBookTickerAsync endpoint

* Version 2.5.0 - 04 Aug 2025
    * Updated CryptoExchange.Net to version 9.4.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added support for multi-symbol Shared socket subscriptions
    * Added code parameter to restClient.SpotApi.Account.GetLedgerAsync, Added Code property to response

* Version 2.4.0 - 23 Jul 2025
    * Updated CryptoExchange.Net to version 9.3.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Updated websocket message matching
    * Added Deduction property to GateIoRiskLimitTier model
    * Added EnableTieredMaintenanceMargin property to GateIoFuturesAccount model
    * Added AverageMaintenanceRate to GateIoPosition model
    * Fixed incorrect orderbook subscription in shared futures SubscribeToOrderBookUpdatesAsync

* Version 2.3.0 - 15 Jul 2025
    * Updated CryptoExchange.Net to version 9.2.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added socketClient.SpotApi.SubscribeToOrderBookV2UpdatesAsync subscription

* Version 2.2.0 - 20 Jun 2025
    * Added DelistTime and TradeUrl properties to GateIoSymbol model
    * Added UpdateTime property to GateIoBalance model
    * Added QuantityFilled property to GateIoOrderUpdate
    * Added Spot websocket Order update Event property mapping to enum values
    * Updated OrderFinishType enum values

* Version 2.1.0 - 02 Jun 2025
    * Updated CryptoExchange.Net to version 9.1.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added (I)GateIoUserClientProvider allowing for easy client management when handling multiple users
    * Added restClient.PerpetualFuturesApi.Account.SetMarginModeAsync endpoint
    * Added OneSecond value to KlineInterval enum
    * Added socketClient.PerpetualFuturesApi.SubscribeToOrderBookV2UpdatesAsync subscription

* Version 2.0.0 - 13 May 2025
    * Updated CryptoExchange.Net to version 9.0.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added support for Native AOT compilation
    * Added RateLimitUpdated event
    * Added SharedSymbol response property to all Shared interfaces response models returning a symbol name
    * Added GenerateClientOrderId method to PerpetualFutures and Spot Shared clients
    * Added IBookTickerRestClient implementation to PerpetualFuturesApi and SpotApi Shared client
    * Added IFuturesOrderClientIdClient implementation to PerpetualFuturesApi Shared client
    * Added IFuturesTriggerOrderRestClient implementation to PerpetualFuturesApi Shared client
    * Added IFuturesTpSlRestClient implementation to PerpetualFuturesApi Shared client
    * Added ISpotOrderClientIdClient implementation to SpotApi Shared client
    * Added ISpotTriggerOrderRestClient implementation to SpotApi Shared client
    * Added MaxLongLeverage, MaxShortLeverage properties to SharedFuturesSymbol model
    * Added QuoteVolume property mapping to SharedSpotTicker model
    * Added OptionalExchangeParameters and Supported properties to EndpointOptions
    * Added All property to retrieve all available environment on GateIoEnvironment
    * Refactored Shared clients quantity parameters and responses to use SharedQuantity
    * Updated all IEnumerable response and model types to array response types
    * Updated order rate limiter to consider rate limits per symbol
    * Updated restClient.PerpetualFuturesApi.Account.GetAccountAsync response model
    * Removed Newtonsoft.Json dependency
    * Removed legacy ISpotClient implementation
    * Removed legacy AddGateIo(restOptions, socketOptions) DI overload
    * Fixed some typos
    * Fixed incorrect DataTradeMode on certain Shared interface responses
    * Fixed restClient.SpotApi.Account.GetTransferHistoryAsync limit parameter serialization
    * Fixed restClient.PerpetualFuturesApi.Trading.UpdateDualModePositionLeverageAsync endpoint

* Version 2.0.0-beta3 - 01 May 2025
    * Updated CryptoExchange.Net version to 9.0.0-beta5
    * Added property to retrieve all available API environments

* Version 2.0.0-beta2 - 23 Apr 2025
    * Updated CryptoExchange.Net to version 9.0.0-beta2
    * Added Shared spot ticker QuoteVolume mapping
    * Fixed incorrect DataTradeMode on responses

* Version 2.0.0-beta1 - 22 Apr 2025
    * Updated CryptoExchange.Net to version 9.0.0-beta1, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added support for Native AOT compilation
    * Added RateLimitUpdated event
    * Added SharedSymbol response property to all Shared interfaces response models returning a symbol name
    * Added GenerateClientOrderId method to PerpetualFutures and Spot Shared clients
    * Added IBookTickerRestClient implementation to PerpetualFuturesApi and SpotApi Shared client
    * Added IFuturesOrderClientIdClient implementation to PerpetualFuturesApi Shared client
    * Added IFuturesTriggerOrderRestClient implementation to PerpetualFuturesApi Shared client
    * Added IFuturesTpSlRestClient implementation to PerpetualFuturesApi Shared client
    * Added ISpotOrderClientIdClient implementation to SpotApi Shared client
    * Added ISpotTriggerOrderRestClient implementation to SpotApi Shared client
    * Added MaxLongLeverage, MaxShortLeverage properties to SharedFuturesSymbol model
    * Added OptionalExchangeParameters and Supported properties to EndpointOptions
    * Refactored Shared clients quantity parameters and responses to use SharedQuantity
    * Updated all IEnumerable response and model types to array response types
    * Updated order rate limiter to consider rate limits per symbol
    * Updated restClient.PerpetualFuturesApi.Account.GetAccountAsync response model
    * Removed Newtonsoft.Json dependency
    * Removed legacy ISpotClient implementation
    * Removed legacy AddGateIo(restOptions, socketOptions) DI overload
    * Fixed some typos
    * Fixed restClient.SpotApi.Account.GetTransferHistoryAsync limit parameter serialization
    * Fixed restClient.PerpetualFuturesApi.Trading.UpdateDualModePositionLeverageAsync endpoint

* Version 1.22.0 - 24 Mar 2025
    * Added BaseAssetName and QuoteAssetName to GateIoSymbol model
    * Added MarketId property to GateIoTradeUpdate and GateIoUserTradeUpdate models
    * Added socketClient.PerpetualFuturesApi.SubscribeToContractStatsUpdatesAs…ync subscription
    * Fixed shared PerpetualFuturesApi GetBalances returning error

* Version 1.21.0 - 07 Mar 2025
    * Added Networks info to GateIoAsset model
    * Updated shared rest GetAssetsAsync request with network info

* Version 1.20.1 - 03 Mar 2025
    * Fixed incorrect pricing parameters mapping for restClient.PerpetualFuturesApi.Trading.PlaceTriggerOrderAsync

* Version 1.20.0 - 18 Feb 2025
    * Added auto convert to lowercase for settlement asset parameter in futures endpoints and subscriptions
    * Added overloads for multiple contracts to some public futures Subscribe methods
    * Added socketClient.SpotApi.GetOrdersAsync query
    * Added CancellationToken parameter for futures websocket queries
    * Fixed websocket order operations always logging a warning message

* Version 1.19.0 - 11 Feb 2025
    * Updated CryptoExchange.Net to version 8.8.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added support for more SharedKlineInterval values
    * Added setting of DataTime value on websocket DataEvent updates
    * Added actionMode parameter to restClient.SpotApi.Trading.PlaceOrderAsync endpoint and socketClient.SpotApi.PlaceOrderAsync
    * Fix Mono runtime exception on rest client construction using DI
    * Marked cross margin endpoints as deprecated

* Version 1.18.0 - 22 Jan 2025
    * Added transactionType parameter to restClient.SpotApi.Account.GetTransferHistoryAsync endpoint
    * Added NumberOfOrders to restClient.PerpetualFuturesApi.ExchangeData.GetLiquidationsAsync response model
    * Added PreMarketStatus property to restClient.SpotApi.ExchangeData.GetSymbolsAsync response model
    * Fixed deserialization error in restClient.SpotApi.Account.GetSmallBalanceConversionsAsync

* Version 1.17.1 - 07 Jan 2025
    * Updated CryptoExchange.Net version
    * Added Type property to GateIoExchange class

* Version 1.17.0 - 03 Jan 2025
    * Added restClient.SpotApi.Account.GetInsuranceFundHistoryAsync endpoint
    * Added SingleAsset enum value for restClient.SpotApi.Account.SetUnifiedAccountModeAsync
    * Updated restClient.SpotApi.Account.GetUnifiedAccountInfoAsync() balance response model

* Version 1.16.0 - 23 Dec 2024
    * Updated CryptoExchange.Net to version 8.5.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added SetOptions methods on Rest and Socket clients
    * Added setting of DefaultProxyCredentials to CredentialCache.DefaultCredentials on the DI http client
    * Improved websocket disconnect detection

* Version 1.15.0 - 03 Dec 2024
    * Updated CryptoExchange.Net to version 8.4.3, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added restClient.SpotApi.Account.GetTransferStatusAsync endpoint
    * Added UpdateId to Position model
    * Removed socketClient.SpotApi.SubscribeToOrderBookUpdatesAsync updateMs parameter
    * Removed socketClient.PerpetualFuturesApi.SubscribeToOrderBookUpdatesAsync 1000ms updateMs and 5 and 10 depth valid parameter values
    * Fixed orderbook creation via GateIoBookFactory

* Version 1.14.0 - 28 Nov 2024
    * Updated CryptoExchange.Net to version 8.4.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.4.0
    * Added GetFeesAsync Shared REST client implementations
    * Updated GateIoOptions to LibraryOptions implementation
    * Updated UpdatePositionModeAsync response model
    * Updated test and analyzer package versions

* Version 1.13.0 - 19 Nov 2024
    * Updated CryptoExchange.Net to version 8.3.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.3.0
    * Added support for loading client settings from IConfiguration
    * Added DI registration method for configuring Rest and Socket options at the same time
    * Added DisplayName and ImageUrl properties to GateIoExchange class
    * Updated client constructors to accept IOptions from DI
    * Updated GateIoExchange.ExchangeName value from Gate.io to GateIo
    * Removed redundant GateIoSocketClient constructor

* Version 1.12.1 - 08 Nov 2024
    * Fixed restClient.PerpetualFuturesApi.Trading.PlaceTriggerOrderAsync parameter serialization

* Version 1.12.0 - 06 Nov 2024
    * Updated CryptoExchange.Net to version 8.2.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.2.0

* Version 1.11.0 - 04 Nov 2024
    * Added restClient.SpotApi.Account.GetUnifiedLeverageConfigsAsync endpoint
    * Added restClient.SpotApi.Account.GetUnifiedLeverageAsync endpoint
    * Added restClient.SpotApi.Account.SetUnifiedLeverageAsync endpoint
    * Added Id property to restClient.PerpetualFuturesApi.Account.GetLedgerAsync response model
    * Added Leverage property to restClient.SpotApi.ExchangeData.GetDiscountTiersAsync response model
    * Added BestAskQuantity, BestBidQuantity properties to restClient.SpotApi.ExchangeData.GetTickersAsync response model

* Version 1.10.0 - 28 Oct 2024
    * Updated CryptoExchange.Net to version 8.1.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.1.0
    * Moved FormatSymbol to GateIoExchange class
    * Added support Side setting on SharedTrade model
    * Added GateIoTrackerFactory for creating trackers
    * Added overload to Create method on GateIoOrderBookFactory support SharedSymbol parameter

* Version 1.9.0 - 21 Oct 2024
    * Added restClient.SpotApi.Account.GetRateLimitsAsync endpoint
    * Added support for clientOrderId to restClient.PerpetualFuturesApi.Trading.GetOrderAsync, CancelOrderAsync and EditOrderAsync endpoints

* Version 1.8.1 - 14 Oct 2024
    * Updated CryptoExchange.Net to version 8.0.3, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.0.3
    * Fixed TypeLoadException during initialization

* Version 1.8.0 - 14 Oct 2024
    * Fixed ICoinbaseOrderBookFactory DI lifetime
    * Added clientOrderId parameter to restClient.SpotApi.Trading.EditOrderAsync
    * Added clientOrderId parameter to socketClient.SpotApi.EditOrderAsync

* Version 1.7.0 - 08 Oct 2024
    * Added SpotApi.Account.GetTransferHistoryAsync endpoint
    * Added SpotApi.Account.TransferToAccountAsync endpoint
    * Added PerpetualFuturesApi.Trading.EditMultipleOrdersAsync endpoint
    * Added BestBidQuantity/BestAskQuantity properties to GateIoPerpTicker response model
    * Added startTime/endTime parameters to PerpetualFuturesApi.ExchangeData.GetFundingRateHistoryAsync and updated shared implementation to support pagination
    * Added support for clientOrderId to SpotApi.Trading.GetOrderAsync endpoint
    * Fixed some serialization issues in batch endpoints

* Version 1.6.0 - 27 Sep 2024
    * Updated CryptoExchange.Net to version 8.0.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.0.0
    * Added Shared client interfaces implementation for Spot and Perpetual Futures Rest and Socket clients
    * Added api credentials check for Spot user subscriptions
    * Added InitialMargin and MaintenanceMargin properties to GateIoPosition model
    * Added FeeAsset property to GateIoUserTradeUpdate model
    * Updated Sourcelink package version
    * Updated KlineInterval Enum values to match number of seconds
    * Fixed PerpetualFutures.Account.UpdatePositionModeAsync endpoint
    * Fixed SpotApi.ExchangeData.GetTradesAsync reverse parameter
    * Marked ISpotClient references as deprecated

* Version 1.5.1 - 11 Sep 2024
    * Added startTime and endTime filter to SpotApi.Account.GetUnifiedAccountInterestHistoryAsync endpoint
    * Added options to SpotApi.Account.SetUnifiedAccountModeAsync and GetUnifiedAccountModeAsync endpoints
    * Added BlockNumber field to SpotApi.Account.GetWithdrawalsAsync response

* Version 1.5.0 - 07 Aug 2024
    * Updated CryptoExchange.Net to version 7.11.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/7.11.0
    * Updated XML code comments

* Version 1.4.0 - 27 Jul 2024
    * Updated CryptoExchange.Net to version 7.10.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/7.10.0
    * Fixed FuturesApi.Trading.GetOrdersAsync status parameter being required

* Version 1.3.0 - 16 Jul 2024
    * Updated CryptoExchange.Net to version 7.9.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/7.9.0
    * Updated internal classes to internal access modifier
    * Added BorrowType property to SpotApi.Account.GetUnifiedAccountLoanHistoryAsync response model
    * Added AccumelatedSize to FuturesApi.Trading.GetPositionCloseHistoryAsync response model

* Version 1.2.1 - 02 Jul 2024
    * Updated CryptoExchange.Net to V7.8.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/7.8.0
    * Updated ratelimiting for per-endpoint limits

* Version 1.2.0 - 25 Jun 2024
    * Updated CryptoExchange.Net to 7.7.2, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/7.7.2
    * Added SpotApi.Account.GetGTDeductionStatusAsync endpoint
    * Added SpotApi.Account.SetGTDeductionStatusAsync endpoint

* Version 1.1.0 - 23 Jun 2024
    * Updated CryptoExchange.Net to version 7.7.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/7.7.0
    * Added dedicated connection configuration; a websocket connection can now be established before making the first request by calling `gateIoSocketClient.SpotApi.PrepareConnectionsAsync();`

* Version 1.0.1 - 13 Jun 2024
    * Fixed startTime/endTime filtering on multiple endpoints

* Version 1.0.0 - 12 Jun 2024
    * Initial release

