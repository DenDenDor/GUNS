using UnityEngine;
using System;

public class BarrackController : MonoBehaviour
{
    public event Action<Transform> Created;
    
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

    public void Create(Transform point)
    {
        Created?.Invoke(point);
    }
}