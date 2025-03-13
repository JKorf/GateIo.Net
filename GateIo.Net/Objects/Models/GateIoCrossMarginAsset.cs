using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Cross margin asset
    /// </summary>
    [SerializationModel]
    public record GateIoCrossMarginAsset
    {
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonPropertyName("name")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Minimum lending rate
        /// </summary>
        [JsonPropertyName("rate")]
        public decimal MinLendingRate { get; set; }
        /// <summary>
        /// Asset precision
        /// </summary>
        [JsonPropertyName("prec")]
        public decimal AssetPrecision { get; set; }
        /// <summary>
        /// Discount
        /// </summary>
        [JsonPropertyName("discount")]
        public decimal Discount { get; set; }
        /// <summary>
        /// Min borrow quantity
        /// </summary>
        [JsonPropertyName("min_borrow_amount")]
        public decimal MinBorrowQuantity { get; set; }
        /// <summary>
        /// Max borrow quantity per user in USDT
        /// </summary>
        [JsonPropertyName("user_max_borrow_amount")]
        public decimal UserMaxBorrowQuantity { get; set; }
        /// <summary>
        /// Max borrow quantity total in USDT
        /// </summary>
        [JsonPropertyName("total_max_borrow_amount")]
        public decimal TotalMaxBorrowQuantity { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Asset is loanable
        /// </summary>
        [JsonPropertyName("loanable")]
        public bool Loanable { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public bool Enabled { get; set; }
    }
}
