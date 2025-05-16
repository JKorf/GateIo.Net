using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Asset info
    /// </summary>
    [SerializationModel]
    public record GateIoAsset
    {
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Asset full name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Whether the asset is delisted
        /// </summary>
        [JsonPropertyName("delisted")]
        public bool Delisted { get; set; }
        /// <summary>
        /// Whether the asset has withdrawals disabled
        /// </summary>
        [JsonPropertyName("withdraw_disabled")]
        public bool WithdrawDisabled { get; set; }
        /// <summary>
        /// Whether the asset has withdrawals delayed
        /// </summary>
        [JsonPropertyName("withdraw_delayed")]
        public bool WithdrawDelayed { get; set; }
        /// <summary>
        /// Whether the asset has deposits disabled
        /// </summary>
        [JsonPropertyName("deposit_disabled")]
        public bool DepositDisabled { get; set; }
        /// <summary>
        /// Whether the asset has trading disabled
        /// </summary>
        [JsonPropertyName("trade_disabled")]
        public bool TradeDisabled { get; set; }
        /// <summary>
        /// Fixed fee rate
        /// </summary>
        [JsonPropertyName("fixed_rate")]
        public decimal? FixedFeeRate { get; set; }
        /// <summary>
        /// Network of the asset
        /// </summary>
        [JsonPropertyName("chain")]
        public string? Network { get; set; }
        /// <summary>
        /// Network list
        /// </summary>
        [JsonPropertyName("chains")]
        public GateIoAssetNetwork[] Networks { get; set; } = [];
    }
}
