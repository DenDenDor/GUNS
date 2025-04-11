using System;
using UnityEngine;

public class ToCursorMovement : IMovement
{
    private readonly Transform _transform;
    private readonly Func<float> _getSpeed;
    private Vector3 _movementDirection;
    private bool _isMoving = false;
    private float _currentSpeed;

    public ToCursorMovement(Func<float> getSpeed, Transform transform)
    {
        _getSpeed = getSpeed;
        _transform = transform;
    }

    private Vector3 GetCursorWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, _transform.position);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            return ray.GetPoint(distance);
        }
        return _transform.position;
    }

    public Vector3 GetPosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Начало свайпа - запоминаем начальную позицию
            _movementDirection = GetCursorWorldPosition() - _transform.position;
            _movementDirection.y = 0; // Игнорируем вертикальную составляющую
            
            if (_movementDirection.magnitude > 0.1f) // Минимальный порог для начала движения
            {
                _movementDirection.Normalize();
                _isMoving = true;
                _currentSpeed = _getSpeed();
            }
        }
        else if (Input.GetMouseButton(0))
        {
            // Во время удержания кнопки - обновляем направление при движении мыши
            Vector3 newDirection = GetCursorWorldPosition() - _transform.position;
            newDirection.y = 0;
            
            if (newDirection.magnitude > 0.1f)
            {
                _movementDirection = newDirection.normalized;
                _currentSpeed = _getSpeed();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // Конец движения при отпускании кнопки
            _isMoving = false;
        }

        if (_isMoving)
        {
            // Двигаем объект в сохраненном направлении
            return _transform.position + _movementDirection * _currentSpeed * Time.deltaTime;
        }
        
        return _transform.position;
    }
}