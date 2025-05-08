using System;
using System.Collections;
using UnityEngine;

public class BombView : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    
    private Transform _target;

    public void UpdateTarget(Transform target)
    {
        _target = target;
        _startPos = transform.position;
        _targetPos = new Vector3(_target.position.x, _startPos.y, _target.position.z);
        _progress = 0f;
    }

    public float height = 5f;
    public float speed = 5f;

    private Vector3 _startPos;
    private Vector3 _targetPos;
    private float _progress = 0f;
    private Vector3 _lastPosition;
    private bool _isWorking;

    public event Action<BombView> Fallen;

    void Start()
    {
        transform.position = new Vector3(transform.position.x, 1.693f, transform.position.z);
        
        _lastPosition = transform.position;

        transform.rotation = Quaternion.Euler(-90, 0, 0);

        transform.localScale = Vector3.zero;

        StartCoroutine(ScaleUp());
    }

    private IEnumerator ScaleUp()
    {
        float duration = 2f;
        float elapsed = 0f;
        Vector3 targetScale = new Vector3(2, 2, 2);

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            // Эффект OutBack можно имитировать с помощью AnimationCurve или упрощенной формулы
            t = Mathf.Sin(t * Mathf.PI * 0.5f); // Упрощенный вариант для эффекта "отскока"
            transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }

    public void StartMoving()
    {
        _isWorking = true;
    }

    void Update()
    {
        if (_isWorking == false)
        {
            return;
        }
        
        if (_progress > 1)
        {
            ParticleSystem particleSystem = Instantiate(_particleSystem, transform.position, Quaternion.identity);
            Destroy(particleSystem.gameObject, 5);
            
            Fallen?.Invoke(this);
            Destroy(gameObject);
        }

        _progress += Time.deltaTime * speed;

        float x = Mathf.Lerp(_startPos.x, _targetPos.x, _progress);
        float z = Mathf.Lerp(_startPos.z, _targetPos.z, _progress);
        float y = _startPos.y + height * Mathf.Sin(_progress * Mathf.PI);

        Vector3 newPosition = new Vector3(x, y, z);

        Vector3 moveDirection = (newPosition - _lastPosition).normalized;

        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }

        transform.position = newPosition;
        _lastPosition = newPosition;
    }
}