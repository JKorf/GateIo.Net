using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Trade update
    /// </summary>
    [SerializationModel]
    public record GateIoTradeUpdate
    {
        /// <summary>
        /// Trade id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// Market unique id
        /// </summary>
        [JsonPropertyName("id_market")]
        public long MarketId { get; set; }
        /// <summary>
        /// Create time
        /// </summary>
        [JsonPropertyName("create_time_ms")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("currency_pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Trade quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Trade price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// The start and end sequence id
        /// </summary>
        [JsonPropertyName("range")]
        public string TradeRange { get; set; } = string.Empty;
    }
}
