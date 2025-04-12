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
        if (_point != null)
        {
            return _point.position;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
