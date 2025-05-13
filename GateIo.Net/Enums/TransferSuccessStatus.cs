using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Transfr status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TransferSuccessStatus>))]
    public enum TransferSuccessStatus
    {
        /// <summary>
        /// Pending
        /// </summary>
        [Map("PENDING")]
        Pending,
        /// <summary>
        /// Success
        /// </summary>
        [Map("SUCCESS")]
        Success,
        /// <summary>
        /// Failed
        /// </summary>
        [Map("FAIL")]
        Failed,
        /// <summary>
        /// Partial success
        /// </summary>
        [Map("PARTIAL_SUCCESS")]
        PartialSuccess
    }
}
