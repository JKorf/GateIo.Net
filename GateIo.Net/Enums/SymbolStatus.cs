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
        /// Not tradeable
        /// </summary>
        [Map("untradable")]
        Untradable,
        /// <summary>
        /// Only buyable
        /// </summary>
        [Map("buyable")]
        Buyable,
        /// <summary>
        /// Only sellable
        /// </summary>
        [Map("sellable")]
        Sellable,
        /// <summary>
        /// Can be bought and sold
        /// </summary>
        [Map("tradable")]
        Tradable,
    }
}
