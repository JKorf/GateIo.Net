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
using GateIo.Net.Interfaces.Clients.PerpetualFuturesApi;
using GateIo.Net.Interfaces.Clients.SpotApi;
using GateIo.Net.Objects.Options;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace GateIo.Net.Clients.FuturesApi
{
    /// <inheritdoc cref="IGateIoRestClientPerpetualFuturesApi" />
    internal partial class GateIoRestClientPerpetualFuturesApi : RestApiClient<GateIoEnvironment, GateIoAuthenticationProvider, GateIoCredentials>, IGateIoRestClientPerpetualFuturesApi
    {
        #region fields 
        internal new GateIoRestOptions ClientOptions => (GateIoRestOptions)base.ClientOptions;
        protected override ErrorMapping ErrorMapping => GateIoErrors.RestErrors;
        protected override IRestMessageHandler MessageHandler { get; } = new GateIoRestMessageHandler(GateIoErrors.RestErrors);

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
        internal GateIoRestClientPerpetualFuturesApi(ILoggerFactory? loggerFactory, HttpClient? httpClient, GateIoRestOptions options)
            : base(loggerFactory, GateIoExchange.Metadata.Id, httpClient, options.Environment.RestClientAddress!, options, options.PerpetualFuturesOptions)
        {
            StandardRequestHeaders = new Dictionary<string, string>
            {
                { "X-Gate-Size-Decimal", "1" }
            };

            Account = new GateIoRestClientPerpetualFuturesApiAccount(this);
            ExchangeData = new GateIoRestClientPerpetualFuturesApiExchangeData(_logger, this);
            Trading = new GateIoRestClientPerpetualFuturesApiTrading(_logger, this);

            ParameterPositions[HttpMethod.Delete] = HttpMethodParameterPosition.InUri;

            RequestBodyEmptyContent = "";
        }

        #endregion

        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(GateIoExchange._serializerContext));

        public IGateIoRestClientPerpetualFuturesApiShared SharedClient => this;

        /// <inheritdoc />
        protected override GateIoAuthenticationProvider CreateAuthenticationProvider(GateIoCredentials credentials)
            => new GateIoAuthenticationProvider(credentials);

        internal async Task<HttpResult<T>> SendAsync<T>(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? requestHeaders = null)
        {
            return await base.SendAsync<T>(definition, parameters, cancellationToken, requestHeaders, weight).ConfigureAwait(false);
        }

        /// <inheritdoc />
        protected override Task<HttpResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => GateIoExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);
    }
}
