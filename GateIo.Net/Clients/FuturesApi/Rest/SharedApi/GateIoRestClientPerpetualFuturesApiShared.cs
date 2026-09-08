using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using GateIo.Net.Enums;
using GateIo.Net.Interfaces.Clients.SpotApi;
using GateIo.Net.Objects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace GateIo.Net.Clients.FuturesApi
{
    internal partial class GateIoRestClientPerpetualFuturesSharedApi :
        SharedApiBase,
        IGateIoRestClientPerpetualFuturesApiShared,
        IGateIoRestClientPerpetualFuturesSharedApi
    {
        private readonly GateIoRestClientPerpetualFuturesApi _api;

        private const string _topicId = "GateIoFutures";
        private const string _exchangeName = "GateIo";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(GateIoExchange.Metadata, this);

        public GateIoRestClientPerpetualFuturesSharedApi(GateIoRestClientPerpetualFuturesApi api)
            : base(
                  SharedTransport.Rest,
                  api.Exchange,
                  [TradingMode.PerpetualLinear, TradingMode.PerpetualInverse],
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                GetBalancesOptions,
                GetTickerOptions,
                GetAllTickersOptions,
                GetBookTickerOptions,
                GetFuturesSymbolsOptions,
                PlaceFuturesOrderOptions,
                GetFuturesOrderOptions,
                GetOpenFuturesOrdersOptions,
                GetClosedFuturesOrdersOptions,
                GetFuturesOrderTradesOptions,
                GetFuturesUserTradeHistoryOptions,
                CancelFuturesOrderOptions,
                GetPositionsOptions,
                CloseFullPositionOptions,
                GetFuturesOrderByClientOrderIdOptions,
                CancelFuturesOrderByClientOrderIdOptions,
                GetKlinesOptions,
                GetIndexPriceKlinesOptions,
                GetRecentTradesOptions,
                GetTradeHistoryOptions,
                GetLeverageOptions,
                SetLeverageOptions,
                GetOrderBookOptions,
                GetOpenInterestOptions,
                GetFundingRateHistoryOptions,
                GetPositionModeOptions,
                SetPositionModeOptions,
                GetPositionHistoryOptions,
                GetFeeOptions,
                PlaceFuturesTriggerOrderOptions,
                GetFuturesTriggerOrderOptions,
                CancelFuturesTriggerOrderOptions,
                SetFuturesTpSlOptions,
                CancelFuturesTpSlOptions
                );
        }
    }
}
