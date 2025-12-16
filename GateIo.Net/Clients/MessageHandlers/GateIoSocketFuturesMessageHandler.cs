using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Converters.SystemTextJson.MessageHandlers;
using GateIo.Net.Objects.Internal;
using GateIo.Net.Objects.Models;
using System.Linq;
using System.Text.Json;

namespace GateIo.Net.Clients.MessageHandlers
{
    internal class GateIoSocketFuturesMessageHandler : JsonSocketMessageHandler
    {
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(GateIoExchange._serializerContext);

        public GateIoSocketFuturesMessageHandler()
        {
            AddTopicMapping<GateIoSocketRequestResponse>(x => x.RequestId);
            
            AddTopicMapping<GateIoSocketMessage<GateIoPerpOrderBookV2Update>>(x => x.Result.Contract + x.Result.Depth);
            AddTopicMapping<GateIoSocketMessage<GateIoPartialOrderBookUpdate>>(x => x.Result.Symbol);
            AddTopicMapping<GateIoSocketMessage<GateIoPerpTradeUpdate[]>>(x => x.Result.First().Contract);
            AddTopicMapping<GateIoSocketMessage<GateIoPerpTickerUpdate[]>>(x => x.Result.First().Contract);
            AddTopicMapping<GateIoSocketMessage<GateIoPerpBookTickerUpdate>>(x => x.Result.Contract);
            AddTopicMapping<GateIoSocketMessage<GateIoPerpOrderBookUpdate>>(x => x.Result.Contract);
            AddTopicMapping<GateIoSocketMessage<GateIoPerpKlineUpdate[]>>(x => x.Result.First().Contract + x.Result.First().Interval);
            AddTopicMapping<GateIoSocketMessage<GateIoPerpContractStats>>(x => x.Result.Contract);
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
