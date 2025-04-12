using System;
using UnityEngine;

public class LookAtModel : IRotation
{
    private Func<Transform> _transform;
    private Transform _target;
    private float _turnSmoothVelocity;
    private float _turnSmoothTime = 0.05f;

    public LookAtModel(Func<Transform> transform, Transform target, float turnSmoothVelocity = 0.05f)
    {
        _transform = transform;
        _target = target;
        _turnSmoothVelocity = turnSmoothVelocity;
    }

    public Quaternion RotateTo()
    {
        Transform transform = _transform();
        
        if (_target == null)
        {
            return transform.rotation;
        }

        Vector3 direction = transform.position - _target.position;
        direction.y = 0;

        if (direction == Vector3.zero)
        {
            return transform.rotation;
        }

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        
        float smoothedAngle = Mathf.SmoothDampAngle(
            transform.eulerAngles.y, 
            targetAngle, 
            ref _turnSmoothVelocity, 
            _turnSmoothTime
        );
        
        return Quaternion.Euler(0f, smoothedAngle, 0f);
    }
}