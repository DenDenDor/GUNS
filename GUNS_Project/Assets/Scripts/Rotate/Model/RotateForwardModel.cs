using System;
using Unity.Mathematics;
using UnityEngine;

public class RotateForwardModel : IRotation
{
    private Vector3 _previousPosition;
    private Func<float> _speed;
    private Func<Transform> _transform;

    public RotateForwardModel(Func<float> speed, Func<Transform> transform)
    {
        _speed = speed;
        _transform = transform;
    }

    public Quaternion RotateTo()
    {
        Vector3 direction = _previousPosition - _transform().position;

        Quaternion currentRotation = _transform().rotation;
        
        if (direction.magnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            currentRotation = Quaternion.Slerp(
                _transform().rotation, 
                targetRotation, 
                _speed() * Time.deltaTime);
        }
        
        _previousPosition = _transform().position;

        return currentRotation;
    }
}
