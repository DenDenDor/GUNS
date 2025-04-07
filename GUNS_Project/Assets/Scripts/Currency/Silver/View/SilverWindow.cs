using System.Collections.Generic;
using UnityEngine;

public class SilverWindow : AbstractWindowUi
{
    [SerializeField] private Transform _startPoint;

    public Transform StartPoint => _startPoint;

    public override void Init()
    {
        
    }

    public void Create(SilverPickUp prefab, Transform point)
    {
        SilverPickUp pickUp = Instantiate(prefab, point);

        pickUp.transform.localPosition += new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));

        CurrencyController.Instance.AddPickUp(pickUp);
    }
}