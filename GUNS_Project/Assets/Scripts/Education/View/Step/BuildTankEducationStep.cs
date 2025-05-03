using UnityEngine;

public class BuildTankEducationStep : AbstractEducationStep
{
    protected override void OnOpen()
    {
        BuildingController.Instance.CreatedBuilding += OnCreatedBuilding;
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
    }
}
