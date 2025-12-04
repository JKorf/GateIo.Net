using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;
using CryptoExchange.Net;
using System;
using GateIo.Net.Objects.Internal;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Clients;

namespace GateIo.Net.Objects.Sockets
{
    internal class GateIoAuthQuery<T> : Query<GateIoSocketResponse<T>>
    {
        private readonly SocketApiClient _client;

        public GateIoAuthQuery(SocketApiClient client, string channel, string evnt, string[]? payload) 
            : base(new GateIoSocketAuthRequest<string[]> { Channel = channel, Event = evnt, Id = ExchangeHelpers.NextId(), Payload = payload, Timestamp = (long)DateTimeConverter.ConvertToSeconds(DateTime.UtcNow) }, false, 1)
        {
            _client = client;
            MessageMatcher = MessageMatcher.Create<GateIoSocketResponse<T>>(((GateIoSocketAuthRequest<string[]>)Request).Id.ToString(), HandleMessage);
            MessageRouter = MessageRouter.CreateWithoutTopicFilter<GateIoSocketResponse<T>>(((GateIoSocketAuthRequest<string[]>)Request).Id.ToString(), HandleMessage);
        }

        public CallResult<GateIoSocketResponse<T>> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, GateIoSocketResponse<T> message)
        {
            if (message.Error != null)
                return new CallResult<GateIoSocketResponse<T>>(new ServerError(message.Error.Code, _client.GetErrorInfo(message.Error.Code, message.Error.Message)), originalData);

            return new CallResult<GateIoSocketResponse<T>>(message, originalData, null);
        }
    }
}
