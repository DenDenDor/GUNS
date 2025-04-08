using UnityEngine;

public class ToMoveTowardsMovement : IMovement
{
    private readonly Transform _transform;
    private Vector3 _direction;
    
    private readonly float _speed = 5f;

    public ToMoveTowardsMovement(float speed, Transform transform, Transform target)
    {
        _speed = speed;
        _transform = transform;

        _direction = target.position - _transform.position;
    }
    
    public Vector3 GetPosition()
    {
        if (_transform == null)
        {
            return Vector3.zero;
        }
        
        return _transform.position + _direction.normalized * _speed * Time.deltaTime;
    }
}
