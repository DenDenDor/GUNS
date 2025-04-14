using UnityEngine;

public class WatchableReward
{
    public int Amount { get; set; }
    public RewardType RewardType { get; set; }
    public float CurrentTime { get; private set; }
    public float MaxTime { get; private set; }

    public WatchableReward(int amount, RewardType rewardType)
    {
        Amount = amount;
        RewardType = rewardType;
        CurrentTime = 20;
        MaxTime = CurrentTime;
    }

    public void DecreaseTime()
    {
        CurrentTime -= Time.deltaTime;
    }
}
