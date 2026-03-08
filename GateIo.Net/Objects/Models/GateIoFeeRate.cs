using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Fee rate info
    /// </summary>
    [SerializationModel]
    public record GateIoFeeRate
    {
        /// <summary>
        /// ["<c>user_id</c>"] User id
        /// </summary>
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }
        /// <summary>
        /// ["<c>taker_fee</c>"] Taker fee
        /// </summary>
        [JsonPropertyName("taker_fee")]
        public decimal TakerFee { get; set; }
        /// <summary>
        /// ["<c>maker_fee</c>"] Maker fee
        /// </summary>
        [JsonPropertyName("maker_fee")]
        public decimal MakerFee { get; set; }
        /// <summary>
        /// ["<c>futures_taker_fee</c>"] Futures maker fee
        /// </summary>
        [JsonPropertyName("futures_taker_fee")]
        public decimal FuturesTakerFee { get; set; }
        /// <summary>
        /// ["<c>futures_maker_fee</c>"] Futures taker fee
        /// </summary>
        [JsonPropertyName("futures_maker_fee")]
        public decimal FuturesMakerFee { get; set; }
        /// <summary>
        /// ["<c>gt_discount</c>"] If GT deduction is enabled
        /// </summary>
        [JsonPropertyName("gt_discount")]
        public bool GtDiscount { get; set; }
        /// <summary>
        /// ["<c>gt_taker_fee</c>"] Maker fee rate if using GT deduction. It will be 0 if GT deduction is disabled
        /// </summary>
        [JsonPropertyName("gt_taker_fee")]
        public decimal GtTakerFee { get; set; }
        /// <summary>
        /// ["<c>gt_maker_fee</c>"] Taker fee rate if using GT deduction. It will be 0 if GT deduction is disabled
        /// </summary>
        [JsonPropertyName("gt_maker_fee")]
        public decimal GtMakerFee { get; set; }
        /// <summary>
        /// ["<c>loan_fee</c>"] Loan fee rate of margin lending
        /// </summary>
        [JsonPropertyName("loan_fee")]
        public decimal LoanFee { get; set; }
        /// <summary>
        /// ["<c>delivery_taker_fee</c>"] Delivery futures trading taker fee
        /// </summary>
        [JsonPropertyName("delivery_taker_fee")]
        public decimal DeliveryTakerFee { get; set; }
        /// <summary>
        /// ["<c>delivery_maker_fee</c>"] Delivery futures trading maker fee
        /// </summary>
        [JsonPropertyName("delivery_maker_fee")]
        public decimal DeliveryMakerFee { get; set; }
        /// <summary>
        /// ["<c>debit_fee</c>"] Deduction types for rates, 1 - GT deduction, 2 - Point card deduction, 3 - VIP rates
        /// </summary>
        [JsonPropertyName("debit_fee")]
        public int DebitFee { get; set; }
    }
}
