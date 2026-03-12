using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Transfer status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TransferStatus>))]
    public enum TransferStatus
    {
        /// <summary>
        /// ["<c>CREATING</c>"] Creating
        /// </summary>
        [Map("CREATING")]
        Creating,
        /// <summary>
        /// ["<c>PENDING</c>"] Waiting for receiving (please contact the other party to accept the transfer on the gate official website)
        /// </summary>
        [Map("PENDING")]
        Pending,
        /// <summary>
        /// ["<c>CANCELLING</c>"] Cancelling
        /// </summary>
        [Map("CANCELLING")]
        Cancelling,
        /// <summary>
        /// ["<c>CANCELLED</c>"] Revoked
        /// </summary>
        [Map("CANCELLED")]
        Cancelled,
        /// <summary>
        /// ["<c>REFUSING</c>"] Rejection
        /// </summary>
        [Map("REFUSING")]
        Refusing,
        /// <summary>
        /// ["<c>REFUSED</c>"] Rejected
        /// </summary>
        [Map("REFUSED")]
        Refused,
        /// <summary>
        /// ["<c>RECEIVING</c>"] Receiving
        /// </summary>
        [Map("RECEIVING")]
        Receiving,
        /// <summary>
        /// ["<c>RECEIVED</c>"] Success
        /// </summary>
        [Map("RECEIVED")]
        Received,
    }

}
