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
        /// ["<c>currency</c>"] Asset name
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>name</c>"] Asset full name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>delisted</c>"] Whether the asset is delisted
        /// </summary>
        [JsonPropertyName("delisted")]
        public bool Delisted { get; set; }
        /// <summary>
        /// ["<c>withdraw_disabled</c>"] Whether the asset has withdrawals disabled
        /// </summary>
        [JsonPropertyName("withdraw_disabled")]
        public bool WithdrawDisabled { get; set; }
        /// <summary>
        /// ["<c>withdraw_delayed</c>"] Whether the asset has withdrawals delayed
        /// </summary>
        [JsonPropertyName("withdraw_delayed")]
        public bool WithdrawDelayed { get; set; }
        /// <summary>
        /// ["<c>deposit_disabled</c>"] Whether the asset has deposits disabled
        /// </summary>
        [JsonPropertyName("deposit_disabled")]
        public bool DepositDisabled { get; set; }
        /// <summary>
        /// ["<c>trade_disabled</c>"] Whether the asset has trading disabled
        /// </summary>
        [JsonPropertyName("trade_disabled")]
        public bool TradeDisabled { get; set; }
        /// <summary>
        /// ["<c>fixed_rate</c>"] Fixed fee rate
        /// </summary>
        [JsonPropertyName("fixed_rate")]
        public decimal? FixedFeeRate { get; set; }
        /// <summary>
        /// ["<c>chain</c>"] Network of the asset
        /// </summary>
        [JsonPropertyName("chain")]
        public string? Network { get; set; }
        /// <summary>
        /// ["<c>chains</c>"] Network list
        /// </summary>
        [JsonPropertyName("chains")]
        public GateIoAssetNetwork[] Networks { get; set; } = [];
    }
}
