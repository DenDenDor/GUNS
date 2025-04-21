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
    
    public Transform Point => _point;

    private void Start()
    {
        _targetHeight = transform.position.y + _ascendHeight;
        StartCoroutine(Ascend());
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

    private void Update()
    {
        if (_isAscending) 
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
}