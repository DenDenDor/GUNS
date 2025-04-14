using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BarrackRouter : IRouter
{
    private readonly Dictionary<BuildingType, AbstractBarrackView> _barrackByViews = new();
    private BuildingController Building => BuildingController.Instance;

    public void Init()
    {
        _barrackByViews.Add(BuildingType.Barrack, FactoryController.Instance.FindPrefab<SoldierBarrackView>());
        _barrackByViews.Add(BuildingType.Tank, FactoryController.Instance.FindPrefab<TankBarrackView>());

        WaveController.Instance.StartedNewWave += StartNewWave;

        
        BuildingController.Instance.GeneratedPoints += OnSubscribePoins; 
        
        UpdateController.Instance.Add(OnUpdate);
    }

    private void StartNewWave()
    {
        OnSubscribePoins(Building.BuildingPoints);
    }

    private void OnSubscribePoins(IEnumerable<BuildingPoint> buildingPoints)
    {
        foreach (var model in buildingPoints.Where(x=>x.Type == BuildingType.Barrack || x.Type == BuildingType.Tank))
        {
            AbstractPressurePlateView obj = PressurePlateController.Instance.PressurePlateViewsByPoints[model.Point];
            obj.FilledIn += (a) => OnFilledIn(a, model.Type);
        }
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
        
        BarrackController.Instance.Create(view.transform, model.BuildingType);
    }

    private void OnFilledIn(AbstractPressurePlateView obj, BuildingType type)
    {
        AbstractBarrackView barrack = UiController.Instance.GetWindow<BarrackWindow>().CreateBarrack(_barrackByViews[type], obj.transform.position);
        
        Building.AddBuilding(barrack, new BuildingModel(4, type));
    }

    public void Exit()
    {
        
    }
}