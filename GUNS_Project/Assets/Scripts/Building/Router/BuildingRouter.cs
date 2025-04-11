using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingRouter : IRouter
{
    private BuildingWindow Window => UiController.Instance.GetWindow<BuildingWindow>();
    private BuildingController Building => BuildingController.Instance;

    
    public void Init()
    {
        GeneratePoints(Building.BuildingPoints);

        Building.GeneratedPoints += GeneratePoints;
        
        UpdateController.Instance.Add(OnUpdate);
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