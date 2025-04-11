using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using Action = Unity.Android.Gradle.Manifest.Action;

public class BuildingController : MonoBehaviour
{
    private Dictionary<AbstractBuildingView, BuildingModel> _buildings = new();

    public Dictionary<AbstractBuildingView, BuildingModel> Buildings => _buildings;

    public IEnumerable<BuildingPoint> BuildingPoints =>
        WaveController.Instance.GenerateWaveInfo().BuildingPoints.Select(x => x.Current);

    public Dictionary<BarrackView, BuildingModel> Barracks
    {
        get
        {
            Dictionary<BarrackView, BuildingModel> dictionary = new();

            foreach (var item in _buildings.Where(x=>x.Key is BarrackView))
            {
                if (item.Key is BarrackView view)
                {
                    dictionary.Add(view, item.Value);
                }
            }

            return dictionary;
        }
    }

    private static BuildingController _instance;

    public static BuildingController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<BuildingController>();

                if (_instance == null)
                {
                    throw new NotImplementedException("BuildingController not found!");
                }
            }

            return _instance;
        }
    }

    public event Action<IEnumerable<BuildingPoint>> GeneratedPoints;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
    }

    public void AddBuilding(AbstractBuildingView view, BuildingModel model)
    {
        _buildings.Add(view, model);
    }

    public void GenerateNewBuilding(IEnumerable<BuildingPoint> points)
    {
        GeneratedPoints?.Invoke(points);
    }
}