using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class CameraWindow : AbstractWindowUi, IMoveTo
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LookAtConstraint _lookAtConstraint;
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _smoothTime = 0.3f;

    public Transform CurrentCamera
    {
        get 
        {
            if (_camera == null)
            {
                _camera = Camera.main;
            }
            
            return  _camera.transform;
            
        }
    }

    public float Speed => _speed;

    public Vector3 Offset => _offset;

    public float SmoothTime => _smoothTime;

    public override void Init()
    {
        
    }

    public void UpdateLookAt(Transform target)
    {
        _lookAtConstraint.RemoveSource(0); 
    
        if (target != null)
        {
            var source = new ConstraintSource
            {
                sourceTransform = target,
                weight = 1f
            };
        
            _lookAtConstraint.AddSource(source);
            _lookAtConstraint.constraintActive = true;
        }
        else
        {
            _lookAtConstraint.constraintActive = false;
        }
    }
    public void MoveTo(Vector3 getPosition)
    {
        Debug.Log(getPosition + " TO MOVE!");
        CurrentCamera.position = getPosition;
    }
}