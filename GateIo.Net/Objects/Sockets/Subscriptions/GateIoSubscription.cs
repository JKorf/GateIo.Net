using CryptoExchange.Net;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using GateIo.Net.Objects.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace GateIo.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class GateIoSubscription<T> : Subscription<GateIoSocketResponse<GateIoSubscriptionResponse>, GateIoSocketResponse<GateIoSubscriptionResponse>>
    {
        private readonly SocketApiClient _client;
        private readonly Action<DataEvent<T>> _handler;
        private readonly string _channel;
        private readonly string[] _payload;

        public GateIoSubscription(ILogger logger, SocketApiClient client, string channel, IEnumerable<string> identifiers, string[] payload, Action<DataEvent<T>> handler, bool auth) : base(logger, auth)
        {
            _client = client;
            _handler = handler;
            _channel = channel;
            _payload = payload;

            MessageMatcher = MessageMatcher.Create<GateIoSocketMessage<T>>(identifiers, DoHandleMessage);
        }

        /// <inheritdoc />
        protected override Query? GetSubQuery(SocketConnection connection)
            => new GateIoQuery<string[], GateIoSubscriptionResponse>(_client, ExchangeHelpers.NextId(), _channel, "subscribe", _payload);

        /// <inheritdoc />
        protected override Query? GetUnsubQuery(SocketConnection connection)
            => new GateIoQuery<string[], GateIoSubscriptionResponse>(_client, ExchangeHelpers.NextId(), _channel, "unsubscribe", _payload);


        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<GateIoSocketMessage<T>> message)
        {
            _handler.Invoke(message.As(message.Data.Result, message.Data.Channel, null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Timestamp));
            return CallResult.SuccessResult;
        }
    }
}
