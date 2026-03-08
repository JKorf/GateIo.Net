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
        /// ["<c>details</c>"] Details
        /// </summary>
        [JsonPropertyName("details")]
        public GateIoAccountValues Details { get; set; } = null!;
        /// <summary>
        /// ["<c>total</c>"] Total value
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
        /// ["<c>cross_margin</c>"] Cross margin value
        /// </summary>
        [JsonPropertyName("cross_margin")]
        public GateIoAccountValue CrossMargin { get; set; } = null!;
        /// <summary>
        /// ["<c>spot</c>"] Spot value
        /// </summary>
        [JsonPropertyName("spot")]
        public GateIoAccountValue Spot { get; set; } = null!;
        /// <summary>
        /// ["<c>finance</c>"] Finance value
        /// </summary>
        [JsonPropertyName("finance")]
        public GateIoAccountValue Finance { get; set; } = null!;
        /// <summary>
        /// ["<c>margin</c>"] Margin value
        /// </summary>
        [JsonPropertyName("margin")]
        public GateIoAccountValue Margin { get; set; } = null!;
        /// <summary>
        /// ["<c>quant</c>"] Quant value
        /// </summary>
        [JsonPropertyName("quant")]
        public GateIoAccountValue Quant { get; set; } = null!;
        /// <summary>
        /// ["<c>futures</c>"] Futures value
        /// </summary>
        [JsonPropertyName("futures")]
        public GateIoAccountValue Futures { get; set; } = null!;
        /// <summary>
        /// ["<c>delivery</c>"] Delivery value
        /// </summary>
        [JsonPropertyName("delivery")]
        public GateIoAccountValue Delivery { get; set; } = null!;
        /// <summary>
        /// ["<c>warrant</c>"] Warrant value
        /// </summary>
        [JsonPropertyName("warrant")]
        public GateIoAccountValue Warrant { get; set; } = null!;
        /// <summary>
        /// ["<c>cbbc</c>"] CBBC value
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
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amount</c>"] Total account balance
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>unrealised_pnl</c>"] Unrelealised profit and loss
        /// </summary>
        [JsonPropertyName("unrealised_pnl")]
        public decimal? UnrealisedPnl { get; set; }
        /// <summary>
        /// ["<c>borrowed</c>"] Borrowed
        /// </summary>
        [JsonPropertyName("borrowed")]
        public decimal? Borrowed { get; set; }
    }
}
