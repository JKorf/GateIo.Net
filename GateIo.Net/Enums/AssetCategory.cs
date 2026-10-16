using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Asset category
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AssetCategory>))]
    public enum AssetCategory
    {
        /// <summary>
        /// ["<c>stocks</c>"] Stocks
        /// </summary>
        [Map("stocks")]
        Stocks,
        /// <summary>
        /// ["<c>gstocks</c>"] G-Stocks
        /// </summary>
        [Map("gstocks")]
        GStocks,
        /// <summary>
        /// ["<c>ondo-stocks</c>"] Ondo stocks
        /// </summary>
        [Map("ondo-stocks")]
        OndoStocks,
        /// <summary>
        /// ["<c>xstocks</c>"] X-Stocks
        /// </summary>
        [Map("xstocks")]
        XStocks,
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
        Commodities,
    }
}
