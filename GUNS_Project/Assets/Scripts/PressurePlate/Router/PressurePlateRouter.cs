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

    private void OnCreated(Transform transform, PressurePlateType type, BuildingType buildingType)
    {
        AbstractPressurePlateView prefab = null;
        AbstractPlateByBuilding buildingPlate = null;

        switch (buildingType)
        {
            case BuildingType.Barrack:
                buildingPlate = FactoryController.Instance.FindPrefab<BarrackPlate>();
                break;
            case BuildingType.NextLevel:
                buildingPlate = FactoryController.Instance.FindPrefab<NextLevelPlate>();
                break;
            case BuildingType.Tank:
                buildingPlate = FactoryController.Instance.FindPrefab<TankPlate>();
                break;
            case BuildingType.Attack:
                buildingPlate = FactoryController.Instance.FindPrefab<AttackPlate>();
                break;
            default:
                buildingPlate = FactoryController.Instance.FindPrefab<EmptyPlate>();
                break;
        }

        AbstractPlateByBuilding createdBuildingPlate = Object.Instantiate(buildingPlate, transform);
        
        createdBuildingPlate.transform.localRotation = Quaternion.Euler(90,0,0);

        switch (type)
        {
            case PressurePlateType.FillingUp:
                prefab = createdBuildingPlate.gameObject.AddComponent<FillingUpPressurePlateView>();
                break;
            case PressurePlateType.Gold:
                prefab = createdBuildingPlate.gameObject.AddComponent<GoldPressurePlateView>();
                break;
            case PressurePlateType.Silver:
                prefab = createdBuildingPlate.gameObject.AddComponent<SilverPressurePlateView>();
                break;
            case PressurePlateType.Timer:
                prefab = createdBuildingPlate.gameObject.AddComponent<TimerPressurePlateView>();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        BoxCollider boxCollider = createdBuildingPlate.gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = true;
        boxCollider.size = new Vector3(67, 95, 1);

        // AbstractPressurePlateView plateView = null;
        
        PressurePlateController.Instance.Register(transform, prefab);
        
        prefab.FilledIn += OnFilledIn;
    }

    private void OnFilledIn(AbstractPressurePlateView obj)
    {
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

        Debug.Log("PRESSURE PLATE!");
    
        foreach (var unblockingBuilding in abstractWaveInfo.BuildingPoints)
        {
            if (unblockingBuilding.Current.Point == plateTransform)
            {
                BuildingController.Instance.GenerateNewBuilding(unblockingBuilding.BlockedPoints.Select(x=>x.Current));
            
                break;
            }
        }
    }

    public void Exit()
    {
        PressurePlateController.Instance.Created -= OnCreated;
    }
}