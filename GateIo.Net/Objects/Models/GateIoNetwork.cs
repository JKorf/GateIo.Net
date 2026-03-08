using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Asset network info
    /// </summary>
    [SerializationModel]
    public record GateIoNetwork
    {
        /// <summary>
        /// ["<c>chain</c>"] Network name
        /// </summary>
        [JsonPropertyName("chain")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>name_cn</c>"] Network name in Chinese
        /// </summary>
        [JsonPropertyName("name_cn")]
        public string NetworkCn { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>name_en</c>"] Network name in English
        /// </summary>
        [JsonPropertyName("name_en")]
        public string NetworkEn { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contract_address</c>"] Contract address
        /// </summary>
        [JsonPropertyName("contract_address")]
        public string? ContractAddress { get; set; }
        /// <summary>
        /// ["<c>decimal</c>"] Withdrawal precision
        /// </summary>
        [JsonPropertyName("decimal")]
        public int? WithdrawalPrecision { get; set; }
        /// <summary>
        /// ["<c>is_disabled</c>"] Is network disabled
        /// </summary>
        [JsonPropertyName("is_disabled")]
        public bool IsDisabled { get; set; }
        /// <summary>
        /// ["<c>is_deposit_disabled</c>"] Is deposit disabled
        /// </summary>
        [JsonPropertyName("is_deposit_disabled")]
        public bool IsDepositDisabled { get; set; }
        /// <summary>
        /// ["<c>is_withdraw_disabled</c>"] Is withdrawal disabled
        /// </summary>
        [JsonPropertyName("is_withdraw_disabled")]
        public bool IsWithdrawalDisabled { get; set; }
        /// <summary>
        /// ["<c>is_tag</c>"] Is tag
        /// </summary>
        [JsonPropertyName("is_tag")]
        public bool IsTag { get; set; }
    }
}
