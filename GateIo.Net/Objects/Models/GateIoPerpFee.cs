using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Fee info
    /// </summary>
    [SerializationModel]
    public record GateIoPerpFee
    {
        /// <summary>
        /// Taker fee rate
        /// </summary>
        [JsonPropertyName("taker_fee")]
        public decimal TakerFee { get; set; }
        /// <summary>
        /// Maker fee rate
        /// </summary>
        [JsonPropertyName("maker_fee")]
        public decimal MakerFee { get; set; }
    }
}
