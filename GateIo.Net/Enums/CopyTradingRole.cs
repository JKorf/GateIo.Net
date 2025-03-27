namespace GateIo.Net.Enums
{
    public enum CopyTradingRole
    {
        OrdinaryUser = 0,
        OrderLeader = 1,
        Follower = 2,
        OrderLeaderAndFollower = OrderLeader | Follower,
    }
}
