using System.Collections;
using UnityEngine;
using DG.Tweening;

public class LookAtObject : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _rotationSpeed = 5f; // Скорость плавного поворота
    
    private Material _defaultMaterial;
    [SerializeField] private Material _disasterMaterial;
    
    private Transform _currentTarget;
    private Coroutine _pulseCoroutine;
    private bool _isVisible;
    private Quaternion _targetRotation;
    private bool _isRotating;
    
    private const float AnimationDuration = 0.8f;
    private const float PulseScale = 1.2f;

    private void Awake()
    {
        _defaultMaterial = _meshRenderer.material;
        transform.localScale = Vector3.zero;
        _isVisible = false;
        _isRotating = false;
    }

    public void Appear(Transform target)
    {
        if (target == null) return;
        
        _currentTarget = target;
        UpdateTargetRotation();
        
        if (_isVisible)
        {
            // Если уже виден, просто обновляем target и прерываем текущие анимации
            transform.DOKill();
            if (_pulseCoroutine != null) StopCoroutine(_pulseCoroutine);
            _pulseCoroutine = StartCoroutine(PulseAnimation());
        }
        else
        {
            // Появление с анимацией
            _isVisible = true;
            transform.DOScale(new Vector3(PulseScale, PulseScale, PulseScale), AnimationDuration * 0.75f)
                .OnComplete(() => 
                {
                    transform.DOScale(Vector3.one, AnimationDuration * 0.25f)
                        .OnComplete(() => _pulseCoroutine = StartCoroutine(PulseAnimation()));
                });
        }
    }

    public void Disappear()
    {
        if (!_isVisible) return;
        
        _isVisible = false;
        transform.DOKill();
        if (_pulseCoroutine != null) StopCoroutine(_pulseCoroutine);
        
        transform.DOScale(Vector3.zero, AnimationDuration)
            .OnComplete(() => _currentTarget = null);
    }

    private IEnumerator PulseAnimation()
    {
        while (_isVisible)
        {
            transform.DOScale(new Vector3(PulseScale, 1.3f, PulseScale), AnimationDuration * 0.75f)
                .OnComplete(() => transform.DOScale(Vector3.one, AnimationDuration * 0.25f));
            
            yield return new WaitForSeconds(AnimationDuration);
        }
    }

    private void Update()
    {
        if (_currentTarget != null)
        {
            UpdateTargetRotation();
            
            // Плавное вращение к цели
            if (_isRotating)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, _rotationSpeed * Time.deltaTime);
                
                // Проверяем, достигли ли мы нужного поворота
                if (Quaternion.Angle(transform.rotation, _targetRotation) < 0.1f)
                {
                    transform.rotation = _targetRotation;
                    _isRotating = false;
                }
            }
        }
    }

    private void UpdateTargetRotation()
    {
        Vector3 direction = _currentTarget.position - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            // Сначала вычисляем целевой поворот
            Quaternion newTargetRotation = Quaternion.LookRotation(direction);
            newTargetRotation = Quaternion.Euler(
                0 + _offset.x, 
                newTargetRotation.eulerAngles.y + _offset.y, 
                0 + _offset.z);

            // Учитываем вертикальный угол
            Vector3 targetDirection = _currentTarget.position - transform.position;
            float angleX = Mathf.Atan2(targetDirection.y, targetDirection.magnitude) * Mathf.Rad2Deg;
            newTargetRotation *= Quaternion.Euler(-angleX, 0, 0);

            // Если поворот изменился, начинаем плавное вращение
            if (_targetRotation != newTargetRotation)
            {
                _targetRotation = newTargetRotation;
                _isRotating = true;
            }
        }
    }

    // Методы для работы с материалами
    public void SetDefaultMaterial() => _meshRenderer.material = _defaultMaterial;
    public void SetDisasterMaterial() => _meshRenderer.material = _disasterMaterial;
    public void UpdateMaterial(Material newMaterial) => _meshRenderer.material = newMaterial;
}