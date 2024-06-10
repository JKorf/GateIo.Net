using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.JsonNet;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using GateIo.Net.Clients;
using GateIo.Net;

namespace Gate.io.Net.UnitTests
{
    [TestFixture()]
    public class GateIoRestClientTests
    {
        //[Test]
        public void CheckSignatureExample1()
        {
            var authProvider = new GateIoAuthenticationProvider(new ApiCredentials("vmPUZE6mv9SD5VNHk4HlWFsOr6aKE2zvsw0MuIgwCIPy6utIco14y7Ju91duEh8A", "NhqPtmdSJYdKjVHjA7PZj4Mge3R5YNiP1e3UZjInClVN65XAbvqqM6A7H5fATj0j"));
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
                new Dictionary<string, object>
                {
                    { "symbol", "LTCBTC" },
                },
                DateTimeConverter.ParseFromLong(1499827319559),
                true,
                false);
        }

        [Test]
        public void CheckInterfaces()
        {
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingRestInterfaces<GateIoRestClient>();
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingSocketInterfaces<GateIoSocketClient>();
        }
    }
}
