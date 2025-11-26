using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Converters.SystemTextJson.MessageConverters;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GateIo.Net.Clients.MessageHandlers
{
    internal class GateIoRestMessageHandler : JsonRestMessageHandler
    {
        private readonly ErrorMapping _errorMapping;

        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(GateIoExchange._serializerContext);

        public GateIoRestMessageHandler(ErrorMapping errorMapping)
        {
            _errorMapping = errorMapping;
        }

        public override async ValueTask<Error> ParseErrorResponse(int httpStatusCode, object? state, HttpResponseHeaders responseHeaders, Stream responseStream)
        {
            var (parseError, document) = await GetJsonDocument(responseStream, state).ConfigureAwait(false);
            if (parseError != null)
                return parseError;

            var label = document!.RootElement.TryGetProperty("label", out var codeProp) ? codeProp.GetString() : null;
            if(label == null)
                return new ServerError(ErrorInfo.Unknown);

            var msg = document!.RootElement.TryGetProperty("message", out var msgProp) ? msgProp.GetString() : null;
            return new ServerError(label!, _errorMapping.GetErrorInfo(label!, msg));
        }

        public override async ValueTask<ServerRateLimitError> ParseErrorRateLimitResponse(int httpStatusCode, object? state, HttpResponseHeaders responseHeaders, Stream responseStream)
        {
            var (parseError, document) = await GetJsonDocument(responseStream, state).ConfigureAwait(false);
            if (parseError != null)
                return new ServerRateLimitError();

            var label = document!.RootElement.TryGetProperty("label", out var codeProp) ? codeProp.GetString() : null;
            if (label == null)
                return new ServerRateLimitError();

            var msg = document!.RootElement.TryGetProperty("message", out var msgProp) ? msgProp.GetString() : null;
            var error = new ServerRateLimitError(label + ": " + msg);

            var resetTime = responseHeaders.SingleOrDefault(x => x.Key.Equals("X-Gate-RateLimit-Reset-Timestamp"));
            if (resetTime.Value?.Any() != true)
                return error;

            var value = resetTime.Value.First();
            var timestamp = DateTimeConverter.ParseFromString(value, null);

            error.RetryAfter = timestamp.AddSeconds(1);
            return error;
        }
    }
}
