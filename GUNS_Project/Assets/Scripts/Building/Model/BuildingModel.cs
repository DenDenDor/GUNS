using System;
using UnityEngine;

public class BuildingModel
{
    public float CurrentTime { get; private set; } = 0.5f;

    public Action<BuildingModel> OnTimeReset;
    
    public void Update()
    {
        CurrentTime -= Time.deltaTime;
        
        if (CurrentTime < 0)
        {
            CurrentTime = 5;
            OnTimeReset?.Invoke(this);
        }
    }
}
