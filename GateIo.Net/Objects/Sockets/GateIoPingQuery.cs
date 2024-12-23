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
    internal class GateIoPingQuery : Query<GateIoSocketResponse<object>>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; }

        public GateIoPingQuery(string channel) : base(new GateIoSocketRequest<object> { Channel = channel, Id = ExchangeHelpers.NextId(), Timestamp = (long)DateTimeConverter.ConvertToSeconds(DateTime.UtcNow) }, false, 1)
        {
            RequestTimeout = TimeSpan.FromSeconds(5);
            ListenerIdentifiers = new HashSet<string> { ((GateIoSocketRequest<object>)Request).Id.ToString() };
        }

        public override CallResult<GateIoSocketResponse<object>> HandleMessage(SocketConnection connection, DataEvent<GateIoSocketResponse<object>> message)
        {
            return message.ToCallResult(message.Data);
        }
    }
}
