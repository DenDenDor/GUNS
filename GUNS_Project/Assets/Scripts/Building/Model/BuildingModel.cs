using System;
using UnityEngine;

public class BuildingModel
{
    public float MaxTime { get; private set; }
    public BuildingType BuildingType { get; private set; }
    public float CurrentTime { get; private set; } = 0.5f;

    public BuildingModel(float maxTime, BuildingType buildingType)
    {
        MaxTime = maxTime;
        BuildingType = buildingType;
    }

    public Action<BuildingModel> OnTimeReset;
    
    public void Update()
    {
        CurrentTime -= Time.deltaTime;
        
        if (CurrentTime < 0)
        {
            CurrentTime = MaxTime;
            OnTimeReset?.Invoke(this);
        }
    }
}
