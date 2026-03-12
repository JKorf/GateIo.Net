using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Transaction type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TransactionType>))]
    public enum TransactionType
    {
        /// <summary>
        /// ["<c>deposit</c>"] Deposit
        /// </summary>
        [Map("deposit")]
        Deposit,
        /// <summary>
        /// ["<c>withdraw</c>"] Withdrawal
        /// </summary>
        [Map("withdraw")]
        Withdrawal
    }
}
