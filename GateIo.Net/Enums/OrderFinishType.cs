using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// How an order was finished
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderFinishType>))]
    public enum OrderFinishType
    {
        /// <summary>
        /// New order
        /// </summary>
        [Map("_new")]
        New,
        /// <summary>
        /// Still open
        /// </summary>
        [Map("open")]
        Open,
        /// <summary>
        /// Filled
        /// </summary>
        [Map("filled")]
        Filled,
        /// <summary>
        /// Manually canceled
        /// </summary>
        [Map("cancelled")]
        Canceled,
        /// <summary>
        /// IOC order was immediately canceled
        /// </summary>
        [Map("ioc")]
        ImmediatelyCanceled,
        /// <summary>
        /// Canceled because of STP
        /// </summary>
        [Map("stp")]
        SelfTradePrevention,
        /// <summary>
        /// Canceled because of liquidation
        /// </summary>
        [Map("liquidated")]
        Liquidated,
        /// <summary>
        /// Finished by ADL
        /// </summary>
        [Map("auto_deleveraged")]
        AutoDeleveraged,
        /// <summary>
        /// Canceled because of increasing position while reduce-only set
        /// </summary>
        [Map("reduce_only")]
        ReduceOnly,
        /// <summary>
        /// Canceled because of position close
        /// </summary>
        [Map("position_closed")]
        PositionClosed
    }
}
