using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GoldWindow : AbstractCurrencyWindow
{
    public override void Init()
    {
        
    }
    
    public void CreateCurrency(GoldPickUp prefab, Vector3 position)
    {
        GoldPickUp pickUp = CreatePrefab(prefab, position);

        pickUp.transform.localPosition += new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2));

        CurrencyController.Instance.AddPickUp(pickUp);
    }
}