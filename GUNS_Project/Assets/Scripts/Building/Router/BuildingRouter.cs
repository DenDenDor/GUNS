using System.Linq;
using UnityEngine;

public class BuildingRouter : IRouter
{
    private BuildingWindow Window => UiController.Instance.GetWindow<BuildingWindow>();

    
    public void Init()
    {
        foreach (var item in Window.Models)
        {
            Transform point = item.Point;
            PressurePlateType type;

            switch (item.Type)
            {
                case BuildingType.NextLevel:
                    type = PressurePlateType.Gold;
                    break;
                default:
                    type = PressurePlateType.Silver;
                    break;
            }
            
            PressurePlateController.Instance.AddPressurePlate(point, type);
        }
        
        UpdateController.Instance.Add(OnUpdate);
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