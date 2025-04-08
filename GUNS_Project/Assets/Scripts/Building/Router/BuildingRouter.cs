using System.Linq;
using UnityEngine;

public class BuildingRouter : IRouter
{
    private BuildingWindow Window => UiController.Instance.GetWindow<BuildingWindow>();

    
    public void Init()
    {
        foreach (var point in Window.Models.Select(x=>x.Point))
        {
            PressurePlateController.Instance.AddPressurePlate(point, PressurePlateType.Gold);
            
            //PressurePlateController.Instance.PressurePlateViewsByPoints[point].Entered
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
    NextLevel
}