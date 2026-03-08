using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Trade update
    /// </summary>
    [SerializationModel]
    public record GateIoPerpTradeUpdate
    {
        /// <summary>
        /// ["<c>id</c>"] Trade id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>create_time_ms</c>"] Create time
        /// </summary>
        [JsonPropertyName("create_time_ms")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>contract</c>"] Contract
        /// </summary>
        [JsonPropertyName("contract")]
        public string Contract { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>size</c>"] Trade quantity, negative means sell, positive is buy
        /// </summary>
        [JsonPropertyName("size")]
        public int Quantity { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Trade price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>is_internal</c>"] Whether internal trade. Internal trade refers to the takeover of liquidation orders by the insurance fund and ADL users
        /// </summary>
        [JsonPropertyName("is_internal")]
        public bool IsInternal { get; set; }
    }
}
