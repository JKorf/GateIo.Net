using GateIo.Net.Clients;
using GateIo.Net.Enums;
using GateIo.Net.Objects.Models;
using GateIo.Net.Objects.Options;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GateIo.Net.UnitTests
{
    [TestFixture]
    public class SocketRequestTests
    {
        private GateIoSocketClient CreateClient()
        {
            var fact = new LoggerFactory();
            fact.AddProvider(new TraceLoggerProvider());
            var client = new GateIoSocketClient(Options.Create(new GateIoSocketOptions
            {
                OutputOriginalData = true,
                RequestTimeout = TimeSpan.FromSeconds(5),
                ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456")
            }), fact);
            return client;
        }

        [Test]
        public async Task ValidateExchangeApiCalls()
        {
            var tester = new SocketRequestValidator<GateIoSocketClient>("Socket/SpotApi");

            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.PlaceOrderAsync("ETH_USDT", OrderSide.Buy, NewOrderType.Limit, 1), "PlaceOrder", nestedJsonProperty: "data.result", ignoreProperties: [ "create_time", "update_time", "fill_price" ]);
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.EditOrderAsync("ETH_USDT", 123), "EditOrder", nestedJsonProperty: "data.result", ignoreProperties: [ "create_time", "update_time", "fill_price" ]);
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.CancelOrderAsync("ETH_USDT", 123), "CancelOrder", nestedJsonProperty: "data.result", ignoreProperties: [ "create_time", "update_time", "fill_price" ]);
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.CancelAllOrdersAsync("ETH_USDT"), "CancelAllOrders", nestedJsonProperty: "data.result", ignoreProperties: [ "create_time", "update_time", "fill_price" ]);
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.GetOrderAsync("ETH_USDT", 123L), "GetOrder", nestedJsonProperty: "data.result", ignoreProperties: [ "create_time", "update_time", "fill_price" ]);
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.GetOrdersAsync("ETH_USDT", true), "GetOrders", nestedJsonProperty: "data.result", ignoreProperties: [ "create_time", "update_time", "fill_price" ]);
        }
    }
}
