using System.Collections.Generic;
using UnityEngine;

public class WatchableNotificationWindow : AbstractWindowUi
{
    [SerializeField] private Transform _point;

    private readonly Dictionary<WatchableNotificationView, WatchableReward> _viewsByModels = new();

    public Dictionary<WatchableNotificationView, WatchableReward> ViewsByModels => _viewsByModels;

    public override void Init()
    {
        
    }

    public WatchableNotificationView Create(WatchableNotificationView prefab, WatchableReward model)
    {
        WatchableNotificationView view = Instantiate(prefab, _point);
        
        _viewsByModels.Add(view, model);

        return view;
    }

    public void Remove(WatchableNotificationView view)
    {
        _viewsByModels.Remove(view);
        
        Destroy(view.gameObject);
    }
}