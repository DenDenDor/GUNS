using UnityEngine;

public class ToPointMovement : IMovement
{
    private Transform _point;
    
    public ToPointMovement(Transform point)
    {
        _point = point;
    }

    public Vector3 GetPosition()
    {
        return _point.position;
    }
}
