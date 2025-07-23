using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;
using CryptoExchange.Net;
using System;
using GateIo.Net.Objects.Internal;
using CryptoExchange.Net.Converters.SystemTextJson;

namespace GateIo.Net.Objects.Sockets
{
    internal class GateIoAuthQuery<T> : Query<GateIoSocketResponse<T>>
    {
        public GateIoAuthQuery(string channel, string evnt, string[]? payload) 
            : base(new GateIoSocketAuthRequest<string[]> { Channel = channel, Event = evnt, Id = ExchangeHelpers.NextId(), Payload = payload, Timestamp = (long)DateTimeConverter.ConvertToSeconds(DateTime.UtcNow) }, false, 1)
        {
            MessageMatcher = MessageMatcher.Create<GateIoSocketResponse<T>>(((GateIoSocketAuthRequest<string[]>)Request).Id.ToString(), HandleMessage);
        }

        public CallResult<GateIoSocketResponse<T>> HandleMessage(SocketConnection connection, DataEvent<GateIoSocketResponse<T>> message)
        {
            if (message.Data.Error != null)
                return message.ToCallResult<GateIoSocketResponse<T>>(new ServerError(message.Data.Error.Code, message.Data.Error.Message));

            return message.ToCallResult();
        }
    }
}
