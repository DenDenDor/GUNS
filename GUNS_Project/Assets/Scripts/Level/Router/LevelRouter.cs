using System.Linq;
using UnityEngine;

public class LevelRouter : IRouter
{
    private NextLevelBuildingView _prefab;

    public void Init()
    {
       // _prefab = Resources.Load<NextLevelBuildingView>("Prefabs/NextLevel");

        foreach (var model in UiController.Instance.GetWindow<BuildingWindow>().Models.Where(x=>x.Type == BuildingType.NextLevel))
        {
            PressurePlateController.Instance.PressurePlateViewsByPoints[model.Point].FilledIn += OnFilledIn;
        }
    }
    
    private void OnFilledIn(AbstractPressurePlateView obj)
    {
        PressurePlateController.Instance.ResetRegister();
    }

    public void Exit()
    {
        
    }
}