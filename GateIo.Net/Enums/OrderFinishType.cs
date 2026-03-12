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
        /// ["<c>_new</c>"] New order
        /// </summary>
        [Map("_new")]
        New,
        /// <summary>
        /// ["<c>open</c>"] Still open
        /// </summary>
        [Map("open")]
        Open,
        /// <summary>
        /// ["<c>filled</c>"] Filled
        /// </summary>
        [Map("filled")]
        Filled,
        /// <summary>
        /// ["<c>cancelled</c>"] Manually canceled
        /// </summary>
        [Map("cancelled")]
        Canceled,
        /// <summary>
        /// ["<c>ioc</c>"] IOC order was immediately canceled
        /// </summary>
        [Map("ioc")]
        ImmediatelyCanceled,
        /// <summary>
        /// ["<c>poc</c>"] Pending order policy is not met
        /// </summary>
        [Map("poc")]
        PendingOrCancelCancel,
        /// <summary>
        /// ["<c>fok</c>"] Not fully filled immediately 
        /// </summary>
        [Map("fok")]
        FillOrKillCancel,
        /// <summary>
        /// ["<c>stp</c>"] Canceled because of STP
        /// </summary>
        [Map("stp")]
        SelfTradePrevention,
        /// <summary>
        /// ["<c>liquidated</c>"] Canceled because of liquidation
        /// </summary>
        [Map("liquidated", "liquidate_cancelled")]
        Liquidated,
        /// <summary>
        /// ["<c>auto_deleveraged</c>"] Finished by ADL
        /// </summary>
        [Map("auto_deleveraged")]
        AutoDeleveraged,
        /// <summary>
        /// ["<c>reduce_only</c>"] Canceled because of increasing position while reduce-only set
        /// </summary>
        [Map("reduce_only")]
        ReduceOnly,
        /// <summary>
        /// ["<c>position_closed</c>"] Canceled because of position close
        /// </summary>
        [Map("position_closed")]
        PositionClosed,
        /// <summary>
        /// ["<c>trader_not_enough</c>"] Insufficient counterparties lead to order cancellation
        /// </summary>
        [Map("trader_not_enough")]
        NotEnoughTraders,
        /// <summary>
        /// ["<c>depth_not_enough</c>"] Insufficient depth leads to order cancellation
        /// </summary>
        [Map("depth_not_enough")]
        NotEnoughCounterParties,
        /// <summary>
        /// ["<c>small</c>"] Order amount too small
        /// </summary>
        [Map("small")]
        TooSmall,
        /// <summary>
        /// ["<c>price_protect_cancelled</c>"] Price protect canceled
        /// </summary>
        [Map("price_protect_cancelled")]
        PriceProtectCancelled,
        /// <summary>
        /// ["<c>-</c>"] Unknown
        /// </summary>
        [Map("-")]
        Unknown
    }
}
