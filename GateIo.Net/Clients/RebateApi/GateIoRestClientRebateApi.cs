using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using GateIo.Net.Objects.Options;
using Microsoft.Extensions.Logging;
using GateIo.Net.Interfaces.Clients.RebateApi;
using CryptoExchange.Net.Converters.MessageParsing;
using System.Linq;
using GateIo.Net.Clients.SpotApi;

namespace GateIo.Net.Clients.RebateApi
{
    /// <inheritdoc cref="GateIoRestClientRebateApi" />
    internal partial class GateIoRestClientRebateApi : RestApiClient, IGateIoRestClientRebateApi
    {
        #region fields 
        internal static TimeSyncState _timeSyncState = new TimeSyncState("Rebate Api");
        internal string _brokerId;
        #endregion

        #region Api clients
        /// <inheritdoc />
        public IGateIoRestClientRebateApiPartner Partner { get; }
        /// <inheritdoc />
        public IGateIoRestClientRebateApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public string ExchangeName => "GateIo";

        #endregion

        internal GateIoRestClientRebateApi(ILogger logger, HttpClient? httpClient, GateIoRestOptions options)
            : base(logger, httpClient, options.Environment.RestClientAddress!, options, options.RebateOptions)
        {
            ExchangeData = new GateIoRestClientRebateApiExchangeData(logger, this);
            Partner = new GateIoRestClientRebateApiPartner(this);

            _brokerId = string.IsNullOrEmpty(options.BrokerId) ? "copytraderpw" : options.BrokerId!;
            ParameterPositions[HttpMethod.Delete] = HttpMethodParameterPosition.InUri;
        }

        /// <inheritdoc />
        protected override IStreamMessageAccessor CreateAccessor() => new SystemTextJsonStreamMessageAccessor(SerializerOptions.WithConverters(GateIoExchange._serializerContext));
        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(GateIoExchange._serializerContext));

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new GateIoAuthenticationProvider(credentials);

        internal Task<WebCallResult> SendAsync(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
            => base.SendAsync(BaseAddress, definition, parameters, cancellationToken, null, weight);

        internal Task<WebCallResult<T>> SendAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? requestHeaders = null) where T : class
            => SendToAddressAsync<T>(BaseAddress, definition, parameters, cancellationToken, weight, requestHeaders);

        internal async Task<WebCallResult<T>> SendToAddressAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? requestHeaders = null) where T : class
        {
            var result = await base.SendAsync<T>(baseAddress, definition, parameters, cancellationToken, requestHeaders, weight).ConfigureAwait(false);

            // GateIo Optional response checking

            return result;
        }

        internal Task<WebCallResult<T>> SendAsync<T>(RequestDefinition definition, ParameterCollection? queryParameters, ParameterCollection? bodyParameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? requestHeaders = null) where T : class
            => SendToAddressAsync<T>(BaseAddress, definition, queryParameters, bodyParameters, cancellationToken, weight, requestHeaders);

        internal async Task<WebCallResult<T>> SendToAddressAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? queryParameters, ParameterCollection? bodyParameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? requestHeaders = null) where T : class
        {
            var result = await base.SendAsync<T>(baseAddress, definition, queryParameters, bodyParameters, cancellationToken, requestHeaders, weight).ConfigureAwait(false);

            // GateIo Optional response checking

            return result;
        }

        /// <inheritdoc />
        protected override Error ParseErrorResponse(int httpStatusCode, KeyValuePair<string, string[]>[] responseHeaders, IMessageAccessor accessor, Exception? exception)
        {
            if (!accessor.IsJson)
                return new ServerError(null, "Unknown request error", exception: exception);

            var lbl = accessor.GetValue<string>(MessagePath.Get().Property("label"));
            if (lbl == null)
                return new ServerError(null, "Unknown request error", exception: exception);

            var msg = accessor.GetValue<string>(MessagePath.Get().Property("message"));
            return new ServerError(null, lbl + ": " + msg, exception);
        }

        /// <inheritdoc />
        protected override ServerRateLimitError ParseRateLimitResponse(int httpStatusCode, KeyValuePair<string, string[]>[] responseHeaders, IMessageAccessor accessor)
        {
            if (!accessor.IsJson)
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
            if (!accessor.IsJson)
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






    }
}
