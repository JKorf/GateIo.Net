using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Borrow direction
    /// </summary>
    [JsonConverter(typeof(EnumConverter<BorrowDirection>))]
    public enum BorrowDirection
    {
        /// <summary>
        /// Borrow
        /// </summary>
        [Map("borrow")]
        Borrow,
        /// <summary>
        /// Repay
        /// </summary>
        [Map("repay")]
        Repay
    }
}
