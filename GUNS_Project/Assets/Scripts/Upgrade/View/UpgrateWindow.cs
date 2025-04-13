using System.Collections.Generic;
using UnityEngine;

public class UpgrateWindow : AbstractFactoryWindow
{
    [SerializeField] private Transform _spawnPoint;

    private Dictionary<UpgradedStatView, UpgradeType> _viewsByModels = new();

    public Dictionary<UpgradedStatView, UpgradeType> ViewsByModels => _viewsByModels;

    public override void Init()
    {
    }

    public UpgradedStatView CreateUi(UpgradedStatView prefab, UpgradeType type)
    {
        UpgradedStatView view = CreatePrefab(prefab, _spawnPoint, true);

        _viewsByModels.Add(view, type);
        
        return view;
    }

    public void Open()
    {
        
    }

    public void ClearAll()
    {
        _viewsByModels.DestroyAllMonoBehaviours();
        
        _viewsByModels.Clear();
    }
}