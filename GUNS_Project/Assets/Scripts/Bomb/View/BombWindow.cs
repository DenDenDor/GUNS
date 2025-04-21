using System;
using UnityEngine;

public class BombWindow : AbstractFactoryWindow
{
    public event Action<Transform> Created; 
    public event Action Removed; 
    public override void Init()
    {
        
    }

    public BombView Create(BombView prefab, Vector3 transformPosition)
    {
        BombView bombView = CreatePrefab(prefab, transformPosition);
        
        Created?.Invoke(bombView.transform);
        
        return bombView;
    }

    public void RemoveBomb()
    {
        Removed?.Invoke();
    }
}