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
        /// ["<c>mode</c>"] Account mode
        /// </summary>
        [JsonPropertyName("mode")]
        public UnifiedAccountMode Mode { get; set; }
        /// <summary>
        /// ["<c>settings</c>"] Settings
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
        /// ["<c>usdt_futures</c>"] USDT contract switch
        /// </summary>
        [JsonPropertyName("usdt_futures")]
        public bool UsdtFutures { get; set; }
        /// <summary>
        /// ["<c>spot_hedge</c>"] Spot hedging switch
        /// </summary>
        [JsonPropertyName("spot_hedge")]
        public bool SpotHedge { get; set; }
        /// <summary>
        /// ["<c>use_funding</c>"] When the mode is set to combined margin mode, will funds be used as margin
        /// </summary>
        [JsonPropertyName("use_funding")]
        public bool UseFunding { get; set; }
        /// <summary>
        /// ["<c>options</c>"] Option switch
        /// </summary>
        [JsonPropertyName("options")]
        public bool Options { get; set; }
    }
}
