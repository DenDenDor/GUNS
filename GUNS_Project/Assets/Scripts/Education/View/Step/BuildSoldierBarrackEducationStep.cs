using System.Linq;
using UnityEngine;

public class BuildSoldierBarrackEducationStep : AbstractEducationStep
{
    protected override void OnOpen()
    {
        BuildingController.Instance.CreatedBuilding += OnCreatedBuilding;
        
        EnterArrow(GetTarget);
    }

    private Transform GetTarget()
    {
        return BuildingController.Instance.BuildingPoints.FirstOrDefault(x=>x.Type == BuildingType.Barrack).Point;
    }

    private void OnCreatedBuilding(BuildingType obj)
    {
        if (obj == BuildingType.Barrack)
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
