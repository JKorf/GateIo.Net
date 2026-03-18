using CryptoExchange.Net.Authentication;

namespace GateIo.Net
{
    /// <summary>
    /// GateIo API credentials
    /// </summary>
    public class GateIoCredentials : HMACCredential
    {
        /// <summary>
        /// Create new credentials providing only credentials in HMAC format
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="secret">API secret</param>
        public GateIoCredentials(string key, string secret) : base(key, secret)
        {
        }
    }
}
