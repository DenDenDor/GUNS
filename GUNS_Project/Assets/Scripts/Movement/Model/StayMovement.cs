using UnityEngine;

public class StayMovement : IMovement
{
    private Transform _transform;

    public StayMovement(Transform transform)
    {
        _transform = transform;
    }

    public Vector3 GetPosition()
    {
        return _transform.position;
    }
}
