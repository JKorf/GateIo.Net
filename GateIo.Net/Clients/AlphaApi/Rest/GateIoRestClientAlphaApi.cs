using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using GateIo.Net.Clients.MessageHandlers;
using GateIo.Net.Interfaces.Clients.AlphaApi;
using GateIo.Net.Interfaces.Clients.RebateApi;
using GateIo.Net.Objects.Options;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace GateIo.Net.Clients.AlphaApi
{
    /// <inheritdoc cref="GateIoRestClientAlphaApi" />
    internal partial class GateIoRestClientAlphaApi : RestApiClient<GateIoEnvironment, GateIoAuthenticationProvider, GateIoCredentials>, IGateIoRestClientAlphaApi
    {
        #region fields 
        private readonly GateIoRestClient _baseClient;
        protected override IRestMessageHandler MessageHandler { get; } = new GateIoRestMessageHandler(GateIoErrors.RestErrors);

        internal new GateIoRestOptions ClientOptions => (GateIoRestOptions)base.ClientOptions;
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

        internal GateIoRestClientAlphaApi(ILoggerFactory? loggerFactory, HttpClient? httpClient, GateIoRestClient baseClient, GateIoRestOptions options)
            : base(loggerFactory, GateIoExchange.Metadata.Id, httpClient, options.Environment.RestClientAddress!, options, options.RebateOptions)
        {
            _baseClient = baseClient;

            Account = new GateIoRestClientAlphaApiAccount(this);
            ExchangeData = new GateIoRestClientAlphaApiExchangeData(this);
            Trading = new GateIoRestClientAlphaApiTrading(this);

            ParameterPositions[HttpMethod.Delete] = HttpMethodParameterPosition.InUri;
        }

        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(GateIoExchange._serializerContext));

        /// <inheritdoc />
        protected override GateIoAuthenticationProvider CreateAuthenticationProvider(GateIoCredentials credentials)
            => new GateIoAuthenticationProvider(credentials);

        internal async Task<HttpResult<T>> SendAsync<T>(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? requestHeaders = null) where T : class
        {
            return await base.SendAsync<T>(definition, parameters, cancellationToken, requestHeaders, weight).ConfigureAwait(false);
        }

        /// <inheritdoc />
        protected override Task<HttpResult<DateTime>> GetServerTimestampAsync()
            => _baseClient.SpotApi.ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => GateIoExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);
    }
}
