using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Self trade prevention mode
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SelfTradePreventionMode>))]
    public enum SelfTradePreventionMode
    {
        /// <summary>
        /// None
        /// </summary>
        [Map("", "-")]
        None,
        /// <summary>
        /// ["<c>cn</c>"] Cancel newest
        /// </summary>
        [Map("cn")]
        CancelNewest,
        /// <summary>
        /// ["<c>co</c>"] Cancel oldest
        /// </summary>
        [Map("co")]
        CancelOldest,
        /// <summary>
        /// ["<c>cb</c>"] Cancel both
        /// </summary>
        [Map("cb")]
        CancelBoth
    }
}
