using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net.Enums;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Batch order edit request
    /// </summary>
    public record GateIoBatchEditRequest
    {
        /// <summary>
        /// The order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// The symbol the order is on
        /// </summary>
        [JsonPropertyName("currency_pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// amend text
        /// </summary>
        [JsonPropertyName("amend_text")]
        public string AmendText { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// Order price
        /// </summary>
        [JsonPropertyName("price"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? Price { get; set; }
        /// <summary>
        /// The type of account
        /// </summary>
        [JsonPropertyName("account"), JsonConverter(typeof(EnumConverter))]
        public SpotAccountType AccountType { get; set; }

    }
}
