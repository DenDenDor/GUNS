using UnityEngine;

public class BuildSoldierBarrackEducationStep : AbstractEducationStep
{
    protected override void OnOpen()
    {
        BuildingController.Instance.CreatedBuilding += OnCreatedBuilding;
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
    }
}
