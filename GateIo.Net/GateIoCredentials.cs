using CryptoExchange.Net.Authentication;
using System;

namespace GateIo.Net
{
    /// <summary>
    /// GateIo credentials
    /// </summary>
    public class GateIoCredentials : ApiCredentials
    {
        /// <summary>
        /// </summary>
        [Obsolete("Parameterless constructor is only for deserialization purposes and should not be used directly. Use parameterized constructor instead.")]
        public GateIoCredentials() { }

        /// <summary>
        /// Create credentials using an HMAC key, and secret
        /// </summary>
        /// <param name="apiKey">The API key</param>
        /// <param name="secret">The API secret</param>
        public GateIoCredentials(string apiKey, string secret) : this(new HMACCredential(apiKey, secret)) { }

        /// <summary>
        /// Create GateIo credentials using HMAC credentials
        /// </summary>
        /// <param name="credential">The HMAC credentials</param>
        public GateIoCredentials(HMACCredential credential) : base(credential) { }

        /// <inheritdoc />
#pragma warning disable CS0618 // Type or member is obsolete
        public override ApiCredentials Copy() => new GateIoCredentials { CredentialPairs = CredentialPairs };
#pragma warning restore CS0618 // Type or member is obsolete
    }
}
