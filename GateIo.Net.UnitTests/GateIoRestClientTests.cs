using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using GateIo.Net.Clients;
using GateIo.Net.Clients.SpotApi;
using GateIo.Net.Interfaces.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;

namespace GateIo.Net.UnitTests
{
    [TestFixture()]
    public class GateIoRestClientTests
    {
        //[Test]
        public void CheckSignatureExample1()
        {
            var authProvider = new GateIoAuthenticationProvider(new GateIoCredentials("vmPUZE6mv9SD5VNHk4HlWFsOr6aKE2zvsw0MuIgwCIPy6utIco14y7Ju91duEh8A", "NhqPtmdSJYdKjVHjA7PZj4Mge3R5YNiP1e3UZjInClVN65XAbvqqM6A7H5fATj0j"));
            var client = (RestApiClient)new GateIoRestClient().SpotApi;

            CryptoExchange.Net.Testing.TestHelpers.CheckSignature(
                client,
                authProvider,
                HttpMethod.Post,
                "/api/v3/order",
                (uriParams, bodyParams, headers) =>
                {
                    return bodyParams["signature"].ToString();
                },
                "c8db56825ae71d6d79447849e617115f4a920fa2acdcab2b053c4b2838bd6b71",
                new Parameters(GateIoExchange._parameterSerializationSettings)
                {
                    { "symbol", "LTCBTC" },
                },
                DateTimeConverter.ParseFromDouble(1499827320559),
                false);
        }

        [Test]
        public void CheckInterfaces()
        {
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingRestInterfaces<GateIoRestClient>();
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingSocketInterfaces<GateIoSocketClient>();
        }

        [Test]
        [TestCase(TradeEnvironmentNames.Live, "https://api.gateio.ws")]
        [TestCase(TradeEnvironmentNames.Testnet, "https://api-testnet.gateapi.io")]
        [TestCase("", "https://api.gateio.ws")]
        public void TestConstructorEnvironments(string environmentName, string expected)
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "GateIo:Environment:Name", environmentName },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddGateIo(configuration.GetSection("GateIo"));
            var provider = collection.BuildServiceProvider();

            var client = provider.GetRequiredService<IGateIoRestClient>();

            var address = client.SpotApi.BaseAddress;

            Assert.That(address, Is.EqualTo(expected));
        }

        [Test]
        public void TestConstructorNullEnvironment()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "GateIo", null },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddGateIo(configuration.GetSection("GateIo"));
            var provider = collection.BuildServiceProvider();

            var client = provider.GetRequiredService<IGateIoRestClient>();

            var address = client.SpotApi.BaseAddress;

            Assert.That(address, Is.EqualTo("https://api.gateio.ws"));
        }

        [Test]
        public void TestConstructorApiOverwriteEnvironment()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "GateIo:Environment:Name", "test" },
                    { "GateIo:Rest:Environment:Name", "live" },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddGateIo(configuration.GetSection("GateIo"));
            var provider = collection.BuildServiceProvider();

            var client = provider.GetRequiredService<IGateIoRestClient>();

            var address = client.SpotApi.BaseAddress;

            Assert.That(address, Is.EqualTo("https://api.gateio.ws"));
        }

        [Test]
        public void TestConstructorConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "ApiCredentials:Key", "123" },
                    { "ApiCredentials:Secret", "456" },
                    { "Socket:ApiCredentials:Key", "456" },
                    { "Socket:ApiCredentials:Secret", "789" },
                    { "Rest:OutputOriginalData", "true" },
                    { "Socket:OutputOriginalData", "false" },
                    { "Rest:Proxy:Host", "host" },
                    { "Rest:Proxy:Port", "80" },
                    { "Socket:Proxy:Host", "host2" },
                    { "Socket:Proxy:Port", "81" },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddGateIo(configuration);
            var provider = collection.BuildServiceProvider();

            var restClient = provider.GetRequiredService<IGateIoRestClient>();
            var socketClient = provider.GetRequiredService<IGateIoSocketClient>();

            Assert.That(((BaseApiClient)restClient.SpotApi).OutputOriginalData, Is.True);
            Assert.That(((BaseApiClient)socketClient.SpotApi).OutputOriginalData, Is.False);
            Assert.That(((GateIoRestClientSpotApi)restClient.SpotApi).AuthenticationProvider.Key, Is.EqualTo("123"));
            Assert.That(((GateIoSocketClientSpotApi)socketClient.SpotApi).AuthenticationProvider.Key, Is.EqualTo("456"));
            Assert.That(((BaseApiClient)restClient.SpotApi).ClientOptions.Proxy.Host, Is.EqualTo("host"));
            Assert.That(((BaseApiClient)restClient.SpotApi).ClientOptions.Proxy.Port, Is.EqualTo(80));
            Assert.That(((BaseApiClient)socketClient.SpotApi).ClientOptions.Proxy.Host, Is.EqualTo("host2"));
            Assert.That(((BaseApiClient)socketClient.SpotApi).ClientOptions.Proxy.Port, Is.EqualTo(81));
        }

        [Test]
        public void TestSpotRestSharedApiDiscoveryMatchesAggregate()
        {
            var (missingOptions, missingInterfaces) = TestHelpers.ValidateSharedApi(new GateIoRestClient().SpotApi.SharedApi);

            Assert.That(missingOptions, Is.Empty);
            Assert.That(missingInterfaces, Is.Empty);
        }

        [Test]
        public void TestSpotSocketSharedApiDiscoveryMatchesAggregate()
        {
            var (missingOptions, missingInterfaces) = TestHelpers.ValidateSharedApi(new GateIoSocketClient().SpotApi.SharedApi);

            Assert.That(missingOptions, Is.Empty);
            Assert.That(missingInterfaces, Is.Empty);
        }

        [Test]
        public void TestFuturesRestSharedApiDiscoveryMatchesAggregate()
        {
            var (missingOptions, missingInterfaces) = TestHelpers.ValidateSharedApi(new GateIoRestClient().PerpetualFuturesApi.SharedApi);

            Assert.That(missingOptions, Is.Empty);
            Assert.That(missingInterfaces, Is.Empty);
        }

        [Test]
        public void TestFuturesSocketSharedApiDiscoveryMatchesAggregate()
        {
            var (missingOptions, missingInterfaces) = TestHelpers.ValidateSharedApi(new GateIoSocketClient().PerpetualFuturesApi.SharedApi);

            Assert.That(missingOptions, Is.Empty);
            Assert.That(missingInterfaces, Is.Empty);
        }

        [Test]
        public void TestSpotRestSharedApiDoesntHaveUnsupportedCapabilities()
        {
            var unsupported = TestHelpers.ValidateUnsupportedCapabilities(new GateIoRestClient().SpotApi.SharedApi);

            Assert.That(unsupported, Is.Empty);
        }

        [Test]
        public void TestSpotSocketSharedApiDoesntHaveUnsupportedCapabilities()
        {
            var unsupported = TestHelpers.ValidateUnsupportedCapabilities(new GateIoSocketClient().SpotApi.SharedApi);

            Assert.That(unsupported, Is.Empty);
        }

        [Test]
        public void TestFuturesRestSharedApiDoesntHaveUnsupportedCapabilities()
        {
            var unsupported = TestHelpers.ValidateUnsupportedCapabilities(new GateIoRestClient().PerpetualFuturesApi.SharedApi);

            Assert.That(unsupported, Is.Empty);
        }

        [Test]
        public void TestFuturesSocketSharedApiDoesntHaveUnsupportedCapabilities()
        {
            var unsupported = TestHelpers.ValidateUnsupportedCapabilities(new GateIoSocketClient().PerpetualFuturesApi.SharedApi);

            Assert.That(unsupported, Is.Empty);
        }
    }
}
