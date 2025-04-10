using System.Collections;
using System.Linq;
using UnityEngine;

public class LevelRouter : IRouter
{
    private NextLevelBuildingView _prefab;

    public void Init()
    {
       // _prefab = Resources.Load<NextLevelBuildingView>("Prefabs/NextLevel");

        foreach (var model in WaveController.Instance.GenerateWaveInfo().BuildingPoints.Select(x=>x.Current).Where(x=>x.Type == BuildingType.NextLevel))
        {
            PressurePlateController.Instance.PressurePlateViewsByPoints[model.Point].FilledIn += OnFilledIn;
        }
    }
    
    private void OnFilledIn(AbstractPressurePlateView obj)
    {
        CoroutineController.Instance.RunCoroutine(Cooldown(obj));
    }

    private IEnumerator Cooldown(AbstractPressurePlateView obj)
    {
        yield return new WaitForSeconds(1);
        
        PressurePlateController.Instance.ResetRegister(obj, 5);
    }

    public void Exit()
    {
        
    }
}