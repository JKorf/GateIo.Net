using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    [SerializationModel]
    internal record GateIoServerTime
    {
        [JsonPropertyName("server_time")]
        public DateTime ServerTime { get; set; }
    }
}
