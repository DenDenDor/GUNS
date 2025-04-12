using UnityEngine;
using System;

public class CameraController : MonoBehaviour
{
    private IMoveTo _moveTo;
    private IMovement _movement;
    
    private static CameraController _instance;


    public static CameraController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CameraController>();

                if (_instance == null)
                {
                    throw new NotImplementedException("CameraController not found!");
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
    }

    public void Deconstructor(out IMoveTo moveTo, out IMovement movement)
    {
        moveTo = _moveTo;
        movement = _movement;
    }

    public void Init(IMoveTo moveTo, IMovement movement)
    {
        _moveTo = moveTo;
        _movement = movement;
    }
}