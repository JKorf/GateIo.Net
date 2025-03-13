using CryptoExchange.Net;
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
        /// <inheritdoc />
        public override HashSet<string> ListenerIdentifiers { get; set; }

        private readonly Action<DataEvent<T>> _handler;
        private readonly string _channel;
        private readonly string[]? _payload;

        /// <inheritdoc />
        public override Type? GetMessageType(IMessageAccessor message)
        {
            return typeof(GateIoSocketMessage<T>);
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="channel"></param>
        /// <param name="handler"></param>
        /// <param name="identifiers"></param>
        /// <param name="payload"></param>
        public GateIoAuthSubscription(ILogger logger, string channel, IEnumerable<string> identifiers, string[]? payload, Action<DataEvent<T>> handler) : base(logger, false)
        {
            _handler = handler;
            _channel = channel;
            _payload = payload;
            ListenerIdentifiers = new HashSet<string>(identifiers);
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
        public override CallResult DoHandleMessage(SocketConnection connection, DataEvent<object> message)
        {
            var data = (GateIoSocketMessage<T>)message.Data;
            _handler.Invoke(message.As(data.Result, data.Channel, null, SocketUpdateType.Update).WithDataTimestamp(data.Timestamp));
            return CallResult.SuccessResult;
        }
    }
}
