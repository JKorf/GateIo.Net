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
        /// ["<c>classic</c>"] Classic account mode
        /// </summary>
        [Map("classic")]
        Classic,
        /// <summary>
        /// ["<c>multi_currency</c>"] Multi-currency margin mode
        /// </summary>
        [Map("multi_currency")]
        MultiAsset,
        /// <summary>
        /// ["<c>portfolio</c>"] Portfolio margin mode
        /// </summary>
        [Map("portfolio")]
        Portfolio,
        /// <summary>
        /// ["<c>single_currency</c>"] Single-currency margin mode
        /// </summary>
        [Map("single_currency")]
        SingleAsset
    }
}
