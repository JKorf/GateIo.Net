using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using GateIo.Net.Enums;
using GateIo.Net.Interfaces.Clients.PerpetualFuturesApi;
using GateIo.Net.Objects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace GateIo.Net.Clients.FuturesApi
{
    internal partial class GateIoSocketClientPerpetualFuturesSharedApi:
        SharedApiBase,
        IGateIoSocketClientPerpetualFuturesApiShared,
        IGateIoSocketClientPerpetualFuturesSharedApi
    {
        private readonly GateIoSocketClientPerpetualFuturesApi _api;

        private const string _topicId = "GateIoFutures";
        private const string _exchangeName = "GateIo";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(GateIoExchange.Metadata, this);

        public GateIoSocketClientPerpetualFuturesSharedApi(GateIoSocketClientPerpetualFuturesApi api)
           : base(
                 SharedTransport.Socket,
                 api,
                 [TradingMode.PerpetualLinear, TradingMode.PerpetualInverse],
                 () => api.Authenticated,
                 api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                SubscribeTickerOptions,
                SubscribeTradeOptions,
                SubscribeBookTickerOptions,
                SubscribeKlineOptions,
                SubscribeOrderBookOptions,
                SubscribeBalanceOptions,
                SubscribeFuturesOrderOptions,
                SubscribeUserTradeOptions,
                SubscribePositionOptions,
                PlaceFuturesOrderOptions,
                CancelFuturesOrderOptions
                );
        }
    }
}
