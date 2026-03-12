using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Repay type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<RepayType>))]
    public enum RepayType
    {
        /// <summary>
        /// ["<c>none</c>"] No repay
        /// </summary>
        [Map("none")]
        None,
        /// <summary>
        /// ["<c>manual_repay</c>"] Manual repayment
        /// </summary>
        [Map("manual_repay")]
        ManualRepay,
        /// <summary>
        /// ["<c>auto_repay</c>"] Automatic repayment
        /// </summary>
        [Map("auto_repay")]
        AutoRepay,
        /// <summary>
        /// ["<c>cancel_auto_repay</c>"] Automatic repayment after cancelation
        /// </summary>
        [Map("cancel_auto_repay")]
        CancelAutoRepay
    }
}
