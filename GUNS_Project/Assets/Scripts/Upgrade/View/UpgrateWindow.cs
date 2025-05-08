using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgrateWindow : AbstractFactoryWindow
{
    [SerializeField] private Transform _spawnPoint;

    private Dictionary<UpgradedStatView, UpgradeType> _viewsByModels = new();

    private UpgradedStatPanel _upgradedStatPanel;
    public Dictionary<UpgradedStatView, UpgradeType> ViewsByModels => _viewsByModels;
    public event Action Closed;

    public override void Init()
    {
        
    }

    public UpgradedStatView CreateUi(UpgradedStatView prefab, UpgradeType type)
    {
        UpgradedStatView view = CreatePrefab(prefab, _spawnPoint, true);

        _viewsByModels.Add(view, type);
        
        return view;
    }

    public void InitStat(UpgradedStatView view, UpgradeType type)
    {
        _viewsByModels.Add(view, type);
    }
    
    public UpgradedStatPanel CreateUi(UpgradedStatPanel prefab)
    {
        UpgradedStatPanel view = CreatePrefab(prefab, _spawnPoint, true);
        view.Closed += () => Closed?.Invoke();
        _upgradedStatPanel = view;
        return view;
    }

    public void Open()
    {
        
    }

    public void ClearAll()
    {
        if (_upgradedStatPanel != null)
        {
            Destroy(_upgradedStatPanel.gameObject);
        }
        
        _viewsByModels.Clear();
    }
}