using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net.Enums;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Transfer status
    /// </summary>
    [SerializationModel]
    public record GateIoTransferStatus
    {
        /// <summary>
        /// ["<c>tx_id</c>"] Transation id
        /// </summary>
        [JsonPropertyName("tx_id")]
        public long TransactionId { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public TransferSuccessStatus Status { get; set; }
    }
}
