using CryptoExchange.Net.Objects;
using CryptoExchange.Net.RateLimiting.Guards;
using CryptoExchange.Net.RateLimiting.Interfaces;
using CryptoExchange.Net.RateLimiting;
using System;

namespace GateIo.Net
{
    /// <summary>
    /// Gate.io exchange information and configuration
    /// </summary>
    public static class GateIoExchange
    {
        /// <summary>
        /// Exchange name
        /// </summary>
        public static string ExchangeName => "Gate.io";

        /// <summary>
        /// Url to the main website
        /// </summary>
        public static string Url { get; } = "https://www.gate.io";

        /// <summary>
        /// Urls to the API documentation
        /// </summary>
        public static string[] ApiDocsUrl { get; } = new[] {
            "https://www.gate.io/docs/developers/apiv4/en/"
            };

        /// <summary>
        /// Rate limiter configuration for the Gate.io API
        /// </summary>
        public static GateIoRateLimiters RateLimiter { get; } = new GateIoRateLimiters();
    }

    /// <summary>
    /// Rate limiter configuration for the GateIo API
    /// </summary>
    public class GateIoRateLimiters
    {
        /// <summary>
        /// Event for when a rate limit is triggered
        /// </summary>
        public event Action<RateLimitEvent> RateLimitTriggered;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        internal GateIoRateLimiters()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Initialize();
        }

        private void Initialize()
        {
            Public = new RateLimitGate("Public")
                                    .AddGuard(new RateLimitGuard(RateLimitGuard.PerEndpoint, Array.Empty<IGuardFilter>(), 200, TimeSpan.FromSeconds(10), RateLimitWindowType.FixedAfterFirst)); // IP limit of 200 request per endpoint per 10 seconds
            RestSpotOrderPlacement = new RateLimitGate("SpotOrderPlacement")
                                    .AddGuard(new RateLimitGuard(RateLimitGuard.PerApiKey, Array.Empty<IGuardFilter>(), 10, TimeSpan.FromSeconds(1), RateLimitWindowType.FixedAfterFirst)); // Uid limit of 10 request per second
            RestSpotOrderCancelation = new RateLimitGate("SpotOrderCancelation")
                                    .AddGuard(new RateLimitGuard(RateLimitGuard.PerApiKey, Array.Empty<IGuardFilter>(), 200, TimeSpan.FromSeconds(1), RateLimitWindowType.FixedAfterFirst)); // Uid limit of 200 request per second
            RestFuturesOrderPlacement = new RateLimitGate("FuturesOrderPlacement")
                                    .AddGuard(new RateLimitGuard(RateLimitGuard.PerApiKey, Array.Empty<IGuardFilter>(), 100, TimeSpan.FromSeconds(1), RateLimitWindowType.FixedAfterFirst)); // Uid limit of 100 request per second
            RestFuturesOrderCancelation = new RateLimitGate("FuturesOrderCancelation")
                                    .AddGuard(new RateLimitGuard(RateLimitGuard.PerApiKey, Array.Empty<IGuardFilter>(), 200, TimeSpan.FromSeconds(1), RateLimitWindowType.FixedAfterFirst)); // Uid limit of 200 request per second
            RestSpotOther = new RateLimitGate("SpotOther")
                        .AddGuard(new RateLimitGuard(RateLimitGuard.PerApiKeyPerEndpoint, Array.Empty<IGuardFilter>(), 200, TimeSpan.FromSeconds(10), RateLimitWindowType.FixedAfterFirst)); // Uid limit of 200 request per 10 seconds
            RestFuturesOther = new RateLimitGate("FuturesOther")
                        .AddGuard(new RateLimitGuard(RateLimitGuard.PerApiKeyPerEndpoint, Array.Empty<IGuardFilter>(), 200, TimeSpan.FromSeconds(10), RateLimitWindowType.FixedAfterFirst)); // Uid limit of 200 request per 10 seconds
            RestPrivate = new RateLimitGate("PrivateOther")
                        .AddGuard(new RateLimitGuard(RateLimitGuard.PerApiKeyPerEndpoint, Array.Empty<IGuardFilter>(), 150, TimeSpan.FromSeconds(10), RateLimitWindowType.FixedAfterFirst)); // Uid limit of 150 request per 10 seconds
            RestOther = new RateLimitGate("Other")
                        .AddGuard(new RateLimitGuard(RateLimitGuard.PerApiKeyPerEndpoint, Array.Empty<IGuardFilter>(), 150, TimeSpan.FromSeconds(10), RateLimitWindowType.FixedAfterFirst)); // Uid limit of 150 request per 10 seconds

            Public.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            RestSpotOrderPlacement.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            RestSpotOrderCancelation.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            RestFuturesOrderPlacement.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            RestFuturesOrderCancelation.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            RestSpotOther.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            RestFuturesOther.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            RestPrivate.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            RestOther.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
        }


        internal IRateLimitGate Public { get; private set; }
        internal IRateLimitGate RestSpotOrderPlacement { get; private set; }
        internal IRateLimitGate RestSpotOrderCancelation { get; private set; }
        internal IRateLimitGate RestFuturesOrderPlacement { get; private set; }
        internal IRateLimitGate RestFuturesOrderCancelation { get; private set; }
        internal IRateLimitGate RestSpotOther { get; private set; }
        internal IRateLimitGate RestFuturesOther { get; private set; }
        internal IRateLimitGate RestPrivate { get; private set; }
        internal IRateLimitGate RestOther { get; private set; }
    }
}
