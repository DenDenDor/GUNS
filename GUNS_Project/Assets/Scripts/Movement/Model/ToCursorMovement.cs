using System;
using UnityEngine;

public class ToCursorMovement : IMovement
{
    private readonly Transform _transform;
    private readonly Func<float> _getSpeed;
    private bool _isMovingToCursor = false;
    private Vector3 _currentTargetPosition;

    public ToCursorMovement(Func<float> getSpeed, Transform transform)
    {
        _getSpeed = getSpeed;
        _transform = transform;
    }

    private bool CheckPlayerViewHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 difference = hit.point - EntityController.Instance.Player.transform.position;

            Debug.Log(hit.collider.name + " HIT WITH!  == " + difference.sqrMagnitude);
            
            if (difference.sqrMagnitude < 16)
            {
                return true;
            }
        }
        return false;
    }

    private Vector3 GetTargetPosition()
    {
        if (Input.GetMouseButton(0))
        {
            // Если только что нажали - проверяем попадание в PlayerView
            if (Input.GetMouseButtonDown(0))
            {
                _isMovingToCursor = CheckPlayerViewHit();
            }

            if (_isMovingToCursor)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Plane plane = new Plane(Vector3.up, _transform.position);
                float distance;
                if (plane.Raycast(ray, out distance))
                {
                    _currentTargetPosition = ray.GetPoint(distance);
                    return _currentTargetPosition;
                }
            }
        }
        else
        {
            _isMovingToCursor = false;
        }

        return _transform.position;
    }

    public Vector3 GetPosition()
    {
        Vector3 targetPosition = GetTargetPosition();
        
        if (_isMovingToCursor)
        {
            return Vector3.MoveTowards(_transform.position, targetPosition, _getSpeed() * Time.deltaTime);
        }
        
        return _transform.position;
    }
}