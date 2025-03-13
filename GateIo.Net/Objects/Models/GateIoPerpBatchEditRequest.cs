using CryptoExchange.Net.Converters.SystemTextJson;
using GateIo.Net.Enums;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Batch order edit request
    /// </summary>
    [SerializationModel]
    public record GateIoPerpBatchEditRequest
    {
        /// <summary>
        /// The order id, either this or Text should be provied
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// User defined order text, either this or OrderId should be provied
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;
        /// <summary>
        /// Amend text
        /// </summary>
        [JsonPropertyName("amend_text")]
        public string AmendText { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("size"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Quantity { get; set; }
        /// <summary>
        /// Order price
        /// </summary>
        [JsonPropertyName("price"), JsonConverter(typeof(DecimalStringWriterConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? Price { get; set; }

    }
}
