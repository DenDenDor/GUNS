using System.Linq;
using UnityEngine;

public class BuildTankEducationStep : AbstractEducationStep
{
    protected override void OnOpen()
    {
        BuildingController.Instance.CreatedBuilding += OnCreatedBuilding;
        
        EnterArrow(GetTarget);
    }

    private Transform GetTarget()
    {
        return BuildingController.Instance.BuildingPoints.FirstOrDefault(x=>x.Type == BuildingType.Tank).Point;
    }
    
    private void OnCreatedBuilding(BuildingType obj)
    {
        if (obj == BuildingType.Tank)
        {
            Close();
        }
    }

    protected override void OnClose()
    {
        BuildingController.Instance.CreatedBuilding -= OnCreatedBuilding;
        ExitArrow();
    }
}
