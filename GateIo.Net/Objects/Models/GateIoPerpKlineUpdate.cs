using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Kline update
    /// </summary>
    [SerializationModel]
    public record GateIoPerpKlineUpdate
    {
        /// <summary>
        /// Open time
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// Volume in quote asset
        /// </summary>
        [JsonPropertyName("v")]
        public long QuoteVolume { get; set; }
        /// <summary>
        /// Close price
        /// </summary>
        [JsonPropertyName("c")]
        public decimal ClosePrice { get; set; }
        /// <summary>
        /// High price
        /// </summary>
        [JsonPropertyName("h")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// Low price
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// Open price
        /// </summary>
        [JsonPropertyName("o")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// Stream
        /// </summary>
        [JsonPropertyName("n")]
        public string Stream { get; set; } = string.Empty;

        /// <summary>
        /// The contract
        /// </summary>
        public string Contract => Stream.Substring(Stream.IndexOf('_') + 1);

        /// <summary>
        /// The interval
        /// </summary>
        public string Interval => Stream.Substring(0, Stream.IndexOf('_'));
    }
}
