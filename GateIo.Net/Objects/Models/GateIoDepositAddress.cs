using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
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
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Address
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// Min deposit quantity
        /// </summary>
        [JsonPropertyName("min_deposit_amount")]
        public decimal? MinDepositQuantity { get; set; }
        /// <summary>
        /// Min number of confirmations
        /// </summary>
        [JsonPropertyName("min_confirms")]
        public int? MinConfirmations { get; set; }
        /// <summary>
        /// Multichain addresses
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
        /// Network
        /// </summary>
        [JsonPropertyName("chain")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// Address
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// Notes that some currencies required(e.g., Tag, Memo) when depositing
        /// </summary>
        [JsonPropertyName("payment_id")]
        public string PaymentId { get; set; } = string.Empty;
        /// <summary>
        /// Note type; tag or memo
        /// </summary>
        [JsonPropertyName("payment_name")]
        public string PaymentName { get; set; } = string.Empty;
        /// <summary>
        /// Failed
        /// </summary>
        [JsonPropertyName("obtain_failed")]
        public bool Failed { get; set; }

        /// <summary>
        /// Min number of confirmations
        /// </summary>
        [JsonPropertyName("min_confirms")]
        public int MinConfirmations { get; set; }
    }
}
