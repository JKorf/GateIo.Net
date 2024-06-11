using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters.JsonNet;
using System;
using System.Collections;
using GateIo.Net.Objects.Internal;

namespace GateIo.Net.Objects.Sockets
{
    internal class GateIoQuery<TRequest, TResponse> : Query<GateIoSocketResponse<TResponse>>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; }

        public GateIoQuery(long id, string channel, string evnt, TRequest? payload, bool authenticated = false) : base(new GateIoSocketRequest<TRequest> { Channel = channel, Event = evnt, Id = id, Payload = payload, Timestamp = (long)DateTimeConverter.ConvertToSeconds(DateTime.UtcNow) }, authenticated, 1)
        {
            ListenerIdentifiers = new HashSet<string> { id.ToString() };
        }

        public override CallResult<GateIoSocketResponse<TResponse>> HandleMessage(SocketConnection connection, DataEvent<GateIoSocketResponse<TResponse>> message)
        {
            if (message.Data.Error != null)
                return message.ToCallResult<GateIoSocketResponse<TResponse>>(new ServerError(message.Data.Error.Code, message.Data.Error.Message));

            return message.ToCallResult(message.Data);
        }
    }
}
