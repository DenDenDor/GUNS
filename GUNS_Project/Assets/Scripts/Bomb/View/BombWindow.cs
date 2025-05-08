using System;
using UnityEngine;

public class BombWindow : AbstractFactoryWindow
{
    public event Action<Transform> Created; 
    public event Action Removed; 
    public override void Init()
    {
        
    }
    
    public void SetForCamera(BombView bombView)
    {
        Created?.Invoke(bombView.transform);
    }


    public BombView Create(BombView prefab, Vector3 transformPosition)
    {
        BombView bombView = CreatePrefab(prefab, transformPosition);
        
        return bombView;
    }

    public void RemoveBomb()
    {
        Removed?.Invoke();
    }
}