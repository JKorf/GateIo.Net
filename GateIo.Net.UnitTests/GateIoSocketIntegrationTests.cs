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
        public override bool Run { get; set; } = true;

        public GateIoSocketIntegrationTests()
        {
        }

        public override GateIoSocketClient GetClient(ILoggerFactory loggerFactory, bool useUpdatedDeserialization)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new GateIoSocketClient(Options.Create(new GateIoSocketOptions
            {
                OutputOriginalData = true,
                UseUpdatedDeserialization = useUpdatedDeserialization,
                ApiCredentials = Authenticated ? new CryptoExchange.Net.Authentication.ApiCredentials(key, sec) : null
            }), loggerFactory);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestSubscriptions(bool useUpdatedDeserialization)
        {
            await RunAndCheckUpdate<GateIoTicker>(useUpdatedDeserialization , (client, updateHandler) => client.SpotApi.SubscribeToBalanceUpdatesAsync(default , default), false, true);
            await RunAndCheckUpdate<GateIoTickerUpdate>(useUpdatedDeserialization, (client, updateHandler) => client.SpotApi.SubscribeToTickerUpdatesAsync("BTC_USDT", updateHandler, default), true, false);

            await RunAndCheckUpdate<GateIoPerpTickerUpdate[]>(useUpdatedDeserialization, (client, updateHandler) => client.PerpetualFuturesApi.SubscribeToTickerUpdatesAsync("usdt", "ETH_USDT", updateHandler, default), true, false);
        } 
    }
}
