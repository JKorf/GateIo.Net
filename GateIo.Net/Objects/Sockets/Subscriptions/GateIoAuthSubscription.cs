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
    internal class GateIoAuthSubscription<T> : Subscription<GateIoSocketResponse<GateIoSubscriptionResponse>, GateIoSocketResponse<GateIoSubscriptionResponse>>
    {
        private readonly SocketApiClient _client;
        private readonly Action<DataEvent<T>> _handler;
        private readonly string _channel;
        private readonly string[]? _payload;

        /// <summary>
        /// ctor
        /// </summary>
        public GateIoAuthSubscription(ILogger logger, SocketApiClient client, string channel, IEnumerable<string> identifiers, string[]? payload, Action<DataEvent<T>> handler) : base(logger, false)
        {
            _client = client;
            _handler = handler;
            _channel = channel;
            _payload = payload;
            MessageMatcher = MessageMatcher.Create<GateIoSocketMessage<T>>(identifiers, DoHandleMessage);
        }

        /// <inheritdoc />
        protected override Query? GetSubQuery(SocketConnection connection)
        {
            var provider = (GateIoAuthenticationProvider)connection.ApiClient.AuthenticationProvider!;
            var query = new GateIoAuthQuery<GateIoSubscriptionResponse>(_client, _channel, "subscribe", _payload);
            var request = (GateIoSocketAuthRequest<string[]>)query.Request;
            var sign = provider.SignSocketRequest($"channel={_channel}&event=subscribe&time={request.Timestamp}");
            request.Auth = new GateIoSocketAuth { Key = provider.ApiKey, Sign = sign, Method = "api_key" };
            return query;
        }

        /// <inheritdoc />
        protected override Query? GetUnsubQuery(SocketConnection connection)
        { 
            return new GateIoQuery<string[], GateIoSubscriptionResponse>(_client, ExchangeHelpers.NextId(), _channel, "unsubscribe", _payload);
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<GateIoSocketMessage<T>> message)
        {
            _handler.Invoke(message.As(message.Data.Result, message.Data.Channel, null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Timestamp));
            return CallResult.SuccessResult;
        }
    }
}
