using UnityEngine;
using System;
using System.Collections.Generic;

public class PressurePlateController : MonoBehaviour
{
    private readonly Dictionary<Transform, AbstractPressurePlateView> _pressurePlateViewsByPoints = new();

    public Dictionary<Transform, AbstractPressurePlateView> PressurePlateViewsByPoints => _pressurePlateViewsByPoints;
    
    private static PressurePlateController _instance;

    public static PressurePlateController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PressurePlateController>();

                if (_instance == null)
                {
                    throw new NotImplementedException("PressurePlateController not found!");
                }
            }

            return _instance;
        }
    }

    public event Action<Transform, PressurePlateType> Created;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
    }

    public void AddPressurePlate(Transform point, PressurePlateType type)
    {
        Created?.Invoke(point, type);
    }

    public void Register(Transform point, AbstractPressurePlateView plate)
    {
        _pressurePlateViewsByPoints.Add(point, plate);
    }
}