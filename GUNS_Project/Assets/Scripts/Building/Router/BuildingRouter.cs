using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingRouter : IRouter
{
    private BuildingWindow Window => UiController.Instance.GetWindow<BuildingWindow>();
    private BuildingController Building => BuildingController.Instance;

    
    public void Init()
    {
        CreateBuildings();

        Building.GeneratedPoints += GeneratePoints;
        
        UpdateController.Instance.Add(OnUpdate);

        WaveController.Instance.Cleared += OnClear;
    }

    private void CreateBuildings()
    {
        GeneratePoints(Building.BuildingPoints);
    }

    private void OnClear()
    {
        BuildingController.Instance.ClearAll();
    }

    private void GeneratePoints(IEnumerable<BuildingPoint> points)
    {
        foreach (var item in points)
        {
            int price = 5;
            
            Transform point = item.Point;
            PressurePlateType type;

            switch (item.Type)
            {
                case BuildingType.NextLevel:
                    type = PressurePlateType.Gold;
                    price = 10;
                    break;
                default:
                    type = PressurePlateType.Gold;
                    break;
            }
            
            PressurePlateController.Instance.AddPressurePlate(point, type);
            PressurePlateController.Instance.UpdateAllPrice(point, price);
        }
    }

    private void OnUpdate()
    {
        foreach (var item in BuildingController.Instance.Buildings)
        {
            item.Value.Update();
        }
    }

    public void Exit()
    {
        
    }
}

public enum BuildingType
{
    Barrack,
    NextLevel,
    Armor,
    Tank
}