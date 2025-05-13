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

        public override void AuthenticateRequest(
            RestApiClient apiClient,
            Uri uri,
            HttpMethod method,
            ref IDictionary<string, object>? uriParameters,
            ref IDictionary<string, object>? bodyParameters,
            ref Dictionary<string, string>? headers,
            bool auth,
            ArrayParametersSerialization arraySerialization,
            HttpMethodParameterPosition parameterPosition,
            RequestBodyFormat requestBodyFormat)
        {
            if (!auth)
                return;

            if (uriParameters != null)
                uri = uri.SetParameters(uriParameters, arraySerialization);

            var timestamp = long.Parse(GetMillisecondTimestamp(apiClient)) / 1000;
            var payload = SignSHA512(bodyParameters?.Any() == true ? GetSerializedBody(_serializer, bodyParameters) : "").ToLowerInvariant();
            var signStr = $"{method.ToString().ToUpper()}\n{uri.AbsolutePath}\n{uri.Query.Replace("?", "")}\n{payload}\n{timestamp}";
            var signed = SignHMACSHA512(signStr).ToLowerInvariant();

            headers ??= new Dictionary<string, string>();
            headers["KEY"] = ApiKey;
            headers["Timestamp"] = timestamp.ToString();
            headers["SIGN"] = signed;
        }

        public string SignSocketRequest(string signStr) => SignHMACSHA512(signStr).ToLowerInvariant();
    }
}
