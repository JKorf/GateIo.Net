using CryptoExchange.Net.Authentication;

namespace GateIo.Net
{
    /// <summary>
    /// GateIo credentials
    /// </summary>
    public class GateIoCredentials : ApiCredentials
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="apiKey">The API key</param>
        /// <param name="secret">The API secret</param>
        public GateIoCredentials(string apiKey, string secret) : this(new HMACCredential(apiKey, secret)) { }
       
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="credential">The HMAC credentials</param>
        public GateIoCredentials(HMACCredential credential) : base(credential) { }

        /// <inheritdoc />
        public override ApiCredentials Copy() => new GateIoCredentials(Hmac!);
    }
}
