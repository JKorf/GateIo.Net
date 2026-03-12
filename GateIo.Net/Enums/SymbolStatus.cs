using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Symbol trade status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SymbolStatus>))]
    public enum SymbolStatus
    {
        /// <summary>
        /// ["<c>untradable</c>"] Not tradeable
        /// </summary>
        [Map("untradable")]
        Untradable,
        /// <summary>
        /// ["<c>buyable</c>"] Only buyable
        /// </summary>
        [Map("buyable")]
        Buyable,
        /// <summary>
        /// ["<c>sellable</c>"] Only sellable
        /// </summary>
        [Map("sellable")]
        Sellable,
        /// <summary>
        /// ["<c>tradable</c>"] Can be bought and sold
        /// </summary>
        [Map("tradable")]
        Tradable,
    }
}
