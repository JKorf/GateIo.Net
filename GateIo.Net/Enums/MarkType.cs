using CryptoExchange.Net.Attributes;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Mark type
    /// </summary>
    public enum MarkType
    {
        /// <summary>
        /// Internal
        /// </summary>
        [Map("internal")]
        Internal,
        /// <summary>
        /// Index
        /// </summary>
        [Map("index")]
        Index
    }
}
