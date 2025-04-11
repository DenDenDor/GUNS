using UnityEngine;

public class PlayerView : AbstractEntity, IRotatableView
{
    [SerializeField] private Transform _currencyPoint;
    [SerializeField] private Transform _child;
    [SerializeField] private Transform _lookAtTransform;

    public Transform LookAtTransform => _lookAtTransform;

    public Transform CurrencyPoint => _currencyPoint;

    public Transform Child => _child;

    public override void MoveTo(Vector3 getPosition)
    {
        transform.position = getPosition;
    }

    public void Rotate(Quaternion toRotate)
    {
        _child.transform.rotation = toRotate;
    }
}
