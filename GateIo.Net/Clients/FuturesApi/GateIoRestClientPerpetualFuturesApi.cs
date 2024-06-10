using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GateIo.Net.Objects.Options;
using CryptoExchange.Net.Clients;
using GateIo.Net.Interfaces.Clients.PerpetualFuturesApi;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Converters.MessageParsing;
using System.Linq;

namespace GateIo.Net.Clients.FuturesApi
{
    /// <inheritdoc cref="IGateIoRestClientPerpetualFuturesApi" />
    public class GateIoRestClientPerpetualFuturesApi : RestApiClient, IGateIoRestClientPerpetualFuturesApi
    {
        #region fields 
        internal static TimeSyncState _timeSyncState = new TimeSyncState("Perpetual Futures Api");
        internal string _brokerId;
        #endregion

        #region Api clients
        /// <inheritdoc />
        public IGateIoRestClientPerpetualFuturesApiAccount Account { get; }
        /// <inheritdoc />
        public IGateIoRestClientPerpetualFuturesApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IGateIoRestClientPerpetualFuturesApiTrading Trading { get; }
        /// <inheritdoc />
        public string ExchangeName => "GateIo";
        #endregion

        #region constructor/destructor
        internal GateIoRestClientPerpetualFuturesApi(ILogger logger, HttpClient? httpClient, GateIoRestOptions options)
            : base(logger, httpClient, options.Environment.RestClientAddress!, options, options.PerpetualFuturesOptions)
        {
            Account = new GateIoRestClientPerpetualFuturesApiAccount(this);
            ExchangeData = new GateIoRestClientPerpetualFuturesApiExchangeData(logger, this);
            Trading = new GateIoRestClientPerpetualFuturesApiTrading(logger, this);

            _brokerId = string.IsNullOrEmpty(options.BrokerId) ? "copytraderpw" : options.BrokerId!;
            ParameterPositions[HttpMethod.Delete] = HttpMethodParameterPosition.InUri;
        }

        #endregion

        /// <inheritdoc />
        protected override IStreamMessageAccessor CreateAccessor() => new SystemTextJsonStreamMessageAccessor();
        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer();

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
        protected override Error ParseErrorResponse(int httpStatusCode, IEnumerable<KeyValuePair<string, IEnumerable<string>>> responseHeaders, IMessageAccessor accessor)
        {
            if (!accessor.IsJson)
                return new ServerError(accessor.GetOriginalString());

            var lbl = accessor.GetValue<string>(MessagePath.Get().Property("label"));
            if (lbl == null)
                return new ServerError(accessor.GetOriginalString());

            var msg = accessor.GetValue<string>(MessagePath.Get().Property("message"));
            return new ServerError(lbl + ": " + msg);
        }

        /// <inheritdoc />
        protected override ServerRateLimitError ParseRateLimitResponse(int httpStatusCode, IEnumerable<KeyValuePair<string, IEnumerable<string>>> responseHeaders, IMessageAccessor accessor)
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
        public override string FormatSymbol(string baseAsset, string quoteAsset) => baseAsset.ToUpperInvariant() + "_" + quoteAsset.ToUpperInvariant();
    }
}
