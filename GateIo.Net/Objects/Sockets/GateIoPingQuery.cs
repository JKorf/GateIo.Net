﻿using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;
using CryptoExchange.Net;
using System;
using System.Collections;
using GateIo.Net.Objects.Internal;
using CryptoExchange.Net.Converters.SystemTextJson;

namespace GateIo.Net.Objects.Sockets
{
    internal class GateIoPingQuery : Query<GateIoSocketResponse<string>>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; }

        public GateIoPingQuery(string channel) : base(new GateIoSocketRequest<object> { Channel = channel, Id = ExchangeHelpers.NextId(), Timestamp = (long)DateTimeConverter.ConvertToSeconds(DateTime.UtcNow) }, false, 1)
        {
            RequestTimeout = TimeSpan.FromSeconds(5);
            ListenerIdentifiers = new HashSet<string> { ((GateIoSocketRequest<object>)Request).Id.ToString() };
        }

        public override CallResult<GateIoSocketResponse<string>> HandleMessage(SocketConnection connection, DataEvent<GateIoSocketResponse<string>> message)
        {
            return message.ToCallResult(message.Data);
        }
    }
}
