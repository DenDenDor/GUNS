using UnityEngine;

public class ToCursorMovement : IMovement
{
    private readonly Transform _transform;
    
    private readonly float _speed = 5f;

    public ToCursorMovement(float speed, Transform transform)
    {
        _speed = speed;
        _transform = transform;
    }

    private Vector3 GetTargetPosition()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, _transform.position);
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                return ray.GetPoint(distance);
            }
        }

        return _transform.position;
    }

    public Vector3 GetPosition()
    {
        Vector3 targetPosition = GetTargetPosition();
        return Vector3.Lerp(_transform.position, targetPosition, _speed * Time.deltaTime);
    }
}
