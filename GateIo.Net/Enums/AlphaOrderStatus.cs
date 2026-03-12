using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AlphaOrderStatus>))]
    public enum AlphaOrderStatus
    {
        /// <summary>
        /// ["<c>1</c>"] Processing
        /// </summary>
        [Map("1")]
        Processing,
        /// <summary>
        /// ["<c>2</c>"] Successful
        /// </summary>
        [Map("2")]
        Successful,
        /// <summary>
        /// ["<c>3</c>"] Failed
        /// </summary>
        [Map("3")]
        Failed,
        /// <summary>
        /// ["<c>4</c>"] Cancelled
        /// </summary>
        [Map("4")]
        Cancelled,
        /// <summary>
        /// ["<c>5</c>"] Buy order placed but transfer not completed
        /// </summary>
        [Map("5")]
        BuyPlacedNotTransferred,
        /// <summary>
        /// ["<c>6</c>"] Canceled but not transferred yet
        /// </summary>
        [Map("6")]
        CanceledNotTransferred
    }
}
