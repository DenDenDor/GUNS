using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WatchableNotificationRouter : IRouter
{
    private WatchableNotificationView _prefab;
    private WatchableNotificationWindow Window => UiController.Instance.GetWindow<WatchableNotificationWindow>();
        
    public void Init()
    {
        _prefab = Resources.Load<WatchableNotificationView>("Prefabs/WatchableNotification");

        WatchableReward watchableReward = new WatchableReward(10, RewardType.Gold);

        WatchableNotificationView view = Window.Create(_prefab, watchableReward);

        view.UpdateInfo(watchableReward);
        
        view.Clicked += OnClicked;
    }

    private void OnClicked(WatchableNotificationView view)
    {
        WatchableReward model = Window.ViewsByModels[view];

        SDKMediator.Instance.Watch(() => OnWatch(model), model.RewardType);

        Window.Remove(view);
    }

    private void OnWatch(WatchableReward reward)
    {
        for (int i = 0; i < reward.Amount; i++)
        {
            int border = 2;
            
            Vector3 additionalPosition = new Vector3(Random.Range(-border, border), 0, Random.Range(-border, border));
            
            Vector3 position = EntityController.Instance.Player.transform.position + additionalPosition;
        
            switch (reward.RewardType)
            {
                case RewardType.Gold:
                    CurrencyController.Instance.CreateGold(position);
                    break;
                case RewardType.Silver:
                    CurrencyController.Instance.CreateSilver(position);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public void Exit()
    {
        
    }
}