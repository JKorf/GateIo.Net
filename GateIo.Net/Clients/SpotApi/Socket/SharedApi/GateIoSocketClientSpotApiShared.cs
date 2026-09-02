using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using GateIo.Net.Clients.FuturesApi;
using GateIo.Net.Enums;
using GateIo.Net.Interfaces.Clients.SpotApi;
using GateIo.Net.Objects.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GateIo.Net.Clients.SpotApi
{
    internal partial class GateIoSocketClientSpotSharedApi : 
        SharedApiBase,
        IGateIoSocketClientSpotApiShared,
        IGateIoSocketClientSpotSharedApi
    {
        private readonly GateIoSocketClientSpotApi _api;

        private const string _topicId = "GateIoSpot";
        private const string _exchangeName = "GateIo";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(GateIoExchange.Metadata, this);

        public GateIoSocketClientSpotSharedApi(GateIoSocketClientSpotApi api)
           : base(
                 SharedTransport.Socket,
                 api.Exchange,
                 [TradingMode.Spot],
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
                SubscribeSpotOrderOptions,
                SubscribeUserTradeOptions,
                PlaceSpotOrderOptions,
                CancelSpotOrderOptions
                );
        }
    }
}
