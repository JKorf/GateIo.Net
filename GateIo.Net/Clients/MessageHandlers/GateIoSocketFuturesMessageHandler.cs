using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net;
using GateIo.Net.Objects.Internal;
using GateIo.Net.Objects.Models;
using System;
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

        protected override MessageEvaluator[] TypeEvaluators { get; } = [

             new MessageEvaluator {
                Priority = 1,
                ForceIfFound = true,
                Fields = [
                    new PropertyFieldReference("id"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("id")!
            },

            // new MessageEvaluator {
            //    Priority = 2,
            //    Fields = [
            //        new PropertyFieldReference("request_id"),
            //        new PropertyFieldReference("ack") { Constraint = x => x!.Equals("True", System.StringComparison.Ordinal) },
            //        new PropertyFieldReference("status") { Depth = 2, Constraint = x => x!.Equals("200", System.StringComparison.Ordinal) },
            //    ],
            //    IdentifyMessageCallback = x => $"{x.FieldValue("request_id")}ack"
            //},

             new MessageEvaluator {
                Priority = 3,
                Fields = [
                    new PropertyFieldReference("request_id"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("request_id")!
            },

//             new MessageEvaluator {
//                Priority = 4,
//                Fields = [
//                    new PropertyFieldReference("channel") { Constraint = x => x!.Equals("futures.obu", StringComparison.Ordinal) },
//                    new PropertyFieldReference("s") { Depth = 2 },
//                ],
//#warning ?
//                 IdentifyMessageCallback = x => x.FieldValue("s")!
//            },

//            new MessageEvaluator {
//                Priority = 5,
//                Fields = [
//                    new PropertyFieldReference("channel") 
//                    {
//                        Constraint = x => x!.Equals("futures.trades", StringComparison.Ordinal) || x!.Equals("futures.tickers", StringComparison.Ordinal) 
//                    },
//                    new PropertyFieldReference("contract") { Depth = 3 },
//                ],
//                IdentifyMessageCallback = x => $"{x.FieldValue("channel")}.{x.FieldValue("contract")}"
//            },

//            new MessageEvaluator {
//                Priority = 6,
//                Fields = [
//                    new PropertyFieldReference("channel")
//                    {
//                        Constraint = x => x!.Equals("futures.candlesticks", StringComparison.Ordinal)
//                    },
//                    new PropertyFieldReference("n") { Depth = 3 },
//                ],
//                IdentifyMessageCallback = x => $"{x.FieldValue("channel")}.{x.FieldValue("n")}"
//            },

//            new MessageEvaluator {
//                Priority = 6,
//                Fields = [
//                    new PropertyFieldReference("channel")
//                    {
//                        Constraint = x => x!.Equals("futures.contract_stats", StringComparison.Ordinal)
//                    },
//                    new PropertyFieldReference("contract") { Depth = 2 },
//                ],
//                IdentifyMessageCallback = x => $"{x.FieldValue("channel")}.{x.FieldValue("contract")}"
//            },

//            new MessageEvaluator {
//                Priority = 6,
//                Fields = [
//                    new PropertyFieldReference("channel")
//                    {
//                        Constraint = x => x!.Equals("futures.book_ticker", StringComparison.Ordinal)
//                                        || x!.Equals("futures.order_book_update", StringComparison.Ordinal)
//                                        || x!.Equals("futures.order_book", StringComparison.Ordinal)
//                    },
//                    new PropertyFieldReference("s") { Depth = 2 },
//                ],
//                IdentifyMessageCallback = x => $"{x.FieldValue("channel")}.{x.FieldValue("s")}"
//            },

             new MessageEvaluator {
                Priority = 7,
                Fields = [
                    new PropertyFieldReference("channel"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("channel")!
            }
        ];
    }
}
