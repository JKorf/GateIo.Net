using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net.Enums;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Unified account mode
    /// </summary>
    [SerializationModel]
    public record GateIoUnifiedAccountMode
    {
        /// <summary>
        /// Account mode
        /// </summary>
        [JsonPropertyName("mode")]
        public UnifiedAccountMode Mode { get; set; }
        /// <summary>
        /// Settings
        /// </summary>
        [JsonPropertyName("settings")]
        public GateIoUnifiedAccountSettings Settings { get; set; } = null!;
    }

    /// <summary>
    /// Unified account settings
    /// </summary>
    [SerializationModel]
    public record GateIoUnifiedAccountSettings
    {
        /// <summary>
        /// USDT contract switch
        /// </summary>
        [JsonPropertyName("usdt_futures")]
        public bool UsdtFutures { get; set; }
        /// <summary>
        /// Spot hedging switch
        /// </summary>
        [JsonPropertyName("spot_hedge")]
        public bool SpotHedge { get; set; }
        /// <summary>
        /// When the mode is set to combined margin mode, will funds be used as margin
        /// </summary>
        [JsonPropertyName("use_funding")]
        public bool UseFunding { get; set; }
        /// <summary>
        /// Option switch
        /// </summary>
        [JsonPropertyName("options")]
        public bool Options { get; set; }
    }
}
