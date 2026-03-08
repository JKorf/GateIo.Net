using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Transfer
    /// </summary>
    [SerializationModel]
    public record GateIoTransfer
    {
        /// <summary>
        /// ["<c>tx_id</c>"] Transation id
        /// </summary>
        [JsonPropertyName("tx_id")]
        public long TransactionId { get; set; }
    }
}
