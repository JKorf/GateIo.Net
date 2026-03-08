using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Kline update
    /// </summary>
    [SerializationModel]
    public record GateIoKlineUpdate
    {
        /// <summary>
        /// ["<c>t</c>"] Open time
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// ["<c>v</c>"] Volume in quote asset
        /// </summary>
        [JsonPropertyName("v")]
        public decimal QuoteVolume { get; set; }
        /// <summary>
        /// ["<c>c</c>"] Close price
        /// </summary>
        [JsonPropertyName("c")]
        public decimal ClosePrice { get; set; }
        /// <summary>
        /// ["<c>h</c>"] High price
        /// </summary>
        [JsonPropertyName("h")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// ["<c>l</c>"] Low price
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// ["<c>o</c>"] Open price
        /// </summary>
        [JsonPropertyName("o")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// ["<c>n</c>"] Stream
        /// </summary>
        [JsonPropertyName("n")]
        public string Stream { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>a</c>"] Open price
        /// </summary>
        [JsonPropertyName("a")]
        public decimal BaseVolume { get; set; }
        /// <summary>
        /// ["<c>w</c>"] Is final update  for this timeframe
        /// </summary>
        [JsonPropertyName("w")]
        public bool Final { get; set; }

        /// <summary>
        /// The symbol
        /// </summary>
        public string Symbol => Stream.Substring(Stream.IndexOf('_') + 1);

        /// <summary>
        /// The interval
        /// </summary>
        public string Interval => Stream.Substring(0, Stream.IndexOf('_'));
    }
}
