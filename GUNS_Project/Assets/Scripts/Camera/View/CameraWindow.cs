using Unity.VisualScripting;
using UnityEngine;

public class CameraWindow : AbstractWindowUi, IMoveTo
{
    [SerializeField] private Camera _camera;
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



    public void MoveTo(Vector3 getPosition)
    {
        Debug.Log(getPosition + " TO MOVE!");
        CurrentCamera.position = getPosition;
    }
}