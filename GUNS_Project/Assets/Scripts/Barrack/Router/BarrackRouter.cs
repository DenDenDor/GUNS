using System.Linq;
using UnityEngine;

public class BarrackRouter : IRouter
{
    private BarrackView _prefab;
    private BuildingController Building => BuildingController.Instance;

    public void Init()
    {
        _prefab = Resources.Load<BarrackView>("Prefabs/Barrack");

        foreach (var model in Building.BuildingPoints.Where(x=>x.Type == BuildingType.Barrack))
        {
            PressurePlateController.Instance.PressurePlateViewsByPoints[model.Point].FilledIn += OnFilledIn;
        }
        
        UpdateController.Instance.Add(OnUpdate);
    }

    private void OnUpdate()
    {
        foreach (var item in Building.Barracks)
        {
            item.Value.OnTimeReset = OnTimeReset;
        }
    }

    private void OnTimeReset(BuildingModel model)
    {
        AbstractBuildingView view = Building.Barracks.FirstOrDefault(x=>x.Value == model).Key;
        
        BarrackController.Instance.Create(view.transform);
    }

    private void OnFilledIn(AbstractPressurePlateView obj)
    {
        BarrackView barrack = Object.Instantiate(_prefab, obj.transform.position, Quaternion.identity);
        
        Building.AddBuilding(barrack, new BuildingModel());
    }

    public void Exit()
    {
        
    }
}