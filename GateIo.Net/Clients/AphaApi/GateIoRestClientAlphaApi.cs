using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using GateIo.Net.Clients.RebateApi;
using GateIo.Net.Interfaces.Clients.AlphaApi;
using GateIo.Net.Interfaces.Clients.RebateApi;
using GateIo.Net.Objects.Options;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GateIo.Net.Clients.AlphaApi
{
    /// <inheritdoc cref="GateIoRestClientAlphaApi" />
    internal partial class GateIoRestClientAlphaApi : RestApiClient, IGateIoRestClientAlphaApi
    {
        #region fields 
        internal static TimeSyncState _timeSyncState = new TimeSyncState("Alpha Api");
        internal string _brokerId;
        private readonly GateIoRestClient _baseClient;

        protected override ErrorMapping ErrorMapping => GateIoErrors.RestErrors;
        #endregion

        #region Api clients
        /// <inheritdoc />
        public IGateIoRestClientAlphaApiAccount Account { get; }
        /// <inheritdoc />
        public IGateIoRestClientAlphaApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IGateIoRestClientAlphaApiTrading Trading { get; }
        /// <inheritdoc />
        public string ExchangeName => "GateIo";

        #endregion

        internal GateIoRestClientAlphaApi(ILogger logger, HttpClient? httpClient, GateIoRestClient baseClient, GateIoRestOptions options)
            : base(logger, httpClient, options.Environment.RestClientAddress!, options, options.RebateOptions)
        {
            _baseClient = baseClient;

            Account = new GateIoRestClientAlphaApiAccount(this);
            ExchangeData = new GateIoRestClientAlphaApiExchangeData(this);
            Trading = new GateIoRestClientAlphaApiTrading(this);

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
            if (!accessor.IsValid)
                return new ServerError(ErrorInfo.Unknown, exception: exception);

            var lbl = accessor.GetValue<string>(MessagePath.Get().Property("label"));
            if (lbl == null)
                return new ServerError(ErrorInfo.Unknown, exception: exception);

            var msg = accessor.GetValue<string>(MessagePath.Get().Property("message"));
            return new ServerError(lbl, GetErrorInfo(lbl, msg), exception);
        }

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => _baseClient.SpotApi.ExchangeData.GetServerTimeAsync();

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
