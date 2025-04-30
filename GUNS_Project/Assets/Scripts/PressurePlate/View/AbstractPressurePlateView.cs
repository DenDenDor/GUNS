using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractPressurePlateView : MonoBehaviour
{
    [SerializeField] private Image _bar;

    private bool _isFilled;
    
    public event Action<AbstractPressurePlateView> Entered;
    public event Action<AbstractPressurePlateView> FilledIn;
    public event Action Exited;
    public event Action Reseted;
    
    public void UpdateBar(float fillness)
    {
        if (_bar != null)
        {
            _bar.fillAmount = fillness;
        }

        if (fillness == 1 && _isFilled == false)
        {
            FilledIn?.Invoke(this);
            _isFilled = true;
        }
    }

    public void FillIn()
    {
        FilledIn?.Invoke(this);
    }

    public void Reset()
    {
        _isFilled = false;
        Reseted?.Invoke();
    }

    private bool _isEnter;
    private PlayerTriggerView _playerTriggerView;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerTriggerView>(out PlayerTriggerView playerTriggerView))
        {
            Debug.LogError("TRIGGER ");

            _playerTriggerView = playerTriggerView;
            _isEnter = true;
        }
    }

    private Vector3 _lastPosition;
    private float _positionCheckInterval = 0.1f;
    private float _nextPositionCheckTime;
    private double _lastMovementTime;

    private void Update()
    {
        if (_isEnter && _playerTriggerView != null)
        {
            Debug.LogError("FOUND . . . ");
            if (Time.time >= _nextPositionCheckTime)
            {
                _nextPositionCheckTime = Time.time + _positionCheckInterval;
            
                Vector3 currentPosition = _playerTriggerView.transform.position;
                bool isMoving = Vector3.Distance(currentPosition, _lastPosition) > 0.01f;
                _lastPosition = currentPosition;
            
                if (!isMoving)
                {
                    if (Time.time - _lastMovementTime >= RequiredIdleTime)
                    {
                        Debug.LogError("ENTERED!!!");
                        Entered?.Invoke(this);
                    }
                }
                else
                {
                    _lastMovementTime = Time.time;
                }
            }
        }
    }

    public double RequiredIdleTime { get; set; } = 0.2;

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerTriggerView>())
        {
            Exited?.Invoke();
            
            _isEnter = false;
        }
    }
}
