using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;
using System;
using System.Collections;
using GateIo.Net.Objects.Internal;
using CryptoExchange.Net.Converters.SystemTextJson;

namespace GateIo.Net.Objects.Sockets
{
    internal class GateIoQuery<TRequest, T> : Query<GateIoSocketResponse<T>>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; }

        public GateIoQuery(long id, string channel, string evnt, TRequest? payload, bool authenticated = false) : base(new GateIoSocketRequest<TRequest> { Channel = channel, Event = evnt, Id = id, Payload = payload, Timestamp = (long)DateTimeConverter.ConvertToSeconds(DateTime.UtcNow) }, authenticated, 1)
        {
            ListenerIdentifiers = new HashSet<string> { id.ToString() };
        }

        public override CallResult<GateIoSocketResponse<T>> HandleMessage(SocketConnection connection, DataEvent<GateIoSocketResponse<T>> message)
        {
            if (message.Data.Error != null)
                return message.ToCallResult<GateIoSocketResponse<T>>(new ServerError(message.Data.Error.Code, message.Data.Error.Message));

            return message.ToCallResult(message.Data);
        }
    }
}
