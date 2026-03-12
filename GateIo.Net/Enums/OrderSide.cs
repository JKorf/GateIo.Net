using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Side of an order
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderSide>))]
    public enum OrderSide
    {
        /// <summary>
        /// ["<c>buy</c>"] Buy order
        /// </summary>
        [Map("buy")]
        Buy,
        /// <summary>
        /// ["<c>sell</c>"] Sell order
        /// </summary>
        [Map("sell")]
        Sell
    }
}
