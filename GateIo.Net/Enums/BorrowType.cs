using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Borrow type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<BorrowType>))]
    public enum BorrowType
    {
        /// <summary>
        /// ["<c>manual_borrow</c>"] Manual borrow
        /// </summary>
        [Map("manual_borrow")]
        ManualBorrow,
        /// <summary>
        /// ["<c>auto_borrow</c>"] Auto borrow
        /// </summary>
        [Map("auto_borrow")]
        AutoBorrow,
    }
}
