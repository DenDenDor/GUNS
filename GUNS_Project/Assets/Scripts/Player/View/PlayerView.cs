using UnityEngine;

public class PlayerView : AbstractEntity
{
    [SerializeField] private Transform _currencyPoint;

    public Transform CurrencyPoint => _currencyPoint;

    public override void MoveTo(Vector3 getPosition)
    {
        transform.position = getPosition;
    }
}
