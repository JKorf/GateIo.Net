﻿using CryptoExchange.Net;
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
        private readonly Action<DataEvent<T>> _handler;
        private readonly string _channel;
        private readonly string[]? _payload;

        /// <summary>
        /// ctor
        /// </summary>
        public GateIoAuthSubscription(ILogger logger, string channel, IEnumerable<string> identifiers, string[]? payload, Action<DataEvent<T>> handler) : base(logger, false)
        {
            _handler = handler;
            _channel = channel;
            _payload = payload;
            MessageMatcher = MessageMatcher.Create<GateIoSocketMessage<T>>(identifiers, DoHandleMessage);
        }

        /// <inheritdoc />
        public override Query? GetSubQuery(SocketConnection connection)
        {
            var provider = (GateIoAuthenticationProvider)connection.ApiClient.AuthenticationProvider!;
            var query = new GateIoAuthQuery<GateIoSubscriptionResponse>(_channel, "subscribe", _payload);
            var request = (GateIoSocketAuthRequest<string[]>)query.Request;
            var sign = provider.SignSocketRequest($"channel={_channel}&event=subscribe&time={request.Timestamp}");
            request.Auth = new GateIoSocketAuth { Key = provider.ApiKey, Sign = sign, Method = "api_key" };
            return query;
        }

        /// <inheritdoc />
        public override Query? GetUnsubQuery()
        { 
            return new GateIoQuery<string[], GateIoSubscriptionResponse>(ExchangeHelpers.NextId(), _channel, "unsubscribe", _payload);
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<GateIoSocketMessage<T>> message)
        {
            _handler.Invoke(message.As(message.Data.Result, message.Data.Channel, null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Timestamp));
            return CallResult.SuccessResult;
        }
    }
}
