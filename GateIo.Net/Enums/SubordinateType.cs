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
        /// ["<c>1</c>"] Sub-agent
        /// </summary>
        [Map("1")]
        SubAgent,
        /// <summary>
        /// ["<c>2</c>"] Indirect direct customer
        /// </summary>
        [Map("2")]
        IndirectCustomer,
        /// <summary>
        /// ["<c>3</c>"] Direct direct customer
        /// </summary>
        [Map("3")]
        DirectCustomer,
    }
}
