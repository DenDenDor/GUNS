using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
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
        Debug.Log(_pressurePlateViewsByPoints.Count + " HOW MANY ADD" + point);

        if (_pressurePlateViewsByPoints.ContainsKey(point))
        {
            //            Debug.Log("ERROR IT HERE!!! " + point.name + "  PARENT IS " + point.GetComponentsInParent<Transform>().Skip(1).FirstOrDefault().name);
        }
        _pressurePlateViewsByPoints.Add(point, plate);
    }

    public void ClearAll()
    {
        AbstractPressurePlateView[] plates = _pressurePlateViewsByPoints.Values.ToArray();
        
        for (int i = 0; i < plates.Length; i++)
        {
            Destroy(plates[i].gameObject);
        }
        
        _pressurePlateViewsByPoints.Clear();
        _startMaxPrice.Clear();
        _pressurePlatesByAmount.Clear();
    }
    
    public void UpdateCurrentPrice(AbstractPressurePlateView view, int current)
    {
        _pressurePlatesByAmount[view] = current;
    }

    public void ResetRegister(AbstractPressurePlateView view, int max)
    {
        _pressurePlatesByAmount[view] = max;
        _startMaxPrice[view] = max;
        
        view.Reset();

    }
}