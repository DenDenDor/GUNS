using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class PressurePlateController : MonoBehaviour
{
    private readonly Dictionary<Transform, AbstractPressurePlateView> _pressurePlateViewsByPoints = new();
    
    private readonly Dictionary<AbstractPressurePlateView, int> _pressurePlatesByAmount = new();
    private readonly Dictionary<AbstractPressurePlateView, int> _startMaxPrice = new();

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

    public void GetPriceBy(AbstractPressurePlateView plate, out int current, out int max)
    {
        current = _pressurePlatesByAmount[plate];
        max = _startMaxPrice[plate];
    }
    
    public void AddPressurePlate(Transform point, PressurePlateType type)
    {
        Created?.Invoke(point, type);
    }

    public void UpdateAllPrice(Transform point, int price)
    {
        AbstractPressurePlateView plate = _pressurePlateViewsByPoints[point];
        
        _pressurePlatesByAmount.Add(plate, price);
        _startMaxPrice.Add(plate, price);

    }

    public void Register(Transform point, AbstractPressurePlateView plate)
    {
        _pressurePlateViewsByPoints.Add(point, plate);
    }
    
    public void UpdateCurrentPrice(AbstractPressurePlateView view, int current)
    {
        _pressurePlatesByAmount[view] = current;
    }

    public void ResetRegister()
    {
        
    }
}