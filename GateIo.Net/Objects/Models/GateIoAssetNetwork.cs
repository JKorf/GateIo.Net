using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Asset network info
    /// </summary>
    [SerializationModel]
    public record GateIoAssetNetwork
    {
        /// <summary>
        /// Network name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Address
        /// </summary>
        [JsonPropertyName("addr")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// Is withdrawal disabled
        /// </summary>
        [JsonPropertyName("withdraw_disabled")]
        public bool WithdrawDisabled { get; set; }
        /// <summary>
        /// Is withdrawal delayed
        /// </summary>
        [JsonPropertyName("withdraw_delayed")]
        public bool WithdrawDelayed { get; set; }
        /// <summary>
        /// Is deposit disabled
        /// </summary>
        [JsonPropertyName("deposit_disabled")]
        public bool DepositDisabled { get; set; }
    }
}
