using CryptoExchange.Net.Converters.SystemTextJson;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Withdraw status
    /// </summary>
    [SerializationModel]
    public record GateIoWithdrawStatus
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>name</c>"] Asset name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>name_cn</c>"] Asset name in Chinese
        /// </summary>
        [JsonPropertyName("name_cn")]
        public string NameCn { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>deposit</c>"] Deposit fee
        /// </summary>
        [JsonPropertyName("deposit")]
        public decimal DepositFee { get; set; }
        /// <summary>
        /// ["<c>withdraw_percent</c>"] Withdrawal fee percentage
        /// </summary>
        [JsonPropertyName("withdraw_percent")]
        public string WithdrawalFeePercentage { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>withdraw_fix</c>"] Withdrawal fee fixed quantity
        /// </summary>
        [JsonPropertyName("withdraw_fix")]
        public decimal WithdrawalFeeFixed { get; set; }
        /// <summary>
        /// ["<c>withdraw_day_limit</c>"] Withdrawal day limit
        /// </summary>
        [JsonPropertyName("withdraw_day_limit")]
        public decimal WithdrawalDayLimit { get; set; }
        /// <summary>
        /// ["<c>withdraw_day_limit_remain</c>"] Withdrawal day limit left
        /// </summary>
        [JsonPropertyName("withdraw_day_limit_remain")]
        public decimal WithdrawalDayLimitLeft { get; set; }
        /// <summary>
        /// ["<c>withdraw_amount_mini</c>"] Minimal quantity that can be withdrawn
        /// </summary>
        [JsonPropertyName("withdraw_amount_mini")]
        public decimal MinimalWithdrawalQuantity { get; set; }
        /// <summary>
        /// ["<c>withdraw_eachtime_limit</c>"] Max quantity per withdrawal
        /// </summary>
        [JsonPropertyName("withdraw_eachtime_limit")]
        public decimal MaxPerWithdrawalQuantity { get; set; }
        /// <summary>
        /// ["<c>withdraw_fix_on_chains</c>"] Fixed withdrawal fee on multiple networks
        /// </summary>
        [JsonPropertyName("withdraw_fix_on_chains")]
        public Dictionary<string, decimal> WithdrawalFeeNetworksFixed { get; set; } = new Dictionary<string, decimal>();
        /// <summary>
        /// ["<c>withdraw_percent_on_chains</c>"] Percentage withdrawal fee on multiple networks
        /// </summary>
        [JsonPropertyName("withdraw_percent_on_chains")]
        public Dictionary<string, string> WithdrawalFeeNetworksPercentage { get; set; } = new Dictionary<string, string>();
    }
}
