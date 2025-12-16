using System.Text.Json.Serialization;

namespace GateIo.Net.Objects.Internal
{
    internal class GateIoFuturesGetOrderRequest
    {
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
    }
}
