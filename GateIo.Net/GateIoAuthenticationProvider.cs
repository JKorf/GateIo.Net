using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using GateIo.Net.Clients.SpotApi;
using GateIo.Net.Objects.Internal;
using GateIo.Net.Objects.Sockets;
using System.Collections.Generic;
using System.Threading.Channels;

namespace GateIo.Net
{
    internal class GateIoAuthenticationProvider : AuthenticationProvider
    {
        private static IMessageSerializer _serializer = new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(GateIoExchange._serializerContext));

        public override ApiCredentialsType[] SupportedCredentialTypes => [ApiCredentialsType.Hmac];

        public GateIoAuthenticationProvider(ApiCredentials credentials) : base(credentials)
        {
        }

        public override void ProcessRequest(RestApiClient apiClient, RestRequestConfiguration request)
        {
            if (!request.Authenticated)
                return;

            var timestamp = GetMillisecondTimestampLong(apiClient) / 1000;
            var requestBody = request.BodyParameters?.Count > 0 ? GetSerializedBody(_serializer, request.BodyParameters) : string.Empty;
            var queryString = request.QueryParameters?.Count > 0 ? request.GetQueryString(false) : string.Empty;
            var bodyPayload = SignSHA512(requestBody).ToLowerInvariant();

            var signStr = $"{request.Method}\n{request.Path}\n{queryString}\n{bodyPayload}\n{timestamp}";
            var signature = SignHMACSHA512(signStr).ToLowerInvariant();

            request.Headers ??= new Dictionary<string, string>();
            request.Headers["KEY"] = ApiKey;
            request.Headers["Timestamp"] = timestamp.ToString();
            request.Headers["SIGN"] = signature;

            request.SetBodyContent(requestBody);
            request.SetQueryString(queryString);
        }

        public override Query? GetAuthenticationQuery(SocketApiClient apiClient, SocketConnection connection, Dictionary<string, object?>? context = null)
        {
            if (context?.ContainsKey("channel") == true)
            {
                var channel = (string)context["channel"]!;
                var type = (string)context["type"]!;
                var query = new GateIoAuthQuery<GateIoSubscriptionResponse>(apiClient, channel, type, (string[]?)context["payload"]);
                var request = (GateIoSocketAuthRequest<string[]>)query.Request;
                var sign = SignHMACSHA512($"channel={channel}&event={type}&time={request.Timestamp}").ToLowerInvariant();
                request.Auth = new GateIoSocketAuth { Key = ApiKey, Sign = sign, Method = "api_key" };
                return query;
            }
            else
            {
                var timestamp = GetMillisecondTimestampLong(apiClient) / 1000;
                var channel = apiClient is GateIoSocketClientSpotApi ? "spot.login" : "futures.login";
                var signStr = $"api\n{channel}\n\n{timestamp}";
                var id = ExchangeHelpers.NextId();

                return new GateIoLoginQuery(apiClient, id, channel, "api", ApiKey, SignHMACSHA512(signStr).ToLowerInvariant(), timestamp);
            }
        }

    }
}
