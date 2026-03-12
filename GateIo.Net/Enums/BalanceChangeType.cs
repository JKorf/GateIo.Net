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
        /// ["<c>withdraw</c>"] Withdrawal
        /// </summary>
        [Map("withdraw")]
        Withdraw,
        /// <summary>
        /// ["<c>deposit</c>"] Deposit
        /// </summary>
        [Map("deposit")]
        Deposit,
        /// <summary>
        /// ["<c>trade-fee-deduct</c>"] Trade fee deduction
        /// </summary>
        [Map("trade-fee-deduct")]
        TradeFeeDeduct,
        /// <summary>
        /// ["<c>order-create</c>"] Order creation
        /// </summary>
        [Map("order-create")]
        OrderCreate,
        /// <summary>
        /// ["<c>order-match</c>"] Order match
        /// </summary>
        [Map("order-match")]
        OrderMatch,
        /// <summary>
        /// ["<c>order-update</c>"] Order update
        /// </summary>
        [Map("order-update")]
        OrderUpdate,
        /// <summary>
        /// ["<c>margin-transfer</c>"] Margin transfer
        /// </summary>
        [Map("margin-transfer")]
        MarginTransfer,
        /// <summary>
        /// ["<c>future-transfer</c>"] Futures transfer
        /// </summary>
        [Map("future-transfer")]
        FutureTransfer,
        /// <summary>
        /// ["<c>cross-margin-transfer</c>"] Cross margin transfer
        /// </summary>
        [Map("cross-margin-transfer")]
        CrossMarginTransfer,
        /// <summary>
        /// ["<c>other</c>"] Other
        /// </summary>
        [Map("other")]
        Other
    }
}
