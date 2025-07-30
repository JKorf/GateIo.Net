using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using GateIo.Net.Interfaces.Clients.RebateApi;
using GateIo.Net.Objects.Options;
using Microsoft.Extensions.Logging;

namespace GateIo.Net.Clients.RebateApi
{
    /// <inheritdoc cref="GateIoRestClientRebateApi" />
    internal partial class GateIoRestClientRebateApi : RestApiClient, IGateIoRestClientRebateApi
    {
        #region fields 
        internal static TimeSyncState _timeSyncState = new TimeSyncState("Rebate Api");
        internal string _brokerId;
        private readonly GateIoRestClient _baseClient;
        #endregion

        #region Api clients
        /// <inheritdoc />
        public IGateIoRestClientRebateApiPartner Partner { get; }
        /// <inheritdoc />
        public string ExchangeName => "GateIo";

        #endregion

        internal GateIoRestClientRebateApi(ILogger logger, HttpClient? httpClient, GateIoRestClient baseClient, GateIoRestOptions options)
            : base(logger, httpClient, options.Environment.RestClientAddress!, options, options.RebateOptions)
        {
            _baseClient = baseClient;

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
