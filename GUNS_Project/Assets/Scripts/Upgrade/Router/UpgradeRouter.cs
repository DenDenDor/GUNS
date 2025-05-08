using System.Collections.Generic;
using UnityEngine;

public class UpgradeRouter : IRouter
{
    private UpgradeStatSO _data;
    private UpgradedStatPanel _prefab;
    private Dictionary<UpgradeType, int> _levelsByTypes = new();

    private UpgrateWindow Window => UiController.Instance.GetWindow<UpgrateWindow>();
    
    public void Init()
    {
        _data = Resources.Load<UpgradeStatSO>("Prefabs/UpgradeStatSO");
        _prefab = FactoryController.Instance.FindPrefab<UpgradedStatPanel>();//Resources.Load<UpgradedStatView>("Prefabs/StatView");
        
        _levelsByTypes.Add(UpgradeType.Health, 0);
        _levelsByTypes.Add(UpgradeType.Speed, 0);
        _levelsByTypes.Add(UpgradeType.Damage, 0);

        foreach (var item in _levelsByTypes)
        {
            UpgradeController.Instance.SetUpgradeLevel(item.Key, item.Value);
        }
        
        UpgradeStatModel stat = new UpgradeStatModel();
        
        UpgradeController.Instance.UpdateStat(stat);
        
        UpdateController.Instance.Add(OnUpdate);

        WaveController.Instance.StartedNewWave += OpenWindow;
    }

    private void OnClear()
    {
        UpgradeController.Instance.StopUpgraded();
        Window.ClearAll();
    }

    private void OpenWindow()
    {
        if (WaveController.Instance.GenerateWaveInfo().IdLevel > 1)
        {
            Window.Open();
            Window.Closed += OnClear;
        
            UpgradedStatPanel panel = Window.CreateUi(_prefab);
            int i = 0;
        
            foreach (var item in _levelsByTypes)
            {
                UpgradedStatView view = panel.Views[i];//Window.CreateUi(_prefab, item.Key);
                view.UpdateSprite(_data.GetUpgradeSprite(item.Key));
                view.Bought += OnBought;
                view.WatchedAd += OnWatchAd;

                UpdateView(view, item.Key, item.Value, UpgradeController.Instance.Stat.Health);
            
                Window.InitStat(view, item.Key);
                i++;
            }
        }
    }

    private void OnWatchAd(UpgradedStatView obj)
    {
        
    }

    private void OnBought(UpgradedStatView view)
    {
        UpgradeType type = Window.ViewsByModels[view];

        int currentLevel = _levelsByTypes[type];
        int newLevel = currentLevel + 1;
        _levelsByTypes[type] = newLevel;
        UpgradeController.Instance.SetUpgradeLevel(type, newLevel);

        UpgradeStatModel currentStats = UpgradeController.Instance.Stat;

        Debug.Log("AD " + currentLevel);
        
        if (_data.TryGetValueForLevel(type, newLevel, out float value))
        {
            switch (type)
            {
                case UpgradeType.Health:
                    currentStats.Health = value;
                    break;
                case UpgradeType.Speed:
                    currentStats.Speed = value;
                    break;
                case UpgradeType.Damage:
                    currentStats.Strength = value;
                    break;
            }
            
            UpdateView(view, type, newLevel, value);

            UpgradeController.Instance.UpdateStat(currentStats);
        }
    }

    private void UpdateView(UpgradedStatView view, UpgradeType type, int newLevel, float value)
    {
        if (_data.TryGetLevel(type, newLevel, out int previousLevel, out int nextLevel))
        {
            view.UpdateProgressAmount(newLevel, nextLevel);

            if (nextLevel == -1)
            {
                view.ShowMaxLevelPanel();
            }

            int correctNewLevel = newLevel - previousLevel;
            int correctNextLevel = nextLevel - previousLevel;

            float fillAmount = (float) correctNewLevel / correctNextLevel;
            view.UpdateProgressBar(fillAmount);
        }

        if (_data.TryGetIndexForLevel(type, value, out int index))
        {
            view.UpdateLevel(index + 1);
        }
    }

    private void OnUpdate()
    {
        
    }

    public void Exit()
    {
        
    }
}