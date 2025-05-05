using System;
using System.Collections.Generic;
using UnityEngine;

public class SilverWindow : AbstractCurrencyWindow
{
    [SerializeField] private Transform _startPoint;

    
    public Transform StartPoint => _startPoint;

    public override void Init()
    {
        
    }

    public void CreateCurrency(SilverPickUp prefab, Vector3 position, Action<Transform> getPoint)
    {
        SilverPickUp pickUp = CreatePrefab(prefab, position);
        
        getPoint?.Invoke(pickUp.transform);

        CurrencyController.Instance.AddPickUp(pickUp);
    }
}