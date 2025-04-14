using System;
using UnityEngine;

public class AnimationData
{
    public float CurrentTime { get; set; }
    public float StartTime { get; set; } = 2;
    
    public Vector3 OriginalPosition;
    public Quaternion OriginalRotation;
    
    public event Action TimeLessZero;
    public event Action UpdatedTime;
    public event Action StartedAfterLongTime;

    public AnimationData(Vector3 originalPosition, Quaternion originalRotation)
    {
        OriginalPosition = originalPosition;
        OriginalRotation = originalRotation;
    }

    public void ResetTime()
    {
        if (CurrentTime <= 0)
        {
            StartedAfterLongTime?.Invoke();
        }
        else
        {
            UpdatedTime?.Invoke();
        }

        CurrentTime = StartTime;
    }
    
    public void DecreasedTime()
    {
        if (CurrentTime >= 0)
        {
            CurrentTime -= Time.deltaTime;

            if (CurrentTime < 0)
            {
                TimeLessZero?.Invoke();
            }
        }
    }
}
