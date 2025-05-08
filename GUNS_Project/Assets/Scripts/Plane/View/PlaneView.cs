using System.Collections;
using UnityEngine;

public class PlaneView : MonoBehaviour
{
    [SerializeField] private Transform _point;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _ascendSpeed = 2f;
    [SerializeField] private float _ascendHeight = 10f;

    private float _targetHeight;
    private bool _isAscending = true;
    private Vector3 _startPosition;

    public Transform Point => _point;

    private bool _isWorking = true;

    private void Start()
    {
        transform.position += new Vector3(0, 0, 2);
        _startPosition = transform.position;
        _targetHeight = transform.position.y + _ascendHeight;
        
    }

    private IEnumerator Ascend()
    {
        while (transform.position.y < _targetHeight)
        {
            float newY = Mathf.MoveTowards(transform.position.y, _targetHeight, _ascendSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            yield return null;
        }
        _isAscending = false;
    }

    public void StartMoving()
    {
        if (_targetHeight == 0)
        {
            _startPosition = transform.position;
            _targetHeight = transform.position.y + _ascendHeight;
        }
        
        _isAscending = true;
        _isWorking = true;
        
        StartCoroutine(Ascend());
    }


    private void Update()
    {
        if (_isAscending || _isWorking == false)
            return;

        MoveTowardsCursor();
    }

    private void MoveTowardsCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 direction = (hit.point - transform.position).normalized;
            direction.y = 0;
            transform.position += direction * _moveSpeed * Time.deltaTime;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _moveSpeed * Time.deltaTime);
            }
        }
    }

    public void StopMoving()
    {
        _isWorking = false;
        
        StartCoroutine(ReturnSequence());
    }

    private IEnumerator ReturnSequence()
    {
        _isAscending = true;

        // Шаг 1: Перемещение по XZ
        Vector3 targetXZ = new Vector3(_startPosition.x, transform.position.y, _startPosition.z);
        while (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                   new Vector3(_startPosition.x, 0, _startPosition.z)) > 0.1f)
        {
            Vector3 direction = (targetXZ - transform.position).normalized;
            transform.position += new Vector3(direction.x, 0, direction.z) * _moveSpeed * Time.deltaTime;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _moveSpeed * Time.deltaTime);
            }

            yield return null;
        }

        // Перед опусканием поворачиваем к стартовой точке (на случай, если немного не успел довернуться)
        Vector3 flatDirection = (_startPosition - transform.position);
        flatDirection.y = 0;
        if (flatDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(flatDirection);
            while (Quaternion.Angle(transform.rotation, targetRotation) > 0.5f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _moveSpeed * Time.deltaTime);
                yield return null;
            }
        }

        // Шаг 2: Опускание по Y
        while (transform.position.y > _startPosition.y)
        {
            float newY = Mathf.MoveTowards(transform.position.y, _startPosition.y, _ascendSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            yield return null;
        }

        _isAscending = false;
    }

}
