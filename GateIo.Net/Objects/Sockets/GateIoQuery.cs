using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using System;
using GateIo.Net.Objects.Internal;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Sockets.Default;

namespace GateIo.Net.Objects.Sockets
{
    internal class GateIoQuery<TRequest, T> : Query<GateIoSocketResponse<T>>
    {
        private readonly SocketApiClient _client;

        public GateIoQuery(SocketApiClient client, long id, string channel, string evnt, TRequest? payload, bool authenticated = false) : base(new GateIoSocketRequest<TRequest> { Channel = channel, Event = evnt, Id = id, Payload = payload, Timestamp = (long)DateTimeConverter.ConvertToSeconds(DateTime.UtcNow) }, authenticated, 1)
        {
            _client = client;
            MessageMatcher = MessageMatcher.Create<GateIoSocketResponse<T>>(id.ToString(), HandleMessage);
            MessageRouter = MessageRouter.CreateWithoutTopicFilter<GateIoSocketResponse<T>>(id.ToString(), HandleMessage);
        }

        public CallResult<GateIoSocketResponse<T>> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, GateIoSocketResponse<T> message)
        {
            if (message.Error != null)
                return new CallResult<GateIoSocketResponse<T>>(new ServerError(message.Error.Code, _client.GetErrorInfo(message.Error.Code, message.Error.Message)));

            return new CallResult<GateIoSocketResponse<T>>(message, originalData, null);
        }
    }
}
