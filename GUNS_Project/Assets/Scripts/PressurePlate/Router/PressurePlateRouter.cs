using System;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class PressurePlateRouter : IRouter
{
    public void Init()
    {
        PressurePlateController.Instance.Created += OnCreated;
        
        WaveController.Instance.Cleared += OnClear;
    }

    private void OnClear()
    {
        PressurePlateController.Instance.ClearAll();
    }

    private void OnCreated(Transform transform, PressurePlateType type)
    {
        AbstractPressurePlateView prefab = null;

        switch (type)
        {
            case PressurePlateType.FillingUp:
                prefab = Resources.Load<FillingUpPressurePlateView>("Prefabs/FillingUpPressurePlateView");
                break;
            case PressurePlateType.Gold:
                prefab = Resources.Load<GoldPressurePlateView>("Prefabs/GoldPressurePlateView");
                break;
            case PressurePlateType.Silver:
                prefab = Resources.Load<SilverPressurePlateView>("Prefabs/SilverPressurePlateView");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
        
        AbstractPressurePlateView plateView = Object.Instantiate(prefab, transform);
        
        PressurePlateController.Instance.Register(transform, plateView);
        
        plateView.FilledIn += OnFilledIn;
    }

    private void OnFilledIn(AbstractPressurePlateView obj)
    {
        Debug.Log("Pressure plate filled in, unblocking corresponding buildings");
    
        AbstractWaveInfo abstractWaveInfo = WaveController.Instance.GenerateWaveInfo();
    
        Transform plateTransform = null;
        foreach (var pair in PressurePlateController.Instance.PressurePlateViewsByPoints)
        {
            if (pair.Value == obj)
            {
                plateTransform = pair.Key;
                break;
            }
        }
    
        if (plateTransform == null)
        {
            Debug.LogError("Could not find transform for the filled pressure plate");
            return;
        }
    
        foreach (var unblockingBuilding in abstractWaveInfo.BuildingPoints)
        {
            if (unblockingBuilding.Current.Point == plateTransform)
            {
                Debug.Log($"Found matching building point for pressure plate. Unblocking {unblockingBuilding.BlockedPoints.Count} points.");
                
                BuildingController.Instance.GenerateNewBuilding(unblockingBuilding.BlockedPoints.Select(x=>x.Current));
            
                foreach (var blockedPoint in unblockingBuilding.BlockedPoints)
                {
                   // Debug.Log($"Unblocking building point at position: {blockedPoint.Current.Point.position}");
                
                    // For example:
                    // blockedPoint.Current.Activate();
                }
            
                break;
            }
        }
    }

    public void Exit()
    {
        PressurePlateController.Instance.Created -= OnCreated;
    }
}