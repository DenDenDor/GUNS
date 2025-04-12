using System.Collections.Generic;
using UnityEngine;

public class UpgradeRouter : IRouter
{
    private UpgradeStatSO _data;
    private UpgradedStatView _prefab;
    private Dictionary<UpgradeType, int> _levelsByTypes = new();

    private UpgrateWindow Window => UiController.Instance.GetWindow<UpgrateWindow>();
    
    public void Init()
    {
        _data = Resources.Load<UpgradeStatSO>("Prefabs/UpgradeStatSO");
        _prefab = Resources.Load<UpgradedStatView>("Prefabs/StatView");
        
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

    private void OpenWindow()
    {
        Window.Open();

        foreach (var item in _levelsByTypes)
        {
            UpgradedStatView view = Window.Create(_prefab, item.Key);
            view.UpdateSprite(_data.GetUpgradeSprite(item.Key));
            view.Bought += OnBought;
            
            if (_data.TryGetNextLevel(item.Key, 0, out int previousLevel, out int nextLevel))
            {
                view.UpdateProgressAmount(0, nextLevel);

                int correctNewLevel = 0 - previousLevel;
                int correctNextLevel = nextLevel - previousLevel;

                float fillAmount = (float) correctNewLevel / correctNextLevel;
                view.UpdateProgressBar(fillAmount);
            }

        }
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
            
            if (_data.TryGetNextLevel(type, newLevel, out int previousLevel, out int nextLevel))
            {
                view.UpdateProgressAmount(newLevel, nextLevel);

                int correctNewLevel = newLevel - previousLevel;
                int correctNextLevel = nextLevel - previousLevel;

                float fillAmount = (float) correctNewLevel / correctNextLevel;
                view.UpdateProgressBar(fillAmount);
            }
            
            UpgradeController.Instance.UpdateStat(currentStats);
        }
        
    }

    private void OnUpdate()
    {
        
    }

    public void Exit()
    {
        
    }
}