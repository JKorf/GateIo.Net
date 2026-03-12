using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Asset status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AlphaAssetStatus>))]
    public enum AlphaAssetStatus
    {
        /// <summary>
        /// ["<c>1</c>"] Normal
        /// </summary>
        [Map("1")]
        Normal,
        /// <summary>
        /// ["<c>2</c>"] Suspended
        /// </summary>
        [Map("2")]
        Suspended,
        /// <summary>
        /// ["<c>3</c>"] Delisted
        /// </summary>
        [Map("3")]
        Delisted
    }
}
