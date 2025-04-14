using UnityEngine;
using System;

public class BarrackController : MonoBehaviour
{
    public event Action<Transform> CreatedSoldier;
    public event Action<Transform> CreatedTank;
    
    private static BarrackController _instance;

    public static BarrackController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<BarrackController>();

                if (_instance == null)
                {
                    throw new NotImplementedException("BarrackController not found!");
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

    public void Create(Transform point, BuildingType type)
    {
        switch (type)
        {
            case BuildingType.Tank:
                CreatedTank?.Invoke(point);
                break;
            case BuildingType.Barrack:
                CreatedSoldier?.Invoke(point);
                break;
        }
    }
}