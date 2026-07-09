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
    /// Hedge status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<HedgeStatus>))]
    public enum HedgeStatus
    {
        /// <summary>
        /// ["<c>partial_hedged</c>"] Partially hedged
        /// </summary>
        [Map("partial_hedged")]
        PartiallyHedged,
        /// <summary>
        /// ["<c>full_hedged</c>"] Fully hedged
        /// </summary>
        [Map("full_hedged")]
        FullyHedged
    }

}
