using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GateIo.Net.Enums
{
    /// <summary>
    /// Asset status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AlphaAssetStatus>))]
    public enum AlphaAssetStatus
    {
        /// <summary>
        /// Normal
        /// </summary>
        [Map("1")]
        Normal,
        /// <summary>
        /// Suspended
        /// </summary>
        [Map("2")]
        Suspended,
        /// <summary>
        /// Delisted
        /// </summary>
        [Map("3")]
        Delisted
    }
}
