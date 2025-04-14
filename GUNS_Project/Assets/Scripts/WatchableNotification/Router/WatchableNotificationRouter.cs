using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class WatchableNotificationRouter : IRouter
{
    private WatchableNotificationView _prefab;
    private WatchableNotificationWindow Window => UiController.Instance.GetWindow<WatchableNotificationWindow>();
    
    public void Init()
    {
        _prefab = FactoryController.Instance.FindPrefab<WatchableNotificationView>();

        WatchableReward watchableReward = new WatchableReward(10, RewardType.Silver);

        WatchableNotificationView view = Window.Create(_prefab, watchableReward);

        view.UpdateInfo(watchableReward);
        
        view.Clicked += OnClicked;
        
        UpdateController.Instance.Add(OnUpdate);
    }

    private void OnUpdate()
    {
        for (int i = 0; i < Window.ViewsByModels.Count; i++)
        {
            KeyValuePair<WatchableNotificationView, WatchableReward> pair = Window.ViewsByModels.ToArray()[i];
            
            WatchableReward reward = pair.Value;
            WatchableNotificationView view = pair.Key;

            if (view != null)
            {
                reward.DecreaseTime();

                float fillAmount = (float) reward.CurrentTime / reward.MaxTime;
            
                if (reward.CurrentTime < 0)
                {
                    view.Clicked -= OnClicked;

                    Window.Remove(view);
                }
                else
                {
                    view.UpdateBar(fillAmount);
                }
            }
        }
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