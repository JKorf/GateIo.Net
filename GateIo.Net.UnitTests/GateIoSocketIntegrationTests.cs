using GateIo.Net.Clients;
using GateIo.Net.Objects.Models;
using GateIo.Net.Objects.Options;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GateIo.Net.UnitTests
{
    [NonParallelizable]
    internal class GateIoSocketIntegrationTests : SocketIntegrationTest<GateIoSocketClient>
    {
        public override bool Run { get; set; } = false;

        public GateIoSocketIntegrationTests()
        {
        }

        public override GateIoSocketClient GetClient(ILoggerFactory loggerFactory)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new GateIoSocketClient(Options.Create(new GateIoSocketOptions
            {
                OutputOriginalData = true,
                ApiCredentials = Authenticated ? new CryptoExchange.Net.Authentication.ApiCredentials(key, sec) : null
            }), loggerFactory);
        }

        [Test]
        public async Task TestSubscriptions()
        {
            await RunAndCheckUpdate<GateIoTicker>((client, updateHandler) => client.SpotApi.SubscribeToBalanceUpdatesAsync(default , default), false, true);
            await RunAndCheckUpdate<GateIoTickerUpdate>((client, updateHandler) => client.SpotApi.SubscribeToTickerUpdatesAsync("BTC_USDT", updateHandler, default), true, false);

            await RunAndCheckUpdate<GateIoPerpTickerUpdate[]>((client, updateHandler) => client.PerpetualFuturesApi.SubscribeToTickerUpdatesAsync("usdt", "ETH_USDT", updateHandler, default), true, false);
        } 
    }
}
