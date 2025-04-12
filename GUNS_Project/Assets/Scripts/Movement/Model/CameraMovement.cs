using System;
using UnityEngine;

public class CameraMovement : IMovement
{
    private readonly Func<float> _getMaxSpeed;
    private readonly Func<Vector3> _getOffset;
    
    private readonly Transform _transform;
    private readonly Func<Transform> _getTarget;

    private readonly Func<float> _getSmoothTime;

    public CameraMovement(Transform transform, Func<Transform> getTarget, Func<float> getMaxSpeed, Func<Vector3> getOffset, Func<float> getGetSmoothTime)
    {
        _transform = transform;
        _getTarget = getTarget;
        _getMaxSpeed = getMaxSpeed;
        _getOffset = getOffset;
        _getSmoothTime = getGetSmoothTime;
    }

    private Vector3 _velocity = Vector3.zero;

    public Vector3 GetPosition()
    {
        if (_getTarget() == null)
        {
            return _transform.position;
        }
    
        Vector3 targetPosition = _getTarget().TransformPoint(_getOffset());

        Vector3 result = Vector3.SmoothDamp(
            _transform.position, 
            targetPosition, 
            ref _velocity, 
            _getSmoothTime(),
            _getMaxSpeed()
        );
        
        return result;
    }
}
