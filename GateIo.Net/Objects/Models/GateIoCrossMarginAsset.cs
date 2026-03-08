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
        /// ["<c>name</c>"] Asset name
        /// </summary>
        [JsonPropertyName("name")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>rate</c>"] Minimum lending rate
        /// </summary>
        [JsonPropertyName("rate")]
        public decimal MinLendingRate { get; set; }
        /// <summary>
        /// ["<c>prec</c>"] Asset precision
        /// </summary>
        [JsonPropertyName("prec")]
        public decimal AssetPrecision { get; set; }
        /// <summary>
        /// ["<c>discount</c>"] Discount
        /// </summary>
        [JsonPropertyName("discount")]
        public decimal Discount { get; set; }
        /// <summary>
        /// ["<c>min_borrow_amount</c>"] Min borrow quantity
        /// </summary>
        [JsonPropertyName("min_borrow_amount")]
        public decimal MinBorrowQuantity { get; set; }
        /// <summary>
        /// ["<c>user_max_borrow_amount</c>"] Max borrow quantity per user in USDT
        /// </summary>
        [JsonPropertyName("user_max_borrow_amount")]
        public decimal UserMaxBorrowQuantity { get; set; }
        /// <summary>
        /// ["<c>total_max_borrow_amount</c>"] Max borrow quantity total in USDT
        /// </summary>
        [JsonPropertyName("total_max_borrow_amount")]
        public decimal TotalMaxBorrowQuantity { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>loanable</c>"] Asset is loanable
        /// </summary>
        [JsonPropertyName("loanable")]
        public bool Loanable { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public bool Enabled { get; set; }
    }
}
