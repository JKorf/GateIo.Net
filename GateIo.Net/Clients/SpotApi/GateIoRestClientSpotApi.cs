using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GateIo.Net.Interfaces.Clients.SpotApi;
using GateIo.Net.Objects.Options;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Linq;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Objects.Errors;

namespace GateIo.Net.Clients.SpotApi
{
    /// <inheritdoc cref="IGateIoRestClientSpotApi" />
    internal partial class GateIoRestClientSpotApi : RestApiClient, IGateIoRestClientSpotApi
    {
        #region fields 
        internal static TimeSyncState _timeSyncState = new TimeSyncState("Spot Api");


        internal new GateIoRestOptions ClientOptions => (GateIoRestOptions)base.ClientOptions;
        protected override ErrorMapping ErrorMapping => GateIoErrors.RestErrors;
        #endregion

        #region Api clients
        /// <inheritdoc />
        public IGateIoRestClientSpotApiAccount Account { get; }
        /// <inheritdoc />
        public IGateIoRestClientSpotApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IGateIoRestClientSpotApiTrading Trading { get; }
        /// <inheritdoc />
        public string ExchangeName => "GateIo";
        #endregion

        #region constructor/destructor
        internal GateIoRestClientSpotApi(ILogger logger, HttpClient? httpClient, GateIoRestOptions options)
            : base(logger, httpClient, options.Environment.RestClientAddress, options, options.SpotOptions)
        {
            Account = new GateIoRestClientSpotApiAccount(this);
            ExchangeData = new GateIoRestClientSpotApiExchangeData(logger, this);
            Trading = new GateIoRestClientSpotApiTrading(logger, this);

            ParameterPositions[HttpMethod.Delete] = HttpMethodParameterPosition.InUri;
        }
        #endregion

        /// <inheritdoc />
        protected override IStreamMessageAccessor CreateAccessor() => new SystemTextJsonStreamMessageAccessor(SerializerOptions.WithConverters(GateIoExchange._serializerContext));
        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(GateIoExchange._serializerContext));

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new GateIoAuthenticationProvider(credentials);

        internal Task<WebCallResult> SendAsync(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
            => base.SendAsync(BaseAddress, definition, parameters, cancellationToken, null, weight);

        internal Task<WebCallResult<T>> SendAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? requestHeaders = null, string? rateLimitKeySuffix = null) where T : class
            => SendToAddressAsync<T>(BaseAddress, definition, parameters, cancellationToken, weight, requestHeaders, rateLimitKeySuffix);

        internal async Task<WebCallResult<T>> SendToAddressAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? requestHeaders = null, string? rateLimitKeySuffix = null) where T : class
        {
            var result = await base.SendAsync<T>(baseAddress, definition, parameters, cancellationToken, requestHeaders, weight, rateLimitKeySuffix: rateLimitKeySuffix).ConfigureAwait(false);

            // GateIo Optional response checking

            return result;
        }

        internal Task<WebCallResult<T>> SendAsync<T>(RequestDefinition definition, ParameterCollection? queryParameters, ParameterCollection? bodyParameters, CancellationToken cancellationToken, int? weight = null, string? rateLimitKeySuffix = null) where T : class
            => SendToAddressAsync<T>(BaseAddress, definition, queryParameters, bodyParameters, cancellationToken, weight, rateLimitKeySuffix: rateLimitKeySuffix);

        internal async Task<WebCallResult<T>> SendToAddressAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? queryParameters, ParameterCollection? bodyParameters, CancellationToken cancellationToken, int? weight = null, string? rateLimitKeySuffix = null) where T : class
        {
            var result = await base.SendAsync<T>(baseAddress, definition, queryParameters, bodyParameters, cancellationToken, null, weight, rateLimitKeySuffix: rateLimitKeySuffix).ConfigureAwait(false);

            // GateIo Optional response checking

            return result;
        }

        /// <inheritdoc />
        protected override Error ParseErrorResponse(int httpStatusCode, KeyValuePair<string, string[]>[] responseHeaders, IMessageAccessor accessor, Exception? exception)
        {
            if (!accessor.IsValid)
                return new ServerError(ErrorInfo.Unknown, exception: exception);

            var lbl = accessor.GetValue<string>(MessagePath.Get().Property("label"));
            if (lbl == null)
                return new ServerError(ErrorInfo.Unknown, exception: exception);

            var msg = accessor.GetValue<string>(MessagePath.Get().Property("message"));
            return new ServerError(lbl, GetErrorInfo(lbl, msg), exception);
        }

        /// <inheritdoc />
        protected override ServerRateLimitError ParseRateLimitResponse(int httpStatusCode, KeyValuePair<string, string[]>[] responseHeaders, IMessageAccessor accessor)
        {
            if (!accessor.IsValid)
                return new ServerRateLimitError(accessor.GetOriginalString());

            var error = GetRateLimitError(accessor);

            var resetTime = responseHeaders.SingleOrDefault(x => x.Key.Equals("X-Gate-RateLimit-Reset-Timestamp"));
            if (resetTime.Value?.Any() != true)
                return error;

            var value = resetTime.Value.First();
            var timestamp = DateTimeConverter.ParseFromString(value);
            
            error.RetryAfter = timestamp.AddSeconds(1);
            return error;
        }

        private ServerRateLimitError GetRateLimitError(IMessageAccessor accessor)
        {
            if (!accessor.IsValid)
                return new ServerRateLimitError(accessor.GetOriginalString());

            var lbl = accessor.GetValue<string>(MessagePath.Get().Property("label"));
            if (lbl == null)
                return new ServerRateLimitError(accessor.GetOriginalString());

            var msg = accessor.GetValue<string>(MessagePath.Get().Property("message"));
            return new ServerRateLimitError(lbl + ": " + msg);
        }

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override TimeSyncInfo? GetTimeSyncInfo()
            => new TimeSyncInfo(_logger, ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp, ApiOptions.TimestampRecalculationInterval ?? ClientOptions.TimestampRecalculationInterval, _timeSyncState);

        /// <inheritdoc />
        public override TimeSpan? GetTimeOffset()
            => _timeSyncState.TimeOffset;

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => GateIoExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        public IGateIoRestClientSpotApiShared SharedClient => this;
    }
}
