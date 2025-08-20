namespace GateIo.Net.Enums
{
    /// <summary>
    /// Copy trading role
    /// </summary>
    public enum CopyTradingRole
    {
        /// <summary>
        /// Ordinary user
        /// </summary>
        OrdinaryUser = 0,
        /// <summary>
        /// Order leader (lead trader)
        /// </summary>
        OrderLeader = 1,
        /// <summary>
        /// Follower
        /// </summary>
        Follower = 2,
        /// <summary>
        ///  Order leader and follower
        /// </summary>
        OrderLeaderAndFollower = OrderLeader | Follower,
    }
}
