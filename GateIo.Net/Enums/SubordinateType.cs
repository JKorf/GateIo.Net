using System.Text.Json.Serialization;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Subordinate type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SubordinateType>))]
    public enum SubordinateType
    {
        /// <summary>
        /// Sub-agent
        /// </summary>
        [Map("1")]
        SubAgent = 1,
        /// <summary>
        /// Indirect direct customer
        /// </summary>
        [Map("2")]
        IndirectCustomer = 2,
        /// <summary>
        /// Direct direct customer
        /// </summary>
        [Map("3")]
        DirectCustomer = 3,
    }
}
