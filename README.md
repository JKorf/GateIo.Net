# ![GateIo.Net](https://github.com/JKorf/GateIo.Net/blob/07cc75279de9dc4b9c7dbcf4df6bf6a8cc3cf9ef/GateIo.Net/Icon/icon.png) GateIo.Net  

[![.NET](https://img.shields.io/github/actions/workflow/status/JKorf/GateIo.Net/dotnet.yml?style=for-the-badge)](https://github.com/JKorf/GateIo.Net/actions/workflows/dotnet.yml) ![License](https://img.shields.io/github/license/JKorf/GateIo.Net?style=for-the-badge)

GateIo.Net is a client library for accessing the [Gate.io REST and Websocket API](https://www.gate.io/docs/developers/apiv4).
## Features
* Response data is mapped to descriptive models
* Input parameters and response values are mapped to discriptive enum values where possible
* Automatic websocket (re)connection management 
* Client side rate limiting 
* Client side order book implementation
* Extensive logging
* Support for different environments
* Easy integration with other exchange client based on the CryptoExchange.Net base library

## Supported Frameworks
The library is targeting both `.NET Standard 2.0` and `.NET Standard 2.1` for optimal compatibility

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

For information on the clients, dependency injection, response processing and more see the [documentation](https://jkorf.github.io/GateIo.Net), or have a look at the examples [here](https://github.com/JKorf/GateIo.Net/tree/main/Examples) or [here](https://github.com/JKorf/CryptoExchange.Net/tree/master/Examples).

## CryptoExchange.Net
GateIo.Net is based on the [CryptoExchange.Net](https://github.com/JKorf/CryptoExchange.Net) base library. Other exchange API implementations based on the CryptoExchange.Net base library are available and follow the same logic.

CryptoExchange.Net also allows for [easy access to different exchange API's](https://jkorf.github.io/CryptoExchange.Net#idocs_common).

|Exchange|Repository|Nuget|
|--|--|--|
|Binance|[JKorf/Binance.Net](https://github.com/JKorf/Binance.Net)|[![Nuget version](https://img.shields.io/nuget/v/Binance.net.svg?style=flat-square)](https://www.nuget.org/packages/Binance.Net)|
|BingX|[JKorf/BingX.Net](https://github.com/JKorf/BingX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.BingX.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.BingX.Net)|
|Bitfinex|[JKorf/Bitfinex.Net](https://github.com/JKorf/Bitfinex.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bitfinex.net.svg?style=flat-square)](https://www.nuget.org/packages/Bitfinex.Net)|
|Bitget|[JKorf/Bitget.Net](https://github.com/JKorf/Bitget.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.Bitget.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.Bitget.Net)|
|BitMart|[JKorf/BitMart.Net](https://github.com/JKorf/BitMart.Net)|[![Nuget version](https://img.shields.io/nuget/v/BitMart.net.svg?style=flat-square)](https://www.nuget.org/packages/BitMart.Net)|
|Bybit|[JKorf/Bybit.Net](https://github.com/JKorf/Bybit.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bybit.net.svg?style=flat-square)](https://www.nuget.org/packages/Bybit.Net)|
|CoinEx|[JKorf/CoinEx.Net](https://github.com/JKorf/CoinEx.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinEx.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinEx.Net)|
|CoinGecko|[JKorf/CoinGecko.Net](https://github.com/JKorf/CoinGecko.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinGecko.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinGecko.Net)|
|Huobi/HTX|[JKorf/Huobi.Net](https://github.com/JKorf/Huobi.Net)|[![Nuget version](https://img.shields.io/nuget/v/Huobi.net.svg?style=flat-square)](https://www.nuget.org/packages/Huobi.Net)|
|Kraken|[JKorf/Kraken.Net](https://github.com/JKorf/Kraken.Net)|[![Nuget version](https://img.shields.io/nuget/v/KrakenExchange.net.svg?style=flat-square)](https://www.nuget.org/packages/KrakenExchange.Net)|
|Kucoin|[JKorf/Kucoin.Net](https://github.com/JKorf/Kucoin.Net)|[![Nuget version](https://img.shields.io/nuget/v/Kucoin.net.svg?style=flat-square)](https://www.nuget.org/packages/Kucoin.Net)|
|Mexc|[JKorf/Mexc.Net](https://github.com/JKorf/Mexc.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.Mexc.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.Mexc.Net)|
|OKX|[JKorf/OKX.Net](https://github.com/JKorf/OKX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.OKX.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.OKX.Net)|

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
I develop and maintain this package on my own for free in my spare time, any support is greatly appreciated.

### Donate
Make a one time donation in a crypto currency of your choice. If you prefer to donate a currency not listed here please contact me.

**Btc**:  bc1q277a5n54s2l2mzlu778ef7lpkwhjhyvghuv8qf  
**Eth**:  0xcb1b63aCF9fef2755eBf4a0506250074496Ad5b7   
**USDT (TRX)**  TKigKeJPXZYyMVDgMyXxMf17MWYia92Rjd

### Sponsor
Alternatively, sponsor me on Github using [Github Sponsors](https://github.com/sponsors/JKorf). 

## Release notes
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

