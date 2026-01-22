using CryptoExchange.Net;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using GateIo.Net.Objects.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace GateIo.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class GateIoSubscription<T> : Subscription
    {
        private readonly SocketApiClient _client;
        private readonly Action<DateTime, string?, GateIoSocketMessage<T>> _handler;
        private readonly string _channel;
        private readonly string[] _payload;
        private readonly string[]? _symbols;

        public GateIoSubscription(ILogger logger, SocketApiClient client, string channel, string[]? symbols, IEnumerable<string> identifiers, string[] payload, Action<DateTime, string?, GateIoSocketMessage<T>> handler, bool auth) : base(logger, auth)
        {
            _client = client;
            _handler = handler;
            _channel = channel;
            _payload = payload;
            _symbols = symbols;

            IndividualSubscriptionCount = symbols?.Length ?? 1;

            MessageRouter = MessageRouter.CreateWithOptionalTopicFilters<GateIoSocketMessage<T>>(channel, _symbols, DoHandleMessage);
        }

        /// <inheritdoc />
        protected override Query? GetSubQuery(SocketConnection connection)
            => new GateIoQuery<string[], GateIoSubscriptionResponse>(_client, ExchangeHelpers.NextId(), _channel, "subscribe", _payload);

        /// <inheritdoc />
        protected override Query? GetUnsubQuery(SocketConnection connection)
            => new GateIoQuery<string[], GateIoSubscriptionResponse>(_client, ExchangeHelpers.NextId(), _channel, "unsubscribe", _payload);


        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, GateIoSocketMessage<T> message)
        {
            _handler.Invoke(receiveTime, originalData, message);
            return CallResult.SuccessResult;
        }
    }
}
