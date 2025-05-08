using System.Linq;
using UnityEngine;

public class UpgradeLevelEducationStep : AbstractEducationStep
{
    private BuildingPoint _buildingPoint;
    protected override void OnOpen()
    {
        _buildingPoint  = BuildingController.Instance.BuildingPoints.FirstOrDefault(x => x.Type == BuildingType.NextLevel);
        
        LevelController.Instance.Updated += OnUpdateLevel;
        
        EnterArrow(GetTarget);
    }

    private Transform GetTarget()
    {
        return _buildingPoint.Point;
    }
    
    private void OnUpdateLevel()
    {
        if (LevelController.Instance.Level == 1)
        {
            Close();
        }
    }

    protected override void OnClose()
    {
        LevelController.Instance.Updated -= OnUpdateLevel;
        ExitArrow();
    }
}
