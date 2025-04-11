using UnityEngine;

public class WatchableReward
{
    public int Amount { get; set; }
    public RewardType RewardType { get; set; }

    public WatchableReward(int amount, RewardType rewardType)
    {
        Amount = amount;
        RewardType = rewardType;
    }
}
