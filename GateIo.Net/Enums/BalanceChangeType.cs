using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Reason for balance change
    /// </summary>
    [JsonConverter(typeof(EnumConverter<BalanceChangeType>))]
    public enum BalanceChangeType
    {
        /// <summary>
        /// Withdrawal
        /// </summary>
        [Map("withdraw")]
        Withdraw,
        /// <summary>
        /// Deposit
        /// </summary>
        [Map("deposit")]
        Deposit,
        /// <summary>
        /// Trade fee deduction
        /// </summary>
        [Map("trade-fee-deduct")]
        TradeFeeDeduct,
        /// <summary>
        /// Order creation
        /// </summary>
        [Map("order-create")]
        OrderCreate,
        /// <summary>
        /// Order match
        /// </summary>
        [Map("order-match")]
        OrderMatch,
        /// <summary>
        /// Order update
        /// </summary>
        [Map("order-update")]
        OrderUpdate,
        /// <summary>
        /// Margin transfer
        /// </summary>
        [Map("margin-transfer")]
        MarginTransfer,
        /// <summary>
        /// Futures transfer
        /// </summary>
        [Map("future-transfer")]
        FutureTransfer,
        /// <summary>
        /// Cross margin transfer
        /// </summary>
        [Map("cross-margin-transfer")]
        CrossMarginTransfer,
        /// <summary>
        /// Other
        /// </summary>
        [Map("other")]
        Other
    }
}
