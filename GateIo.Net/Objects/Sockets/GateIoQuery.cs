using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters.JsonNet;
using System;

namespace GateIo.Net.Objects.Sockets
{
    internal class GateIoQuery<T> : Query<GateIoSocketResponse<T>>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; }

        public GateIoQuery(string channel, string evnt, IEnumerable<string>? payload) : base(new GateIoSocketRequest { Channel = channel, Event = evnt, Id = ExchangeHelpers.NextId(), Payload = payload, Timestamp = (long)DateTimeConverter.ConvertToSeconds(DateTime.UtcNow) }, false, 1)
        {
            ListenerIdentifiers = new HashSet<string> { ((GateIoSocketRequest)Request).Id.ToString() };
        }

        public override CallResult<GateIoSocketResponse<T>> HandleMessage(SocketConnection connection, DataEvent<GateIoSocketResponse<T>> message)
        {
            if (message.Data.Error != null)
                return message.ToCallResult<GateIoSocketResponse<T>>(new ServerError(message.Data.Error.Code, message.Data.Error.Message));

            return message.ToCallResult(message.Data);
        }
    }
}
