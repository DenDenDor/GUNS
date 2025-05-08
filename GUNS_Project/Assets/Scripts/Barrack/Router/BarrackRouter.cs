using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BarrackRouter : IRouter
{
    private readonly Dictionary<BuildingType, AbstractBarrackView> _barrackByViews = new();
    private bool _isWorking;
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
        Debug.Log("STEP IS " + SDKMediator.Instance.GenerateSaveData().EducationStep);
        AbstractBuildingView view = Building.Barracks.FirstOrDefault(x=>x.Value == model).Key;
        
        if (SDKMediator.Instance.GenerateSaveData().EducationStep == 1 && _isWorking == false)
        {
            for (int i = 0; i < 24; i++)
            {
                BarrackController.Instance.Create(view.transform, model.BuildingType);
            }
            
            _isWorking = true;
            return;
        }

        BarrackController.Instance.Create(view.transform, model.BuildingType);
    }

    private void OnFilledIn(AbstractPressurePlateView obj, BuildingType type)
    {
        Vector3 positionNew = obj.transform.position;

        Transform go = obj.GetComponentsInChildren<Transform>().FirstOrDefault(x=>x.name == "Model");
        
        if (go != null)
        {
            Vector3 oldPos = go.transform.position;

            positionNew = new Vector3(oldPos.x, positionNew.y, oldPos.z);
        }
        
        AbstractBarrackView barrack = UiController.Instance.GetWindow<BarrackWindow>().CreateBarrack(_barrackByViews[type], positionNew);

        barrack.transform.localRotation = Quaternion.Euler(0, 90, 0);
        
        Building.AddBuilding(barrack, new BuildingModel(4, type));
    }

    public void Exit()
    {
        
    }
}