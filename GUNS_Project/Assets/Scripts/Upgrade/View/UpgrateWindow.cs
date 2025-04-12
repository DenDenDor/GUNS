using System.Collections.Generic;
using UnityEngine;

public class UpgrateWindow : AbstractWindowUi
{
    [SerializeField] private Transform _spawnPoint;

    private Dictionary<UpgradedStatView, UpgradeType> _viewsByModels = new();

    public Dictionary<UpgradedStatView, UpgradeType> ViewsByModels => _viewsByModels;

    public override void Init()
    {
    }

    public UpgradedStatView Create(UpgradedStatView prefab, UpgradeType type)
    {
        UpgradedStatView view = Instantiate(prefab, _spawnPoint);

        _viewsByModels.Add(view, type);
        
        return view;
    }

    public void Open()
    {
        
    }
}