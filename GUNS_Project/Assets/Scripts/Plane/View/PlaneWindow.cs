using System;
using UnityEngine;

public class PlaneWindow : AbstractFactoryWindow
{
    public event Action<Transform> Created; 
    public event Action Removed; 

    public override void Init()
    {
        
    }

    public PlaneView Create(PlaneView prefab, Vector3 transformPosition)
    {
        PlaneView planeView =  CreatePrefab(prefab, transformPosition);
        return planeView;
    }

    public void SetForCamera(PlaneView planeView)
    {
        Created?.Invoke(planeView.transform);
    }
    
    public void RemovePlane()
    {
        Removed?.Invoke();
    }

    public PlaneBombView CreateBomb(PlaneBombView prefabBomb, Transform point)
    {
        PlaneBombView bomb =  CreatePrefab(prefabBomb, point.position);

        return bomb;
    }
}