using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net;
using System;
using System.Linq;
using System.Text.Json;

namespace GateIo.Net.Clients.MessageHandlers
{
    internal class GateIoSocketSpotMessageHandler : JsonSocketMessageHandler
    {
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(GateIoExchange._serializerContext);

        protected override MessageEvaluator[] MessageEvaluators { get; } = [

             new MessageEvaluator {
                Priority = 1,
                ForceIfFound = true,
                Fields = [
                    new PropertyFieldReference("id"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("id")!
            },

             new MessageEvaluator {
                Priority = 2,
                Fields = [
                    new PropertyFieldReference("request_id"),
                    new PropertyFieldReference("ack") { Constraint = x => x!.Equals("True", System.StringComparison.Ordinal) },
                    new PropertyFieldReference("status") { Depth = 2, Constraint = x => x!.Equals("200", System.StringComparison.Ordinal) },
                ],
                IdentifyMessageCallback = x => $"{x.FieldValue("request_id")}ack"
            },

             new MessageEvaluator {
                Priority = 3,
                Fields = [
                    new PropertyFieldReference("request_id"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("request_id")!
            },

             new MessageEvaluator {
                Priority = 4,
                Fields = [
                    new PropertyFieldReference("channel") { Constraint = x => x!.Equals("spot.obu", StringComparison.Ordinal) },
                    new PropertyFieldReference("s") { Depth = 2 },
                ],
#warning ?
                 IdentifyMessageCallback = x => x.FieldValue("s")!
            },

            new MessageEvaluator {
                Priority = 5,
                Fields = [
                    new PropertyFieldReference("channel") 
                    {
                        Constraint = x => x!.Equals("spot.trades", StringComparison.Ordinal) || x!.Equals("spot.tickers", StringComparison.Ordinal) 
                    },
                    new PropertyFieldReference("currency_pair") { Depth = 2 },
                ],
                IdentifyMessageCallback = x => $"{x.FieldValue("channel")}.{x.FieldValue("currency_pair")}"
            },

            new MessageEvaluator {
                Priority = 6,
                Fields = [
                    new PropertyFieldReference("channel")
                    {
                        Constraint = x => x!.Equals("spot.candlesticks", StringComparison.Ordinal)
                    },
                    new PropertyFieldReference("n") { Depth = 2 },
                ],
                IdentifyMessageCallback = x => $"{x.FieldValue("channel")}.{x.FieldValue("n")}"
            },

            new MessageEvaluator {
                Priority = 6,
                Fields = [
                    new PropertyFieldReference("channel")
                    {
                        Constraint = x => x!.Equals("spot.book_ticker", StringComparison.Ordinal)
                                        || x!.Equals("spot.order_book_update", StringComparison.Ordinal)
                                        || x!.Equals("spot.order_book", StringComparison.Ordinal)
                    },
                    new PropertyFieldReference("s") { Depth = 2 },
                ],
                IdentifyMessageCallback = x => $"{x.FieldValue("channel")}.{x.FieldValue("s")}"
            },

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
