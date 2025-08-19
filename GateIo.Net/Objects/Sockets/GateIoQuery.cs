using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;
using System;
using System.Collections;
using GateIo.Net.Objects.Internal;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Clients;

namespace GateIo.Net.Objects.Sockets
{
    internal class GateIoQuery<TRequest, T> : Query<GateIoSocketResponse<T>>
    {
        private readonly SocketApiClient _client;

        public GateIoQuery(SocketApiClient client, long id, string channel, string evnt, TRequest? payload, bool authenticated = false) : base(new GateIoSocketRequest<TRequest> { Channel = channel, Event = evnt, Id = id, Payload = payload, Timestamp = (long)DateTimeConverter.ConvertToSeconds(DateTime.UtcNow) }, authenticated, 1)
        {
            _client = client;
            MessageMatcher = MessageMatcher.Create<GateIoSocketResponse<T>>(id.ToString(), HandleMessage);
        }

        public CallResult<GateIoSocketResponse<T>> HandleMessage(SocketConnection connection, DataEvent<GateIoSocketResponse<T>> message)
        {
            if (message.Data.Error != null)
                return message.ToCallResult<GateIoSocketResponse<T>>(new ServerError(message.Data.Error.Code, _client.GetErrorInfo(message.Data.Error.Code, message.Data.Error.Message)));

            return message.ToCallResult();
        }
    }
}
