using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.Net.Interfaces.CommonClients;
using GateIo.Net.Interfaces.Clients.SpotApi;
using GateIo.Net.Objects.Options;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Globalization;
using System.Linq;

namespace GateIo.Net.Clients.SpotApi
{
    /// <inheritdoc cref="IGateIoRestClientSpotApi" />
    internal class GateIoRestClientSpotApi : RestApiClient, IGateIoRestClientSpotApi, ISpotClient
    {
        #region fields 
        internal static TimeSyncState _timeSyncState = new TimeSyncState("Spot Api");
        internal string _brokerId;
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

        /// <summary>
        /// Event triggered when an order is placed via this client. Only available for Spot orders
        /// </summary>
        public event Action<OrderId>? OnOrderPlaced;
        /// <summary>
        /// Event triggered when an order is canceled via this client. Note that this does not trigger when using CancelAllOrdersAsync. Only available for Spot orders
        /// </summary>
        public event Action<OrderId>? OnOrderCanceled;

        #region constructor/destructor
        internal GateIoRestClientSpotApi(ILogger logger, HttpClient? httpClient, GateIoRestOptions options)
            : base(logger, httpClient, options.Environment.RestClientAddress, options, options.SpotOptions)
        {
            Account = new GateIoRestClientSpotApiAccount(this);
            ExchangeData = new GateIoRestClientSpotApiExchangeData(logger, this);
            Trading = new GateIoRestClientSpotApiTrading(logger, this);

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

        internal Task<WebCallResult<T>> SendAsync<T>(RequestDefinition definition, ParameterCollection? queryParameters, ParameterCollection? bodyParameters, CancellationToken cancellationToken, int? weight = null) where T : class
            => SendToAddressAsync<T>(BaseAddress, definition, queryParameters, bodyParameters, cancellationToken, weight);

        internal async Task<WebCallResult<T>> SendToAddressAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? queryParameters, ParameterCollection? bodyParameters, CancellationToken cancellationToken, int? weight = null) where T : class
        {
            var result = await base.SendAsync<T>(baseAddress, definition, queryParameters, bodyParameters, cancellationToken, null, weight).ConfigureAwait(false);

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

        /// <inheritdoc />
        public ISpotClient CommonSpotClient => this;

        /// <inheritdoc />
        public string GetSymbolName(string baseAsset, string quoteAsset) => baseAsset.ToUpperInvariant() + "_" + quoteAsset.ToUpperInvariant();

        internal void InvokeOrderPlaced(OrderId id)
        {
            OnOrderPlaced?.Invoke(id);
        }

        internal void InvokeOrderCanceled(OrderId id)
        {
            OnOrderCanceled?.Invoke(id);
        }

        async Task<WebCallResult<OrderId>> ISpotClient.PlaceOrderAsync(string symbol, CommonOrderSide side, CommonOrderType type, decimal quantity, decimal? price, string? accountId, string? clientOrderId, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for Gate.io " + nameof(ISpotClient.PlaceOrderAsync), nameof(symbol));

            var result = await Trading.PlaceOrderAsync(symbol,
                side == CommonOrderSide.Sell ? Enums.OrderSide.Sell : Enums.OrderSide.Buy,
                type == CommonOrderType.Limit ? Enums.NewOrderType.Limit : Enums.NewOrderType.Market,
                quantity,
                price,
                timeInForce: type == CommonOrderType.Limit ? Enums.TimeInForce.GoodTillCancel : null,
                text: clientOrderId).ConfigureAwait(false);
            if (!result)
                return result.As<OrderId>(null);

            return result.As(new OrderId
            {
                SourceObject = result.Data,
                Id = result.Data.Id.ToString(CultureInfo.InvariantCulture)
            });
        }

        async Task<WebCallResult<Order>> IBaseRestClient.GetOrderAsync(string orderId, string? symbol, CancellationToken ct)
        {
            if (!long.TryParse(orderId, out var id))
                throw new ArgumentException("Order id invalid", nameof(orderId));

            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for Gate.io " + nameof(ISpotClient.GetOrderAsync), nameof(symbol));

            var order = await Trading.GetOrderAsync(symbol!, id, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.As<Order>(null);

            return order.As(new Order
            {
                SourceObject = order,
                Id = order.Data.Id.ToString(CultureInfo.InvariantCulture),
                Symbol = order.Data.Symbol,
                Price = order.Data.Price,
                Quantity = order.Data.Quantity,
                QuantityFilled = order.Data.QuantityFilled,
                Side = order.Data.Side == Enums.OrderSide.Buy ? CommonOrderSide.Buy : CommonOrderSide.Sell,
                Type = order.Data.Type == Enums.OrderType.Market ? CommonOrderType.Market : order.Data.Type == Enums.OrderType.Limit ? CommonOrderType.Limit : CommonOrderType.Other,
                Status = order.Data.Status == Enums.OrderStatus.Open ? CommonOrderStatus.Active : order.Data.Status == Enums.OrderStatus.Canceled ? CommonOrderStatus.Canceled : CommonOrderStatus.Filled,
                Timestamp = order.Data.CreateTime
            });
        }

        async Task<WebCallResult<IEnumerable<UserTrade>>> IBaseRestClient.GetOrderTradesAsync(string orderId, string? symbol, CancellationToken ct)
        {
            if (!long.TryParse(orderId, out var id))
                throw new ArgumentException("Order id invalid", nameof(orderId));

            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for Gate.io " + nameof(ISpotClient.GetOrderTradesAsync), nameof(symbol));

            var trades = await Trading.GetUserTradesAsync(symbol!, id, ct: ct).ConfigureAwait(false);
            if (!trades)
                return trades.As<IEnumerable<UserTrade>>(null);

            return trades.As(trades.Data.Select(t =>
                new UserTrade
                {
                    SourceObject = t,
                    Id = t.Id.ToString(CultureInfo.InvariantCulture),
                    OrderId = t.OrderId.ToString(CultureInfo.InvariantCulture),
                    Symbol = t.Symbol,
                    Price = t.Price,
                    Quantity = t.Quantity,
                    Fee = t.Fee,
                    FeeAsset = t.FeeAsset,
                    Timestamp = t.CreateTime
                }));
        }

        async Task<WebCallResult<IEnumerable<Order>>> IBaseRestClient.GetOpenOrdersAsync(string? symbol, CancellationToken ct)
        {
            var orderInfo = await Trading.GetOpenOrdersAsync(ct: ct).ConfigureAwait(false);
            if (!orderInfo)
                return orderInfo.As<IEnumerable<Order>>(null);

            return orderInfo.As(orderInfo.Data.SelectMany(d => d.Orders).Select(s =>
                new Order
                {
                    SourceObject = s,
                    Id = s.Id.ToString(CultureInfo.InvariantCulture),
                    Symbol = s.Symbol,
                    Price = s.Price,
                    Quantity = s.Quantity,
                    QuantityFilled = s.QuantityFilled,
                    Side = s.Side == Enums.OrderSide.Buy ? CommonOrderSide.Buy : CommonOrderSide.Sell,
                    Type = s.Type == Enums.OrderType.Market ? CommonOrderType.Market : s.Type == Enums.OrderType.Limit ? CommonOrderType.Limit : CommonOrderType.Other,
                    Status = s.Status == Enums.OrderStatus.Open ? CommonOrderStatus.Active : s.Status == Enums.OrderStatus.Canceled ? CommonOrderStatus.Canceled : CommonOrderStatus.Filled,
                    Timestamp = s.CreateTime
                }));
        }

        async Task<WebCallResult<IEnumerable<Order>>> IBaseRestClient.GetClosedOrdersAsync(string? symbol, CancellationToken ct)
        {
            var orderInfo = await Trading.GetOrdersAsync(false, symbol, ct: ct).ConfigureAwait(false);
            if (!orderInfo)
                return orderInfo.As<IEnumerable<Order>>(null);

            return orderInfo.As(orderInfo.Data.Select(s =>
                new Order
                {
                    SourceObject = s,
                    Id = s.Id.ToString(CultureInfo.InvariantCulture),
                    Symbol = s.Symbol,
                    Price = s.Price,
                    Quantity = s.Quantity,
                    QuantityFilled = s.QuantityFilled,
                    Side = s.Side == Enums.OrderSide.Buy ? CommonOrderSide.Buy : CommonOrderSide.Sell,
                    Type = s.Type == Enums.OrderType.Market ? CommonOrderType.Market : s.Type == Enums.OrderType.Limit ? CommonOrderType.Limit : CommonOrderType.Other,
                    Status = s.Status == Enums.OrderStatus.Open ? CommonOrderStatus.Active : s.Status == Enums.OrderStatus.Canceled ? CommonOrderStatus.Canceled : CommonOrderStatus.Filled,
                    Timestamp = s.CreateTime
                }));
        }

        async Task<WebCallResult<OrderId>> IBaseRestClient.CancelOrderAsync(string orderId, string? symbol, CancellationToken ct)
        {
            if (!long.TryParse(orderId, out var id))
                throw new ArgumentException("Order id invalid", nameof(orderId));

            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for Gate.io " + nameof(ISpotClient.CancelOrderAsync), nameof(symbol));

            var order = await Trading.CancelOrderAsync(symbol!, id, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.As<OrderId>(null);

            return order.As(new OrderId
            {
                SourceObject = order.Data,
                Id = order.Data.Id.ToString(CultureInfo.InvariantCulture)
            });
        }

        async Task<WebCallResult<IEnumerable<Symbol>>> IBaseRestClient.GetSymbolsAsync(CancellationToken ct)
        {
            var exchangeInfo = await ExchangeData.GetSymbolsAsync(ct: ct).ConfigureAwait(false);
            if (!exchangeInfo)
                return exchangeInfo.As<IEnumerable<Symbol>>(null);

            return exchangeInfo.As(exchangeInfo.Data.Select(s =>
                new Symbol
                {
                    SourceObject = s,
                    Name = s.Name,
                    MinTradeQuantity = s.MinBaseQuantity,
                    PriceStep = s.PricePrecision,
                    QuantityStep = s.QuantityPrecision
                }));
        }

        async Task<WebCallResult<Ticker>> IBaseRestClient.GetTickerAsync(string symbol, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for Gate.io " + nameof(ISpotClient.GetTickerAsync), nameof(symbol));

            var tickers = await ExchangeData.GetTickersAsync(symbol, ct: ct).ConfigureAwait(false);
            if (!tickers)
                return tickers.As<Ticker>(null);

            var ticker = tickers.Data.Single();

            return tickers.As(new Ticker
            {
                SourceObject = ticker,
                Symbol = ticker.Symbol,
                HighPrice = ticker.HighPrice,
                LowPrice = ticker.LowPrice,
                Price24H = ticker.LastPrice * (1 - ticker.ChangePercentage24h / 100),
                LastPrice = ticker.LastPrice,
                Volume = ticker.BaseVolume
            });
        }

        async Task<WebCallResult<IEnumerable<Ticker>>> IBaseRestClient.GetTickersAsync(CancellationToken ct)
        {
            var tickers = await ExchangeData.GetTickersAsync(ct: ct).ConfigureAwait(false);
            if (!tickers)
                return tickers.As<IEnumerable<Ticker>>(null);

            return tickers.As(tickers.Data.Select(t => new Ticker
            {
                SourceObject = t,
                Symbol = t.Symbol,
                HighPrice = t.HighPrice,
                LowPrice = t.LowPrice,
                Price24H = t.LastPrice * (1 - t.ChangePercentage24h / 100),
                LastPrice = t.LastPrice,
                Volume = t.BaseVolume
            }));
        }

        async Task<WebCallResult<IEnumerable<Kline>>> IBaseRestClient.GetKlinesAsync(string symbol, TimeSpan timespan, DateTime? startTime, DateTime? endTime, int? limit, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for Gate.io " + nameof(ISpotClient.GetKlinesAsync), nameof(symbol));

            var klines = await ExchangeData.GetKlinesAsync(symbol, GetKlineIntervalFromTimespan(timespan), startTime, endTime, limit, ct: ct).ConfigureAwait(false);
            if (!klines)
                return klines.As<IEnumerable<Kline>>(null);

            return klines.As(klines.Data.Select(t => new Kline
            {
                SourceObject = t,
                HighPrice = t.HighPrice,
                LowPrice = t.LowPrice,
                OpenTime = t.OpenTime,
                ClosePrice = t.ClosePrice,
                OpenPrice = t.OpenPrice,
                Volume = t.BaseVolume
            }));
        }

        async Task<WebCallResult<OrderBook>> IBaseRestClient.GetOrderBookAsync(string symbol, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for Gate.io " + nameof(ISpotClient.GetOrderBookAsync), nameof(symbol));

            var orderbook = await ExchangeData.GetOrderBookAsync(symbol, ct: ct).ConfigureAwait(false);
            if (!orderbook)
                return orderbook.As<OrderBook>(null);

            return orderbook.As(new OrderBook
            {
                SourceObject = orderbook.Data,
                Asks = orderbook.Data.Asks.Select(a => new OrderBookEntry { Price = a.Price, Quantity = a.Quantity }),
                Bids = orderbook.Data.Bids.Select(b => new OrderBookEntry { Price = b.Price, Quantity = b.Quantity })
            });
        }

        async Task<WebCallResult<IEnumerable<Trade>>> IBaseRestClient.GetRecentTradesAsync(string symbol, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for Gate.io " + nameof(ISpotClient.GetRecentTradesAsync), nameof(symbol));

            var trades = await ExchangeData.GetTradesAsync(symbol, ct: ct).ConfigureAwait(false);
            if (!trades)
                return trades.As<IEnumerable<Trade>>(null);

            return trades.As(trades.Data.Select(t => new Trade
            {
                SourceObject = t,
                Symbol = symbol,
                Price = t.Price,
                Quantity = t.Quantity,
                Timestamp = t.CreateTime
            }));
        }

        async Task<WebCallResult<IEnumerable<Balance>>> IBaseRestClient.GetBalancesAsync(string? accountId, CancellationToken ct)
        {
            var balances = await Account.GetBalancesAsync(ct: ct).ConfigureAwait(false);
            if (!balances)
                return balances.As<IEnumerable<Balance>>(null);

            return balances.As(balances.Data.Select(t => new Balance
            {
                SourceObject = t,
                Asset = t.Asset,
                Available = t.Available,
                Total = t.Available + t.Locked
            }));
        }

        private static Enums.KlineInterval GetKlineIntervalFromTimespan(TimeSpan timeSpan)
        {
            if (timeSpan == TimeSpan.FromMinutes(1)) return Enums.KlineInterval.OneMinute;
            if (timeSpan == TimeSpan.FromMinutes(5)) return Enums.KlineInterval.FiveMinutes;
            if (timeSpan == TimeSpan.FromMinutes(15)) return Enums.KlineInterval.FifteenMinutes;
            if (timeSpan == TimeSpan.FromMinutes(30)) return Enums.KlineInterval.ThirtyMinutes;
            if (timeSpan == TimeSpan.FromHours(1)) return Enums.KlineInterval.OneHour;
            if (timeSpan == TimeSpan.FromHours(4)) return Enums.KlineInterval.FourHours;
            if (timeSpan == TimeSpan.FromHours(8)) return Enums.KlineInterval.EightHours;
            if (timeSpan == TimeSpan.FromDays(1)) return Enums.KlineInterval.OneDay;
            if (timeSpan == TimeSpan.FromDays(7)) return Enums.KlineInterval.OneWeek;
            if (timeSpan == TimeSpan.FromDays(30) || timeSpan == TimeSpan.FromDays(31)) return Enums.KlineInterval.OneMonth;

            throw new ArgumentException("Unsupported timespan for Gate.io Klines, check supported intervals using GateIo.Net.Enums.KlineInterval");
        }
    }
}
