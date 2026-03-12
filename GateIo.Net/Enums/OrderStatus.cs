using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderStatus>))]
    public enum OrderStatus
    {
        /// <summary>
        /// ["<c>open</c>"] Open
        /// </summary>
        [Map("open")]
        Open,
        /// <summary>
        /// ["<c>closed</c>"] Closed
        /// </summary>
        [Map("closed", "finished")]
        Closed,
        /// <summary>
        /// ["<c>cancelled</c>"] Cancelled
        /// </summary>
        [Map("cancelled")]
        Canceled
    }
}
