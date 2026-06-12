using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using GateIo.Net.Clients.MessageHandlers;
using GateIo.Net.Interfaces.Clients.RebateApi;
using GateIo.Net.Objects.Options;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GateIo.Net.Clients.RebateApi
{
    /// <inheritdoc cref="GateIoRestClientRebateApi" />
    internal partial class GateIoRestClientRebateApi : RestApiClient<GateIoEnvironment, GateIoAuthenticationProvider, GateIoCredentials>, IGateIoRestClientRebateApi
    {
        #region fields 
        private readonly GateIoRestClient _baseClient;
        protected override IRestMessageHandler MessageHandler { get; } = new GateIoRestMessageHandler(GateIoErrors.RestErrors);
        #endregion

        #region Api clients
        /// <inheritdoc />
        public IGateIoRestClientRebateApiPartner Partner { get; }
        /// <inheritdoc />
        public string ExchangeName => "GateIo";

        #endregion

        internal GateIoRestClientRebateApi(ILogger logger, HttpClient? httpClient, GateIoRestClient baseClient, GateIoRestOptions options)
            : base(logger, GateIoExchange.Metadata.Id, httpClient, options.Environment.RestClientAddress!, options, options.RebateOptions)
        {
            _baseClient = baseClient;

            Partner = new GateIoRestClientRebateApiPartner(this);

            ParameterPositions[HttpMethod.Delete] = HttpMethodParameterPosition.InUri;
        }

        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(GateIoExchange._serializerContext));

        /// <inheritdoc />
        protected override GateIoAuthenticationProvider CreateAuthenticationProvider(GateIoCredentials credentials)
            => new GateIoAuthenticationProvider(credentials);

        internal Task<HttpResult<T>> SendAsync<T>(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? requestHeaders = null)
        {
            return base.SendAsync<T>(definition, parameters, cancellationToken, requestHeaders, weight);
        }

        /// <inheritdoc />
        protected override Task<HttpResult<DateTime>> GetServerTimestampAsync()
            => _baseClient.SpotApi.ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => GateIoExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);
    }
}
