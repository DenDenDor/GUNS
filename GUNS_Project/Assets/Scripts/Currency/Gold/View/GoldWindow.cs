using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GoldWindow : AbstractCurrencyWindow
{
    public override void Init()
    {
        
    }
    
    public void Create(GoldPickUp prefab, Vector3 position)
    {
        GoldPickUp pickUp = Instantiate(prefab, position, Quaternion.identity);

        pickUp.transform.localPosition += new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2));

        CurrencyController.Instance.AddPickUp(pickUp);
    }
}