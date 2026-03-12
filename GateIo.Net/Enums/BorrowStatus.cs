using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Borrow loan status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<BorrowStatus>))]
    public enum BorrowStatus
    {
        /// <summary>
        /// ["<c>1</c>"] Failed to borrow
        /// </summary>
        [Map("1")]
        Failed,
        /// <summary>
        /// ["<c>2</c>"] Borrowed but not repaid
        /// </summary>
        [Map("2")]
        Borrowed,
        /// <summary>
        /// ["<c>3</c>"] Repaid
        /// </summary>
        [Map("3")]
        Repaid
    }
}
