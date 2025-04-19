using System.Collections;
using System.Linq;
using UnityEngine;

public class LevelRouter : IRouter
{
    private NextLevelBuildingView _prefab;
    private RankUpView _prefabUi;
    private int _level;
    private RankUpView _rankUpView;
    private BuildingController Building => BuildingController.Instance;
    
    private LevelWindow Window => UiController.Instance.GetWindow<LevelWindow>();

    public void Init()
    {
       // _prefab = Resources.Load<NextLevelBuildingView>("Prefabs/NextLevel");
       
       WaveController.Instance.StartedNewWave += StartNewWave;

       _prefabUi = FactoryController.Instance.FindPrefab<RankUpView>();
    }

    private void StartNewWave()
    {
        foreach (var model in Building.BuildingPoints.Where(x=>x.Type == BuildingType.NextLevel))
        {
            var plate = PressurePlateController.Instance.PressurePlateViewsByPoints[model.Point];

            if (plate.TryGetComponent(out IValueDisplay valueDisplay))
            {
                valueDisplay.DisplayValue(_level);
            }
            
            plate.FilledIn += OnFilledIn;
        }
    }

    private void OnFilledIn(AbstractPressurePlateView plate)
    {
        _level++;
        
        if (plate.TryGetComponent(out IValueDisplay valueDisplay))
        {
            valueDisplay.DisplayValue(_level);
        }
        
        _rankUpView = Window.Create(_prefabUi);

        _rankUpView.Closed += () => OnClosed(plate);
        _rankUpView.UpdateView(_level);
        
    }

    private void OnClosed(AbstractPressurePlateView obj)
    {
        Object.Destroy(_rankUpView.gameObject);

        CoroutineController.Instance.RunCoroutine(Cooldown(obj));
    }

    private IEnumerator Cooldown(AbstractPressurePlateView obj)
    {
        EntityController.Instance.Player.UpdateTriggerView();
        yield return null;
        
        PressurePlateController.Instance.ResetRegister(obj, 5 * (_level + 2));
    }

    public void Exit()
    {
        
    }
}