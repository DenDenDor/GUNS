using System;
using UnityEngine;

public class CameraMovement : IMovement
{
    private readonly Func<float> _getMaxSpeed;
    private readonly Func<Vector3> _getOffset;
    
    private readonly Transform _transform;
    private readonly Transform _target;

    private readonly Func<float> _getSmoothTime;

    public CameraMovement(Transform transform, Transform target, Func<float> getMaxSpeed, Func<Vector3> getOffset, Func<float> getGetSmoothTime)
    {
        _transform = transform;
        _target = target;
        _getMaxSpeed = getMaxSpeed;
        _getOffset = getOffset;
        _getSmoothTime = getGetSmoothTime;
    }

    private Vector3 _velocity = Vector3.zero;

    public Vector3 GetPosition()
    {
        Vector3 targetPosition = _target.TransformPoint(_getOffset());
        return Vector3.SmoothDamp(
            _transform.position, 
            targetPosition, 
            ref _velocity, 
            _getSmoothTime(),
            _getMaxSpeed()
        );
    }
}
