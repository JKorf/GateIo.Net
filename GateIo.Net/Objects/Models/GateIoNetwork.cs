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
        /// Network name
        /// </summary>
        [JsonPropertyName("chain")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// Network name in Chinese
        /// </summary>
        [JsonPropertyName("name_cn")]
        public string NetworkCn { get; set; } = string.Empty;
        /// <summary>
        /// Network name in English
        /// </summary>
        [JsonPropertyName("name_en")]
        public string NetworkEn { get; set; } = string.Empty;
        /// <summary>
        /// Contract address
        /// </summary>
        [JsonPropertyName("contract_address")]
        public string? ContractAddress { get; set; }
        /// <summary>
        /// Withdrawal precision
        /// </summary>
        [JsonPropertyName("decimal")]
        public int? WithdrawalPrecision { get; set; }
        /// <summary>
        /// Is network disabled
        /// </summary>
        [JsonPropertyName("is_disabled")]
        public bool IsDisabled { get; set; }
        /// <summary>
        /// Is deposit disabled
        /// </summary>
        [JsonPropertyName("is_deposit_disabled")]
        public bool IsDepositDisabled { get; set; }
        /// <summary>
        /// Is withdrawal disabled
        /// </summary>
        [JsonPropertyName("is_withdraw_disabled")]
        public bool IsWithdrawalDisabled { get; set; }
        /// <summary>
        /// Is tag
        /// </summary>
        [JsonPropertyName("is_tag")]
        public bool IsTag { get; set; }
    }
}
