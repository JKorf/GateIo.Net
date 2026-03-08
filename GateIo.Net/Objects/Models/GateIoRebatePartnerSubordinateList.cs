using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Partner subordinate list
    /// </summary>
    [SerializationModel]
    public record GateIoRebatePartnerSubordinateList
    {
        /// <summary>
        /// ["<c>total</c>"] Total
        /// </summary>
        [JsonPropertyName("total")]
        public long Total { get; set; }

        /// <summary>
        /// ["<c>list</c>"] List
        /// </summary>
        [JsonPropertyName("list")]
        public GateIoRebatePartnerSubordinate[] List { get; set; } = [];
    }
}
