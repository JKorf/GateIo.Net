using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GateIo.Net.Objects.Options
{
    /// <summary>
    /// GateIo options
    /// </summary>
    public class GateIoOptions : LibraryOptions<GateIoRestOptions, GateIoSocketOptions, ApiCredentials, GateIoEnvironment>
    {
    }
}
