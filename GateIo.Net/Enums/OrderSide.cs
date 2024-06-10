using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Side of an order
    /// </summary>
    public enum OrderSide
    {
        /// <summary>
        /// Buy order
        /// </summary>
        [Map("buy")]
        Buy,
        /// <summary>
        /// Sell order
        /// </summary>
        [Map("sell")]
        Sell
    }
}
