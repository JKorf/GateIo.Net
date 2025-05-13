using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net.Converters;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Kline/candlestick info
    /// </summary>
    [JsonConverter(typeof(ArrayConverter<GateIoKline>))]
    [SerializationModel]
    public record GateIoKline
    {
        /// <summary>
        /// Open timestamp
        /// </summary>
        [ArrayProperty(0), JsonConverter(typeof(DateTimeConverter))]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// Quote volume
        /// </summary>
        [ArrayProperty(1)]
        public decimal QuoteVolume { get; set; }
        /// <summary>
        /// Close price
        /// </summary>
        [ArrayProperty(2)]
        public decimal ClosePrice { get; set; }
        /// <summary>
        /// High price
        /// </summary>
        [ArrayProperty(3)]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// Low price
        /// </summary>
        [ArrayProperty(4)]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// Open price
        /// </summary>
        [ArrayProperty(5)]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// Base volume
        /// </summary>
        [ArrayProperty(6)]
        public decimal BaseVolume { get; set; }
        /// <summary>
        /// Is the kline final/closed
        /// </summary>
        [ArrayProperty(7)]
        public bool Final { get; set; }
    }
}
