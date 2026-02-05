using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Trackers.UserData.Interfaces;
using CryptoExchange.Net.Trackers.UserData.Objects;

namespace GateIo.Net.Interfaces
{
    /// <summary>
    /// Tracker factory
    /// </summary>
    public interface IGateIoTrackerFactory : ITrackerFactory
    {
        /// <summary>
        /// Create a new Spot user data tracker
        /// </summary>
        /// <param name="userIdentifier">User identifier</param>
        /// <param name="config">Configuration</param>
        /// <param name="credentials">Credentials</param>
        /// <param name="environment">Environment</param>
        IUserSpotDataTracker CreateUserSpotDataTracker(string userIdentifier, ApiCredentials credentials, SpotUserDataTrackerConfig? config = null, GateIoEnvironment? environment = null);
        /// <summary>
        /// Create a new spot user data tracker
        /// </summary>
        /// <param name="config">Configuration</param>
        IUserSpotDataTracker CreateUserSpotDataTracker(SpotUserDataTrackerConfig? config = null);

        /// <summary>
        /// Create a new futures user data tracker
        /// </summary>
        /// <param name="userIdentifier">User identifier</param>
        /// <param name="settleAsset">Settlement asset, for example `usdt`</param>
        /// <param name="userId">The user id</param>
        /// <param name="config">Configuration</param>
        /// <param name="credentials">Credentials</param>
        /// <param name="environment">Environment</param>
        IUserFuturesDataTracker CreateUserPerpetualFuturesDataTracker(string userIdentifier, ApiCredentials credentials, string settleAsset, long userId, FuturesUserDataTrackerConfig? config = null, GateIoEnvironment? environment = null);
        /// <summary>
        /// Create a new futures user data tracker
        /// </summary>
        /// <param name="settleAsset">Settlement asset, for example `usdt`</param>
        /// <param name="userId">The user id</param>
        /// <param name="config">Configuration</param>
        IUserFuturesDataTracker CreateUserPerpetualFuturesDataTracker(string settleAsset, long userId, FuturesUserDataTrackerConfig? config = null);
    }
}
