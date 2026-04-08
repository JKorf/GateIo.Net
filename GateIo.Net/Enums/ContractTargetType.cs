using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Contract target type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ContractTargetType>))]
    public enum ContractTargetType
    {
        /// <summary>
        /// ["<c>stocks</c>"] Stocks
        /// </summary>
        [Map("stocks")]
        Stocks,
        /// <summary>
        /// ["<c>metals</c>"] Metals
        /// </summary>
        [Map("metals")]
        Metals,
        /// <summary>
        /// ["<c>indices</c>"] Indices
        /// </summary>
        [Map("indices")]
        Indices,
        /// <summary>
        /// ["<c>forex</c>"] Forex
        /// </summary>
        [Map("forex")]
        Forex,
        /// <summary>
        /// ["<c>commodities</c>"] Commodities
        /// </summary>
        [Map("commodities")]
        Commodities
    }
}
