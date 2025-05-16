using CryptoExchange.Net.Testing;
using NUnit.Framework;
using System.Threading.Tasks;
using GateIo.Net.Clients;
using GateIo.Net.Objects.Models;
using System.Collections.Generic;

namespace Gate.io.Net.UnitTests
{
    [TestFixture]
    public class SocketSubscriptionTests
    {
        [Test]
        public async Task ValidateSpotSubscriptions()
        {
            var client = new GateIoSocketClient(opts =>
            {
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new SocketSubscriptionValidator<GateIoSocketClient>(client, "Subscriptions/Spot", "wss://api.gateio.ws", "result");
            await tester.ValidateAsync<GateIoTickerUpdate>((client, handler) => client.SpotApi.SubscribeToTickerUpdatesAsync("ETH_USDT", handler), "SubscribeToTickerUpdates", ignoreProperties: new List<string> { "time" });
            await tester.ValidateAsync<GateIoTradeUpdate>((client, handler) => client.SpotApi.SubscribeToTradeUpdatesAsync("ETH_USDT", handler), "SubscribeToTradeUpdates", ignoreProperties: new List<string> { "time", "create_time" });
            await tester.ValidateAsync<GateIoKlineUpdate>((client, handler) => client.SpotApi.SubscribeToKlineUpdatesAsync("ETH_USDT", GateIo.Net.Enums.KlineInterval.OneMinute, handler), "SubscribeToKlineUpdates", ignoreProperties: new List<string> { "time" });
            await tester.ValidateAsync<GateIoBookTickerUpdate>((client, handler) => client.SpotApi.SubscribeToBookTickerUpdatesAsync("ETH_USDT", handler), "SubscribeToBookTickerUpdates", ignoreProperties: new List<string> { "time" });
            await tester.ValidateAsync<GateIoOrderBookUpdate>((client, handler) => client.SpotApi.SubscribeToOrderBookUpdatesAsync("ETH_USDT", handler), "SubscribeToOrderBookUpdates", ignoreProperties: new List<string> { "time", "E" });
            await tester.ValidateAsync<GateIoPartialOrderBookUpdate>((client, handler) => client.SpotApi.SubscribeToPartialOrderBookUpdatesAsync("ETH_USDT", 5, 1000, handler), "SubscribeToPartialOrderBookUpdates", ignoreProperties: new List<string> { "time", "E" });
            await tester.ValidateAsync<GateIoOrderUpdate[]>((client, handler) => client.SpotApi.SubscribeToOrderUpdatesAsync(handler), "SubscribeToOrderUpdates", ignoreProperties: new List<string> { "time", "create_time", "update_time", "biz_info" });
            await tester.ValidateAsync<GateIoUserTradeUpdate[]>((client, handler) => client.SpotApi.SubscribeToUserTradeUpdatesAsync(handler), "SubscribeToUserTradeUpdates", ignoreProperties: new List<string> { "time", "create_time" });
            await tester.ValidateAsync<GateIoBalanceUpdate[]>((client, handler) => client.SpotApi.SubscribeToBalanceUpdatesAsync(handler), "SubscribeToBalanceUpdates", ignoreProperties: new List<string> { "time", "timestamp" });
            await tester.ValidateAsync<GateIoMarginBalanceUpdate[]>((client, handler) => client.SpotApi.SubscribeToMarginBalanceUpdatesAsync(handler), "SubscribeToMarginBalanceUpdates", ignoreProperties: new List<string> { "time", "timestamp" });
            await tester.ValidateAsync<GateIoFundingBalanceUpdate[]>((client, handler) => client.SpotApi.SubscribeToFundingBalanceUpdatesAsync(handler), "SubscribeToFundingBalanceUpdates", ignoreProperties: new List<string> { "time", "timestamp" });
            await tester.ValidateAsync<GateIoCrossMarginBalanceUpdate[]>((client, handler) => client.SpotApi.SubscribeToCrossMarginBalanceUpdatesAsync(handler), "SubscribeToCrossMarginBalanceUpdates", ignoreProperties: new List<string> { "time", "timestamp" });
            await tester.ValidateAsync<GateIoTriggerOrderUpdate>((client, handler) => client.SpotApi.SubscribeToTriggerOrderUpdatesAsync(handler), "SubscribeToTriggerOrderUpdates", ignoreProperties: new List<string> { "time", "timestamp" });
        }

        [Test]
        public async Task ValidatePerpFuturesSubscriptions()
        {
            var client = new GateIoSocketClient(opts =>
            {
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new SocketSubscriptionValidator<GateIoSocketClient>(client, "Subscriptions/Futures", "wss://fx-ws.gateio.ws", "result");
            await tester.ValidateAsync<GateIoPerpTickerUpdate[]>((client, handler) => client.PerpetualFuturesApi.SubscribeToTickerUpdatesAsync("usdt", "ETH_USDT", handler), "SubscribeToTickerUpdates", ignoreProperties: new List<string> { "time" });
            await tester.ValidateAsync<GateIoPerpTradeUpdate[]>((client, handler) => client.PerpetualFuturesApi.SubscribeToTradeUpdatesAsync("usdt", "ETH_USDT", handler), "SubscribeToTradeUpdates", ignoreProperties: new List<string> { "time", "create_time" });
            await tester.ValidateAsync<GateIoPerpBookTickerUpdate>((client, handler) => client.PerpetualFuturesApi.SubscribeToBookTickerUpdatesAsync("usdt", "ETH_USDT", handler), "SubscribeToBookTickerUpdates", ignoreProperties: new List<string> { "time" });
            await tester.ValidateAsync<GateIoPerpOrderBookUpdate>((client, handler) => client.PerpetualFuturesApi.SubscribeToOrderBookUpdatesAsync("usdt", "ETH_USDT", 100, 20, handler), "SubscribeToBookUpdates", ignoreProperties: new List<string> { "time" });
            await tester.ValidateAsync<GateIoPerpKlineUpdate[]>((client, handler) => client.PerpetualFuturesApi.SubscribeToKlineUpdatesAsync("usdt", "ETH_USDT", GateIo.Net.Enums.KlineInterval.FiveMinutes, handler), "SubscribeToKlineUpdates", ignoreProperties: new List<string> { "time" });
            await tester.ValidateAsync<GateIoPerpOrder[]>((client, handler) => client.PerpetualFuturesApi.SubscribeToOrderUpdatesAsync(123, "usdt", handler), "SubscribeToOrderUpdates", ignoreProperties: new List<string> { "time", "create_time_ms", "finish_time_ms", "refr" });
            await tester.ValidateAsync<GateIoPerpUserTrade[]>((client, handler) => client.PerpetualFuturesApi.SubscribeToUserTradeUpdatesAsync(123, "usdt", handler), "SubscribeToUserTradeUpdates", ignoreProperties: new List<string> { "time", "create_time_ms" });
            await tester.ValidateAsync<GateIoPerpLiquidation[]>((client, handler) => client.PerpetualFuturesApi.SubscribeToUserLiquidationUpdatesAsync(123, "usdt", handler), "SubscribeToUserLiquidationUpdates", ignoreProperties: new List<string> { "time", "time_ms", "create_time_ms" });
            await tester.ValidateAsync<GateIoPerpAutoDeleverage[]>((client, handler) => client.PerpetualFuturesApi.SubscribeToUserAutoDeleverageUpdatesAsync(123, "usdt", handler), "SubscribeToUserAutoDeleverageUpdates", ignoreProperties: new List<string> { "time", "time_ms", "create_time_ms" });
            await tester.ValidateAsync<GateIoPerpPositionCloseUpdate[]>((client, handler) => client.PerpetualFuturesApi.SubscribeToPositionCloseUpdatesAsync(123, "usdt", handler), "SubscribeToPositionCloseUpdates", ignoreProperties: new List<string> { "time", "time_ms", "create_time_ms" });
            await tester.ValidateAsync<GateIoPerpBalanceUpdate[]>((client, handler) => client.PerpetualFuturesApi.SubscribeToBalanceUpdatesAsync(123, "usdt", handler), "SubscribeToBalanceUpdates", ignoreProperties: new List<string> { "time", "time_ms", "create_time_ms" });
            await tester.ValidateAsync<GateIoPerpRiskLimitUpdate[]>((client, handler) => client.PerpetualFuturesApi.SubscribeToReduceRiskLimitUpdatesAsync(123, "usdt", handler), "SubscribeToReduceRiskLimitUpdates", ignoreProperties: new List<string> { "time", "time_ms", "create_time_ms" });
            await tester.ValidateAsync<GateIoPositionUpdate[]>((client, handler) => client.PerpetualFuturesApi.SubscribeToPositionUpdatesAsync(123, "usdt", handler), "SubscribeToPositionUpdates", ignoreProperties: new List<string> { "time", "time_ms", "create_time_ms" });
            await tester.ValidateAsync<GateIoPerpTriggerOrderUpdate[]>((client, handler) => client.PerpetualFuturesApi.SubscribeToTriggerOrderUpdatesAsync(123, "usdt", handler), "SubscribeToTriggerOrderUpdates", ignoreProperties: new List<string> { "time", "time_ms", "create_time_ms", "strategy_type", "iceberg", "stop_trigger" });
			await tester.ValidateAsync<GateIoPerpContractStats>((client, handler) => client.PerpetualFuturesApi.SubscribeToContractStatsUpdatesAsync("usdt", "ETH_USDT", GateIo.Net.Enums.KlineInterval.OneMinute, handler), "SubscribeToContractStatsUpdates", ignoreProperties: new List<string> { "time" });
        }
    }
}
