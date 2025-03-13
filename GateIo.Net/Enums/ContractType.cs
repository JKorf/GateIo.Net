using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Contract type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ContractType>))]
    public enum ContractType
    {
        /// <summary>
        /// Inverse
        /// </summary>
        [Map("inverse")]
        Inverse,
        /// <summary>
        /// Direct
        /// </summary>
        [Map("direct")]
        Direct
    }
}
