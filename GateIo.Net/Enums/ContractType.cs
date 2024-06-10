using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Contract type
    /// </summary>
    public enum ContractType
    {
        /// <summary>
        /// Inverse
        /// </summary>
        [Map("inverse")]
        Inverse,
        /// <summary>
        /// Direct
        /// </summary>
        [Map("direct")]
        Direct
    }
}
