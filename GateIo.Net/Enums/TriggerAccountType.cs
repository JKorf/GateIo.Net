using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Trigger account type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TriggerAccountType>))]
    public enum TriggerAccountType
    {
        /// <summary>
        /// Normal spot
        /// </summary>
        [Map("normal")]
        Normal,
        /// <summary>
        /// Margin
        /// </summary>
        [Map("margin")]
        Margin,
        /// <summary>
        /// Cross margin
        /// </summary>
        [Map("cross_margin")]
        CrossMargin
    }
}
