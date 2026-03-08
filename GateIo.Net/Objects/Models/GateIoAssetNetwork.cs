using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// ["<c>name</c>"] Network name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>addr</c>"] Address
        /// </summary>
        [JsonPropertyName("addr")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>withdraw_disabled</c>"] Is withdrawal disabled
        /// </summary>
        [JsonPropertyName("withdraw_disabled")]
        public bool WithdrawDisabled { get; set; }
        /// <summary>
        /// ["<c>withdraw_delayed</c>"] Is withdrawal delayed
        /// </summary>
        [JsonPropertyName("withdraw_delayed")]
        public bool WithdrawDelayed { get; set; }
        /// <summary>
        /// ["<c>deposit_disabled</c>"] Is deposit disabled
        /// </summary>
        [JsonPropertyName("deposit_disabled")]
        public bool DepositDisabled { get; set; }
    }
}
