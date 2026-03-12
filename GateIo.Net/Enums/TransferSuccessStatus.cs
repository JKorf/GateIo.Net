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
        /// ["<c>PENDING</c>"] Pending
        /// </summary>
        [Map("PENDING")]
        Pending,
        /// <summary>
        /// ["<c>SUCCESS</c>"] Success
        /// </summary>
        [Map("SUCCESS")]
        Success,
        /// <summary>
        /// ["<c>FAIL</c>"] Failed
        /// </summary>
        [Map("FAIL")]
        Failed,
        /// <summary>
        /// ["<c>PARTIAL_SUCCESS</c>"] Partial success
        /// </summary>
        [Map("PARTIAL_SUCCESS")]
        PartialSuccess
    }
}
