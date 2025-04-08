using System.Linq;
using UnityEngine;

public class BarrackRouter : IRouter
{
    private BarrackView _prefab;
    
    public void Init()
    {
        _prefab = Resources.Load<BarrackView>("Prefabs/Barrack");

        foreach (var model in UiController.Instance.GetWindow<BuildingWindow>().Models.Where(x=>x.Type == BuildingType.Barrack))
        {
            PressurePlateController.Instance.PressurePlateViewsByPoints[model.Point].FilledIn += OnFilledIn;
        }
        
        UpdateController.Instance.Add(OnUpdate);
    }

    private void OnUpdate()
    {
        foreach (var item in BuildingController.Instance.Barracks)
        {
            item.Value.OnTimeReset = OnTimeReset;
        }
    }

    private void OnTimeReset(BuildingModel model)
    {
        AbstractBuildingView view = BuildingController.Instance.Barracks.FirstOrDefault(x=>x.Value == model).Key;
        
        BarrackController.Instance.Create(view.transform);
    }

    private void OnFilledIn(AbstractPressurePlateView obj)
    {
        BarrackView barrack = Object.Instantiate(_prefab, obj.transform.position, Quaternion.identity);
        
        BuildingController.Instance.AddBuilding(barrack, new BuildingModel());
    }

    public void Exit()
    {
        
    }
}