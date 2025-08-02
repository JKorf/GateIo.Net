using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Account info
    /// </summary>
    [SerializationModel]
    public record GateIoAccountInfo
    {
        /// <summary>
        /// User id
        /// </summary>
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }
        /// <summary>
        /// IP whitelist for this API Key
        /// </summary>
        [JsonPropertyName("ip_whitelist")]
        public string[] IpWhitelist { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Symbol whitelist for this API Key
        /// </summary>
        [JsonPropertyName("currency_pairs")]
        public string[] SymbolWhitelist { get; set; } = Array.Empty<string>();
        /// <summary>
        /// VIP tier
        /// </summary>
        [JsonPropertyName("tier")]
        public int VipLevel { get; set; }
        /// <summary>
        /// Copy trading role
        /// </summary>
        [JsonPropertyName("copy_trading_role")]
        public CopyTradingRole CopyTradingRole { get; set; }
        /// <summary>
        /// Key info
        /// </summary>
        [JsonPropertyName("key")]
        public GateIoKeyInfo KeyInfo { get; set; } = null!;
    }

    /// <summary>
    /// Key info
    /// </summary>
    [SerializationModel]
    public record GateIoKeyInfo
    {
        /// <summary>
        /// Mode: 1 - classic account 2 - portfolio margin account
        /// </summary>
        [JsonPropertyName("mode")]
        public int Mode { get; set; }
    }
}
