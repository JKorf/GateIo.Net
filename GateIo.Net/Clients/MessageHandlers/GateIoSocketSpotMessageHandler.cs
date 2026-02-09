using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Converters.SystemTextJson.MessageHandlers;
using GateIo.Net.Objects.Internal;
using GateIo.Net.Objects.Models;
using System.Text.Json;

namespace GateIo.Net.Clients.MessageHandlers
{
    internal class GateIoSocketSpotMessageHandler : JsonSocketMessageHandler
    {
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(GateIoExchange._serializerContext);

        public GateIoSocketSpotMessageHandler()
        {
            AddTopicMapping<GateIoSocketRequestResponse>(x => x.RequestId);
            AddTopicMapping<GateIoSocketMessage<GateIoTradeUpdate>>(x => x.Result.Symbol);
            AddTopicMapping<GateIoSocketMessage<GateIoTickerUpdate>>(x => x.Result.Symbol);
            AddTopicMapping<GateIoSocketMessage<GateIoKlineUpdate>>(x => x.Result.Symbol + x.Result.Interval);
            AddTopicMapping<GateIoSocketMessage<GateIoBookTickerUpdate>>(x => x.Result.Symbol);
            AddTopicMapping<GateIoSocketMessage<GateIoOrderBookUpdate>>(x => x.Result.Symbol);
            AddTopicMapping<GateIoSocketMessage<GateIoPartialOrderBookUpdate>>(x => x.Result.Symbol);
            AddTopicMapping<GateIoSocketMessage<GateIoPerpOrderBookV2Update>>(x => x.Result.Contract + x.Result.Depth);
        }

        protected override MessageTypeDefinition[] TypeEvaluators { get; } = [

             new MessageTypeDefinition {
                ForceIfFound = true,
                Fields = [
                    new PropertyFieldReference("id"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("id")!
            },

             new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("request_id"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("request_id")!
            },

             new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("channel"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("channel")!
            }
        ];
    }
}
