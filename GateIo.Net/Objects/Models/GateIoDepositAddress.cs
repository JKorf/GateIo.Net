using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Deposit address info
    /// </summary>
    [SerializationModel]
    public record GateIoDepositAddress
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>address</c>"] Address
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>min_deposit_amount</c>"] Min deposit quantity
        /// </summary>
        [JsonPropertyName("min_deposit_amount")]
        public decimal? MinDepositQuantity { get; set; }
        /// <summary>
        /// ["<c>min_confirms</c>"] Min number of confirmations
        /// </summary>
        [JsonPropertyName("min_confirms")]
        public int? MinConfirmations { get; set; }
        /// <summary>
        /// ["<c>multichain_addresses</c>"] Multichain addresses
        /// </summary>
        [JsonPropertyName("multichain_addresses")]
        public GateIoMultiChainDepositAddress[] MultichainAddress { get; set; } = Array.Empty<GateIoMultiChainDepositAddress>();
    }

    /// <summary>
    /// Multichain address info
    /// </summary>
    [SerializationModel]
    public record GateIoMultiChainDepositAddress
    {
        /// <summary>
        /// ["<c>chain</c>"] Network
        /// </summary>
        [JsonPropertyName("chain")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>address</c>"] Address
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>payment_id</c>"] Notes that some currencies required(e.g., Tag, Memo) when depositing
        /// </summary>
        [JsonPropertyName("payment_id")]
        public string PaymentId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>payment_name</c>"] Note type; tag or memo
        /// </summary>
        [JsonPropertyName("payment_name")]
        public string PaymentName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>obtain_failed</c>"] Failed
        /// </summary>
        [JsonPropertyName("obtain_failed")]
        public bool Failed { get; set; }

        /// <summary>
        /// ["<c>min_confirms</c>"] Min number of confirmations
        /// </summary>
        [JsonPropertyName("min_confirms")]
        public int MinConfirmations { get; set; }
    }
}
