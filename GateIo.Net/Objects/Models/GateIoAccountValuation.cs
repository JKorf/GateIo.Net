using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Models
{
    /// <summary>
    /// Account valuation
    /// </summary>
    [SerializationModel]
    public record GateIoAccountValuation
    {
        /// <summary>
        /// Details
        /// </summary>
        [JsonPropertyName("details")]
        public GateIoAccountValues Details { get; set; } = null!;
        /// <summary>
        /// Total value
        /// </summary>
        [JsonPropertyName("total")]
        public GateIoAccountValue Total { get; set; } = null!;
    }

    /// <summary>
    /// Account values
    /// </summary>
    [SerializationModel]
    public record GateIoAccountValues
    {
        /// <summary>
        /// Cross margin value
        /// </summary>
        [JsonPropertyName("cross_margin")]
        public GateIoAccountValue CrossMargin { get; set; } = null!;
        /// <summary>
        /// Spot value
        /// </summary>
        [JsonPropertyName("spot")]
        public GateIoAccountValue Spot { get; set; } = null!;
        /// <summary>
        /// Finance value
        /// </summary>
        [JsonPropertyName("finance")]
        public GateIoAccountValue Finance { get; set; } = null!;
        /// <summary>
        /// Margin value
        /// </summary>
        [JsonPropertyName("margin")]
        public GateIoAccountValue Margin { get; set; } = null!;
        /// <summary>
        /// Quant value
        /// </summary>
        [JsonPropertyName("quant")]
        public GateIoAccountValue Quant { get; set; } = null!;
        /// <summary>
        /// Futures value
        /// </summary>
        [JsonPropertyName("futures")]
        public GateIoAccountValue Futures { get; set; } = null!;
        /// <summary>
        /// Delivery value
        /// </summary>
        [JsonPropertyName("delivery")]
        public GateIoAccountValue Delivery { get; set; } = null!;
        /// <summary>
        /// Warrant value
        /// </summary>
        [JsonPropertyName("warrant")]
        public GateIoAccountValue Warrant { get; set; } = null!;
        /// <summary>
        /// CBBC value
        /// </summary>
        [JsonPropertyName("cbbc")]
        public GateIoAccountValue Cbbc { get; set; } = null!;
    }

    /// <summary>
    /// Account value
    /// </summary>
    [SerializationModel]
    public record GateIoAccountValue
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Total account balance
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Unrelealised profit and loss
        /// </summary>
        [JsonPropertyName("unrealised_pnl")]
        public decimal? UnrealisedPnl { get; set; }
        /// <summary>
        /// Borrowed
        /// </summary>
        [JsonPropertyName("borrowed")]
        public decimal? Borrowed { get; set; }
    }
}
