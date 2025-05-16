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
        /// Transation id
        /// </summary>
        [JsonPropertyName("tx_id")]
        public long TransactionId { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public TransferSuccessStatus Status { get; set; }
    }
}
