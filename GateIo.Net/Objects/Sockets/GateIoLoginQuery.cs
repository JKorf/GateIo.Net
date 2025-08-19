using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;
using System;
using System.Collections;
using GateIo.Net.Objects.Internal;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Clients;

namespace GateIo.Net.Objects.Sockets
{
    internal class GateIoLoginQuery : Query<GateIoSocketRequestResponse<GateIoSocketLoginResponse>>
    {
        private readonly SocketApiClient _client;

        public GateIoLoginQuery(SocketApiClient client, long id, string channel, string evnt, string key, string sign, long timestamp) 
            : base(new GateIoSocketRequest<GateIoSocketLoginRequest> { 
                Channel = channel,
                Event = evnt,
                Id = id,
                Timestamp = timestamp,
                Payload = new GateIoSocketLoginRequest
                {
                    Key = key,
                    Signature = sign,
                    Timestamp = timestamp.ToString(),
                    Id = id.ToString()
                }
            }, false, 1)
        {
            _client = client;

            MessageMatcher = MessageMatcher.Create<GateIoSocketRequestResponse<GateIoSocketLoginResponse>>(id.ToString(), HandleMessage);
        }

        public CallResult<GateIoSocketRequestResponse<GateIoSocketLoginResponse>> HandleMessage(SocketConnection connection, DataEvent<GateIoSocketRequestResponse<GateIoSocketLoginResponse>> message)
        {
            if (message.Data.Header.Status != 200)
                return message.ToCallResult<GateIoSocketRequestResponse<GateIoSocketLoginResponse>>(new ServerError(message.Data.Header.Status, _client.GetErrorInfo(message.Data.Header.Status, message.Data.Data.Error!.Message)));

            return message.ToCallResult();
        }
    }
}
