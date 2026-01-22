using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;
using System;
using GateIo.Net.Objects.Internal;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Sockets.Default;

namespace GateIo.Net.Objects.Sockets
{
    internal class GateIoRequestQuery<TRequest, T> : Query<T>
    {
        private readonly SocketApiClient _client;

        public GateIoRequestQuery(SocketApiClient client, long id, string channel, string evnt, TRequest? payload, bool authenticated = false, Dictionary<string, string>? headers = null) 
            : base(new GateIoSocketRequest<GateIoSocketRequestWrapper<TRequest>> { 
                Channel = channel,
                Event = evnt,
                Id = id,
                Payload = new GateIoSocketRequestWrapper<TRequest> 
                { 
                    Parameters = payload, 
                    Id = id.ToString(),
                    Headers = headers
                }, 
                Timestamp = (long)DateTimeConverter.ConvertToSeconds(DateTime.UtcNow.AddSeconds(-1)) }, authenticated, 1)
        {
            _client = client;
            RequiredResponses = 1;

            MessageRouter = MessageRouter.CreateWithoutTopicFilter<GateIoSocketRequestResponse<T>>(id.ToString(), HandleMessage);
        }

        public CallResult<T> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, GateIoSocketRequestResponse<T> message)
        {
            // If this is an Acknowledge message we need another message with the actual result
            if (message.Acknowledge)
                RequiredResponses = 2;

            if (message.Header.Status != 200)
                return new CallResult<T>(new ServerError(message.Header.Status, _client.GetErrorInfo(message.Header.Status, message.Data.Error!.Message)));

            return new CallResult<T>(message.Data.Result!, originalData, null);
        }
    }
}
