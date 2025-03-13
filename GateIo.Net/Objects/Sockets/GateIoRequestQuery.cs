﻿using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;
using System;
using System.Collections;
using GateIo.Net.Objects.Internal;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Converters.SystemTextJson;

namespace GateIo.Net.Objects.Sockets
{
    internal class GateIoRequestQuery<TRequest, TResponse> : Query<GateIoSocketRequestResponse<TResponse>, TResponse>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; }

        /// <inheritdoc />
        public override Type? GetMessageType(IMessageAccessor message) => typeof(GateIoSocketRequestResponse<TResponse>);

        public GateIoRequestQuery(long id, string channel, string evnt, TRequest? payload, bool authenticated = false, Dictionary<string, string>? headers = null) 
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
            ListenerIdentifiers = new HashSet<string> { id.ToString(), id + "ack" };
            RequiredResponses = 1;
        }

        public override CallResult<TResponse> HandleMessage(SocketConnection connection, DataEvent<GateIoSocketRequestResponse<TResponse>> message)
        {
            // If this is an Acknowledge message we need another message with the actual result
            if (message.Data.Acknowledge)
                RequiredResponses = 2;

            if (message.Data.Header.Status != 200)
                return message.ToCallResult<TResponse>(new ServerError(message.Data.Header.Status, message.Data.Data.Error!.Message));

            return message.ToCallResult(message.Data.Data.Result!);
        }
    }
}
