using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net.Enums;
using System;
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
        /// ["<c>user_id</c>"] User id
        /// </summary>
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }
        /// <summary>
        /// ["<c>ip_whitelist</c>"] IP whitelist for this API Key
        /// </summary>
        [JsonPropertyName("ip_whitelist")]
        public string[] IpWhitelist { get; set; } = Array.Empty<string>();
        /// <summary>
        /// ["<c>currency_pairs</c>"] Symbol whitelist for this API Key
        /// </summary>
        [JsonPropertyName("currency_pairs")]
        public string[] SymbolWhitelist { get; set; } = Array.Empty<string>();
        /// <summary>
        /// ["<c>tier</c>"] VIP tier
        /// </summary>
        [JsonPropertyName("tier")]
        public int VipLevel { get; set; }
        /// <summary>
        /// ["<c>copy_trading_role</c>"] Copy trading role
        /// </summary>
        [JsonPropertyName("copy_trading_role")]
        public CopyTradingRole CopyTradingRole { get; set; }
        /// <summary>
        /// ["<c>key</c>"] Key info
        /// </summary>
        [JsonPropertyName("key")]
        public GateIoKeyInfo KeyInfo { get; set; } = null!;
        /// <summary>
        /// ["<c>tier_expire_time</c>"] Tier expire time
        /// </summary>
        [JsonPropertyName("tier_expire_time")]
        public DateTime? TierExpireTime { get; set; }
        /// <summary>
        /// ["<c>spot_copy_trading_role</c>"] Spot copy trading role
        /// </summary>
        [JsonPropertyName("spot_copy_trading_role")]
        public int? SpotCopyTradingRole { get; set; }
    }

    /// <summary>
    /// Key info
    /// </summary>
    [SerializationModel]
    public record GateIoKeyInfo
    {
        /// <summary>
        /// ["<c>mode</c>"] Mode: 1 - classic account 2 - portfolio margin account
        /// </summary>
        [JsonPropertyName("mode")]
        public int Mode { get; set; }
    }
}
