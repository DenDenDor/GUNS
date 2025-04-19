using System;
using UnityEngine;

public class ForwardMovement : IMovement
{
    private readonly Transform _transform;
    private Vector3 _direction;
    
    private readonly Func<float> _speed;

    public ForwardMovement(Func<float> speed, Transform transform, Vector3 direction)
    {
        _speed = speed;
        _transform = transform;
        _direction = direction;

        // _direction = target.position - _transform.position;
    }
    
    public Vector3 GetPosition()
    {
        if (_transform == null)
        {
            return Vector3.zero;
        }

        return _transform.position + _direction.normalized * _speed() * Time.deltaTime;
    }
}
