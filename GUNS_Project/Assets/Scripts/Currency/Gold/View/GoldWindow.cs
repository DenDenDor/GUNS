using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GoldWindow : AbstractCurrencyWindow
{
    public override void Init()
    {
        
    }
    
    public void CreateCurrency(GoldPickUp prefab, Vector3 position, Action<Transform> getPoint)
    {
        GoldPickUp pickUp = CreatePrefab(prefab, position);

        getPoint?.Invoke(pickUp.transform);

        CurrencyController.Instance.AddPickUp(pickUp);
    }
}