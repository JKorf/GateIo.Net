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
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Asset name in Chinese
        /// </summary>
        [JsonPropertyName("name_cn")]
        public string NameCn { get; set; } = string.Empty;
        /// <summary>
        /// Deposit fee
        /// </summary>
        [JsonPropertyName("deposit")]
        public decimal DepositFee { get; set; }
        /// <summary>
        /// Withdrawal fee percentage
        /// </summary>
        [JsonPropertyName("withdraw_percent")]
        public string WithdrawalFeePercentage { get; set; } = string.Empty;
        /// <summary>
        /// Withdrawal fee fixed quantity
        /// </summary>
        [JsonPropertyName("withdraw_fix")]
        public decimal WithdrawalFeeFixed { get; set; }
        /// <summary>
        /// Withdrawal day limit
        /// </summary>
        [JsonPropertyName("withdraw_day_limit")]
        public decimal WithdrawalDayLimit { get; set; }
        /// <summary>
        /// Withdrawal day limit left
        /// </summary>
        [JsonPropertyName("withdraw_day_limit_remain")]
        public decimal WithdrawalDayLimitLeft { get; set; }
        /// <summary>
        /// Minimal quantity that can be withdrawn
        /// </summary>
        [JsonPropertyName("withdraw_amount_mini")]
        public decimal MinimalWithdrawalQuantity { get; set; }
        /// <summary>
        /// Max quantity per withdrawal
        /// </summary>
        [JsonPropertyName("withdraw_eachtime_limit")]
        public decimal MaxPerWithdrawalQuantity { get; set; }
        /// <summary>
        /// Fixed withdrawal fee on multiple networks
        /// </summary>
        [JsonPropertyName("withdraw_fix_on_chains")]
        public Dictionary<string, decimal> WithdrawalFeeNetworksFixed { get; set; } = new Dictionary<string, decimal>();
        /// <summary>
        /// Percentage withdrawal fee on multiple networks
        /// </summary>
        [JsonPropertyName("withdraw_percent_on_chains")]
        public Dictionary<string, string> WithdrawalFeeNetworksPercentage { get; set; } = new Dictionary<string, string>();
    }
}
