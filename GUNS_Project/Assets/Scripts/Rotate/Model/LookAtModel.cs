using System;
using UnityEngine;

public class LookAtModel : IRotation
{
    private Func<Transform> _transform;
    private Func<Transform> _target;

    public LookAtModel(Func<Transform> transform, Func<Transform> target)
    {
        _transform = transform;
        _target = target;
    }

    public Quaternion RotateTo()
    {
        Vector3 direction = _transform().position - _target().position;
        direction.y = 0;
        Quaternion rotate = Quaternion.identity;
        
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rotate = Quaternion.Slerp(_transform().rotation, targetRotation, 5 * Time.deltaTime);
        }
        
        return rotate;
    }
}
