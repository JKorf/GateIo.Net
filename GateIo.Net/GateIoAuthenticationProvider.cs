using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;

namespace GateIo.Net
{
    internal class GateIoAuthenticationProvider : AuthenticationProvider
    {
        private static IMessageSerializer _serializer = new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(GateIoExchange._serializerContext));

        public GateIoAuthenticationProvider(ApiCredentials credentials) : base(credentials)
        {
        }

        public override void ProcessRequest(RestApiClient apiClient, RestRequestConfiguration request)
        {
            if (!request.Authenticated)
                return;

            var timestamp = GetMillisecondTimestampLong(apiClient) / 1000;
            var requestBody = request.BodyParameters.Any() ? GetSerializedBody(_serializer, request.BodyParameters) : string.Empty;
            var queryString = request.QueryParameters.Any() ? request.GetQueryString(true) : string.Empty;
            var bodyPayload = SignSHA512(requestBody).ToLowerInvariant();

            var signStr = $"{request.Method}\n{request.Path}\n{queryString}\n{bodyPayload}\n{timestamp}";
            var signature = SignHMACSHA512(signStr).ToLowerInvariant();

            request.Headers["KEY"] = ApiKey;
            request.Headers["Timestamp"] = timestamp.ToString();
            request.Headers["SIGN"] = signature;

            request.SetBodyContent(requestBody);
            request.SetQueryString(queryString);
        }

        public string SignSocketRequest(string signStr) => SignHMACSHA512(signStr).ToLowerInvariant();
    }
}
