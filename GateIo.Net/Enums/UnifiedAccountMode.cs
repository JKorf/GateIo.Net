using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Account mode
    /// </summary>
    [JsonConverter(typeof(EnumConverter<UnifiedAccountMode>))]
    public enum UnifiedAccountMode
    {
        /// <summary>
        /// Classic account mode
        /// </summary>
        [Map("classic")]
        Classic,
        /// <summary>
        /// Multi-currency margin mode
        /// </summary>
        [Map("multi_currency")]
        MultiAsset,
        /// <summary>
        /// Portfolio margin mode
        /// </summary>
        [Map("portfolio")]
        Portfolio,
        /// <summary>
        /// Single-currency margin mode
        /// </summary>
        [Map("single_currency")]
        SingleAsset
    }
}
