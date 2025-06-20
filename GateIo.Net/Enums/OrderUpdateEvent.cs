using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Account type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderUpdateEvent>))]
    public enum OrderUpdateEvent
    {
        /// <summary>
        /// Order creation
        /// </summary>
        [Map("put")]
        Create,
        /// <summary>
        /// Order update
        /// </summary>
        [Map("update")]
        Update,
        /// <summary>
        /// Order finished
        /// </summary>
        [Map("finish")]
        Finish
    }
}
