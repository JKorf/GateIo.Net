using CryptoExchange.Net.Sockets;
using CryptoExchange.Net;
using System;
using GateIo.Net.Objects.Internal;
using CryptoExchange.Net.Converters.SystemTextJson;

namespace GateIo.Net.Objects.Sockets
{
    internal class GateIoPingQuery : Query<GateIoSocketResponse<string>>
    {
        public GateIoPingQuery(string channel) : base(new GateIoSocketRequest<object> { Channel = channel, Id = ExchangeHelpers.NextId(), Timestamp = (long)DateTimeConverter.ConvertToSeconds(DateTime.UtcNow) }, false, 1)
        {
            RequestTimeout = TimeSpan.FromSeconds(5);
            MessageMatcher = MessageMatcher.Create<GateIoSocketResponse<string>>(((GateIoSocketRequest<object>)Request).Id.ToString());
            MessageRouter = MessageRouter.CreateWithoutHandler<GateIoSocketResponse<string>>(((GateIoSocketRequest<object>)Request).Id.ToString());
        }
    }
}
